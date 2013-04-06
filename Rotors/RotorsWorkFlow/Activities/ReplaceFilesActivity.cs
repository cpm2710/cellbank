// author: andyliuliming@outlook.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Net;
using RotorsLib.Helpers;
using RotorsLib.Domain;
using RotorsLib;

namespace RotorsWorkFlow.Activities
{

    public sealed class ReplaceFilesActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            PropertyDescriptorCollection propertyDescriptorCol = context.DataContext.GetProperties();

            foreach (PropertyDescriptor pd in propertyDescriptorCol)
            {
                if (string.Equals(pd.Name, Constants.FileVariableName))
                {
                    try
                    {
                        NetworkCredential networkCredential = new NetworkCredential(Constants.UserName, Constants.PassWord, Constants.Domain);

                        string sharePath = string.Format(@"\\{0}\c$", Constants.MachineName);
                            //Constants.GacEssentialsPath.Substring(0, Constants.GacEssentialsPath.IndexOf(@"c$\Windows") + @"c$\Windows".Length);

                        using (NetworkConnection nc = new NetworkConnection(sharePath, networkCredential))
                        {
                            FileItem[] fileItems = (FileItem[])pd.GetValue(context.DataContext);
                            foreach (FileItem fileItem in fileItems)
                            {
                                ReplaceFileHelper.ReplaceFile(fileItem);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string msg = string.Format("exception encounterred: {0} when executing replacefilesactivity", e);
                        Singleton<ReportMediator>.UniqueInstance.ReportStatus(msg, LogLevel.Warning);
                    }
                }
            }
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
        }
    }
}
