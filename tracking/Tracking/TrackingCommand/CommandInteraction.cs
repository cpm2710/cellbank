using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace TrackingCommands
{
    public class CommandInteraction
    {
        public List<string> getRequiredInputs(string commandName)
        {
            Type[] types=Assembly.Load("TrackingCommands").GetTypes();
            List<string> requiredInputs = new List<string>();
            foreach (Type t in types)
            {
                if (t.Name.Equals(commandName))
                {
                    FieldInfo[] fields = t.GetFields();
                    foreach (FieldInfo field in fields)
                    {
                        requiredInputs.Add(field.Name);
                    }
                }
            }
            return requiredInputs;
        }
        public void executeCommand(string commandName)
        {

        }
    }
}
