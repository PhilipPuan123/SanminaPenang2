/* ================================================================================================
 * Changes:
 * --------
 * 1) Set _connected to false whenever connection lost/disconnect
 * 2) Modified Modbus Function Code to enum
 * 3) Modified Modbus Error to enum
 * ================================================================================================
 */

using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TMModbusIF
{
    /// <summary>
    /// Modbus TCP common driver class. This class implements a modbus TCP master driver.
    /// It supports the following commands:
    /// 
    /// Read coils
    /// Read discrete inputs
    /// Write single coil
    /// Write multiple cooils
    /// Read holding register
    /// Read input register
    /// Write single register
    /// Write multiple register
    /// 
    /// All commands can be sent in synchronous mode. If a value is accessed in synchronous 
    /// mode the program will stop and wait for slave to response. If the slave didn't answer
    /// within a specified time a timeout exception is called.
    /// The class uses multi threading for both synchronous and asynchronous access.
    /// 
    /// </summary>
    internal class Master
    {
        #region Enumeration
            private enum ModbusFunctionCode : byte
            {
                fctReadCoil = 1,
                fctReadDiscreteInputs = 2,
                fctReadHoldingRegister = 3,
                fctReadInputRegister = 4,
                fctWriteSingleCoil = 5,
                fctWriteSingleRegister = 6,
                fctWriteMultipleCoils = 15,
                fctWriteMultipleRegister = 16,
                fctReadWriteMultipleRegister = 23,
            }
        #endregion Enumeration

        #region Declaration
            // Field
            private static ushort _timeout = 500;
            private static ushort _refresh = 10;

            private const ushort MODBUS_SERVER_PORT = 502;
            private IPAddress DeviceAddress = IPAddress.Loopback;
            private int DevicePort = MODBUS_SERVER_PORT;

            private Socket client;
            private byte[] tcpSynClBuffer = new byte[2048];
        #endregion Declaration

        #region Property
            /// <summary>Response timeout. If the slave didn't answers within in this time an exception is called.</summary>
            /// <value>The default value is 500ms.</value>
            public ushort timeout
            {
                get { return _timeout; }
                set { _timeout = value; }
            }

            /// <summary>Refresh timer for slave answer. The class is polling for answer every X ms.</summary>
            /// <value>The default value is 10ms.</value>
            public ushort refresh
            {
                get { return _refresh; }
                set { _refresh = value; }
            }

            /// <summary>Shows if a connection is active.</summary>
            public bool connected
            {
                get
                {
                    if (client != null) return client.Connected;
                    else return false;
                }
            }
        #endregion Property

        #region Constructor/Destructor
        /// <summary>Create master instance with parameters.</summary>
        /// <param name="ip">IP adress of modbus slave.</param>
        /// <param name="port">Port number of modbus slave. Usually port 502 is used.</param>
        public Master(IPAddress ip, int port = MODBUS_SERVER_PORT)
        {
            DeviceAddress = ip;
            DevicePort = port;
            IsConnected();
        }

        /// <summary>Destroy master instance.</summary>
        ~Master()
        {
            Dispose();
        }
        #endregion Constructor/Destructor

        #region Connect
            private bool IsConnected()
            {
                if (client != null) return true;
                try
                {
                    // Connect client
                    client = new Socket(DeviceAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    client.Connect(new IPEndPoint(DeviceAddress, DevicePort));
                    client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, _timeout);
                    client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
                    client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
                }
                catch (Exception)
                {
                    client = null;
                }

                if (client == null) {
                    return false;
                }

                return true;
            }
        #endregion Connect

        #region Disconnect
            /// <summary>Stop connection to slave.</summary>
            public void Disconnect()
            {
                Dispose();
            }

            /// <summary>Destroy master instance</summary>
            public void Dispose()
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        try { client.Shutdown(SocketShutdown.Both); }
                        catch { }
                        client.Close();
                    }
                    client = null;
                }
            }
        #endregion Disconnect

        #region Read
            /// <summary>Read coils from slave synchronous.</summary>
            /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
            /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
            /// <param name="startAddress">Address from where the data read begins.</param>
            /// <param name="numInputs">Length of data.</param>
            /// <param name="values">Contains the result of function.</param>
            public MError ReadCoils(ushort id, byte unit, ushort startAddress, ushort numInputs, ref byte[] values)
            {
                if (numInputs > 2000)
                {
                    return MError.IllegalDataValue;
                }
                return WriteData(CreateReadHeader(id, unit, startAddress, numInputs, (byte)ModbusFunctionCode.fctReadCoil), id, ref values);
            }

            /// <summary>Read discrete inputs from slave synchronous.</summary>
            /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
            /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
            /// <param name="startAddress">Address from where the data read begins.</param>
            /// <param name="numInputs">Length of data.</param>
            /// <param name="values">Contains the result of function.</param>
            public MError ReadDiscreteInputs(ushort id, byte unit, ushort startAddress, ushort numInputs, ref byte[] values)
            {
                if (numInputs > 2000)
                {
                    return MError.IllegalDataValue;
                }
                return WriteData(CreateReadHeader(id, unit, startAddress, numInputs, (byte)ModbusFunctionCode.fctReadDiscreteInputs), id, ref values);
            }

            /// <summary>Read holding registers from slave synchronous.</summary>
            /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
            /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
            /// <param name="startAddress">Address from where the data read begins.</param>
            /// <param name="numInputs">Length of data.</param>
            /// <param name="values">Contains the result of function.</param>
            public MError ReadHoldingRegister(ushort id, byte unit, ushort startAddress, ushort numInputs, ref byte[] values)
            {
                if (numInputs > 125)
                {
                    return MError.IllegalDataValue;
                }
                return WriteData(CreateReadHeader(id, unit, startAddress, numInputs, (byte)ModbusFunctionCode.fctReadHoldingRegister), id, ref values);
            }

            /// <summary>Read input registers from slave synchronous.</summary>
            /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
            /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
            /// <param name="startAddress">Address from where the data read begins.</param>
            /// <param name="numInputs">Length of data.</param>
            /// <param name="values">Contains the result of function.</param>
            public MError ReadInputRegister(ushort id, byte unit, ushort startAddress, ushort numInputs, ref byte[] values)
            {
                if (numInputs > 125)
                {
                    return MError.IllegalDataValue;
                }
                return WriteData(CreateReadHeader(id, unit, startAddress, numInputs, (byte)ModbusFunctionCode.fctReadInputRegister), id, ref values);
            }
        #endregion Read

        #region Write
            /// <summary>Write single coil in slave synchronous.</summary>
            /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
            /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
            /// <param name="startAddress">Address from where the data read begins.</param>
            /// <param name="OnOff">Specifys if the coil should be switched on or off.</param>
            /// <param name="result">Contains the result of the synchronous write.</param>
            public MError WriteSingleCoils(ushort id, byte unit, ushort startAddress, bool OnOff, ref byte[] result)
            {
                byte[] data;
                data = CreateWriteHeader(id, unit, startAddress, 1, 1, (byte)ModbusFunctionCode.fctWriteSingleCoil);
                if (OnOff == true) data[10] = 255;
                else data[10] = 0;
                return WriteData(data, id, ref result);
            }

            /// <summary>Write multiple coils in slave synchronous.</summary>
            /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
            /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
            /// <param name="startAddress">Address from where the data read begins.</param>
            /// <param name="numBits">Specifys number of bits.</param>
            /// <param name="values">Contains the bit information in byte format.</param>
            /// <param name="result">Contains the result of the synchronous write.</param>
            public MError WriteMultipleCoils(ushort id, byte unit, ushort startAddress, ushort numBits, byte[] values, ref byte[] result)
            {
                if (values == null) return MError.IllegalDataValue;
                ushort numBytes = Convert.ToUInt16(values.Length);
                if (numBytes > 250 || numBits > 2000)
                {
                    return MError.IllegalDataValue;
                }

                byte[] data;
                data = CreateWriteHeader(id, unit, startAddress, numBits, (byte)(numBytes + 2), (byte)ModbusFunctionCode.fctWriteMultipleCoils);
                Array.Copy(values, 0, data, 13, numBytes);
                return WriteData(data, id, ref result);
            }

            /// <summary>Write single register in slave synchronous.</summary>
            /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
            /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
            /// <param name="startAddress">Address to where the data is written.</param>
            /// <param name="values">Contains the register information.</param>
            /// <param name="result">Contains the result of the synchronous write.</param>
            public MError WriteSingleRegister(ushort id, byte unit, ushort startAddress, byte[] values, ref byte[] result)
            {
                if (values == null || values.Length < 2) return MError.IllegalDataValue;

                byte[] data;
                data = CreateWriteHeader(id, unit, startAddress, 1, 1, (byte)ModbusFunctionCode.fctWriteSingleRegister);
                data[10] = values[0];
                data[11] = values[1];
                return WriteData(data, id, ref result);
            }

            /// <summary>Write multiple registers in slave synchronous.</summary>
            /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
            /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
            /// <param name="startAddress">Address to where the data is written.</param>
            /// <param name="values">Contains the register information.</param>
            /// <param name="result">Contains the result of the synchronous write.</param>
            public MError WriteMultipleRegister(ushort id, byte unit, ushort startAddress, byte[] values, ref byte[] result)
            {
                if (values == null) return MError.IllegalDataValue;

                ushort numBytes = Convert.ToUInt16(values.Length);
                if (numBytes > 250)
                {
                    return MError.IllegalDataValue;
                }

                if (numBytes % 2 > 0) numBytes++;
                byte[] data;

                data = CreateWriteHeader(id, unit, startAddress, Convert.ToUInt16(numBytes / 2), Convert.ToUInt16(numBytes + 2), (byte)ModbusFunctionCode.fctWriteMultipleRegister);
                Array.Copy(values, 0, data, 13, values.Length);
                return WriteData(data, id, ref result);
            }
        #endregion Write

        #region ReadWrite
            /// <summary>Read/Write multiple registers in slave synchronous. The result is given in the response function.</summary>
            /// <param name="id">Unique id that marks the transaction. In asynchonous mode this id is given to the callback function.</param>
            /// <param name="unit">Unit identifier (previously slave address). In asynchonous mode this unit is given to the callback function.</param>
            /// <param name="startReadAddress">Address from where the data read begins.</param>
            /// <param name="numInputs">Length of data.</param>
            /// <param name="startWriteAddress">Address to where the data is written.</param>
            /// <param name="values">Contains the register information.</param>
            /// <param name="result">Contains the result of the synchronous command.</param>
            public MError ReadWriteMultipleRegister(ushort id, byte unit, ushort startReadAddress, ushort numInputs, ushort startWriteAddress, byte[] values, ref byte[] result)
            {
                if (values == null) return MError.IllegalDataValue;
                ushort numBytes = Convert.ToUInt16(values.Length);
                if (numBytes > 250)
                {
                    return MError.IllegalDataValue;
                }

                if (numBytes % 2 > 0) numBytes++;
                byte[] data;

                data = CreateReadWriteHeader(id, unit, startReadAddress, numInputs, startWriteAddress, Convert.ToUInt16(numBytes / 2));
                Array.Copy(values, 0, data, 17, values.Length);
                return WriteData(data, id, ref result);
            }
        #endregion ReadWrite

        #region Header Construction
            // Create modbus header for read action
            private byte[] CreateReadHeader(ushort id, byte unit, ushort startAddress, ushort length, byte function)
            {
                byte[] data = new byte[12];

                byte[] _id = BitConverter.GetBytes((short)id);
                data[0] = _id[1];			    // Slave id high byte
                data[1] = _id[0];				// Slave id low byte
                data[5] = 6;					// Message size
                data[6] = unit;					// Slave address
                data[7] = function;				// Function code
                byte[] _adr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startAddress));
                data[8] = _adr[0];				// Start address
                data[9] = _adr[1];				// Start address
                byte[] _length = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)length));
                data[10] = _length[0];			// Number of data to read
                data[11] = _length[1];			// Number of data to read
                return data;
            }

            // Create modbus header for write action
            private byte[] CreateWriteHeader(ushort id, byte unit, ushort startAddress, ushort numData, ushort numBytes, byte function)
            {
                byte[] data = new byte[numBytes + 11];

                byte[] _id = BitConverter.GetBytes((short)id);
                data[0] = _id[1];				// Slave id high byte
                data[1] = _id[0];				// Slave id low byte
                byte[] _size = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)(5 + numBytes)));
                data[4] = _size[0];				// Complete message size in bytes
                data[5] = _size[1];				// Complete message size in bytes
                data[6] = unit;					// Slave address
                data[7] = function;				// Function code
                byte[] _adr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startAddress));
                data[8] = _adr[0];				// Start address
                data[9] = _adr[1];				// Start address
                if (function >= (byte)ModbusFunctionCode.fctWriteMultipleCoils)
                {
                    byte[] _cnt = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numData));
                    data[10] = _cnt[0];			// Number of bytes
                    data[11] = _cnt[1];			// Number of bytes
                    data[12] = (byte)(numBytes - 2);
                }
                return data;
            }

            // Create modbus header for read/write action
            private byte[] CreateReadWriteHeader(ushort id, byte unit, ushort startReadAddress, ushort numRead, ushort startWriteAddress, ushort numWrite)
            {
                byte[] data = new byte[numWrite * 2 + 17];

                byte[] _id = BitConverter.GetBytes((short)id);
                data[0] = _id[1];						// Slave id high byte
                data[1] = _id[0];						// Slave id low byte
                byte[] _size = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)(11 + numWrite * 2)));
                data[4] = _size[0];						// Complete message size in bytes
                data[5] = _size[1];						// Complete message size in bytes
                data[6] = unit;							// Slave address
                data[7] = (byte)ModbusFunctionCode.fctReadWriteMultipleRegister;	// Function code
                byte[] _adr_read = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startReadAddress));
                data[8] = _adr_read[0];					// Start read address
                data[9] = _adr_read[1];					// Start read address
                byte[] _cnt_read = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numRead));
                data[10] = _cnt_read[0];				// Number of bytes to read
                data[11] = _cnt_read[1];				// Number of bytes to read
                byte[] _adr_write = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)startWriteAddress));
                data[12] = _adr_write[0];				// Start write address
                data[13] = _adr_write[1];				// Start write address
                byte[] _cnt_write = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)numWrite));
                data[14] = _cnt_write[0];				// Number of bytes to write
                data[15] = _cnt_write[1];				// Number of bytes to write
                data[16] = (byte)(numWrite * 2);

                return data;
            }
        #endregion Header Construction

        #region Write to Modbus
            // Write data and and wait for response
            private MError WriteData(byte[] write_data, ushort id, ref byte[] data)
            {
                if (IsConnected())
                {
                    try
                    {
                        // Sent data to slave and wait for response
                        client.Send(write_data, 0, write_data.Length, SocketFlags.None);
                        int received_length = client.Receive(tcpSynClBuffer, 0, tcpSynClBuffer.Length, SocketFlags.None);

                        byte unit = tcpSynClBuffer[6];
                        byte function = tcpSynClBuffer[7];

                        // Return error if received length is 0
                        if (received_length == 0) return MError.ConnectionLost;
                        /* Response data is slave exception */
                        if (function > (byte)MError.WrongOffset)
                        {
                            return (MError)tcpSynClBuffer[8];
                        }
                        // Function Code is Write response data
                        else if ((function >= (byte)ModbusFunctionCode.fctWriteSingleCoil) && (function != (byte)ModbusFunctionCode.fctReadWriteMultipleRegister))
                        {
                            data = new byte[2];
                            Array.Copy(tcpSynClBuffer, 10, data, 0, 2);
                        }
                        // Function Code is Read response data
                        else
                        {
                            data = new byte[tcpSynClBuffer[8]];
                            Array.Copy(tcpSynClBuffer, 9, data, 0, tcpSynClBuffer[8]);
                        }

                        return MError.OK;
                    }
                    catch (SystemException)
                    {
                        try { client.Close(); } catch { }
                        client = null;
                        return MError.ConnectionLost;
                    }
                }
                else return MError.ConnectionLost;
            }
        #endregion Write to Modbus
        
        internal static UInt16 SwapUInt16(UInt16 inValue)
        {
            return (UInt16)(((inValue & 0xff00) >> 8) |
                     ((inValue & 0x00ff) << 8));
        }
    }
}
