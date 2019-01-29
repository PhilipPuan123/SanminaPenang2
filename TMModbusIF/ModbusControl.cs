using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections;

namespace TMModbusIF
{
    public class ModbusControl
    {
        #region Declaration
            // Constants
            private const byte SLAVE_ID = 1;
       
            // Private declaration
            private static ushort modbusTransactionId = 0;
            private static Master master = null;
        #endregion

        #region Property
            public static bool IsRunning { get; private set; }
        #endregion Property

        #region Connect
            /// <summary>
            /// Start Modbus Communication.
            /// </summary>
            /// <param name="ip"></param>
            /// <param name="port"></param>
            /// <returns></returns>
            public static int Connect(string ip, string port)
            {
                IPAddress _ip;
                int _port;

                // Check IP Address Format
                if (IPAddress.TryParse(ip, out _ip) != true) return (int)MError.InvalidIP;
                if (Int32.TryParse(port, out _port) != true) return (int)MError.InvalidPort;

                return Connect(_ip, _port);
            }

            /// <summary>
            /// Start Modbus Communication.
            /// </summary>
            /// <param name="ip"></param>
            /// <param name="port"></param>
            /// <returns></returns>
            public static int Connect(IPAddress ip, int port)
            {
                // Stop
                Disconnect();

                //System.Threading.Thread.Sleep(100);
                
                // Start Modbus Connection
                if (master == null)
                {
                    try
                    {
                        //master = new Master(ip, port);
                        master = new Master();          // Debug_20190121
                        master.Connect(ip, port);       // Debug_20190121
                    }
                    catch (Exception)
                    {
                        master = null;
                        return (int)MError.InitializeFail;
                    }
                }

                if (master == null || !master.connected)
                {
                    return (int)MError.InitializeFail;
                }

                IsRunning = true;
                return (int)MError.OK;
            }
        #endregion Connect

        #region Disconnect
            /* Stop Modbus Communication */ 
            public static void Disconnect()
            {
                // Stop Modbus Connection
                if (master != null)
                {
                    master.Disconnect();
                }
                IsRunning = false;
                master = null;
            }
        #endregion Disconnect

