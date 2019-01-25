/* To-do:
 * - When send response fail, how to disconnect
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TcpIF
{
    public class Server
    {
        #region Declaration
        // Private
        private const int DEFAULT_TCP_PORT = 19888;
        private IPAddress ipAddress = IPAddress.Loopback;
        private int portNum = DEFAULT_TCP_PORT;
        private bool isRunning = false;
        private TcpListener listener = null;
        #endregion Declaration

        #region Event
        // Events
        public delegate void DataReceived(object sender, string parameter);
        public delegate void DataSend(object sender, string parameter);
        public delegate void ClientDisconnected(object sender);
        public event DataReceived OnDataReceived;
        public event DataSend OnDataSend;
        public event ClientDisconnected OnClientDisconnected;
        #endregion

        #region Property
        public bool IsRunning
        {
            get { return isRunning; }
        }
        #endregion Property

        #region Configuraton
        public TCPError SetConfig(string ip, int port)
        {
            IPAddress _ip = null;
            // Close current connection if it is running
            if (isRunning)
            {
                Stop();
            }

            // Check if IP address format
            if (IPAddress.TryParse(ip, out _ip) != true)
            {
                return TCPError.InvalidListenerIP;
            }
            ipAddress = _ip;
            portNum = port;
            return TCPError.OK;
        }

        public TCPError SetConfig(string ip, string port)
        {
            IPAddress _ip = null;
            int _port = 0;
            // Close current connection if it is running
            if (isRunning)
            {
                Stop();
            }

            // Check if IP address is valid format
            if (IPAddress.TryParse(ip, out _ip) != true)
            {
                return TCPError.InvalidListenerIP;
            }
            // Check if port is valid format
            if (Int32.TryParse(port,out _port)!= true)
            {
                return TCPError.InvalidListenerPort;
            }

            ipAddress = _ip;
            portNum = _port;
            return TCPError.OK;
        }

        public TCPError ChangePort(int port = DEFAULT_TCP_PORT)
        {
            // Return error if Tcp is running
            if (isRunning)
            {
                return TCPError.ListenerBusy;
            }

            portNum = port;
            return TCPError.OK;
        }
        #endregion Configuraton

        /// <summary>
        /// Start Listener.
        /// </summary>
        /// <returns></returns>
        public TCPError Start()
        {
            /* Skip if listener already started */
            if (listener != null) return TCPError.OK;

            try
            {
                listener = new TcpListener(ipAddress, portNum);
                /* Start listening to incoming connections */
                listener.Start();
            }
            catch
            {
                listener = null;
                return TCPError.StartListenerFail;
            }

            // Start thread
            isRunning = true;
            Task.Run(() => { TcpListenThread(); });
            return TCPError.OK;
        }

        /// <summary>
        /// Restart Listerner.
        /// </summary>
        /// <returns></returns>
        public TCPError Restart()
        {
            if(listener != null)
            {
                Stop();
                while (listener != null) ;
            }

            return Start();
        }

        public void Stop()
        {
            isRunning = false;
        }
        
        /// <summary>
        /// Create thread for each connected socket.
        /// </summary>
        private void TcpListenThread()
        {
            try
            {
                while (isRunning)
                {
                    /* Start listening to incoming connections */
                    //listener.Start();     // Removed from here because it may create multiple task for same socket

                    /* Wait for incoming connection */
                    if (!listener.Pending())
                    {
                        SpinWait.SpinUntil(() => false, 100);
                        continue;
                    }

                    /* When there is any incoming connection, accept the connection */
                    var client = listener.AcceptSocket();
                    //Logger.sys(string.Format("Connected to {0}", (client.RemoteEndPoint as IPEndPoint).Address));

                    /* Create a background thread to handle the read and write operation for the socket */
                    Thread tcp_thread = new Thread(new ParameterizedThreadStart(TcpService));
                    tcp_thread.Name = "TcpServerThread";
                    tcp_thread.IsBackground = true;
                    tcp_thread.Start(client);
                }
            }
            catch(Exception)
            {
                isRunning = false;
            }
            try
            {
                /* Stop listener */
                listener.Stop();
                listener = null;
            }
            catch { }
        }

        /// <summary>
        /// Thread worker class to handle read/write operation for the socket.
        /// </summary>
        /// <param name="sender"></param>
        private void TcpService(object sender)
        {
            var client = sender as Socket;
            if (client == null) return;

            const int time_socket_alive = 90 * 1000;    //milliseconds
            const int time_alive_interval = 1 * 1000;   // milliseconds
            //Logger.sys(string.Format("start set socket option of {0}", (client.RemoteEndPoint as IPEndPoint).Address));

            try { SetKeepAlive(client, time_socket_alive, time_alive_interval); }
            catch (Exception) { }

            try
            {
                EndPoint remoteEP = client.RemoteEndPoint;
                //Logger.sys(string.Format("socket is ready to receive data from {0}", (remote_ep as IPEndPoint).Address));

                while (isRunning)
                {
                    /* Wait unit data received from client or listener is stopped */
                    SpinWait.SpinUntil(() => { return client.Poll(0, SelectMode.SelectRead) || !isRunning || !client.Connected; }, -1);
                    if (!isRunning || !client.Connected) break;
                    int length = client.Available;
                    if (length == 0)
                    {
                        //Logger.log("remote socket closed");
                        break;
                    }
                    /* Receive data client */
                    byte[] buffer = new byte[length];
                    var receiveLength = client.ReceiveFrom(buffer, ref remoteEP);

                    /* Fire event to subscriber */
                    string result = Encoding.UTF8.GetString(buffer);
                    if (OnDataReceived != null) OnDataReceived(client,result);

                    ///* Echo back the msg to client */
                    //byte[] echo_msg = Encoding.UTF8.GetBytes(result);
                    //if (OnDataSend != null) OnDataSend(client,result);
                    //client.Send(echo_msg);
                }
            }
            catch (Exception e)
            {
                //string info = "Socket Exception:" + e.Message + Environment.NewLine + e.StackTrace;
                //Logger.log(info)
            }
            /* Disable send and receive on Socket */
            try { client.Shutdown(SocketShutdown.Both); }
            catch { }
            /* Close the socket to allow reuse of socket */
            try { client.Disconnect(false); }
            catch { }
            /* Close the socket and free up all resources */
            try { client.Close(); }
            catch { }
            if (OnClientDisconnected != null) OnClientDisconnected(client);
            client = null;
        }

        public TCPError ResponseToClient(object sender, string response)
        {
            var client = sender as Socket;
            if (client == null || !client.Connected) return TCPError.InvalidClient;
            if (String.IsNullOrEmpty(response)) return TCPError.InvalidResponseData;

            try
            {
                if(isRunning)
                {
                    /* Send response to client */
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    if (OnDataSend != null) OnDataSend(client, response);
                    client.Send(responseBytes);
                }
            }
            catch (Exception e)
            {
                return TCPError.SendResponseDataFail;
            }
            return TCPError.OK;
        }

        /// <summary>
        /// Set keepalive option of socket
        /// </summary>
        /// <param name="sock"></param>
        /// works on which socket
        /// <param name="time"></param>
        /// if there is no any conmunication in this socket, after "time", starting sending beacon to check whether the remote site is alive
        /// "time" is millisecond.
        /// <param name="interval"></param>
        ///  time interval between each two detection beacons
        /// <returns></returns>
        private bool SetKeepAlive(Socket sock, ulong time, ulong interval)
        {
            const int bytesperlong = 4; // 32 / 8
            const int bitsperbyte = 8;
            try
            {
                /* Resulting structure */
                byte[] SIO_KEEPALIVE_VALS = new byte[3 * bytesperlong];

                /* Array to hold input values */
                ulong[] input = new ulong[3];

                /* Put input arguments in input array */
                if (time == 0 || interval == 0) // enable disable keep-alive
                    input[0] = (0UL); // off
                else
                    input[0] = (1UL); // on

                input[1] = (time);  // time millis
                input[2] = (interval);  // interval millis

                /* Pack input into byte struct */
                for (int i = 0; i < input.Length; i++)
                {
                    SIO_KEEPALIVE_VALS[i * bytesperlong + 3] = (byte)(input[i] >> ((bytesperlong - 1) * bitsperbyte) & 0xff);
                    SIO_KEEPALIVE_VALS[i * bytesperlong + 2] = (byte)(input[i] >> ((bytesperlong - 2) * bitsperbyte) & 0xff);
                    SIO_KEEPALIVE_VALS[i * bytesperlong + 1] = (byte)(input[i] >> ((bytesperlong - 3) * bitsperbyte) & 0xff);
                    SIO_KEEPALIVE_VALS[i * bytesperlong + 0] = (byte)(input[i] >> ((bytesperlong - 4) * bitsperbyte) & 0xff);
                }
                /* Create bytestruct for result (bytes pending on server socket) */
                byte[] result = BitConverter.GetBytes(0);
                /* write SIO_VALS to Socket IOControl */
                sock.IOControl(IOControlCode.KeepAliveValues, SIO_KEEPALIVE_VALS, result);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
