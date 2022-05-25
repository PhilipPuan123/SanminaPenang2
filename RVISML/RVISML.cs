
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpIF;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.IO.Compression;
using TMModbusIF;
using RVISData;
using System.Collections;
using System.Timers;
using Newtonsoft.Json;
using MesConduit;
using MesMeasurement;
using MesConduitResponse;
using MesMeasurementResponse;

//**add modbus combination robot status for overall status
namespace RVISMMC
{
    public class RVISML
    {

        #region member data initialization
        //member data
        string TEMP_IMG_ZIP_PATH = @"C:\RVIS\Zip"; //this path directory is used when finish inspection and prepared to zip
        private CancellationTokenSource _cts; //Background task resource cancel
        public bool overallSts { get; set; }//= true; //flag for overall status
        int measurementNameCounter = 0;//test step counter
        public List<RoboticInspectionSystem> ListinspectionData =  new List<RoboticInspectionSystem>(); //List for all 33 parametric data

        //robot sts
        bool robotRunSts; //robot running status 
        bool robotErrSts; //robot error status 
        bool robotPauseSts;//robot pause status 
        bool robotStopSts;// robot stop status
        bool tcpIpSts; //comm sts between robot and PC 
        BitArray digitalInputSts;
        private bool prevRobotRunSts = false;
        private bool prevRobotPauseSts = false;
        private bool prevRobotErrSts = false;
        private bool prevRobotStopSts = false;
        private bool prevErrSts = true; //system error
        //auto implemented properties
        public string ProcTestGroup { get; set; } //payload test group
        public string ProcTestName { get; set; } //payload test name
        public string ProcResult { get; set; } //payload result
        public string ProcLiveImgDir { get; set; } //payload live image dir
        public string ProcTestEnd { get; set; } //payload 1 UUT completion flag
        public string ProcSerial { get; set; } //payload serial number
        public string ProcMasterImgDir { get; set; } //payload master image dir
        public string ProcCoverResult { get; set; }//payload for cover result
        #endregion

        #region Event Initialization
        //serial number result payload publish
        public delegate void SerialResultPub(string serial, string dir);
        public event SerialResultPub OnSerialResultPub;
        //checkpoint payload result publish
        public delegate void DownstreamResultPub(string testgroup, string testname, string result, string masterpath, string testEnd);
        public event DownstreamResultPub OnResultStringPub;
        //cover checking result publish
        public delegate void CoverResultPub(string coverResult);
        public event CoverResultPub OnCoverResultPub;
        //tcpip error status publish
        public delegate void ErrStatusPub(bool tcpErr);
        public event ErrStatusPub OnErrStatusPub;
        //finish UUT inspection publish
        public delegate void FinishInspecPub(bool sts, bool lastSts);
        public event FinishInspecPub OnFinishInspecPub;
        //read whenever physical start button is pressed
        public delegate void PhysicalStartButtonPub(bool sts);
        public event PhysicalStartButtonPub OnPhysicalStartButtonPub;
        //read robot run status
        public delegate void RobotRunStatusPub(bool robotRunSts);
        public event RobotRunStatusPub OnRobotRunStatusPub;
        //read robot pause status
        public delegate void RobotPauseStatusPub(bool robotPauseSts);
        public event RobotPauseStatusPub OnRobotPauseStatusPub;
        //read robot error status
        public delegate void RobotErrStatusPub(bool robotErrSts);
        public event RobotErrStatusPub OnRobotErrStatusPub;
        //read robot stop status
        public delegate void RobotStopStatusPub(bool robotStopSts);
        public event RobotStopStatusPub OnRobotStopStatusPub;
        //read RVIS system error status
        public delegate void SystemErrStatusPub(bool isDoorLockedLeft, bool isDoorLockedRight, bool isTrolleySense1, bool isTrolleySense2);
        public event SystemErrStatusPub OnSystemErrStatusPub;
        //publish ready to GUI
        public delegate void SystemReadyStartStatusPub(bool systemReadyStartSts);
        public event SystemReadyStartStatusPub OnSystemReadyStartStatusPub;
        //publish conduit response to GUI
        public delegate void MESConduitResponsePub(string response);
        public event MESConduitResponsePub OnMESConduitResponsePub;
        //publish parametric response to GUI
        public delegate void MESMeasurementResponsePub(bool response);
        public event MESMeasurementResponsePub OnMESMeasurementResponsePub;
        //publish log when system submit serial # to 42Q
        public delegate void MESSerialSubmitPub(string str);
        public event MESSerialSubmitPub OnMESSerialSubmitPub;
        #endregion 