        public static int ProcCommand(TMModbusCmd command, out string result, bool setOnOff = false)
        {
            int error = 0;
            float coordinate = 0.00f;
            bool onOff = false;
            string data = string.Empty;

            int lastErrCode = 0;
            DateTime lastErrDateTime = new DateTime();

            BitArray bits;
            byte[] bytes;

            switch (command)
            {
                case TMModbusCmd.Start:
                    error = StartOrPauseProject();
                    break;
                case TMModbusCmd.Stop:
                    error = StopProject();
                    break;
                case TMModbusCmd.Pause:
                    error = StartOrPauseProject();
                    break;
                case TMModbusCmd.GetRobotCoorX:
                    error = GetRobotCoordinateX(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetRobotCoorY:
                    error = GetRobotCoordinateY(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetRobotCoorZ:
                    error = GetRobotCoordinateZ(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetRobotCoorRX:
                    error = GetRobotCoordinateRX(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetRobotCoorRY:
                    error = GetRobotCoordinateRY(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetRobotCoorRZ:
                    error = GetRobotCoordinateRZ(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetToolCoorX:
                    error = GetToolCoordinateX(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetToolCoorY:
                    error = GetToolCoordinateY(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetToolCoorZ:
                    error = GetToolCoordinateZ(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetToolCoorRX:
                    error = GetToolCoordinateRX(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetToolCoorRY:
                    error = GetToolCoordinateRY(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetToolCoorRZ:
                    error = GetToolCoordinateRZ(ref coordinate);
                    data = coordinate.ToString();
                    break;
                case TMModbusCmd.GetErrorStatus:
                    error = GetProjectErrorStatus(ref onOff);
                    data = onOff.ToString();
                    break;
                case TMModbusCmd.GetRunStatus:
                    error = GetProjectRunningStatus(ref onOff);
                    data = onOff.ToString();
                    break;
                case TMModbusCmd.GetEditStatus:
                    error = GetProjectEditingStatus(ref onOff);
                    data = onOff.ToString();
                    break;
                case TMModbusCmd.GetPauseStatus:
                    error = GetProjectPauseStatus(ref onOff);
                    data = onOff.ToString();
                    break;
                case TMModbusCmd.GetPermissionStatus:
                    error = GetProjectPermissionStatus(ref onOff);
                    data = onOff.ToString();
                    break;
                case TMModbusCmd.GetLastError:
                    error = GetLastError(out lastErrCode, out lastErrDateTime);
                    data = "Date = " + lastErrDateTime.ToString("yyyy-MM-dd");
                    data += ", Time = " + lastErrDateTime.ToString("hh:mm:ss");
                    data += ", ErrCode = " + lastErrCode.ToString();
                    break;
                case TMModbusCmd.GetControlBoxDIn:
                    bits = new BitArray(16);
                    error = GetControlBoxAllDigitalInputs(ref bits);
                    data = BitsToString(bits);
                    bytes = new byte[2];
                    error = GetControlBoxAllDigitalInputs(ref bytes);
                    data += "  " + BitConverter.ToString(bytes);
                    break;
                case TMModbusCmd.GetEndModuleDIn:
                    bits = new BitArray(3);
                    error = GetEndModuleAllDigitalInputs(ref bits);
                    data = BitsToString(bits);
                    bytes = new byte[1];
                    error = GetEndModuleAllDigitalInputs(ref bytes);
                    data += "  " + BitConverter.ToString(bytes);
                    break;
                case TMModbusCmd.GetControlBoxDOut:
                    bits = new BitArray(16);
                    error = GetControlBoxAllDigitalOutputs(ref bits);
                    data = BitsToString(bits);
                    bytes = new byte[2];
                    error = GetControlBoxAllDigitalOutputs(ref bytes);
                    data += "  " + BitConverter.ToString(bytes);
                    break;
                case TMModbusCmd.GetEndModuleDOut:
                    bits = new BitArray(3);
                    error = GetEndModuleAllDigitalOutputs(ref bits);
                    data = BitsToString(bits);
                    bytes = new byte[1];
                    error = GetEndModuleAllDigitalOutputs(ref bytes);
                    data += "  " + BitConverter.ToString(bytes);
                    break;
                case TMModbusCmd.SetEndModuleDO0:
                    error = SetEndModule_DO0(setOnOff);
                    break;
                case TMModbusCmd.SetEndModuleDO1:
                    error = SetEndModule_DO1(setOnOff);
                    break;
                case TMModbusCmd.SetEndModuleDO2:
                    error = SetEndModule_DO2(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO0:
                    error = SetControlBox_DO0(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO1:
                    error = SetControlBox_DO1(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO2:
                    error = SetControlBox_DO2(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO3:
                    error = SetControlBox_DO3(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO4:
                    error = SetControlBox_DO4(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO5:
                    error = SetControlBox_DO5(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO6:
                    error = SetControlBox_DO6(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO7:
                    error = SetControlBox_DO7(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO8:
                    error = SetControlBox_DO8(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO9:
                    error = SetControlBox_DO9(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO10:
                    error = SetControlBox_DO10(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO11:
                    error = SetControlBox_DO11(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO12:
                    error = SetControlBox_DO12(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO13:
                    error = SetControlBox_DO13(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO14:
                    error = SetControlBox_DO14(setOnOff);
                    break;
                case TMModbusCmd.SetControlBoxDO15:
                    error = SetControlBox_DO15(setOnOff);
                    break;
            }

            /* Set return parameter to empty if error */
            if (error != (int)MError.OK)
            {
                result = string.Empty;
            }
            else
            {
                result = data;
            }

            return error;
        }

        #region Control
            /// <summary>
            /// Play or pause TM Robot project.
            /// </summary>
            /// <returns></returns>
            public static int StartOrPauseProject()
            {
                return WriteSingleDigitalOut(TMModbusAddress.PLAY_PAUSE, true);
            }

            /// <summary>
            /// Pause TM Robot project.
            /// </summary>
            /// <returns></returns>
            public static int PauseProject()
            {
                return WriteSingleDigitalOut(TMModbusAddress.PLAY_PAUSE, false);
            }

            /// <summary>
            /// Stop TM Robot project.
            /// </summary>
            /// <returns></returns>
            public static int StopProject()
            {
                return WriteSingleDigitalOut(TMModbusAddress.STOP, true);
            }
        #endregion Control

        #region Read Project Status
            /// <summary>
            /// Get project error status.
            /// </summary>
            /// <param name="isError">Contains value of error status.</param>
            /// <returns></returns>
            public static int GetProjectErrorStatus(ref bool isError)
            {
                return ReadRobotStatus(TMModbusAddress.STS_ERROR, ref isError);
            }

            /// <summary>
            /// Get project running status.
            /// </summary>
            /// <param name="isRunning">Contains value of project running status.</param>
            /// <returns></returns>
            public static int GetProjectRunningStatus(ref bool isRunning)
            {
                return ReadRobotStatus(TMModbusAddress.STS_RUN, ref isRunning);
            }

            /// <summary>
            /// Get project editing status.
            /// </summary>
            /// <param name="isEditing">Contains value of project editing status.</param>
            /// <returns></returns>
            public static int GetProjectEditingStatus(ref bool isEditing)
            {
                return ReadRobotStatus(TMModbusAddress.STS_EDIT, ref isEditing);
            }

            /// <summary>
            /// Get project pause status.
            /// </summary>
            /// <param name="isPausing">Contains value of project pause status.</param>
            /// <returns></returns>
            public static int GetProjectPauseStatus(ref bool isPausing)
            {
                return ReadRobotStatus(TMModbusAddress.STS_PAUSE, ref isPausing);
            }

            /// <summary>
            /// Get permission get status.
            /// </summary>
            /// <param name="hasPermission">Contains value of permission status.</param>
            /// <returns></returns>
            public static int GetProjectPermissionStatus(ref bool hasPermission)
            {
                return ReadRobotStatus(TMModbusAddress.STS_PERMISSION, ref hasPermission);
            }
        #endregion Read Project Status

        #region Read DI
        public static int GetControlBoxAllDigitalInputs(ref BitArray result)
        {
            return ReadDigitalInputs(TMModbusAddress.CTRL_DI0, 16, ref result);
        }

        public static int GetControlBoxAllDigitalInputs(ref byte[] result)
        {
            return ReadDigitalInputs(TMModbusAddress.CTRL_DI0, 16, ref result);
        }

        public static int GetEndModuleAllDigitalInputs(ref BitArray result)
        {
            return ReadDigitalInputs(TMModbusAddress.EMOD_DI0, 3, ref result);
        }

        public static int GetEndModuleAllDigitalInputs(ref byte[] result)
        {
            return ReadDigitalInputs(TMModbusAddress.EMOD_DI0, 3, ref result);
        }
        #endregion Read DI

        #region Read DO
        public static int GetControlBoxAllDigitalOutputs(ref BitArray result)
        {
            return ReadDigitalOutputs(TMModbusAddress.CTRL_DO0, 16, ref result);
        }

        public static int GetControlBoxAllDigitalOutputs(ref byte[] result)
        {
            return ReadDigitalOutputs(TMModbusAddress.CTRL_DO0, 16, ref result);
        }

        public static int GetEndModuleAllDigitalOutputs(ref BitArray result)
        {
            return ReadDigitalOutputs(TMModbusAddress.EMOD_DO0, 3, ref result);
        }

        public static int GetEndModuleAllDigitalOutputs(ref byte[] result)
        {
            return ReadDigitalOutputs(TMModbusAddress.EMOD_DO0, 3, ref result);
        }
        #endregion Read DO

        #region Write DO
        public static int SetEndModule_DO0(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.EMOD_DO0, onOff);
        }
        public static int SetEndModule_DO1(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.EMOD_DO1, onOff);
        }
        public static int SetEndModule_DO2(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.EMOD_DO2, onOff);
        }
        public static int SetControlBox_DO0(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO0, onOff);
        }
        public static int SetControlBox_DO1(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO1, onOff);
        }
        public static int SetControlBox_DO2(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO2, onOff);
        }
        public static int SetControlBox_DO3(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO3, onOff);
        }
        public static int SetControlBox_DO4(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO4, onOff);
        }
        public static int SetControlBox_DO5(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO2, onOff);
        }
        public static int SetControlBox_DO6(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO6, onOff);
        }
        public static int SetControlBox_DO7(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO7, onOff);
        }
        public static int SetControlBox_DO8(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO8, onOff);
        }
        public static int SetControlBox_DO9(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO9, onOff);
        }
        public static int SetControlBox_DO10(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO10, onOff);
        }
        public static int SetControlBox_DO11(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO11, onOff);
        }
        public static int SetControlBox_DO12(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO12, onOff);
        }
        public static int SetControlBox_DO13(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO13, onOff);
        }
        public static int SetControlBox_DO14(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO14, onOff);
        }
        public static int SetControlBox_DO15(bool onOff)
        {
            return WriteSingleDigitalOut(TMModbusAddress.CTRL_DO15, onOff);
        }
        #endregion Write DO

        #region Read Robot Coordinate
        /// <summary>
        /// Get robot coordinate X.
        /// </summary>
        /// <param name="x">Contains value of robot coordinate X.</param>
        /// <returns></returns>
        public static int GetRobotCoordinateX(ref float x)
            {
                return ReadCoordinate(TMModbusAddress.COOR_ROBOT_X, ref x);
            }

            /// <summary>
            /// Get robot coordinate Y.
            /// </summary>
            /// <param name="y">Contains value of robot coordinate Y.</param>
            /// <returns></returns>
            public static int GetRobotCoordinateY(ref float y)
            {
                return ReadCoordinate(TMModbusAddress.COOR_ROBOT_Y, ref y);
            }

            /// <summary>
            /// Get robot coordinate Z.
            /// </summary>
            /// <param name="z">Contains value of robot coordinate Z.</param>
            /// <returns></returns>
            public static int GetRobotCoordinateZ(ref float z)
            {
                return ReadCoordinate(TMModbusAddress.COOR_ROBOT_Z, ref z);
            }

            /// <summary>
            /// Get robot coordinate RX.
            /// </summary>
            /// <param name="rx">Contains value of robot coordinate RX.</param>
            /// <returns></returns>
            public static int GetRobotCoordinateRX(ref float rx)
            {
                return ReadCoordinate(TMModbusAddress.COOR_ROBOT_RX, ref rx);
            }

            /// <summary>
            /// Get robot coordinate RY.
            /// </summary>
            /// <param name="ry">Contains value of robot coordinate RY.</param>
            /// <returns></returns>
            public static int GetRobotCoordinateRY(ref float ry)
            {
                return ReadCoordinate(TMModbusAddress.COOR_ROBOT_RY, ref ry);
            }

            /// <summary>
            /// Get robot coordinate RZ.
            /// </summary>
            /// <param name="rz">Contains value of robot coordinate RZ.</param>
            /// <returns></returns>
            public static int GetRobotCoordinateRZ(ref float rz)
            {
                return ReadCoordinate(TMModbusAddress.COOR_ROBOT_RZ, ref rz);
            }
        #endregion Robot Coordinate

        #region Tool Coordinate
            /// <summary>
            /// Get tool coordinate X.
            /// </summary>
            /// <param name="x">Contains value of tool coordinate X.</param>
            /// <returns></returns>
            public static int GetToolCoordinateX(ref float x)
            {
                return ReadCoordinate(TMModbusAddress.COOR_TOOL_X, ref x);
            }

            /// <summary>
            /// Get tool coordinate Y.
            /// </summary>
            /// <param name="y">Contains value of tool coordinate Y.</param>
            /// <returns></returns>
            public static int GetToolCoordinateY(ref float y)
            {
                return ReadCoordinate(TMModbusAddress.COOR_TOOL_Y, ref y);
            }

            /// <summary>
            /// Get tool coordinate Z.
            /// </summary>
            /// <param name="z">Contains value of tool coordinate Z.</param>
            /// <returns></returns>
            public static int GetToolCoordinateZ(ref float z)
            {
                return ReadCoordinate(TMModbusAddress.COOR_TOOL_Z, ref z);
            }

            /// <summary>
            /// Get tool coordinate RX.
            /// </summary>
            /// <param name="rx">Contains value of tool coordinate RX.</param>
            /// <returns></returns>
            public static int GetToolCoordinateRX(ref float rx)
            {
                return ReadCoordinate(TMModbusAddress.COOR_TOOL_RX, ref rx);
            }

            /// <summary>
            /// Get tool coordinate RY.
            /// </summary>
            /// <param name="ry">Contains value of tool coordinate RY.</param>
            /// <returns></returns>
            public static int GetToolCoordinateRY(ref float ry)
            {
                return ReadCoordinate(TMModbusAddress.COOR_TOOL_RY, ref ry);
            }

            /// <summary>
            /// Get tool coordinate RZ.
            /// </summary>
            /// <param name="rz">Contains value of tool coordinate RZ</param>
            /// <returns></returns>
            public static int GetToolCoordinateRZ(ref float rz)
            {
                return ReadCoordinate(TMModbusAddress.COOR_TOOL_RZ, ref rz);
            }
        #endregion Tool Coordinate

        #region Get Last Error
            public static int GetLastError(out int errCode, out DateTime errDateTime)
            {
                MError error = MError.OK;
                byte[] data = null;
                ushort length = 8;
                ushort startAddress = TMModbusAddress.LAST_ERROR_CODE;

                // Initialize variables
                errCode = 0;
                errDateTime = new DateTime();

                // Read from modbus
                error = master.ReadInputRegister(modbusTransactionId++, SLAVE_ID, startAddress, length, ref data);
                if (error != MError.OK)
                {
                    return (int)error;
                }

                if (data.Length < 16)
                {
                    return (int)MError.InvalidDataLength;
                }
                try
                {
                    // Convert byte array to error code and date time format
                    int errorCode = BitConverter.ToInt32(new byte[] { data[3], data[2], data[1], data[0] }, 0);
                    short year = BitConverter.ToInt16(new byte[] { data[5], data[4] }, 0);
                    short month = BitConverter.ToInt16(new byte[] { data[7], data[6] }, 0);
                    short day = BitConverter.ToInt16(new byte[] { data[9], data[8] }, 0);
                    short hour = BitConverter.ToInt16(new byte[] { data[11], data[10] }, 0);
                    short minute = BitConverter.ToInt16(new byte[] { data[13], data[12] }, 0);
                    short second = BitConverter.ToInt16(new byte[] { data[15], data[14] }, 0);

                    // Combine DateTime
                    DateTime lastErrDateTime = new DateTime(year, month, day, hour, minute, second);

                    // Pass the value to return parameters
                    errCode = errorCode;
                    errDateTime = lastErrDateTime;
                }
                catch
                {
                    return (int)MError.InvalidDataValue;
                }
                return (int)MError.OK;
            }
        #endregion Get Last Error

        #region Functions
            /// <summary>
            /// Convert bit array to string
            /// </summary>
            /// <param name="bits"></param>
            /// <returns></returns>
            private static string BitsToString(BitArray bits)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var b in bits)
                {
                    sb.Append((bool)b ? "1" : "0");
                }
                return sb.ToString();
            }
        
            /// <summary>
            /// Get all digital inputs on control box.(16-bits) 
            /// </summary>
            /// <param name="result">Return result as bit array.</param>
            /// <returns></returns>
            private static int ReadDigitalInputs(ushort startAddress, ushort numInputs, ref BitArray result)
            {
                MError error = MError.OK;
                byte[] data = null;

                // Read from modbus
                error = master.ReadDiscreteInputs(modbusTransactionId++, SLAVE_ID, startAddress, numInputs, ref data);
                if (error != MError.OK)
                {
                    return (int)error;
                }
                // Check data size
                if (data.Length == 1 || data.Length == 2) result = new BitArray(data);
                else return (int)MError.InvalidDataLength;
                
                return (int)MError.OK;
            }

            /// <summary>
            /// Get all digital inputs on control box.(16-bits) 
            /// </summary>
            /// <param name="result">Return result as bytes.</param>
            /// <returns></returns>
            private static int ReadDigitalInputs(ushort startAddress, ushort numInputs, ref byte[] result)
            {
                MError error = MError.OK;
                byte[] data = null;

                // Read from modbus
                error = master.ReadDiscreteInputs(modbusTransactionId++, SLAVE_ID, startAddress, numInputs, ref data);
                if (error != MError.OK)
                {
                    return (int)error;
                }
                // Check data size
                if (data.Length == 1 || data.Length == 2) result = data;
                else return (int)MError.InvalidDataLength;

                return (int)MError.OK;
            }

            /// <summary>
            /// Get all digital outputs on control box.(16-bits) 
            /// </summary>
            /// <param name="result">Return result as bit array.</param>
            /// <returns></returns>
            private static int ReadDigitalOutputs(ushort startAddress, ushort numInputs, ref BitArray result)
            {
                MError error = MError.OK;
                byte[] data = null;

                // Read from modbus
                error = master.ReadCoils(modbusTransactionId++, SLAVE_ID, startAddress, numInputs, ref data);
                if (error != MError.OK)
                {
                    return (int)error;
                }
                // Check data size
                if (data.Length == 1 || data.Length == 2) result = new BitArray(data);
                else return (int)MError.InvalidDataLength;

                return (int)MError.OK;
            }

            /// <summary>
            /// Get all digital inputs on control box.(16-bits) 
            /// </summary>
            /// <param name="result">Return result as bytes.</param>
            /// <returns></returns>
            private static int ReadDigitalOutputs(ushort startAddress, ushort numInputs, ref byte[] result)
            {
                MError error = MError.OK;
                byte[] data = null;

                // Read from modbus
                error = master.ReadCoils(modbusTransactionId++, SLAVE_ID, startAddress, numInputs, ref data);
                if (error != MError.OK)
                {
                    return (int)error;
                }
                // Check data size
                if (data.Length == 1 || data.Length == 2) result = data;
                else return (int)MError.InvalidDataLength;

                return (int)MError.OK;
            }

            /// <summary>
            /// Read robot status from TM Controller.
            /// </summary>
            /// <param name="startAddress">Address from where the data read begins.</param>
            /// <param name="result">Contains the value of status.</param>
            /// <returns></returns>
            private static int ReadRobotStatus(ushort startAddress, ref bool result)
            {
                MError error = MError.OK;
                byte[] data = null;
                ushort value = 0;
                // Read from modbus
                error = master.ReadDiscreteInputs(modbusTransactionId++, SLAVE_ID, startAddress, 1, ref data);
                if(error != MError.OK)
                {
                    return (int)error;
                }
                // Check data size
                if (data.Length == 1) value = data[0];
                else if (data.Length == 2) value = data[1];
                else return (int)MError.InvalidDataLength;

                // Set data
                if (value > 0) result = true;
                else result = false;

                return (int)MError.OK;
            }

            /// <summary>
            /// Read coordinate from TM Controller.
            /// </summary>
            /// <param name="startAddress">Address from where the data read begins.</param>
            /// <param name="coordinate">Contains the value of coordinate.</param>
            /// <returns></returns>
            private static int ReadCoordinate(ushort startAddress, ref float coordinate)
            {
                MError error = MError.OK;
                byte[] data = null;
                ushort length = 2;
                // Read from modbus
                error = master.ReadInputRegister(modbusTransactionId++, SLAVE_ID, startAddress, length, ref data);
                if (error != MError.OK)
                {
                    return (int)error;
                }

                // Check data size
                if (data.Length < 4)
                {
                   return (int)MError.InvalidDataLength;
                }

                // Convert byte array to float : DC BA 
                coordinate = BitConverter.ToSingle(new byte[] { data[3], data[2], data[1], data[0] }, 0); ;

                return (int)MError.OK;
            }

            /// <summary>
            /// Write single digital output.
            /// </summary>
            /// <param name="startAddress">Address from where the data write begins.</param>
            /// <param name="onOff">Specifys if the coil should be switched on or off.</param>
            /// <returns></returns>
            private static int WriteSingleDigitalOut(ushort startAddress, bool onOff)
            {
                MError error = MError.OK;
                byte[] result = null;

                // Write through modbus
                error = master.WriteSingleCoils(modbusTransactionId++, SLAVE_ID, startAddress, onOff, ref result);
                if (error != MError.OK)
                {
                    return (int)error;
                }

                return (int)MError.OK;
            }

            private static int ProcWriteSingleRegister(ushort startAddress, byte[] values)
            {
                MError error = MError.OK;
                byte[] result = null;

                error = master.WriteSingleRegister(modbusTransactionId++, SLAVE_ID, startAddress, values, ref result);
                if(error != MError.OK)
                {
                    return (int)error;
                }

                return (int)MError.OK;
            }

            private static int ProcWriteMultipleRegister(ushort startAddress, byte[] values)
            {
                MError error = MError.OK;
                byte[] result = null;

                error = master.WriteMultipleRegister(modbusTransactionId++, SLAVE_ID, startAddress, values, ref result);
                if(error!= MError.OK)
                {
                    return (int)error;
                }

                return (int)MError.OK;
            }
        #endregion Functions
    }
}
