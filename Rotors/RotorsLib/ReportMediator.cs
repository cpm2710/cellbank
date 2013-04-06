// author: andyliuliming@outlook.com
using RotorsLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsLib
{
    public class ReportMediator : IReportObserver
    {
        private List<IReportObserver> ReportObservers;
        public ReportMediator()
        {
            ReportObservers = new List<IReportObserver>();
        }

        public void RegisterReportObserver(IReportObserver observer)
        {
            ReportObservers.Add(observer);
        }

        public void ReportStatus(string statusMsg, LogLevel logLevel = LogLevel.Information)
        {
            foreach (IReportObserver reportObserver in ReportObservers)
            {
                reportObserver.ReportStatus(statusMsg, logLevel);
            }
        }
    }
}
