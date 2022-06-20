using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayerRadyofarmasotik.Logger
{
    public class FileLoggerService : ILoggerService
    {
        public void WriteLog(string strLog, string path, string environment)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            //string logFilePath = "C:\\Logs\\";
            string logFilePath = path + $"/{DateTime.Now.ToString("yyyy/MM/dd").Replace(".", "/")}/{environment}/";
            logFilePath = logFilePath + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss").Replace("-", string.Empty) + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);

            if (!logDirInfo.Exists)
                logDirInfo.Create();
            if (!logFileInfo.Exists)
                fileStream = logFileInfo.Create();

            else
                fileStream = new FileStream(logFilePath, FileMode.Append);

            log = new StreamWriter(fileStream);
            log.WriteLine(DateTime.Now.ToLongTimeString() + "----" + strLog);
            log.Close();
        }
    }
}
