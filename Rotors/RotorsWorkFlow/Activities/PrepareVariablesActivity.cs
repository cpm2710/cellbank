// author: andyliuliming@outlook.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Collections.ObjectModel;
using RotorsLib.Helpers;
using RotorsLib;

namespace RotorsWorkFlow.Activities
{
    public sealed class PrepareVariablesActivity : CodeActivity
    {
        //// Define an activity input argument of type string
        public InArgument<string> Text { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            string msg =("Now We Are Executing PrepareVariablesActivity");
            Singleton<ReportMediator>.UniqueInstance.ReportStatus(msg);
            PropertyDescriptorCollection propertyDescriptorCol = context.DataContext.GetProperties();
            
            foreach (PropertyDescriptor pd in propertyDescriptorCol)
            {
                if (string.Equals(pd.Name, Constants.ServiceVariableName))
                {
                    pd.SetValue(context.DataContext, DataInputHelper.BuildServiceNames());
                }

                if (string.Equals(pd.Name, Constants.FileVariableName))
                {
                    object v = pd.GetValue(context.DataContext);
                    if (v == null || ((string[])v).Length == 0)
                    {
                        //@"\\andess1server\c$\windows\system32\Essentials\ConfigTasks.dll"
                        pd.SetValue(context.DataContext, DataInputHelper.BuildFileItems());
                    }
                    
                }
            }
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
        }
    }
}
