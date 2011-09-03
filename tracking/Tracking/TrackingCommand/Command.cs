using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackingCommands
{
    public interface Command
    {
        void execute();
        void validate();
    }
}
