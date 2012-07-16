using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Instrumentation;

namespace SBSWMINotifications
{
    [ManagedName("SBSUserAddedEvent")]
    public class SBSUserAddedEvent : BaseEvent
    {
        public string UserId
        {
            get;
            private set;
        }
        private SBSUserAddedEvent(string userId)
        {
            this.UserId = userId;
        }
        public static void Publish(string userId)
        {
            Instrumentation.Fire(new SBSUserAddedEvent(userId));
        }
    }
}
