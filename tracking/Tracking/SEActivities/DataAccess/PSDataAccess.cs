using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProductStudio;
using System.Xml;
using System.IO;
using CommonResource;
using System.Text.RegularExpressions;

namespace SEActivities.DataAccess
{
    /// <summary>
    /// PSDataAccess
    /// </summary>
    public class PSDataAccess : IDisposable
    {
        #region Private Variables

        private string connectDomain;
        private string productName;
        private ProductStudio.Directory psDirectory;
        private Product psProduct;
        private Datastore psDataStore;
        // Whether disposed is called or not
        private bool disposed = false;

        #endregion Private Variables

        #region Public Functions

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectDomain"></param>
        /// <param name="productName"></param>
        /// <param name="onBehalfOf"></param>
        public PSDataAccess(string connectDomain, string productName, string onBehalfOf = null)
        {
            this.connectDomain = connectDomain;
            this.productName = productName;
            try
            {
                this.psDirectory = new ProductStudio.Directory();
                this.psDirectory.Connect(connectDomain, "", "");
                this.psProduct = this.psDirectory.GetProductByName(productName);
                this.psDataStore = this.psProduct.Connect("", "", onBehalfOf);
            }
            catch (Exception e)
            {
                throw new Exception(String.Format("Failed to initialize Product Studio connection for domain: {0} and product: {1}",
                    connectDomain, productName), e);
            }
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~PSDataAccess()
        {
            Dispose(false);
        }

        /// <summary>
        /// Explicit Dispose Function
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Update Bug in PS using the PS SDK
        /// </summary>
        /// <param name="bugChangeList"></param>
        /// <param name="bugFieldMappingCollection"></param>
        /// <param name="updateAction"></param>
        public void UpdateBug(PSBugChangeList psBugChangeList)
        {
            DatastoreItem psItem = null;
            bool hasInvalidField = false;

            try
            {
                psItem = this.psDataStore.GetDatastoreItem(PsDatastoreItemTypeEnum.psDatastoreItemTypeBugs, psBugChangeList.BugId, null);
                switch (psBugChangeList.Action)
                {
                    case PSUpdateAction.Update:
                        psItem.Edit(PsItemEditActionEnum.psDatastoreItemEditActionEdit, null, PsApplyRulesMask.psApplyRulesAll);
                        break;

                    case PSUpdateAction.Resolve:
                        psItem.Edit(PsItemEditActionEnum.psBugEditActionResolve, null, PsApplyRulesMask.psApplyRulesAll);
                        break;

                    case PSUpdateAction.Close:
                        psItem.Edit(PsItemEditActionEnum.psBugEditActionClose, null, PsApplyRulesMask.psApplyRulesAll);
                        break;

                    case PSUpdateAction.Activate:
                        psItem.Edit(PsItemEditActionEnum.psBugEditActionActivate, null, PsApplyRulesMask.psApplyRulesAll);
                        break;
                }

                foreach (string psFieldName in psBugChangeList.BugChanges.Keys)
                {
                    // We need to skip these values as they are auto filled by PS.
                    if (string.Compare(psFieldName, "Closed By", true) == 0 ||
                        string.Compare(psFieldName, "Resolved By", true) == 0 ||
                        string.Compare(psFieldName, "Changed By", true) == 0)
                    {
                        continue;
                    }
                    Field psField = null;
                    // PendingChangelist doesn't have mapping value in DB because Sync Service doesn't handle it, so we have to map it to a PS value manually.
                    if (string.Compare(psFieldName, BugFields.PendingChangelist.ToString(), true) == 0)
                    {
                        psField = psItem.Fields["ChangelistInfo"];
                    }
                    else
                    {
                        psField = psItem.Fields[psFieldName];
                    }


                    if (psField == null)
                    {
                        throw new Exception("Failed to find property [" + psFieldName + "]");
                    }
                    else if (psField.IsReadOnly)
                    {
                        // We need to do some logging here as we skip some read-only psField when doing update
                        //throw new Exception("Failed to update read-only property [" + psFieldName + "]");
                        continue;
                    }
                    else
                    {
                        // SLA Date in PS is string, but in SysBugItem is DateTime, there comes is datetime string or null
                        if (string.Compare(psField.Name, "SLA Date", true) == 0)
                        {
                            DateTime slaDate;

                            if (DateTime.TryParse(psBugChangeList.BugChanges[psFieldName].ToString(), out slaDate) == true)
                            {
                                psField.Value = string.Format("{0:MM/dd/yyyy}", slaDate);
                            }
                            else
                            {
                                psField.Value = null;
                            }
                        }
                        // PendingChangelist doesn't have mapping value in DB because Sync Service doesn't handle it, so we have to map it to a PS value manually.
                        else if (string.Compare(psField.Name, "ChangelistInfo", true) == 0)
                        {
                            Dictionary<string, ChangelistInfo> changelistDict = new Dictionary<string, ChangelistInfo>(StringComparer.OrdinalIgnoreCase);
                            string changeListInfos = psField.Value;

                            ChangelistInfo newPendingChangelist = psBugChangeList.BugChanges[psFieldName] as ChangelistInfo;

                            if (string.IsNullOrEmpty(changeListInfos) == false && changeListInfos.Length > 0)
                            {
                                Regex r = new Regex(@"^\s*\d+,.+,.+,.+,.+,.*$");

                                string[] split = changeListInfos.Split("\r".ToCharArray());
                                foreach (string s in split)
                                {
                                    // 722: Handle changelist with linefeed in description
                                    if (r.IsMatch(s.Trim("\n".ToCharArray())))
                                    {
                                        ChangelistInfo info = new ChangelistInfo(s.Trim("\n".ToCharArray()));
                                        // If the changelist is related and it also ends with $$SEPortal$$, we will discard them, not adding them into the dictionary
                                        if (string.Compare(info.Relation, "related", true) == 0
                                            && info.Description.EndsWith("$$SEPortal$$") == true)
                                        {
                                            continue;
                                        }
                                        // If the changelist has the same id as the new changelist, we will discard it, not adding it into the dictionary
                                        if (newPendingChangelist != null
                                            && string.Compare(info.ChangelistId, newPendingChangelist.ChangelistId, true) == 0)
                                        {
                                            continue;
                                        }
                                        // Add the changelist to the dictionary if it is not already added
                                        if (changelistDict.ContainsKey(info.ChangelistId) == false)
                                        {
                                            changelistDict.Add(info.ChangelistId, info);
                                        }
                                    }
                                }
                            }

                            if (newPendingChangelist != null)
                            {
                                changelistDict.Add(newPendingChangelist.ChangelistId, newPendingChangelist);
                            }

                            string pendingChangelistStr = string.Empty;
                            foreach (ChangelistInfo changelistInfo in changelistDict.Values)
                            {
                                if (newPendingChangelist != null
                                    && string.Compare(newPendingChangelist.Relation, "Related", true) == 0
                                    && string.Compare(newPendingChangelist.ChangelistId, changelistInfo.ChangelistId, true) == 0)
                                {
                                    pendingChangelistStr += changelistInfo.ToString() + " $$SEPortal$$\r";
                                }
                                else
                                {
                                    pendingChangelistStr += changelistInfo.ToString() + "\r";
                                }
                            }
                            psField.Value = pendingChangelistStr;
                        }
                        else
                        {
                            if (psBugChangeList.BugChanges[psFieldName] != null)
                            {
                                psField.Value = psBugChangeList.BugChanges[psFieldName].ToString();
                            }
                            else
                            {
                                psField.Value = null;
                            }
                        }
                    }
                }

                // Verify that all fields are valid before saving
                foreach (ProductStudio.Field field in psItem.Fields)
                {
                    if (field.Validity != PsFieldStatusEnum.psFieldStatusValid)
                    {
                        hasInvalidField = true;
                        throw new Exception("Updating Field (" + field.Name + ") with Value (" + field.Value + ") is invalid. Counld not edit bug " + psBugChangeList.BugId.ToString());
                    }
                }

                // Commit the save if there is no invalid field
                if (hasInvalidField)
                {
                    throw new Exception("Invalid Field(s) were found. Could not edit bug " + psBugChangeList.BugId.ToString());
                }
                else
                {
                    psItem.Save();
                }
            }
            catch (Exception e)
            {
                throw new Exception(String.Format("Failed to update bug {0} in {1} database.",
                    psBugChangeList.BugId, this.productName), e);
            }
        }

        public Int32 CreateNewBug(string psFieldDefXml)
        {
            bool hasInvalidField = false;

            // Create a new datastore instance
            DatastoreItemList psDataList = new DatastoreItemList();
            psDataList.Datastore = this.psDataStore;

            // Craete a blank bug
            psDataList.CreateBlank(PsDatastoreItemTypeEnum.psDatastoreItemTypeBugs);
            DatastoreItem psDataItem = psDataList.DatastoreItems.Add(null, PsApplyRulesMask.psApplyRulesAll);

            // Set fields for the new bug
            Fields psFields = psDataItem.Fields;

           
            // New PS Bug Field Description XML file will look like
            // =======================================================
            // <psfieldDef>
            //     <Title>
            //         Title (Ex. RFH: Hotfix for ...)
            //     </Title>
            //     <Tree Path>
            //         Path (Ex. SCCM 2007 SP2)
            //     </Tree Path>
            //     <Issue type>
            //         Issue Type (Ex. RFH / CDCR)
            //     </Issue type>
            //     <Get Help>
            //         Get Help (Ex. Yes / No)
            //     </Get Help>
            //     <Priority>
            //         Priority (Ex. 1, 2, 3, 4)
            //     </Priority>
            //     <Severity>
            //         Severity (Ex. 1, 2, 3, 4)
            //     </Severity>
            //     <SMS Product>
            //         SMS Product (Ex. x86 / x64)
            //     </SMS Product>
            //     <Component>
            //         Component (Ex. component name)
            //     </Component>
            //     <FeatureArea>
            //         Feature Area (Ex. feature area name)
            //     </FeatureArea>
            //     <Open Build>
            //         Open Build (Ex. build version 6487.2000)
            //     </Open Build>
            //     <Language>
            //         Language (Ex. ENU)
            //     </Language>
            //     <How found>
            //         How Found (Ex. Internal)
            //     </How found>
            //     <Source>
            //         Source (Ex. Ad Hoc)
            //     </Source>
            //     <Source ID>
            //         Source ID (ex. Airforce)
            //     </Source ID>
            //     <PSS>
            //         SR Number (ex. SR Number)
            //     </PSS>
            //     <KB Article>
            //         KB Number (ex. KB Number 98765432)
            //     </KB Article>
            //     <Repro Steps>
            //         Repro Steps (ex. Repro Steps)
            //     </Repro Steps>
            //     <Description>
            //         Description (ex. Description)
            //     </Description>
            //     <Related Bugs>
            //         SMS Sustained Engineering:5991
            //     </Related Bugs>
            //     <QFE Status>
            //         Core Review
            //     </QFE Status>
            // </psfieldDef>
            XmlReader xmlReader = XmlReader.Create(new StringReader(psFieldDefXml));

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    if (string.Compare(XmlConvert.DecodeName(xmlReader.Name), "psfieldDef", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        xmlReader.Read(); // Read psfieldDef
                    }
                    else if (string.Compare(XmlConvert.DecodeName(xmlReader.Name), "Tree Path", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        xmlReader.ReadStartElement(xmlReader.Name); // Read <Tree Path>
                        psFields["TreeID"].Value = this.GetTreeIDFromTreePath(this.psDataStore.RootNode, xmlReader.Value);
                        xmlReader.Read();
                        xmlReader.ReadEndElement();
                    }
                    else if (string.Compare(XmlConvert.DecodeName(xmlReader.Name), "Related Bugs", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        xmlReader.ReadStartElement(xmlReader.Name); // Read <RelatedBugs>
                        string product = xmlReader.Value.Substring(0, xmlReader.Value.IndexOf(':'));
                        Int32 bugId = 0;
                        Int32.TryParse(xmlReader.Value.Substring(xmlReader.Value.IndexOf(':') + 1), out bugId);
                        ((Bug)psDataItem).AddRelatedLink(bugId, product);
                        xmlReader.Read();
                        xmlReader.ReadEndElement();
                    }
                    else if (string.Compare(XmlConvert.DecodeName(xmlReader.Name), "File", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        xmlReader.ReadStartElement(xmlReader.Name); // Read <File>
                        ((Bug)psDataItem).Files.Add(xmlReader.Value);
                        xmlReader.Read();
                        xmlReader.ReadEndElement();
                    }
                    else
                    {
                        string fieldName = XmlConvert.DecodeName(xmlReader.Name);
                        xmlReader.ReadStartElement(xmlReader.Name); // Read Start Element Ex. <Title>
                        psFields[fieldName].Value = xmlReader.Value;
                        xmlReader.Read();
                        xmlReader.ReadEndElement(); // Read End Element Ex. </Title>
                    }
                }
            }

            //  Let's make sure all fields are valid before saving
            foreach (Field psField in psDataItem.Fields)
            {
                if (psField.Validity != PsFieldStatusEnum.psFieldStatusValid)
                {
                    hasInvalidField = true;
                    throw new Exception("Invalid Field (" + psField.Name + ") with Value (" + psField.Value + "). Counld not new bug!");
                }
            }

            if (hasInvalidField)
            {
                throw (new ApplicationException("Invalid Field(s) were found.  Could not create new bug."));
            }
            else
            {
                psDataItem.Save(true);
                return Convert.ToInt32(psFields["ID"].Value);
            }
        }

        /// <summary>
        /// Clone Bug
        /// </summary>
        /// <param name="bugId"></param>
        /// <param name="targetConnectDomain"></param>
        /// <param name="targetProductName"></param>
        public void CloneBug(int bugId, string targetConnectDomain, string targetProductName)
        {
            DatastoreItem psItem = null;
            DatastoreItem psCloneItem = null;
            ProductStudio.Directory psTargetDirectory = null;
            Product psTargetProduct = null;
            Datastore psTargetDataStore = null;

            try
            {
                if (targetConnectDomain != null && targetProductName != null)
                {
                    psTargetDirectory = new ProductStudio.Directory();
                    psTargetDirectory.Connect(targetConnectDomain, "", "");
                    psTargetProduct = psTargetDirectory.GetProductByName(targetProductName);
                    psTargetDataStore = psTargetProduct.Connect("", "", "");
                }
                else
                {
                    psTargetDataStore = this.psDataStore;
                }

                psItem = this.psDataStore.GetDatastoreItem(PsDatastoreItemTypeEnum.psDatastoreItemTypeBugs, bugId, null);

                psItem.Edit(PsItemEditActionEnum.psDatastoreItemEditActionReadOnly, null, PsApplyRulesMask.psApplyRulesAll);
                // Clone the item to the targetDataStore
                psCloneItem = psItem.Clone(psTargetDataStore);
                // Save the new item
                psCloneItem.Save(true);
            }
            catch (Exception e)
            {
                throw new Exception(String.Format("Failed to clone bug {0} to {1} database.",
                    bugId, targetProductName), e);
            }
            finally
            {
                if (psTargetDirectory != null)
                {
                    psTargetDirectory.Disconnect();
                }
            }
        }

        /// <summary>
        /// Get Source depot from PS
        /// </summary>
        /// <returns></returns>
        public object[] GetSourceDepots()
        {
            return (object[])this.psProduct.SourceDepots;
        }

        /// <summary>
        /// Get changed BUGs from last modified time
        /// </summary>
        /// <param name="LastModifiedTime"></param>
        /// <returns></returns>
        public DatastoreItems GetBugListStartingFrom(DateTime LastModifiedTime)
        {
            string psQueryXml = "<Query>";
            psQueryXml += "<Group GroupOperator='And'>";
            psQueryXml += "<Expression Column='Changed Date' Operator='equalsGreater'><DateTime>" + LastModifiedTime.ToUniversalTime().ToString() + "</DateTime></Expression>";
            //psQueryXml += "<Expression Column='Status' Operator='notequals'><String>Closed</String></Expression>";
            psQueryXml += "</Group>";
            psQueryXml += "</Query>";

            DatastoreItemList psDataList = null;
            try
            {
                Query psQuery = new Query();
                psQuery.CountOnly = false;
                psQuery.SelectionCriteria = psQueryXml;

                psDataList = new DatastoreItemList();
                psDataList.Query = psQuery;
                psDataList.Datastore = this.psDataStore;
                psDataList.Execute();
            }
            catch (Exception e)
            {
                throw new Exception(String.Format("Failed to Query bugs."), e);
            }

            return psDataList.DatastoreItems;
        }

        /// <summary>
        /// Load PS Tree Path
        /// </summary>
        /// <returns></returns>
        public PSTreePath LoadingPSTreePath()
        {
            PSTreePath psTreePath = new PSTreePath();
            foreach (Node node in this.psDataStore.RootNode.Nodes)
            {
                psTreePath.Data.Add(this.GetNode(node, string.Empty));
            }
            return psTreePath;
        }

        /// <summary>
        /// Load Product List
        /// </summary>
        /// <returns></returns>
        public List<string> LoadingProducts()
        {
            List<string> productList = new List<string>();
            Products productCollection = this.psDirectory.GetProducts(PsDirectoryFilterEnum.psDirectoryFilterPSProducts);
            foreach (Product product in productCollection)
            {
                productList.Add(product.Name);
            }
            return productList;
        }

        /// <summary>
        /// Load PS Fields Definition and its parent-child relationship
        /// </summary>
        /// <param name="psFieldNames"></param>
        /// <returns></returns>
        public List<PSFieldDefinition> LoadingPSFields(string psFieldNames)
        {
            // Create a new datasotre instance
            DatastoreItemList psDataList = new ProductStudio.DatastoreItemList();
            psDataList.Datastore = this.psDataStore;

            // Craete a blank bug
            psDataList.CreateBlank(PsDatastoreItemTypeEnum.psDatastoreItemTypeBugs);
            DatastoreItem psDataItem = psDataList.DatastoreItems.Add(null, PsApplyRulesMask.psApplyRulesAll);
            Bug psBug = (Bug)psDataItem;

            // Get the field list for processing
            string[] psFieldNameCollection = psFieldNames.Split(';');
            List<string> psFieldNameList = new List<string>();
            foreach (string psFieldName in psFieldNameCollection)
            {
                psFieldNameList.Add(psFieldName);
            }

            // New PS field definition list
            Dictionary<Int32, PSFieldDefinition> psFieldDefinitionDict = new Dictionary<int, PSFieldDefinition>();

            // Populate the PS field definition
            foreach (Field psField in psBug.Fields)
            {
                // Whether in the request fields
                if (psFieldNameList.Contains(psField.Name) == false)
                {
                    continue;
                }
                // New PSFieldDefinition
                PSFieldDefinition psFieldDefinition = new PSFieldDefinition(psField.ID, psField.Name, psField.FieldDefinition.ToolTip);
                psFieldDefinitionDict.Add(psField.ID, psFieldDefinition);
            }

            List<PSFieldDefinition> psFieldDefinitionList = new List<PSFieldDefinition>();

            // Populate the PS field definition
            foreach (KeyValuePair<Int32, PSFieldDefinition> kvp in psFieldDefinitionDict)
            {
                Field psField = psBug.Fields.get_ItemByID(kvp.Key);

                // Initialize PS field allowed values
                Int32 psFieldAllowedValueCount = GetFieldAllowedValuesCount(psField);
                Values psFieldAllowedValues = this.GetFieldAllowedValues(psField);
                List<object> psFieldDefinitionAllowedValues = new List<object>();
                foreach (object obj in psFieldAllowedValues)
                {
                    psFieldDefinitionAllowedValues.Add(obj);
                }
                kvp.Value.SetAllowedValues(psFieldDefinitionAllowedValues);

                // Add to psFieldDefinitionList
                psFieldDefinitionList.Add(kvp.Value);

                Int32 psFieldValidValueCount = GetFieldValidValuesCount(psField);

                // Detect the dependencies and parent field
                if (psField.FieldDefinition.IsIgnored == false &&
                    psField.FieldDefinition.IsComputed == false &&
                    psFieldAllowedValueCount > 0)
                {
                    if (psFieldAllowedValueCount == 0 || psFieldAllowedValueCount <= psFieldValidValueCount)
                    {
                        continue;
                    }

                    Values psFieldValidValues = GetFieldValidValues(psField);
                    foreach (ProductStudio.Field parentField in psBug.Fields)
                    {
                        if (parentField.ID != psField.ID &&
                            //parentField.IsReadOnly == false &&
                            parentField.EditType == ProductStudio.PsFieldEditTypeEnum.psFieldEditTypeBasicList &&
                            //parentField.Name != "Resolution" &&
                            //parentField.Name != "Status" &&
                            parentField.Name != "TreeType" &&
                            parentField.Name != "Closed By" &&
                            parentField.FieldDefinition.Type != ProductStudio.PsFieldDefinitionTypeEnum.psFieldDefinitionTypeSubObject)
                        {
                            object saved = parentField.Value;

                            // loop through the allowed value list of the parent field instead of the valid value list here
                            // to work with multiple level of dependencies, ex, e.g. Field "Service Pack" depends on "Version", which in 
                            // turn depends on "Product" field
                            foreach (object val in GetFieldAllowedValues(parentField))
                            {
                                parentField.Value = val;
                                Values psFieldNewValidValues = GetFieldValidValues(psField);
                                if (!IsSameValueList(psFieldNewValidValues, psFieldValidValues))
                                {
                                    // Find whether parent field is also in the dictionary
                                    if (psFieldDefinitionDict.ContainsKey(parentField.ID) == true)
                                    {
                                        // If we detect two parent fields, throw exception
                                        if (kvp.Value.ParentField != null && kvp.Value.ParentField.Id != parentField.ID)
                                        {
                                            throw new Exception(string.Format("psField {0}:{1} has more than one parent fields: {2}:{3} and {4}:{5}",
                                                psField.ID, psField.Name,
                                                kvp.Value.ParentField.Id, kvp.Value.ParentField.Name,
                                                parentField.ID, parentField.Name));
                                        }
                                        kvp.Value.ParentField = psFieldDefinitionDict[parentField.ID];
                                        List<object> psFieldDefinitionNewValidValues = new List<object>();
                                        foreach (object obj in psFieldNewValidValues)
                                        {
                                            psFieldDefinitionNewValidValues.Add(obj);
                                        }
                                        kvp.Value.SetValidValues(val, psFieldDefinitionNewValidValues);
                                    }
                                }
                            }
                            parentField.Value = saved;
                        }
                    }
                }
            }

            return psFieldDefinitionList;
        }
        #endregion Public Functions

        #region Private Functions
        /// <summary>
        /// Get Node, utility function for loading PS Tree Path
        /// </summary>
        /// <param name="node"></param>
        /// <param name="psPath"></param>
        /// <returns></returns>
        private PSNode GetNode(Node node, string psPath)
        {
            PSNode psNode = new PSNode();
            string path = string.Empty;
            if (string.IsNullOrEmpty(psPath) == false)
            {
                path = string.Format("{0}\\{1}", psPath, node.Name);
            }
            else
            {
                path = node.Name;
            }

            // Recursively get all the child nodes
            foreach (Node childNode in node.Nodes)
            {
                psNode.Children.Add(this.GetNode(childNode, path));
            }

            psNode.Id = node.ID;
            psNode.Name = node.Name;
            psNode.FullPath = path;
            return psNode;
        }

        /// <summary>
        /// Get field allowed values, utility function for getting all PS field definitions
        /// </summary>
        /// <param name="psField"></param>
        /// <returns></returns>
        private Values GetFieldAllowedValues(Field psField)
        {
            return psField.FieldDefinition.get_AllowedValues(ProductStudio.PsDatastoreItemTypeEnum.psDatastoreItemTypeBugs, Type.Missing, true, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// Get field allowed values count, utility function for getting all PS field definitions
        /// </summary>
        /// <param name="psField"></param>
        /// <returns></returns>
        private int GetFieldAllowedValuesCount(Field psField)
        {
            Values vals = GetFieldAllowedValues(psField);
            if (vals == null)
            {
                return 0;
            }
            return vals.Count;
        }

        /// <summary>
        /// Get field valid values, utility function for getting all PS field definitions
        /// </summary>
        /// <param name="psField"></param>
        /// <returns></returns>
        private Values GetFieldValidValues(Field psField)
        {
            Values retval = null;
            try
            {
                retval = psField.ValidValues;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                if (psField.Name != "Closed By")
                {
                    Console.WriteLine("Error retreiving valid values for field: " + psField.Name, ex);
                }
            }
            return retval;
        }

        /// <summary>
        /// Get field valid values count, utility function for getting all PS field definitions
        /// </summary>
        /// <param name="psField"></param>
        /// <returns></returns>
        private int GetFieldValidValuesCount(Field psField)
        {
            Values vals = GetFieldValidValues(psField);
            if (vals == null)
            {
                return 0;
            }
            return vals.Count;
        }

        /// <summary>
        /// Check whether the two value list is the same, utility function for getting all PS field definitions
        /// </summary>
        /// <param name="vals1"></param>
        /// <param name="vals2"></param>
        /// <returns></returns>
        private bool IsSameValueList(Values vals1, Values vals2)
        {
            if (vals1 == null || vals2 == null || vals1.Count != vals2.Count)
            {
                return false;
            }

            for (int i = 0; i < vals1.Count; i++)
            {
                if (vals1[i] != vals2[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get Tree Id from the tree path
        /// </summary>
        /// <param name="node"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private int GetTreeIDFromTreePath(Node node, string path)
        {
            string[] pathLevelNames;
            char[] separator = { '\\' };
            int pathCount = 0;
            Node currentNode = null;

            pathLevelNames = path.Split(separator);

            currentNode = node;

            if (path.Trim().Length >= 1 && path.Trim() != "\\")
            {
                for (pathCount = 0; pathCount < pathLevelNames.Length; pathCount++)
                {
                    currentNode = currentNode.Nodes[pathLevelNames[pathCount]];
                }
            }

            return currentNode.ID;
        }

        #endregion Private Functions

        #region IDisposable Members

        /// <summary>
        /// Disconnect from the PS directory when done
        /// </summary>
        private void Dispose(bool disposing)
        {
            if (this.disposed != true)
            {
                if (this.psDirectory != null &&
                    this.psDirectory.IsConnected == true)
                {
                    this.psDirectory.Disconnect();
                }
            }
            disposed = true;
        }

        #endregion
    }
}
