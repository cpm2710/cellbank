using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Net;
using RotorsWorkFlow.Helpers;

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

                        string sharePath = Constants.GacEssentialsPath.Substring(0, Constants.GacEssentialsPath.IndexOf(@"c$\Windows") + @"c$\Windows".Length);

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
                        Logger.Error("exception encounterred: {0}", e);
                    }
                }
            }
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
        }
    }
}
