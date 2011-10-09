using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CommonResource
{
    [DataContract]
    public class PSNode
    {
        private List<PSNode> children = new List<PSNode>();

        /// <summary>
        /// All the children of PS Tree Node
        /// </summary>
        [DataMember]
        public List<PSNode> Children
        {
            get
            {
                return this.children;
            }
            set
            {
                this.children = value;
            }
        }

        /// <summary>
        /// PS Tree Node Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// PS Tree Node Full Path
        /// </summary>
        [DataMember]
        public string FullPath { get; set; }

        /// <summary>
        /// PS Tree Node Id
        /// </summary>
        [DataMember]
        public Int32 Id { get; set; }
    }

    public class PSTreePath
    {
        #region Priviate Variables
        List<PSNode> root = new List<PSNode>();
        #endregion

        #region Constructor
        public PSTreePath()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// List of all the PS Node under Root
        /// </summary>
        [DataMember]
        public List<PSNode> Data
        {
            get
            {
                return this.root;
            }
        }
        #endregion
    }
}
