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
        public string UserId;

    }

    public class SBSEventProvider
    {
        public static void FireSBSUserAddedEvent(string userId)
        {
            SBSUserAddedEvent addedEvent = new SBSUserAddedEvent();
            addedEvent.UserId = userId;
            addedEvent.Fire();
            //Instrumentation.Fire(new (userId));

        }
    }
}
