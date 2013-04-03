using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace RotorsWorkFlow
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
                    FileItem[] services = (FileItem[])pd.GetValue(context.DataContext);
                    foreach (FileItem service in services)
                    {
                        ReplaceFileHelper.ReplaceFile(service);
                    }
                }
            }
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
        }
    }
}
