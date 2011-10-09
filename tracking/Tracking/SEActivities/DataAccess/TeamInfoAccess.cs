using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEActivities.DataAccess
{
    public class TeamInfo{
        public string domain;
        public string product;
    }
    public class TeamInfoAccess
    {
        public static TeamInfo getTeam(int id)
        {
            TeamInfo teamInfo= new TeamInfo();
            teamInfo.domain = "redmond.corp.microsoft.com";
            teamInfo.product = "Windows Server Solutions";
            return teamInfo;
        }
    }
}
