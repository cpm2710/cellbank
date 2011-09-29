using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities.Tracking;

namespace TrackingWorkFlow
{
    public class SETrackingParticipant:TrackingParticipant
    {
        public SETrackingParticipant():base(){
        }
        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            
            Console.WriteLine();
           IDictionary<string, string> annotations= record.Annotations;
            Console.WriteLine(record.ToString());
        }
    }
}
