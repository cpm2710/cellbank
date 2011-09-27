using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackingWorkFlow
{
    public class WorkFlowNotFoundException:Exception
    {
        private string p;

        public WorkFlowNotFoundException(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }
    }
}