        #region create related obj required
        //create TcpIF- Server object
        public Server tcpServer = new Server(); //tcp server object
        public FileSystemWatcher filewatcher = new FileSystemWatcher(); //image folder watcher object
        string jpgduplicateCheck = null;
        #endregion

        #region system data status lock
        //Task data lock object
        private readonly object getRobotRunStsLock = new object();
        private readonly object getRobotErrStsLock = new object();
        private readonly object getRobotPauseStsLock = new object();
        private readonly object getTcpIpStsLock = new object();
        private readonly object getRobotControlBoxInputSts = new object();
        private readonly object getRobotStopStsLock = new object();
        #endregion

        #region Main Task to subscribe and start function call
        public void Start() //Main execution function
        {
            bool safety = true;
            overallSts = true;
            RVISData.GuiDataExchange.dutResult = "PASSED";
            tcpServer.OnDataReceived -= TcpServer_OnDataReceived;
            tcpServer.OnDataReceived += TcpServer_OnDataReceived;
            filewatcher.Changed -= OnChanged;
            InitiateWatcher();

            //Refresh Temporary Zip directory
            if (Directory.Exists(TEMP_IMG_ZIP_PATH))
            {
                Directory.Delete(TEMP_IMG_ZIP_PATH, true);
            }
            var dirInfo = Directory.CreateDirectory(TEMP_IMG_ZIP_PATH);
            if (dirInfo.Exists == false)
            {
                // Throw error
            }

            //Start with system preliminary check (READY Condition) //(robotRunSts == false)&& (robotPauseSts == false) ->A-0009 after added Stop as pause
            if ((robotErrSts == false)  && (tcpIpSts == true) && (safety == true))
            {
                // If OK start activate RUN robot
                InitiateRunOrPause();
            }
        }
        #endregion

        #region To stop machine
        public void Stop()
        {
                ModbusControl.StopProject();
                ModbusControl.SetControlBox_DO1(false); //if stop button initiate from soft button, it pull down DO1 as software complete inspection
        }
        #endregion

        #region Initiate start on machine
        public int InitiateRunOrPause() //send RUN cmd to robot
        {
            int startSts = 0;
            return startSts;
        }
        #endregion

