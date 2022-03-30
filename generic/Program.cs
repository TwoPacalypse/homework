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
            LocalFileLogger<string> infoTest = new LocalFileLogger<string>();
            infoTest.Message = "test test test";
            infoTest.LogInfo(infoTest.Message);

            LocalFileLogger<int> warningTest = new LocalFileLogger<int>();
            warningTest.Message = 45;
            warningTest.LogWarning(warningTest.Message);

            LocalFileLogger<int> errorTest = new LocalFileLogger<int>();
            try
            {
                errorTest.Message = 5;
                int y = errorTest.Message / 0;
            }
            catch (Exception ex)
            {
                errorTest.LogError(errorTest.Message, ex);
            }
        }
    }

    public interface ILogger<T>
    {
        T Message { get; set; }

        public void LogInfo(T message);
        public void LogWarning(T message);
        public void LogError(T message, Exception ex);
    }

    public class LocalFileLogger<T> : ILogger<T>
    {
        public StreamWriter f = new StreamWriter("..\\..\\..\\test.txt", true);
        string? GenericTypeName { get; set; }
        public LocalFileLogger()
        {
            GenericTypeName = typeof(T).Name;
        }
        public T? Message { get; set; }
        public void LogInfo(T message)
        {
            f.WriteLine($"[Info]: [{GenericTypeName}] : {message}");
            f.Close();
        }
        public void LogWarning(T message)
        {
            f.WriteLine($"[Warning]: [{GenericTypeName}] : {message}");
            f.Close();
        }
        public void LogError(T message, Exception ex)
        {
            f.WriteLine($"[Error]: [{GenericTypeName}] : {message}. {ex.Message}");
            f.Close();
        }
    }
}

