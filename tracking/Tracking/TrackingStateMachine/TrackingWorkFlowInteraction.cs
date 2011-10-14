using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CommonResource;
using System.Collections.ObjectModel;
using System.Activities.Hosting;
using System.Threading;
using System.Activities;
using System.Xml;
namespace TrackingWorkFlow
{
    public class TrackingWorkFlowInteraction : IDisposable
    {
      
        public string startProcess(string wfName)
        {
            Assembly trackingWorkFlowAssembly = Assembly.Load("TrackingWorkFlow");
            Type[] types = trackingWorkFlowAssembly.GetTypes();
            foreach (Type t in types)
            {
                if (t.Name.Equals(wfName))
                {
                    ConstructorInfo ci = t.GetConstructor(new Type[] { });
                    string appId = null;
                    using (TrackingWorkFlow twf = (TrackingWorkFlow)ci.Invoke(new object[] { }))
                    {
                        twf.Start();
                        appId= twf.app.Id.ToString();
                    }
                    return appId;
                }
            }
            return null;
        }
        public void doCommand(CommandInfo commandInfo)
        {
            Assembly trackingWorkFlowAssembly = Assembly.Load("TrackingWorkFlow");
            Type[] types = trackingWorkFlowAssembly.GetTypes();
            foreach (Type t in types)
            {
                if (t.Name.Equals(commandInfo.WFName))
                {
                    Type tt = typeof(String);
                    ConstructorInfo ci = t.GetConstructor(new Type[] { tt });
                    using (TrackingWorkFlow twf = (TrackingWorkFlow)(ci.Invoke(new object[] { commandInfo.InstanceId })))
                    {
                        twf.AcceptCommand(commandInfo.CommandName);
                    }
                }
            }
        }
        public CandidateCommandList getCandidateCommands(string wfName, string instanceId)
        {
            Assembly trackingWorkFlowAssembly = Assembly.Load("TrackingWorkFlow");
            Type[] types = trackingWorkFlowAssembly.GetTypes();
            List<string> requiredInputs = new List<string>();
            foreach (Type t in types)
            {
                if (t.Name.Equals(wfName))
                {
                    Type tt = typeof(String);
                    ConstructorInfo ci = t.GetConstructor(new Type[] { tt });
                    List<string> bookmarkInfos = null;
                    using (TrackingWorkFlow twf = (TrackingWorkFlow)(ci.Invoke(new object[] { instanceId })))
                    {
                        bookmarkInfos = twf.GetCandidateCommand();                        
                    }
                    CandidateCommandList cmdList=null;
                    if (bookmarkInfos != null)
                    {
                        cmdList = new CandidateCommandList();
                        cmdList.AddRange(bookmarkInfos);
                        return cmdList;
                    }
                    else
                    {
                        return null;
                    }                    
                }
            }
            throw new WorkFlowNotFoundException("workFlow named "+wfName+" not found");
        }
        public WorkFlowDefinitionList getWorkFlowDefinitions()
        {
            WorkFlowDefinitionList l = new WorkFlowDefinitionList();
            Assembly trackingWorkFlowAssembly = Assembly.Load("TrackingWorkFlow");
            Type[] types = trackingWorkFlowAssembly.GetTypes();
            Type target = trackingWorkFlowAssembly.GetType("TrackingWorkFlow.TrackingWorkFlow");
            foreach (Type t in types)
            {
                if (t.IsSubclassOf(target))
                {
                    WorkFlowDefinition WFD = new WorkFlowDefinition();
                    WFD.WFName = t.Name;
                    l.Add(WFD);
                }
            }
            return l;
        }

