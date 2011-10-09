using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CommonResource
{
    /// <summary>
    /// Bug Field Change List
    /// </summary>
    [DataContract]
    [KnownTypeAttribute(typeof(ChangelistInfo))]
    public class BugChangeList
    {
        #region Properties

        /// <summary>
        /// Team Id
        /// </summary>
        [DataMember]
        public Int32 TeamId { get; set; }

        /// <summary>
        /// Bug Id
        /// </summary>
        [DataMember]
        public Int32 BugId { get; set; }

        /// <summary>
        /// Who Am I
        /// </summary>
        [DataMember]
        public string WhoAmI { get; set; }

        /// <summary>
        /// Happend Event
        /// </summary>
        [DataMember]
        public string HappenedEvent { get; set; }

        /// <summary>
        /// Bug changes List
        /// </summary>
        [DataMember]
        public Dictionary<BugFields, Object> BugChanges { get; set; }
        #endregion Properties

        #region Public Functions
        public BugChangeList(Int32 teamId, Int32 bugId, string whoAmI, string happendEvent = null)
        {
            TeamId = teamId;
            BugId = bugId;
            WhoAmI = whoAmI;
            HappenedEvent = happendEvent;
            BugChanges = new Dictionary<BugFields, object>();
        }
        #endregion Public Functions
    }

    /// <summary>
    /// PS Bug Field Change List
    /// </summary>
    public class PSBugChangeList
    {
        #region Properties

        public Int32 BugId { get; set; }

        public PSUpdateAction Action { get; set; }

        public Dictionary<string, Object> BugChanges { get; set; }

        #endregion Properties

        #region Public Functions
        public PSBugChangeList(Int32 bugId, PSUpdateAction action = PSUpdateAction.Update)
        {
            BugId = bugId;
            Action = action;
            BugChanges = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }
        #endregion Public Functions
    }

    /// <summary>
    /// PSUpdateAction
    /// </summary>
    public enum PSUpdateAction
    {
        Update,
        Resolve,
        Close,
        Activate
    }
}
