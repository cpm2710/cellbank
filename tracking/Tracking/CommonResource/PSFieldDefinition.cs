using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CommonResource
{
    [DataContract]
    public class PSFieldDefinition
    {
        /// <summary>
        /// Field Id
        /// </summary>
        [DataMember]
        public Int32 Id { get; private set; }

        /// <summary>
        /// Field Name
        /// </summary>
        [DataMember]
        public string Name { get; private set; }

        /// <summary>
        /// Tool Tip for the field
        /// </summary>
        [DataMember]
        public string ToolTip { get; private set; }

        /// <summary>
        /// Valid Value Dictionary
        /// key is parent value, value is the valid value list based on that parent value
        /// if key is string.empy, then value is all the valid value list
        /// </summary>
        [DataMember]
        public Dictionary<object, List<object>> ValidValues { get; private set; }

        /// <summary>
        /// Parent field
        /// </summary>
        [DataMember]
        public PSFieldDefinition ParentField { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public PSFieldDefinition(Int32 id, string name, string toolTip)
        {
            this.Id = id;
            this.Name = name;
            this.ToolTip = toolTip;
            this.ValidValues = new Dictionary<object, List<object>>();
        }

        /// <summary>
        /// Get allowed value list
        /// </summary>
        /// <returns></returns>
        public List<object> GetAllowedValues()
        {
            if (this.ParentField != null || this.ValidValues.ContainsKey(string.Empty) == false)
            {
                return null;
            }
            else
            {
                return this.ValidValues[string.Empty];
            }
        }

        /// <summary>
        /// Set allowed value list
        /// </summary>
        /// <param name="valueList"></param>
        public void SetAllowedValues(List<object> valueList)
        {
            if (this.ValidValues.ContainsKey(string.Empty) == false)
            {
                this.ValidValues.Add(string.Empty, valueList);
            }
            else
            {
                this.ValidValues[string.Empty] = valueList;
            }
        }

        /// <summary>
        /// Get valid value based on parent field's value
        /// </summary>
        /// <param name="parentFieldValue"></param>
        /// <returns></returns>
        public List<object> GetValidValues(object parentFieldValue)
        {
            if (this.ValidValues.ContainsKey(parentFieldValue) == true)
            {
                return this.ValidValues[parentFieldValue];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Set valid value list based on parent field's value
        /// </summary>
        /// <param name="parentFieldValue"></param>
        /// <param name="valueList"></param>
        public void SetValidValues(object parentFieldValue, List<object> valueList)
        {
            if (this.ValidValues.ContainsKey(parentFieldValue) == false)
            {
                this.ValidValues.Add(parentFieldValue, valueList);
            }
            else
            {
                this.ValidValues[parentFieldValue] = valueList;
            }
        }
    }
}
