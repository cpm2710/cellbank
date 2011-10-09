
function showProcessGraphViewer() {
    fmodel = new flowmodel();
    GetTrackingWorkFlowDefinition(function (workFlowDefinitions) {
        var i = 0;
        for (i = 0; i < workFlowDefinitions.length; i++) {
            //var workFlowDefinition = {WFName:};
            $("#workFlowDefinitionTemplate").tmpl(workFlowDefinitions[i]).appendTo("#workflowlist");
        }
        $(".show_process_item").bind("click", function () {

            var wfName = $(this).tmplItem().data.WFName;
            GetStateMachineDefinition(wfName, function (data) {
                fmodel = new flowmodel();
                //fmodel.
                var states = data.StateList;
                var transitions = data.TransitionList;
                var i = 0;
                for (i = 0; i < states.length; i++) {
                    var c = new controller(states[i].Name, "state");
                    c.x = states[i].x;
                    c.y = states[i].y;
                    fmodel.states.put(states[i].Name, c);
                }
                var j = 0;
                //var statesInModel = fmodel.states.values();
                for (i = 0; i < transitions.length; i++) {
                    //                    for (j = 0; j < statesInModel.length; j++) {
                    //                        
                    //                    }
                    var stateFrom = fmodel.states.get(transitions[i].From);
                    var stateTo = fmodel.states.get(transitions[i].To);
                    var t = new transition(stateFrom, stateTo, "");
                    fmodel.transitions.put(t.name, t);
                }
                repaint();
            });
        });
    });
//    var name = "State" + fmodel.states.values().length;
//    var c = new controller(name, selectedControllerType)
//    c.x = x - (c.width / 2);
//    c.y = y - (c.height / 2);
//    fmodel.states.put(c.name, c);

    canvas = $("#designercanvas")[0];
    canvas.width = $("#designerpanel")[0].offsetWidth;
    canvas.height = $("#designerpanel")[0].offsetHeight;
    initialDesignerPanel();
    $(window).resize(function () {
        resizeDesignerCanvas();
    });
}