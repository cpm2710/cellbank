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
            Type[] types = Assembly.Load("SEActivities").GetTypes();
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
        public void executeCommand(string commandName,Dictionary<string,string> inputs)
        {
            Assembly trackingCommandsAssembly = Assembly.Load("TrackingCommands");
            object command = trackingCommandsAssembly.CreateInstance("TrackingCommands." + commandName);

            Command cmd = (Command)command;
            FieldInfo [] fieldInfos=cmd.GetType().GetFields();
            foreach (FieldInfo fi in fieldInfos)
            {
                fi.SetValue(cmd, inputs[fi.Name]);
            }
            cmd.execute();
            //Command cmd = (Command)command;
            //FieldInfo [] fieldInfos=cmd.GetType().GetFields();
            //Assembly.Load("TrackingCommands").
            ///cmd.execute();
            //Type[] types = Assembly.Load("TrackingCommands").GetTypes();
            //foreach (Type t in types)
            //{
            //    if (t.Name.Equals(commandName))
            //    {
            //        t.
            //        //Assembly.Load("").CreateInstance("TrackingCommands");
            //    }
            //}
        }
    }
}
