using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace RotorsWorkFlow
{

    public sealed class PrepareVariablesActivity : CodeActivity
    {
        //// Define an activity input argument of type string
        public InArgument<string> Text { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            Console.WriteLine("Now We Are Executing PrepareVariablesActivity");
            PropertyDescriptorCollection propertyDescriptorCol = context.DataContext.GetProperties();

            foreach (PropertyDescriptor pd in propertyDescriptorCol)
            {
                if (string.Equals(pd.Name, "services"))
                {
                    pd.SetValue(context.DataContext, new string[] { "shit", "shit2" });
                }

                if (string.Equals(pd.Name, "files"))
                {
                    pd.SetValue(context.DataContext, new FileItem[] { new FileItem(), new FileItem() });
                }
            }
            //context.SetValue(
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
        }
    }
}
