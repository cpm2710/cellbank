﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Collections.ObjectModel;

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
            Logger.Log("Now We Are Executing PrepareVariablesActivity");
            PropertyDescriptorCollection propertyDescriptorCol = context.DataContext.GetProperties();
            
            foreach (PropertyDescriptor pd in propertyDescriptorCol)
            {
                if (string.Equals(pd.Name, Constants.ServiceVariableName))
                {
                    pd.SetValue(context.DataContext, new string[] { "WseComputerBackupSvc" });
                }

                if (string.Equals(pd.Name, Constants.FileVariableName))
                {
                    pd.SetValue(context.DataContext, new FileItem[] { new FileItem("a", @"\\andess1server\c$\windows\system32\Essentials\ConfigTasks.dll")});
                }
            }
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
        }
    }
}
