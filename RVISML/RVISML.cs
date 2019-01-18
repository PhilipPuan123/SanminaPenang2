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
        private CancellationTokenSource _cts; //Background task resource cancel

        //robot sts
        string robotRunSts; //robot running status
        string robotErrSts; //robot error status 
        string robotPauseSts;//robot pause status

        //auto implemented properties
        public string ProcTestName { get; set; } //payload test name
        public string ProcResult { get; set; } //payload result
        public string ProcLiveImgDir { get; set; } //payload live image dir
        public string ProcTestEnd { get; set; } //payload 1 UUT completion flag
        public string ProcSerial { get; set; } //payload serial number
        #endregion

        #region Event Initialization
        //serial number result payload publish
        public delegate void SerialResultPub(string serial);
        public event SerialResultPub OnSerialResultPub;
        //checkpoint payload result publish
        public delegate void DownstreamResultPub(string checkpt, string result, string dir, string testEnd);
        public event DownstreamResultPub OnResultStringPub;
        //Background robot status payload publish
        public delegate void RobotStatusPub(string robotRunSts, string robotErrSts, string robotPauseSts);
        public event RobotStatusPub OnRobotStatusPub;
        #endregion 

        #region create related obj required
        //create TcpIF- Server object
        Server tcpServer = new Server();
        #endregion        
        
        #region Main Task to subscribe and start function call
        public void Start() //Main execution function
        {           
            //subscribing to the TCPIP event for new data input
            tcpServer.OnDataReceived += TcpServer_OnDataReceived;
            //subscribe to OnRecogPub event for process result string
            OnResultStringPub += RVISML_OnResultStringPub;
            //subscribe to OnSerialResultPub event for processing serial result string
            OnSerialResultPub += RVISML_OnSerialResultPub;

            //1.Start with system preliminary check
            
            if ((robotRunSts == "false") && (robotErrSts == "false") && (robotPauseSts == "false"))
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

        #region Check serial number
        public bool CheckSerial() 
        {
            bool serialSts = false;
            //maybe get status from MES, just for record.
            //once GUI receive from MES, GUI called modbus to CONTINUE run robot

            return serialSts;
        }
        #endregion

        #region  Finish checkpoint inspection

        //Finish inspection
        public async Task FinishInspection()
        {
           string imgFolder = null; // folder path of stored image
           string localServerPath = null; // path the zip file going to store at their local server
           string pattern = @"^(?<Folder>[a-zA-Z0-9:\\]+)\\([a-zA-Z0-9.]+)$";

           //get the dir path 
           Match imgPath = Regex.Match(imgDir, pattern);
           if (imgPath.Success)
           {
                imgFolder = imgPath.Result("${Folder}");
                //zip the file & transfer to local server 
                await Task.Factory.StartNew(() => { ZipFile.CreateFromDirectory(imgFolder, localServerPath); },TaskCreationOptions.LongRunning);
           }
        }

        #endregion

        #region Event Handler
        //Event handler for result string publish from robot controller
        private void RVISML_OnResultStringPub(string checkpt, string result, string dir, string testEnd)
        {
          if (testEnd == "True")
          {
                 imgDir = dir;
                 FinishInspection();
          }
          throw new NotImplementedException();
        }
        //Event handler for serial result publish
        private void RVISML_OnSerialResultPub(string serial)
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
            string pattern_serial = @"^ser=(?<Serial>[a-zA-Z0-9]+)$"; //might need imge path zip to the folder

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

            }else if(match_serial.Success)
            {
                ProcSerial = match_serial.Result("${Serial}");

                if(OnSerialResultPub != null)
                {
                    OnSerialResultPub(ProcSerial);
                }
            }

        }
        #endregion

        #region Get system status task

        public void GetRobotRunSts()
        {
            //get robot run status function - this can be replace to calling function like in FrmTestToolKit.cs
             MError Merr = (MError)ModbusControl.ProcCommand(TMModbusCmd.GetRunStatus, out robotRunSts);
            if(robotRunSts == "false")
            {
                robotRunSts = "false";
            }else
            {
                robotRunSts = "true";
            }
           
        }

        public void GetRobotErrSts()
        { 
            MError Merr = (MError)ModbusControl.ProcCommand(TMModbusCmd.GetErrorStatus, out robotErrSts);
            if (robotErrSts == "false")
            {
                robotErrSts = "false";
            }
            else
            {
                robotErrSts = "true";
            }
        }

        public void GetRobotPauseSts()
        {
            MError Merr = (MError)ModbusControl.ProcCommand(TMModbusCmd.GetPauseStatus, out robotPauseSts);
            if (robotPauseSts == "false")
            {
                robotPauseSts = "false";
            }
            else
            {
                robotPauseSts = "true";
            }
        }

        public void GetRobotIOSts()
        {
            ;
        }

        public void EndAllServices()
        {
            if(_cts != null)
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
                        OnRobotStatusPub(robotRunSts, robotErrSts, robotPauseSts); //publish the robot status
                        Thread.Sleep(500);

                    }
                    catch (OperationCanceledException)
                    {
                        //do something
                    }
                }
            }, TaskCreationOptions.LongRunning);

        }
        #endregion
    }






}