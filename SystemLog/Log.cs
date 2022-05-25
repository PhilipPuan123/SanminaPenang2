using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RVISData;


namespace SystemLog
{
    
    public class Log
    {
        public string Syslog { get; set; }
        public static bool SaveLogFlag { get; set; }

        public static void IsSaveLog()
        {
            if (SaveLogFlag ==  false)
            {
                Console.WriteLine("No error, log can be deleted");
                File.Delete(@"C:\RVIS\Err\" + FileLogger.datetime + "_RVIS_Log.txt");
           }
        }   
    }

    public abstract class LogBase
    {
        protected readonly object lockObj = new object();
        public abstract void Logging(string message);
    }

    public class FileLogger : LogBase
    {
        public static string  datetime = DateTime.Now.ToString("yyyyMMddHHmmss");

        public string filePath = @"C:\RVIS\Err\" + datetime + "_RVIS_Log.txt";
  
        public override void Logging(string message)
        {
            lock (lockObj)
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, true))
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }

            
        }
        
    }

}
