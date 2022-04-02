using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generic
{
    class Program
    {
        private static void Main()
        {
            string link = "..\\..\\..\\test.txt";

            LocalFileLogger<string> infoTest = new LocalFileLogger<string>(link);
            infoTest.Message = "test test test";
            infoTest.LogInfo(infoTest.Message);

            LocalFileLogger<int> warningTest = new LocalFileLogger<int>(link);
            warningTest.Message = "testing warning";
            warningTest.LogWarning(warningTest.Message);

            LocalFileLogger<int> errorTest = new LocalFileLogger<int>(link);
            try
            {
                var x = 5;
                int y = x / 0;
            }
            catch (Exception ex)
            {
                errorTest.Message = "deviding by zero";
                errorTest.LogError(errorTest.Message, ex);
            }
        }
    }

    public interface ILogger
    {
        public void LogInfo(string message);
        public void LogWarning(string message);
        public void LogError(string message, Exception ex);
    }

    public class LocalFileLogger<T> : ILogger
    {
        private string link;
       
        string? GenericTypeName { get; set; }
        public LocalFileLogger(string link_)
        {
            this.link = link_;
            GenericTypeName = typeof(T).Name;
        }
        public string Message { get; set; }
        public void LogInfo(string message)
        {
            StreamWriter f = new StreamWriter(this.link, true);
            f.WriteLine($"[Info]: [{GenericTypeName}] : {message}");
            f.Close();
        }
        public void LogWarning(string message)
        {
            StreamWriter f = new StreamWriter(this.link, true);
            f.WriteLine($"[Warning]: [{GenericTypeName}] : {message}");
            f.Close();
        }
        public void LogError(string message, Exception ex)
        {
            StreamWriter f = new StreamWriter(this.link, true);
            f.WriteLine($"[Error]: [{GenericTypeName}] : {message}. {ex.Message}");
            f.Close();
        }
    }
}

