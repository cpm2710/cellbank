using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace TrackingStateMachine
{
    partial class Workflow1
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("", "")]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            this.setStateActivity1 = new System.Workflow.Activities.SetStateActivity();
            this.codeActivity1 = new System.Workflow.Activities.CodeActivity();
            this.stateInitializationActivity1 = new System.Workflow.Activities.StateInitializationActivity();
            this.stateActivity1 = new System.Workflow.Activities.StateActivity();
            this.Working = new System.Workflow.Activities.StateActivity();
            this.Started = new System.Workflow.Activities.StateActivity();
            // 
            // setStateActivity1
            // 
            this.setStateActivity1.Name = "setStateActivity1";
            this.setStateActivity1.TargetStateName = "Working";
            // 
            // codeActivity1
            // 
            this.codeActivity1.Name = "codeActivity1";
            this.codeActivity1.ExecuteCode += new System.EventHandler(this.codeActivity1_ExecuteCode);
            // 
            // stateInitializationActivity1
            // 
            this.stateInitializationActivity1.Activities.Add(this.codeActivity1);
            this.stateInitializationActivity1.Activities.Add(this.setStateActivity1);
            this.stateInitializationActivity1.Name = "stateInitializationActivity1";
            // 
            // stateActivity1
            // 
            this.stateActivity1.Name = "stateActivity1";
            // 
            // Working
            // 
            this.Working.Name = "Working";
            // 
            // Started
            // 
            this.Started.Activities.Add(this.stateInitializationActivity1);
            this.Started.Name = "Started";
            // 
            // Workflow1
            // 
            this.Activities.Add(this.Started);
            this.Activities.Add(this.Working);
            this.Activities.Add(this.stateActivity1);
            this.CompletedStateName = "stateActivity1";
            this.DynamicUpdateCondition = null;
            this.InitialStateName = "Started";
            this.Name = "Workflow1";
            this.CanModifyActivities = false;

        }

        #endregion

        private StateActivity Working;

        private StateActivity stateActivity1;

        private StateInitializationActivity stateInitializationActivity1;

        private CodeActivity codeActivity1;

        private SetStateActivity setStateActivity1;

        private StateActivity Started;









    }
}
