using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CommonResource
{
    [DataContract(Namespace = "")]
    public class Shape
    {
        public Shape()
        {
            this.Name = "fakeName";
        }
        [DataMember]
        public string Name;
        [DataMember]
        public double x;
        [DataMember]
        public double y;
    }
    [DataContract(Namespace = "")]
    public class State:Shape
    {
        public State()
        {
            this.Name = "fakeName";
        }
    }
    [CollectionDataContract(Name = "StateList", Namespace = "")]
    public class StateList : List<State>
    {
    }
    [DataContract(Namespace = "")]
    public class Transition
    {
        public Transition()
        {
            this.Name = "fakeName";
            this.From = "fakeFrom";
            this.To = "fakeTo";
        }
        [DataMember]
        public string Name;
        [DataMember]
        public string From;
        [DataMember]
        public string To;
    }
    [CollectionDataContract(Name = "TransitionList", Namespace = "")]
    public class TransitionList : List<Transition>
    {
    }
    [DataContract(Namespace = "")]
    public class StateMachineDefinition
    {
        public StateMachineDefinition()
        {
            this.StateList = new StateList();
            this.TransitionList = new TransitionList();
        }
        [DataMember]
        public StateList StateList;
        [DataMember]
        public TransitionList TransitionList;
    }
}
