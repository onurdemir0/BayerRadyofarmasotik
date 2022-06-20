using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayerRadyofarmasotik.Logger
{
    public interface ILoggerService
    {
        void WriteLog(string strLog, string path, string environment);
    }
}
