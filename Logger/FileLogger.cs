using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValiData.Logger
{
    public class FileLogger
    {
        private readonly string _logFilePath;
        private readonly IConfiguration _config;

        public FileLogger(/*string path*/IConfiguration config)
        {
            //_logFilePath = path;
            _config = config;
            _logFilePath = config["LogFilePath"];
        }

        public void Log(string message)
        {
            var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            File.AppendAllText(_logFilePath, line + Environment.NewLine);
        }
    }
}
