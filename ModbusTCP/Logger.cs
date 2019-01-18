using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ModbusTCP
{
    class Logger
    {
        const string LOG_PREFIX = "Modbus_";
        const string LOG_FOLDER = @".\Logs";
        static private Object Locker = new Object();
        //初始值為true, 讓程式啟動參數可以寫入log file
        public static bool IsLogToFile = true;

        static public void log(string s, string title = "SYS")
        {
            if (IsLogToFile == false) return;

            if(Directory.Exists(LOG_FOLDER) == false)
                try { Directory.CreateDirectory(LOG_FOLDER); }
                catch { return; }

            string file = System.IO.Path.Combine(LOG_FOLDER, LOG_PREFIX + DateTime.Now.ToString("yyyyMMdd") + ".log");
            try
            {
                string msg = string.Format("[{0}][{1}]{2}\r\n", title, DateTime.Now.ToString("HH:mm:ss:fff"), s);
                lock(Locker)
                    using (var fstream = new FileStream(file, FileMode.Append, FileAccess.Write, FileShare.None, 8, FileOptions.WriteThrough))
                    using (var writer = new StreamWriter(fstream))
                    { writer.Write(msg); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception on log:" + ex.StackTrace.ToString());
            }
        }


        static public void sys(string s)
        {
            log(s, "SYS");
        }

        static public void tcp(string s, bool IsReceived = true)
        {
            if (IsReceived) log(s, "TCP R");
            else log(s, "TCP S");
        }

        static public void con(string s)
        {
            log(s, "CON");
        }

        static public void ack(string s)
        {
            log(s, "ACK");
        }

        static public void log(string s)
        {
            sys(s);
        }

        static public void inp(string s)
        {
            log(s, "INP");
        }

    }

}
