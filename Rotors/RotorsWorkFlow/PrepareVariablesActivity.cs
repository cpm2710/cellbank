using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace RotorsWorkFlow
{

    public sealed class PrepareVariablesActivity : CodeActivity
    {
        //private Collection<Variable> variables;
        //public Collection<Variable> Variables
        //{
        //    get
        //    {
        //        if (this.variables == null)
        //        {
        //            this.variables = new Collection<Variable>();
        //        }
        //        return this.variables;
        //    }
        //} 
        //protected override void CacheMetadata(CodeActivityMetadata metadata)
        //{
        //    metadata.SetArgumentsCollection(this.Variables);
        //    base.CacheMetadata(metadata);
        //} 
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
                    pd.SetValue(context.DataContext, new string[] { "AlipaySecSvc" });
                }

                if (string.Equals(pd.Name, Constants.FileVariableName))
                {
                    pd.SetValue(context.DataContext, new FileItem[] { new FileItem("a", "b"), new FileItem("a2", "b2") });
                }
            }
            //context.SetValue(
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
        }
    }
}
