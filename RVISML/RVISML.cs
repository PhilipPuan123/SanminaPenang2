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


namespace RVISMMC
{
    public class RVISML
    {
        #region member data initialization
        //member data
        bool completeUnitSts = false; //flag for completion of 1 UUT
        string imgDir = null; // store the image directory
        const string LOCAL_SERVER_PATH = null; // path the zip file going to store at their local server (user flexibility)
        private CancellationTokenSource _cts; //Background task resource cancel

        //robot sts
        bool robotRunSts; //robot running status 
        bool robotErrSts; //robot error status 
        bool robotPauseSts;//robot pause status 
        bool tcpIpSts; //comm sts between robot and PC 
       
        //auto implemented properties
        public string ProcTestName { get; set; } //payload test name
        public string ProcResult { get; set; } //payload result
        public string ProcLiveImgDir { get; set; } //payload live image dir
        public string ProcTestEnd { get; set; } //payload 1 UUT completion flag
        public string ProcSerial { get; set; } //payload serial number
        #endregion

        #region Event Initialization
        //serial number result payload publish
        public delegate void SerialResultPub(string serial, string dir);
        public event SerialResultPub OnSerialResultPub;
        //checkpoint payload result publish
        public delegate void DownstreamResultPub(string checkpt, string result, string dir, string testEnd);
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
            //subscribe to OnRecogPub event for process result string
            //OnResultStringPub += RVISML_OnResultStringPub;
            //subscribe to OnSerialResultPub event for processing serial result string
            //OnSerialResultPub += RVISML_OnSerialResultPub;

            //Start with system preliminary check
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
            string imgFolder = null; // folder path of stored image
            string pattern = @"^(?<Folder>[a-zA-Z0-9:\\]+)\\([a-zA-Z0-9.]+)$";

            Match imgPath = Regex.Match(imgDir, pattern);  //get the dir path
            if (imgPath.Success)
            {
                imgFolder = imgPath.Result("${Folder}");
                //zip the file & transfer to local server 
                try
                {
                    await Task.Factory.StartNew(() => { ZipFile.CreateFromDirectory(imgFolder, LOCAL_SERVER_PATH); }, TaskCreationOptions.LongRunning);
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        #endregion

        #region Event Handler
        //Event handler for result string publish from robot controller
        private void RVISML_OnResultStringPub(string checkpt, string result, string dir, string testEnd)
        {
            //nothing
            throw new NotImplementedException();
        }
        //Event handler for serial result publish
        private void RVISML_OnSerialResultPub(string serial, string dir)
        {
            //nothing
            throw new NotImplementedException();
        }

        //Event handler  from the event data received from TCP/IP
        private void TcpServer_OnDataReceived(object sender, string parameter)
        {
            if (string.IsNullOrEmpty(parameter) != true) //use is 
            {
                ResultDataProcces(parameter);
            }

            throw new NotImplementedException();
        }

        #endregion

        #region Result data processing and publishing

        //Data processing after received from TCP/IP
        public void ResultDataProcces(string parameter)
        {
            //process data ready for JSON converter
            //payload data might look like this: "test=ScrewA,result=PASS,path=C:\CurrentImage\ScrewA.bmp,ed=true,ser=true"
            //payload data series 

            //Verify data send from downstream
            string pattern_checkpoint = @"^checkpoint=(?<Testname>[a-zA-Z0-9]+),result=(?<Result>[a-zA-Z0-9]+),dir=(?<Dir>[a-zA-Z0-9:\\.]+),testEnd=(?<End>[a-zA-Z]+)$";
            string pattern_serial = @"^s\n=(?<Serial>[a-zA-Z0-9]+),dir=(?<Dir>[a-zA-Z0-9:\\.]+)$"; //might need imge path zip to the folder

            Match match_checkpoint = Regex.Match(parameter, pattern_checkpoint);
            Match match_serial = Regex.Match(parameter, pattern_serial);

            if (match_checkpoint.Success)
            {
                ProcTestName = match_checkpoint.Result("${Testname}");
                ProcResult = match_checkpoint.Result("${Result}");
                ProcLiveImgDir = match_checkpoint.Result("${Dir}");
                ProcTestEnd = match_checkpoint.Result("${End}");

                if (OnResultStringPub != null)
                {
                    OnResultStringPub(ProcTestName, ProcResult, ProcLiveImgDir, ProcTestEnd);
                }

                if (ProcTestEnd == "True") //check is UUT completed
                {
                    imgDir = ProcLiveImgDir;
                    FinishInspection();
                }

            }
            else if (match_serial.Success)
            {
                ProcSerial = match_serial.Result("${Serial}");
                ProcLiveImgDir = match_serial.Result("${Dir}");

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
            // MError Merr = (MError)ModbusControl.ProcCommand(TMModbusCmd.GetRunStatus, out robotRunSts);
            lock (getRobotRunStsLock)
            {
                ModbusControl.GetProjectRunningStatus(ref robotRunSts);
            }
        }

        public void GetRobotErrSts()
        {
            //MError Merr = (MError)ModbusControl.ProcCommand(TMModbusCmd.GetErrorStatus, out robotErrSts);
            lock (getRobotErrStsLock)
            {
                ModbusControl.GetProjectErrorStatus(ref robotErrSts);
            }
        }

        public void GetRobotPauseSts()
        {
            //MError Merr = (MError)ModbusControl.ProcCommand(TMModbusCmd.GetPauseStatus, out robotPauseSts);
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
                //unsubscribe to OnRecogPub event for process result string
                OnResultStringPub -= RVISML_OnResultStringPub;
                //unsubscribe to OnSerialResultPub event for processing serial result string
                OnSerialResultPub -= RVISML_OnSerialResultPub;
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