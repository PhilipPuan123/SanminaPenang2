using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TMModbusIF
{
    public enum MError : int
    {
        /// <summary>Constant for OK</summary>
        OK = 0,
        /// <summary>Constant for exception illegal function.</summary>
        IllegalFunction         = 1,
        /// <summary>Constant for exception illegal data address.</summary>
        IllegalDataAddress      = 2,
        /// <summary>Constant for exception illegal data value.</summary>
        IllegalDataValue        = 3,
        /// <summary>Constant for exception slave device failure.</summary>
        SlaveDeviceFailure      = 4,
        /// <summary>Constant for exception acknowledge.</summary>
        Ack                     = 5,
        /// <summary>Constant for exception slave is busy/booting up.</summary>
        SlaveIsBusy             = 6,
        /// <summary>Constant for exception gate path unavailable.</summary>
        GatePathUnavailable     = 10,
        /// <summary>Constant for exception send failt.</summary>
        SendFail                = 100,
        /// <summary>Constant for exception wrong offset.</summary>
        WrongOffset             = 128,
        /// <summary>Constant for exception not connected.</summary>
        NotConnected            = 253,
        /// <summary>Constant for exception connection lost.</summary>
        ConnectionLost          = 254,
        /// <summary>Constant for exception response timeout.</summary>
        ResponseTimeout         = 255,
        /// <summary>Constant for invalid IP address.</summary>
        InvalidIP               = -100,
        /// <summary>Constant for invalid port number.</summary>
        InvalidPort             = -101,
        /// <summary>Constant for initialization fail.</summary>
        InitializeFail          = -102,
        /// <summary>Constant for invalid received data length.</summary>
        InvalidDataLength       = -103,
        /// <summary>Constant for invalid received data value.</summary>
        InvalidDataValue        = -104
    }

    public enum TMModbusCmd
    {
        Start,
        Pause,
        Stop,
        GetErrorStatus,
        GetRunStatus,
        GetEditStatus,
        GetPauseStatus,
        GetPermissionStatus,
        GetRobotCoorX,
        GetRobotCoorY,
        GetRobotCoorZ,
        GetRobotCoorRX,
        GetRobotCoorRY,
        GetRobotCoorRZ,
        GetToolCoorX,
        GetToolCoorY,
        GetToolCoorZ,
        GetToolCoorRX,
        GetToolCoorRY,
        GetToolCoorRZ,
        GetLastError,
        GetControlBoxDIn,
        GetControlBoxDOut,
        GetEndModuleDIn,
        GetEndModuleDOut,
        SetEndModuleDO0,
        SetEndModuleDO1,
        SetEndModuleDO2,
        SetControlBoxDO0,
        SetControlBoxDO1,
        SetControlBoxDO2,
        SetControlBoxDO3,
        SetControlBoxDO4,
        SetControlBoxDO5,
        SetControlBoxDO6,
        SetControlBoxDO7,
        SetControlBoxDO8,
        SetControlBoxDO9,
        SetControlBoxDO10,
        SetControlBoxDO11,
        SetControlBoxDO12,
        SetControlBoxDO13,
        SetControlBoxDO14,
        SetControlBoxDO15
    }

    internal class TMModbusAddress
    {
        /* Start Address of project status */
        public const ushort STS_ERROR       = 7201;
        public const ushort STS_RUN         = 7202;
        public const ushort STS_EDIT        = 7203;
        public const ushort STS_PAUSE       = 7204;
        public const ushort STS_PERMISSION  = 7205;
        /* Start Address of End Module Digital IN */
        public const ushort EMOD_DI0    = 0800;
        public const ushort EMOD_DI1    = 0801;
        public const ushort EMOD_DI2    = 0802;
        /* Start Address of End Module Digital OUT */
        public const ushort EMOD_DO0    = 0800;
        public const ushort EMOD_DO1    = 0801;
        public const ushort EMOD_DO2    = 0802;
        public const ushort EMOD_DO3    = 0803;
        /* Start Address of End Module Analog IN */
        public const ushort EMOD_AI0    = 0800;
        /* Start Address of Control Box Digital IN */
        public const ushort CTRL_DI0    = 0000;
        public const ushort CTRL_DI1    = 0001;
        public const ushort CTRL_DI2    = 0002;
        public const ushort CTRL_DI3    = 0003;
        public const ushort CTRL_DI4    = 0004;
        public const ushort CTRL_DI5    = 0005;
        public const ushort CTRL_DI6    = 0006;
        public const ushort CTRL_DI7    = 0007;
        public const ushort CTRL_DI8    = 0008;
        public const ushort CTRL_DI9    = 0009;
        public const ushort CTRL_DI10   = 0010;
        public const ushort CTRL_DI11   = 0011;
        public const ushort CTRL_DI12   = 0012;
        public const ushort CTRL_DI13   = 0013;
        public const ushort CTRL_DI14   = 0014;
        public const ushort CTRL_DI15   = 0015;
        /* Start Address of Control Box Digital OUT */
        public const ushort CTRL_DO0    = 0000;
        public const ushort CTRL_DO1    = 0001;
        public const ushort CTRL_DO2    = 0002;
        public const ushort CTRL_DO3    = 0003;
        public const ushort CTRL_DO4    = 0004;
        public const ushort CTRL_DO5    = 0005;
        public const ushort CTRL_DO6    = 0006;
        public const ushort CTRL_DO7    = 0007;
        public const ushort CTRL_DO8    = 0008;
        public const ushort CTRL_DO9    = 0009;
        public const ushort CTRL_DO10   = 0010;
        public const ushort CTRL_DO11   = 0011;
        public const ushort CTRL_DO12   = 0012;
        public const ushort CTRL_DO13   = 0013;
        public const ushort CTRL_DO14   = 0014;
        public const ushort CTRL_DO15   = 0015;
        /* Start Address of Control Box Analog OUT */
        public const ushort CTRL_AO0    = 0000;
        /* Start Address of Control Box Analog IN */
        public const ushort CTRL_AI1    = 0000;
        public const ushort CTRL_AI2    = 0002;
        /* Start Address of Tool Coordinate */
        public const ushort COOR_TOOL_X = 7025;
        public const ushort COOR_TOOL_Y = 7027;
        public const ushort COOR_TOOL_Z = 7029;
        public const ushort COOR_TOOL_RX = 7031;
        public const ushort COOR_TOOL_RY = 7033;
        public const ushort COOR_TOOL_RZ = 7035;
        /* Start Address of Robot Coordinate */
        public const ushort COOR_ROBOT_X = 7037;
        public const ushort COOR_ROBOT_Y = 7039;
        public const ushort COOR_ROBOT_Z = 7041;
        public const ushort COOR_ROBOT_RX = 7043;
        public const ushort COOR_ROBOT_RY = 7045;
        public const ushort COOR_ROBOT_RZ = 7047;

        public const ushort PROJ_SPEED  = 7101;
        public const ushort M_A_MODE    = 7102;
        public const ushort PLAY_PAUSE  = 7104;
        public const ushort STOP        = 7105;
        public const ushort STICK_PLUS  = 7106;
        public const ushort STICK_MINUS = 7107;

        public const ushort LAST_ERROR_CODE = 7320;
        //public const ushort 
    }
}
