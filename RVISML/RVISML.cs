
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

//**add modbus combination robot status for overall status
namespace RVISMMC
{
    public class RVISML
    {
        #region member data initialization
        //member data
        string LOCAL_SERVER_PATH = RVISData.SettingData.LocalServerPath; // path the zip file going to store at their local server (user flexibility)
        string TEMP_IMG_ZIP_PATH = @"C:\RVIS\ZIP\"; //this path directory is used when finish inspection and prepared to zip

        bool completeUnitSts = false; //flag for completion of 1 UUT
        private CancellationTokenSource _cts; //Background task resource cancel

        //robot sts
        bool robotRunSts; //robot running status 
        bool robotErrSts; //robot error status 
        bool robotPauseSts;//robot pause status 
        bool tcpIpSts; //comm sts between robot and PC 

        //auto implemented properties
        public string ProcTestGroup { get; set; } //payload test group
        public string ProcTestName { get; set; } //payload test name
        public string ProcResult { get; set; } //payload result
        public string ProcLiveImgDir { get; set; } //payload live image dir
        public string ProcTestEnd { get; set; } //payload 1 UUT completion flag
        public string ProcSerial { get; set; } //payload serial number
        public string ProcMasterImgDir { get; set; } //payload master image dir
  
        #endregion

        #region Event Initialization
        //serial number result payload publish
        public delegate void SerialResultPub(string serial, string dir);
        public event SerialResultPub OnSerialResultPub;
        //checkpoint payload result publish
        public delegate void DownstreamResultPub(string testgroup,string testname, string result, string masterpath, string testEnd);
        public event DownstreamResultPub OnResultStringPub;
        //background robot status payload publish
        public delegate void RobotStatusPub(bool robotRunSts, bool robotErrSts, bool robotPauseSts);
        public event RobotStatusPub OnRobotStatusPub;
        //tcpip error status publish
        public delegate void ErrStatusPub(bool tcpErr);
        public event ErrStatusPub OnErrStatusPub;
        #endregion 

        #region create related obj required
        //create TcpIF- Server object
        public Server tcpServer = new Server();
        
        #endregion
   
        #region system data status lock
        //Task data lock object
        private readonly object getRobotRunStsLock = new object();
        private readonly object getRobotErrStsLock = new object();
        private readonly object getRobotPauseStsLock = new object();
        private readonly object getTcpIpStsLock = new object();
        #endregion

        #region Main Task to subscribe and start function call
        public void Start() //Main execution function
        {
            //subscribing to the TCPIP event for new data input            
            tcpServer.OnDataReceived += TcpServer_OnDataReceived;

            //Start with system preliminary check (READY Condition)
            if ((robotRunSts == false) && (robotErrSts == false) && (robotPauseSts == false) && (tcpIpSts == true))
            {
                //2. If OK start activate RUN robot
                InitiateRunOrPause();
            }

        }
        #endregion

        #region Initiate start on machine
        public int InitiateRunOrPause() //send RUN cmd to robot
        {
            //check robot is triggered
            int startSts = 0;
            //start run the robot
            startSts = ModbusControl.StartOrPauseProject();

            return startSts;
        }
        #endregion

