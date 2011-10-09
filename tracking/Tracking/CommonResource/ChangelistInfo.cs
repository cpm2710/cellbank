using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CommonResource
{
    [DataContract]
    public class ChangelistInfo
    {
        /// <summary>
        /// Changelist ID
        /// </summary>
        [DataMember]
        public string ChangelistId { get; set; }


        /// <summary>
        /// Bug Id
        /// </summary>
        public Int32 BugId { get; set; }

        /// <summary>
        /// Depot Port
        /// </summary>
        [DataMember]
        public string DepotPort { get; set; }

        /// <summary>
        /// Relation: Related <-> pending; Fixed <-> submitted
        /// </summary>
        [DataMember]
        public string Relation { get; set; }

        /// <summary>
        /// Alias with domain of who create the changelist
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Last modify time
        /// </summary>
        [DataMember]
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// Changelist Description
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Changelist's status: pending, submitted
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// Client of the changelist
        /// </summary>
        [DataMember]
        public string ClientName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ChangelistInfo()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="changeListInfo">Changelist Info read from the PS</param>
        public ChangelistInfo(string changeListInfo)
        {
            string[] split = changeListInfo.Split(",".ToCharArray(), 6);

            this.ChangelistId = split[0];
            this.DepotPort = split[1];
            this.Relation = split[2];
            this.UserName = split[3];
            this.ModifyTime = DateTime.Parse(split[4]);
            this.Description = split[5];
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="changelistId"></param>
        /// <param name="depotPort"></param>
        /// <param name="userName"></param>
        /// <param name="time"></param>
        /// <param name="description"></param>
        /// <param name="relation"></param>
        /// <param name="clientName"></param>
        /// <param name="status"></param>
        public ChangelistInfo(
            string changelistId,
            Int32 bugId,
            string depotPort,
            string userName,
            DateTime time,
            string description,
            string relation,
            string clientName,
            string status)
        {
            this.ChangelistId = changelistId;
            this.BugId = bugId;
            this.DepotPort = depotPort;
            this.UserName = userName;
            this.ModifyTime = time;
            this.Description = description;
            this.Relation = relation;
            this.ClientName = clientName;
            this.Status = status;
        }

        /// <summary>
        /// Translate chanelist into the string format in PS
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string desc = this.Description.Trim();
            int index = desc.IndexOfAny(new char[] { '\r', '\n' });

            if (-1 < index)
            {
                desc = desc.Substring(0, index);
            }

            return string.Format(
                "{0},{1},{2},{3},{4},{5}",
                this.ChangelistId,
                this.DepotPort,
                this.Relation,
                this.UserName,
                this.ModifyTime.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo),
                desc);
        }
    }
}