        public StateMachineDefinition getStateMachineDefinition(string WFName)
        {
            StateMachineDefinition definition = new StateMachineDefinition();
            
            Assembly trackingWorkFlowAssembly = Assembly.Load("TrackingWorkFlow");
            Type[] types = trackingWorkFlowAssembly.GetTypes();
            Type target = trackingWorkFlowAssembly.GetType("TrackingWorkFlow.TrackingWorkFlow");
            foreach (Type type in types)
            {
                if (type.IsSubclassOf(target)&&type.Name.Equals(WFName,StringComparison.InvariantCultureIgnoreCase))
                {
                    Type tt = typeof(Boolean);
                    ConstructorInfo ci = type.GetConstructor(new Type[] { tt });
                    using (TrackingWorkFlow twf = (TrackingWorkFlow)(ci.Invoke(new object[] { false })))
                    {
                        Activity workflowDefinition = twf.app.WorkflowDefinition;
                        string stateMachineActivityName=twf.app.WorkflowDefinition.DisplayName;
                        
                        string[] resources = workflowDefinition.GetType().Assembly.GetManifestResourceNames();
                        string resourceName = null;
                        for (int i = 0; (i < resources.Length); i = (i + 1))
                        {
                            resourceName = resources[i];
                            if ((resourceName.Contains("."+stateMachineActivityName+".g.xaml") 
                                || resourceName.Equals(stateMachineActivityName+".g.xaml")))
                            {
                                break;
                            }
                        }
                        System.IO.Stream initializeXaml = typeof(SESampleProcess2).Assembly.GetManifestResourceStream(resourceName);
                        
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(initializeXaml);

                        XmlElement root = xDoc.DocumentElement;
                        string documentNameSpace = xDoc.NamespaceURI;
                        string nameSpace = root.NamespaceURI;
                        string xmlns = root.Attributes["xmlns"].Value;
                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xDoc.NameTable);
                        nsmgr.AddNamespace(root.Prefix, nameSpace);
                        nsmgr.AddNamespace("default", xmlns);
                        nsmgr.AddNamespace("x", "http://schemas.microsoft.com/winfx/2006/xaml");
                        nsmgr.AddNamespace("av","http://schemas.microsoft.com/winfx/2006/xaml/presentation");
                        nsmgr.AddNamespace("sap", "http://schemas.microsoft.com/netfx/2009/xaml/activities/presentation");

                        //av xmlns:av="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                        //xmlns:sap="http://schemas.microsoft.com/netfx/2009/xaml/activities/presentation"
                        XmlNode stateMachineNode = root.SelectSingleNode(".//default:StateMachine", nsmgr);

                        XmlNode initialStateNode = stateMachineNode.SelectSingleNode(".//default:StateMachine.InitialState/default:State", nsmgr);
                        definition.InitialState=initialStateNode.Attributes["DisplayName"].Value;
                        //definition.InitialState = initialStateNode;
                        XmlNodeList states = root.SelectNodes(".//default:State", nsmgr);

                        //XmlNodeList transitions = root.SelectNodes(".//default:Transition", nsmgr);

                        if (states != null)
                        {
                            foreach (XmlNode node in states)
                            {
                                State state = new State();
                                string displayName=node.Attributes["DisplayName"].Value;
                                state.Name = displayName;
                                definition.StateList.Add(state);
                                XmlNode point=node.SelectSingleNode("./sap:WorkflowViewStateService.ViewState/*/av:Point", nsmgr);
                                string shapeLocation = point.InnerText;

                                string[] location = shapeLocation.Split(',');
                                state.x = double.Parse(location[0]);
                                state.y = double.Parse(location[1]);
                                /*
                                 <sap:WorkflowViewStateService.ViewState>
            <scg3:Dictionary x:TypeArguments="x:String, x:Object">
              <av:Point x:Key="ShapeLocation">33,79.5</av:Point>
              <av:Size x:Key="ShapeSize">153,90</av:Size>
              <x:Boolean x:Key="IsPinned">False</x:Boolean>
              <x:Double x:Key="StateContainerWidth">139</x:Double>
              <x:Double x:Key="StateContainerHeight">
                54.326666666666654</x:Double>
              </scg3:Dictionary>
          </sap:WorkflowViewStateService.ViewState>*/
                                XmlNodeList transitions = node.SelectNodes("./default:State.Transitions/default:Transition", nsmgr);

                                if (transitions != null)
                                {
                                    foreach (XmlNode tNode in transitions)
                                    {
                                        Transition t = new Transition();
                                        t.Name=tNode.Attributes["DisplayName"].Value;
                                        t.From = state.Name;
                                        XmlNode transitionTo=tNode.SelectSingleNode("./default:Transition.To",nsmgr);

                                        XmlNode transitionToState = transitionTo.SelectSingleNode("./default:State", nsmgr);

                                        if (transitionToState != null)
                                        {
                                            string toStateName = transitionToState.Attributes["DisplayName"].Value;
                                            t.To = toStateName;
                                            
                                        }
                                        else
                                        {
                                            XmlNode referenceNode = transitionTo.SelectSingleNode("./x:Reference", nsmgr);
                                            foreach (XmlNode stateNode in states)
                                            {
                                                string xName = stateNode.Attributes["x:Name"].Value;
                                                if (xName.Equals(referenceNode.InnerText, StringComparison.InvariantCultureIgnoreCase))
                                                {
                                                    string toStateName = stateNode.Attributes["DisplayName"].Value;
                                                    t.To = toStateName;
                                                }
                                            }
                                            //transitionToState = tNode.SelectSingleNode("./default:Transition.To/default:State", nsmgr);
                                        }
                                        definition.TransitionList.Add(t);
                                    }
                                }                                
                            }
                        }
                        //if (transitions != null)
                        //{
                        //    foreach (XmlNode node in transitions)
                        //    {
                        //        definition.TransitionList.Add(new Transition());
                        //    }
                        //}
                    }
                    //WorkFlowDefinition WFD = new WorkFlowDefinition();
                    //WFD.WFName = t.Name;
                }
            }
            return definition;
        }
        public void Dispose()
        {
        }
    }
}
