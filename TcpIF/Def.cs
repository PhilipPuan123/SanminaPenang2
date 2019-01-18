using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpIF
{
    public enum TCPError: int
    {
        OK                      = 0,
        /// <summary>Invalid listener IP address.</summary>
        InvalidListenerIP       = -150,
        /// <summary>Invalid listerner port number.</summary>
        InvalidListenerPort     = -151,
        /// <summary>Fail to start listener.</summary>
        StartListenerFail       = -152,
        /// <summary>Listener busy. Unable to change setting.</summary>
        ListenerBusy            = -153,

        // Client related error
        /// <summary>Invalid server IP address.</summary>
        Cl_InvalidServerIP = -160,
        /// <summary>Server port number out of avaialble range.</summary>
        Cl_InvalidServerPort = -161,
        /// <summary>Client already exist. Unable to start client again.</summary>
        Cl_ClientAlreadyExist = -162,
        /// <summary>Timeout when attemp to connect to server.</summary>
        Cl_ConnectServerTimeout = -163,
        /// <summary>Connection lost.</summary>
        Cl_ConnectionLost = -164,
    }
}
