using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace TrackingStateMachine
{
    public sealed partial class Workflow1 : StateMachineWorkflowActivity
    {
        public Workflow1()
        {
            InitializeComponent();
        }

        private void codeActivity1_ExecuteCode(object sender, EventArgs e)
        {

        }
    }

}
