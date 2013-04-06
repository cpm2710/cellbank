// author: andyliuliming@outlook.com
using RotorsLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsLib
{
    public interface IReportObserver
    {
        void ReportStatus(string statusMsg, LogLevel logLevel = LogLevel.Information);
    }
}
