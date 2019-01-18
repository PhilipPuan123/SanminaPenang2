using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpIF
{
    public class Client
    {
        #region Declaration
        // Constant
        private const int MIN_PORT = 0;
        private const int MAX_PORT = 65535;
        private const int DEFAULT_CONNECT_TIMEOUT = 15000;
        // Fields
        private Socket      _client;
        private IPAddress   _serverIP;
        private int         _serverPort;
        private ushort  _sendTimeout    = 1000;
        private ushort  _receiveTimeout = 1000;
        #endregion Declaration

        #region Event
        // Event
        public delegate void DataReceived(object sender, string data);
        public delegate void DataSend(object sender, string data);
        public DataReceived OnDataReceived;
        public DataSend OnDataSend;
        #endregion Event

        #region Property
        public ushort sendTimeout
        {
            get { return _sendTimeout; }
            set { _sendTimeout = value; }
        }

        public ushort receiveTimeout
        {
            get { return _receiveTimeout; }
            set { _receiveTimeout = value; }
        }

        public bool connected
        {
            get
            {
                if (_client != null) return _client.Connected;
                else return false;
            }
        }
        #endregion Property

        #region Constructor/Destructor
        /// <summary>
        /// Destroy client instance and release all resources
        /// </summary>
        ~Client()
        {
            Dispose();
        }
        #endregion Constructor/Destructor

        #region Connect
        /// <summary>
        /// Connect to server.
        /// </summary>
        /// <param name="ip">Server IP address.</param>
        /// <param name="port">Server port number.</param>
        /// <returns></returns>
        public TCPError Connect(string ip, int port)
        {
            return Connect(ip, port, DEFAULT_CONNECT_TIMEOUT);
        }

        /// <summary>
        /// Connect to server.
        /// </summary>
        /// <param name="ip">Server IP address</param>
        /// <param name="port">Server port number.</param>
        /// <param name="timeout">Connection timeout setting in ms.</param>
        /// <returns></returns>
        public TCPError Connect(string ip, int port, int timeout)
        {
            IPAddress myIP;

            // If client already exist, return error
            if (_client != null) return TCPError.Cl_ClientAlreadyExist;

            // Check if valid IP format and port number
            if (IPAddress.TryParse(ip, out myIP) != true)
            {
                return TCPError.Cl_InvalidServerIP;
            }
            else if (port < MIN_PORT || MAX_PORT < port)
            {
                return TCPError.Cl_InvalidServerPort;
            }
            else
            {
                // Save ip and port
                _serverIP = myIP;
                _serverPort = port;
                
                // Connect to server with timeout
                try
                {   
                    _client = new Socket(_serverIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    var result = _client.BeginConnect(new IPEndPoint(_serverIP, _serverPort), null, null);
                    bool isSuccess = result.AsyncWaitHandle.WaitOne(timeout, true);
                    // If connection successful, end connection and set socket option.
                    if (isSuccess)
                    {
                        _client.EndConnect(result);
                        _client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _sendTimeout);
                        _client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _receiveTimeout);
                        _client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
                    }
                    else
                    {
                        _client.Close();
                        _client = null;
                    }
                }
                catch (Exception)
                {
                    _client = null;
                }
            }

            if(_client == null) return TCPError.Cl_ConnectServerTimeout;

            return TCPError.OK;
        }
        #endregion Connect

        #region Disconnect
        /// <summary>Disconnect from server and release all resources.</summary>
        public void Disconnect()
        {
            Dispose();
        }

        /// <summary>Destroy client instance and release all resources.</summary>
        private void Dispose()
        {
            if(_client != null)
            {
                if (_client.Connected)
                {
                    try { _client.Shutdown(SocketShutdown.Both); }
                    catch { }
                    _client.Close();
                }
                _client = null;
            }
        }
        #endregion Disconnect

        #region Send/Receive
        /// <summary>
        /// Send data to server and wait for response.
        /// </summary>
        /// <returns></returns>
        public TCPError SendAndReceiveData(byte[] writeData)
        {
            if (_client != null)
            {
                try
                {
                    byte[] readData = new byte[2048];

                    //// Flush data in read stream
                    //if(_client.Available > 0)
                    //{
                    //    byte[] flushData = new byte[2048];
                    //    _client.Receive(flushData);
                    //}

                    // Send data to server
                    _client.Send(writeData, SocketFlags.None);
                    string writeText = Encoding.UTF8.GetString(writeData);
                    if (OnDataSend != null) OnDataSend(null, writeText);

                    // Wait for response from server
                    int bytesReceived = _client.Receive(readData, 0, readData.Length, SocketFlags.None);
                    
                    if (bytesReceived == 0) return TCPError.Cl_ConnectionLost;
                    // Convert received data to string
                    string readText = Encoding.UTF8.GetString(readData);
                    if (OnDataReceived != null) OnDataReceived(null, readText);
                    return TCPError.OK;
                }
                catch(SystemException e)
                {
                    Disconnect();
                    return TCPError.Cl_ConnectionLost;
                }
            }
            else
            {
                return TCPError.Cl_ConnectionLost;
            }
        }
        #endregion Send/Receive
    }
}