        #region  Finish checkpoint inspection
        //Finish inspection
        public async Task FinishInspection()
        {
            try
            {
                await Task.Factory.StartNew(() => { ZipFile.CreateFromDirectory(TEMP_IMG_ZIP_PATH, LOCAL_SERVER_PATH); }, TaskCreationOptions.LongRunning);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

        #region Event Handler
        //Event handler  from the event data received from TCP/IP
        private void TcpServer_OnDataReceived(object sender, string parameter)
        {
            if (string.IsNullOrEmpty(parameter) != true) //use is 
            {
                ResultDataProcces(sender,parameter);

            }
        }

        #endregion

        #region Result data processing and publishing

        //Data processing after received from TCP/IP
        public void ResultDataProcces(object sender,string parameter)
        {
            //process data ready for JSON converter
            //payload data during inspection might look like this: "testGroup=DeviceControllerPCBA,testName=BoardAssembly_x7screws,result=PASS,img=10-43-16_067.png,testEnd=TRUE"
            //payload data during checking serial : "s\n=12314ACVBbe,img=10-51-49_737.png"

            string MASTER_IMAGE_PATH = RVISData.SpecialSettingData.UIImageLoadPath + @"MASTER\"; //this path will be eg C:\RVIS\IMG\<ownself add Master>\<img.png>
            string LIVE_IMAGE_PATH = RVISData.SpecialSettingData.UIImageLoadPath + @"LIVE\current.png"; //this path will be eg C:\RVIS\IMG\<ownself add Live>\<img.png>
            string ROBOT_SAVE_PATH = RVISData.SpecialSettingData.TMImageSavePath; //this path is where robot save the image after inspection

            string robot_save_full_path = null; //to obtain the exact image path produced by TM
            string rename_img_path = null;
            string rename_image = null;
            

            //Verify data send from downstream
            string pattern_checkpoint = @"^testGroup=(?<Testgroup>[a-zA-Z0-9_]+),testName=(?<Testname>[a-zA-Z0-9_]+),result=(?<Result>[a-zA-Z0-9]+),img=(?<Img>[a-zA-Z0-9._-]+),testEnd=(?<End>[a-zA-Z]+)$";
            string pattern_serial = @"^s\n=(?<Serial>[a-zA-Z0-9]+),img=(?<Img>[a-zA-Z0-9:._-]+)$"; //might need imge path zip to the folder


            Match match_checkpoint = Regex.Match(parameter, pattern_checkpoint);
            Match match_serial = Regex.Match(parameter, pattern_serial);

            if (match_checkpoint.Success)
            {
                //reply to robot once received data
                tcpServer.ResponseToClient(sender, "OK");

                ProcTestGroup = match_checkpoint.Result("${Testgroup}");
                ProcTestName = match_checkpoint.Result("${Testname}");
                ProcResult = match_checkpoint.Result("${Result}");
                ProcLiveImgDir = match_checkpoint.Result("${Img}");
                ProcTestEnd = match_checkpoint.Result("${End}");


                //robot_save_full_path = ROBOT_SAVE_PATH + ProcLiveImgDir; //take exact path of image file -> eg. \\PN175\Shared\10-17_067.png
                robot_save_full_path = RVISData.SpecialSettingData.TMImageSavePath + ProcLiveImgDir; //take exact path of image file -> eg. \\PN175\Shared\10-17_067.png
                rename_image = ProcTestGroup + "_" + ProcTestName + ".png"; //rename image name to eg DeviceControllerPCBA_BoardAssembly_x7screws.png
                rename_img_path = TEMP_IMG_ZIP_PATH + rename_image; //eg -> C:\RVIS\ZIP\DeviceControllerPCBA_BoardAssembly_x7screws.png
                ProcMasterImgDir = MASTER_IMAGE_PATH + rename_image;
                //publish the rename image to GUI

                if (File.Exists(robot_save_full_path))
                {
                    System.IO.File.Copy(robot_save_full_path, rename_img_path, false); //copy from robot shared folder to to-be-Zipped folder w/o overwrite 
                    System.IO.File.Copy(rename_img_path, LIVE_IMAGE_PATH, true); //copy from to-be-Zipped folder to Live image directory file with overwrite
                }
                else
                {
                    Console.WriteLine("TM failed to saved image");
                }
                //publish to UI
                if (OnResultStringPub != null)
                {
                    OnResultStringPub(ProcTestGroup, ProcTestName, ProcResult, ProcMasterImgDir, ProcTestEnd);
                }
                if (ProcTestEnd.ToUpper() == "TRUE") //check is UUT completed
                {
                    Task.Factory.StartNew(async () => { await FinishInspection(); }, TaskCreationOptions.LongRunning);
                }
            }
            else if (match_serial.Success)
            {
                //reply to robot once received data
                tcpServer.ResponseToClient(sender, "OK");

                ProcSerial = match_serial.Result("${Serial}");
                ProcLiveImgDir = match_serial.Result("${Img}");

                if (OnSerialResultPub != null)
                {
                    OnSerialResultPub(ProcSerial, ProcLiveImgDir);
                }
            }

        }
        #endregion

        #region Get system status task

        public void GetRobotRunSts()
        {
            //get robot run status function - this can be replace to calling function like in FrmTestToolKit.cs
            lock (getRobotRunStsLock)
            {
                ModbusControl.GetProjectRunningStatus(ref robotRunSts);
            }
        }

        public void GetRobotErrSts()
        {
            lock (getRobotErrStsLock)
            {
                ModbusControl.GetProjectErrorStatus(ref robotErrSts);
            }
        }

        public void GetRobotPauseSts()
        {
            lock (getRobotPauseStsLock)
            {
                ModbusControl.GetProjectPauseStatus(ref robotPauseSts);
            }
        }

        public void GetRobotIOSts()
        {
            ;
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
                //cancel and dispose SysPrelimCheck()
                _cts.Cancel();
                _cts.Dispose();

                //unsubscribe to OnRecogPub event for process result string
                tcpServer.OnDataReceived -= TcpServer_OnDataReceived;
            }

        }

        #endregion

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
                        if (OnRobotStatusPub != null)
                        {
                            OnRobotStatusPub(robotRunSts, robotErrSts, robotPauseSts); //publish the robot status
                        }
                        Thread.Sleep(300);

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