        //After last checkpoint inspection
        #region  Finish checkpoint inspection
        //Finish inspection and Zip image to local server
        public async Task FinishInspection()
        {
            string dateTime = null; //** addition naming for zip file 2801-1652
            dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");//** addition naming for zip file 2801-1652
            string LOCAL_SERVER_PATH = RVISData.SettingData.LocalServerPath; // path the zip file going to store at their local server (user flexibility) //e.g. C:\Images\

            try
            {
                if(RVISData.GuiDataExchange.finalSts == true)

                {
                    await Task.Factory.StartNew(() => { ZipFile.CreateFromDirectory(TEMP_IMG_ZIP_PATH, LOCAL_SERVER_PATH +@"PASS\"+ dateTime + "_" + ProcSerial + ".zip"); }, TaskCreationOptions.LongRunning);
                }
                else
                {
                    await Task.Factory.StartNew(() => { ZipFile.CreateFromDirectory(TEMP_IMG_ZIP_PATH, LOCAL_SERVER_PATH + @"FAIL\" + dateTime + "_" + ProcSerial + ".zip"); }, TaskCreationOptions.LongRunning);
                }
                overallSts = true;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex);
            }

        }
        #endregion

        //TCP event handler
        #region Event Handler
        //Event handler  from the event data received from TCP/IP
        private void TcpServer_OnDataReceived(object sender, string parameter)
        {
            if (string.IsNullOrEmpty(parameter) != true)
            {
                ResultDataProcces(sender, parameter);
            }
        }
        #endregion

        //Received and process data send by TM via TCP
        #region Result data processing and publishing

        //Data processing after received from TCP/IP
        public void ResultDataProcces(object sender, string parameter)
        {
            //process data ready for JSON converter
            //payload data during inspection might look like this: "testGroup=DeviceControllerPCBA,testName=BoardAssembly_x7screws,result=PASS,img=10-43-16_067.png,testEnd=TRUE"
            //payload data during checking serial : "sn=12314ACVBbe,img=10-51-49_737.png"

            string MASTER_IMAGE_PATH = RVISData.SpecialSettingData.UIImageLoadPath + @"MASTER\"; //this path will be eg C:\RVIS\IMG\<ownself add Master>\<img.png>
            string LIVE_IMAGE_PATH = RVISData.SpecialSettingData.UIImageLoadPath + @"LIVE\current.png"; //this path will be eg C:\RVIS\IMG\<ownself add Live>\<img.png>
            string ROBOT_SAVE_PATH = RVISData.SpecialSettingData.TMImageSavePath; //this path is where robot save the image after inspection

            string robot_save_full_path = @"C:\RVIS\Img\BUFFER\buffer.png";//ori:null; //to obtain the exact image path produced by TM
            string rename_img_path = null;
            string rename_image = null;

            //MES Response data initialization
            bool sts = false;
            //Verify data send from downstream
            string pattern_checkpoint = @"^testGroup=(?<Testgroup>[a-zA-Z0-9_]+),testName=(?<Testname>[a-zA-Z0-9_]+),result=(?<Result>[a-zA-Z0-9]+),testEnd=(?<End>[a-zA-Z]+)$";
            string pattern_serial = @"^sn=(?<Serial>[A-Z0-9]+)$";
            string pattern_cover = @"^cover=(?<Cover>[A-Za-z]+)$";
            string pattern_systemReady = @"^system=READY$";

            Match match_checkpoint = Regex.Match(parameter, pattern_checkpoint);
            Match match_serial = Regex.Match(parameter, pattern_serial);
            Match match_cover = Regex.Match(parameter, pattern_cover);
            Match match_systemReady = Regex.Match(parameter, pattern_systemReady);
            //process checkpoint result send by TM
            if (match_checkpoint.Success)
            {
                //reply to robot once received data
                tcpServer.ResponseToClient(sender, "OK");
                ProcTestGroup = match_checkpoint.Result("${Testgroup}");
                ProcTestName = match_checkpoint.Result("${Testname}");
                ProcResult = match_checkpoint.Result("${Result}");
                ProcTestEnd = match_checkpoint.Result("${End}");
                if (measurementNameCounter >= 0 && measurementNameCounter < 34)
                {
                    measurementNameCounter++;
                }
                else
                {
                    measurementNameCounter = 0;
                }
                ListTestStepData(ProcTestName, ProcResult);
                rename_image = ProcTestGroup + "_" + ProcTestName + ".png"; //rename image name to eg DeviceControllerPCBA_BoardAssembly_x7screws.png
                rename_img_path = TEMP_IMG_ZIP_PATH + @"\" + rename_image; //eg -> C:\RVIS\ZIP\DeviceControllerPCBA_BoardAssembly_x7screws.png /**addition "\" 28/1-1649
                ProcMasterImgDir = MASTER_IMAGE_PATH + rename_image;
                //publish the rename image to GUI
                if (File.Exists(robot_save_full_path))
                {                      
                    System.IO.File.Copy(robot_save_full_path, rename_img_path); //move from robot shared folder to to-be-Zipped folder w/o overwrite 
                    System.IO.File.Copy(rename_img_path, LIVE_IMAGE_PATH, true); //copy from to-be-Zipped folder to Live image directory file with overwrite
                }
                else
                {
                    Console.WriteLine("TM failed to saved image");
                }
                if(ProcResult == "FAIL")
                {
                    RVISData.GuiDataExchange.dutResult = "FAILED";
                    overallSts = false;
                }
                if (OnResultStringPub != null)
                {
                    OnResultStringPub(ProcTestGroup, ProcTestName, ProcResult, ProcMasterImgDir, ProcTestEnd);
                }
                if (ProcTestEnd.ToUpper() == "TRUE") //check is UUT completed
                {
                    RVISData.GuiDataExchange.stopTime = DateTime.Now;

                    if (RVISData.SettingData.mesController == "ON")
                    {
                        MeasurementCSharp2Json();
                    }
                    RVISData.GuiDataExchange.dutResult = "PASSED";
                    if (OnFinishInspecPub != null)//** addition finish testing publisher  2801-1652
                    {
                        OnFinishInspecPub(true, overallSts);
                    }
                }
               
            }
            //process serial # result send by TM
            else if (match_serial.Success)
            {

                ProcSerial = match_serial.Result("${Serial}");
                if (OnMESSerialSubmitPub != null)
                {
                    OnMESSerialSubmitPub("Submitting serial #");
                }
                RVISData.GuiDataExchange.serialNumber = ProcSerial;
                if(RVISData.SettingData.mesController == "ON")
                {
                    sts = SerialCSharp2Json();
                }
                else if(RVISData.SettingData.mesController == "OFF")
                {
                    sts = true;
                }
                if (sts == true) { ModbusControl.SetControlBox_DO15(true); }
                else if(sts == false) { ModbusControl.SetControlBox_DO15(false); RVISData.GuiDataExchange.prjStillrunSts = false; }

                rename_image = ProcSerial + ".png"; //rename image name to eg 18KLBN456.png
                rename_img_path = TEMP_IMG_ZIP_PATH + @"\" + rename_image; //eg -> C:\RVIS\ZIP\18KLBN456.png /**addition "\" 28/1-1649
                ProcMasterImgDir = MASTER_IMAGE_PATH + "sn.png";

                if (File.Exists(robot_save_full_path))
                {         
                    System.IO.File.Copy(robot_save_full_path, rename_img_path); //move from robot shared folder to to-be-Zipped folder w/o overwrite 
                    System.IO.File.Copy(rename_img_path, LIVE_IMAGE_PATH, true); //copy from to-be-Zipped folder to Live image directory file with overwrite
                }
                else
                {
                    Console.WriteLine("TM failed to saved image");
                }
                if (ProcResult == "FAIL")
                {
                    overallSts = false;
                }
                if (OnSerialResultPub != null)
                {
                    OnSerialResultPub(ProcSerial, ProcMasterImgDir);
                }
            }
            //process cover status send by TM
            else if(match_cover.Success)
            {
                ProcCoverResult = match_cover.Result("${Cover}");
                if(OnCoverResultPub != null)
                {
                    OnCoverResultPub(ProcCoverResult);
                }
            }
            //process system status send by TM
            else if(match_systemReady.Success)
            {
                if(OnSystemReadyStartStatusPub != null)
                {
                    OnSystemReadyStartStatusPub(true);
                }
            }
        }
        #endregion

        //Serial conduit data preparation and send to 42Q
        #region MES CSharp2JSON Serial Checking
        public bool SerialCSharp2Json()
        {
            string response = null;
            string uri = null;
            bool sts = false;
            string host = RVISData.SettingData.MesConduitURL;
            uri = host;

            MesConduit.RVISConduitData conduitData = new RVISConduitData();
            Conduit.Parser(conduitData);
            var json = JsonConvert.SerializeObject(conduitData, Formatting.Indented);
            WebRequestREST webRequestREST = new WebRequestREST();
            response = webRequestREST.SendRequest(uri, json);   
            sts = MESResponseInterpreter(response);
            return sts;
        }
        #endregion

        //Checkpoint parametric data preparation and send to 42Q
        #region MES CSharp2Json Checkpoint Measurement Data
        //input each checkpoint result into list
        public void ListTestStepData(string testName ,string result)
        {
            RoboticInspectionSystem inspectionData = new RoboticInspectionSystem();
            inspectionData.name = measurementNameCounter.ToString();//some counter
            inspectionData.description = testName; //get each inspection steps test name
            inspectionData.comparator = "EQ";
            inspectionData.lowLimit = "N/A";
            inspectionData.highLimit = "N/A";
            inspectionData.expected = "PASS";
            inspectionData.unit = "N/A";
            inspectionData.value = "PASS";
            inspectionData.status = result; //get each inspection steps result
            inspectionData.comment = "";
            inspectionData.sequence = measurementNameCounter.ToString();
            ListinspectionData.Add(inspectionData);
        }
        //ready the parametric data and send to 42Q
        public bool MeasurementCSharp2Json()
        {
            bool sts = false;
            string response = null;
            string uri = null;
            string host = RVISData.SettingData.MesMeasureURL;
            string resource_name = RVISData.SettingData.MesMeasureResourceName;
            uri = host + resource_name;
            RVISData.GuiDataExchange.inspectionPayload = ListinspectionData;
            MesMeasurement.RVISMeasureData measureData = new RVISMeasureData(); 
            Measurement.MeasurementParser(measureData);
            var json = JsonConvert.SerializeObject(measureData, Formatting.Indented);
            WebRequestREST webRequestREST = new WebRequestREST();
            response = webRequestREST.SendRequest(uri, json);
            sts = MESMeasurementResponseInterpreter(response);
            ListinspectionData.Clear();
            RVISData.GuiDataExchange.inspectionPayload.Clear();
            return sts;
        }
        #endregion

        //received and process 42Q data response
        #region MES InterpretJson response
        //received and process conduit data response
        public bool MESResponseInterpreter(string res)
        {
            string code = null;
            string message = null;
            bool sts = false;
            MesConduitResponse.RVISConduitDataResponse responseData = JsonConvert.DeserializeObject<RVISConduitDataResponse>(res);
            code = responseData.transaction_responses.ElementAt(0).scanned_unit.status.code;
            message = responseData.transaction_responses.ElementAt(0).scanned_unit.status.message;
       
            if(responseData.status.code == "OK")
            {
                sts = true;
            }
            else if(responseData.status.code == "ERROR")
            {
                sts = false;
            }
            else
            {
                Console.WriteLine("Undefined Status Code");
            }
            if (OnMESConduitResponsePub != null)
            {
                OnMESConduitResponsePub(message);
            }
            return sts;
        
        }
        //received and process parametric data response
        public bool MESMeasurementResponseInterpreter(string res)
        {
            bool sts = false;
            MesMeasurementResponse.RVISMeasurementDataResponse resp = JsonConvert.DeserializeObject<RVISMeasurementDataResponse>(res);

            if (resp.success == true)
            {
                sts = true;
            }
            if(resp.success == false)
            {
                sts = false;
            }
            if (OnMESMeasurementResponsePub != null)
            {
                OnMESMeasurementResponsePub(resp.success);
            }
            return sts;
        }
        #endregion

        //polling for physical start switch status
        #region physical start button 
        public void StartSwitch()
        {
           
            if(digitalInputSts[4] == true)
            {
                if(OnPhysicalStartButtonPub!= null)
                {
                    OnPhysicalStartButtonPub(true);
                }
            }
        }
        #endregion

        //file monitor when new image send by TM
        #region live image folder watcher
        public void InitiateWatcher()
        {
            filewatcher.Path = RVISData.SpecialSettingData.TMImageSavePath;
            filewatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            filewatcher.Filter = "*.*";
            filewatcher.EnableRaisingEvents = true;
            filewatcher.Changed += OnChanged;

        }
        public void OnChanged(object source, FileSystemEventArgs e)
        {
           string jpg_pattern = @"[^a-zA-Z\:]+([0-9-_.]+jpg)$";
           Match jpg_format = Regex.Match(e.FullPath, jpg_pattern);
            if (jpg_format.Success)
            {
                if (e.FullPath != jpgduplicateCheck)           
                {
                    if (Directory.Exists(@"C:\RVIS\Img\BUFFER"))
                    {       
                        File.Copy($"{e.FullPath}", @"C:\RVIS\Img\BUFFER\buffer.png",true);
                        jpgduplicateCheck = e.FullPath;
                    }
                    else
                    {
                        Directory.CreateDirectory(@"C:\RVIS\Img\BUFFER");
                        File.Copy($"{e.FullPath}", @"C:\RVIS\Img\BUFFER\buffer.png",true);
                        jpgduplicateCheck = e.FullPath;
                    }
                }
            }
        }
        #endregion

        //get robot system status
        #region Get system status task
        public void GetRobotRunSts()
        {
            //get robot run status function - this can be replace to calling function like in FrmTestToolKit.cs
            lock (getRobotRunStsLock)
            {
                ModbusControl.GetProjectRunningStatus(ref robotRunSts);
            }
            if(prevRobotRunSts != robotRunSts)
            {
                if(OnRobotRunStatusPub != null)
                {
                    OnRobotRunStatusPub(robotRunSts);
                }
                prevRobotRunSts = robotRunSts;
            }
        }

        public void GetRobotErrSts()
        {
            lock (getRobotErrStsLock)
            {
                ModbusControl.GetProjectErrorStatus(ref robotErrSts);
            }
            if (prevRobotErrSts != robotErrSts)
            {
                if (OnRobotErrStatusPub != null)
                {
                    OnRobotErrStatusPub(robotErrSts);
                }
                prevRobotErrSts = robotErrSts;
            }
        }

        public void GetRobotPauseSts()
        {
            lock (getRobotPauseStsLock)
            {
                ModbusControl.GetProjectPauseStatus(ref robotPauseSts);
            }
            if (prevRobotPauseSts != robotPauseSts)
            {
                if (OnRobotPauseStatusPub != null)
                {
                    OnRobotPauseStatusPub(robotPauseSts);
                }
                prevRobotPauseSts = robotPauseSts;
            }
        }

        public void GetRobotControlBoxInputSts()
        {
            lock(getRobotControlBoxInputSts)
            {
                ModbusControl.GetControlBoxAllDigitalInputs(ref digitalInputSts); //in bit array form [0,0,0,0...,0] 16
            }
            StartSwitch();
            GetErrorSts();
        }
        public bool GetTcpIPSts()
        {
            lock (getTcpIpStsLock)
            {
                if (tcpServer.IsRunning == true)
                {
                    tcpIpSts = tcpServer.IsRunning;
                }
                else
                {
                    if (OnErrStatusPub != null)
                    {
                        OnErrStatusPub(tcpServer.IsRunning);
                    }
                    tcpServer.Restart();
                }
            }
            return tcpIpSts;
        }
        public void EndAllServices() //Dispose and unsubscribe event
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                //unsubscribe to OnRecogPub event for process result string
                tcpServer.OnDataReceived -= TcpServer_OnDataReceived;
            }

        }
        public void AutoStartAfterPause()
        {
            if((robotPauseSts == true) && (robotRunSts==true) && (robotErrSts == false) && (digitalInputSts[7]== false) && (digitalInputSts[6] == false))
            {
                ModbusControl.StartOrPauseProject();
                
            }
        }
        public void GetRobotStopSts()
        {
            lock (getRobotStopStsLock)
            {
                if((robotPauseSts == false) && (robotRunSts == false) && (robotErrSts == false))
                {
                    robotStopSts = true;
                    ModbusControl.SetControlBox_DO1(false);
                }
                else
                {
                    robotStopSts = false;
                }
            }
            if (prevRobotStopSts != robotStopSts)
            {
                if (OnRobotStopStatusPub != null)
                {
                    OnRobotStopStatusPub(robotStopSts);
                }
                prevRobotStopSts = robotStopSts;
            }
        }
        public void GetErrorSts()
        {
            if (digitalInputSts[0] != prevErrSts)
            {
                if (digitalInputSts[0] == false)
                {
                    OnSystemErrStatusPub(digitalInputSts[8], digitalInputSts[9], digitalInputSts[10], digitalInputSts[11]);
                }
                prevErrSts = digitalInputSts[0];
            }
        }
        #endregion

        //run background task
        #region Background Task system preliminary check - Background Process
        public async Task BackgroundSysCheck() //initial checking of system status whenever connection established- will be called when Form_Load()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            await Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested) //continue task polling if not cancel request
                {
                    try
                    {
                        token.ThrowIfCancellationRequested();
                        GetRobotRunSts();
                        GetRobotErrSts();
                        GetRobotPauseSts();
                        GetTcpIPSts();
                        GetRobotControlBoxInputSts();
                        AutoStartAfterPause();
                        Thread.Sleep(200);
                    }
                    catch (OperationCanceledException)
                    {
                        //do something
                    }
                }
            }, TaskCreationOptions.LongRunning);
            #endregion
        }

    }






}