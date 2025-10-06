
using System;
using System.IO;

namespace HotelManagement.Infrastructure.Services
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception exception = null);
    }

    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;
        private readonly object _lockObject = new object();

        public FileLogger()
        {
            var logFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "HotelManagement", "Logs");

            Directory.CreateDirectory(logFolder);

            _logFilePath = Path.Combine(logFolder, $"log_{DateTime.Now:yyyyMMdd}.txt");
        }

        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogWarning(string message)
        {
            WriteLog("WARN", message);
        }

        public void LogError(string message, Exception exception = null)
        {
            var fullMessage = exception != null
                ? $"{message}\nException: {exception.Message}\nStackTrace: {exception.StackTrace}"
                : message;

            WriteLog("ERROR", fullMessage);
        }

        private void WriteLog(string level, string message)
        {
            lock (_lockObject)
            {
                try
                {
                    var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}{Environment.NewLine}";
                    File.AppendAllText(_logFilePath, logEntry);
                }
                catch
                {
                    
                }
            }
        }
    }
}