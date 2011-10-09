/*
{"CommandName":"shit","InstanceId":"123","ParameterList":[{"Name":"pname","Type"
:"string","Value":"value1"}],"WFName":"wf1"}

*/

function initializeProcessStartInteraction() {
    var commandName = "ProcessStart";
    $interactionPanel = $("#interactionTemplate").tmpl(null);
    $interactionFields = $interactionPanel.find(".interactionfields");
    var selectedTR = $("#sestartprojectreport").find("tr.selected");
    var workFlowName = (selectedTR.find("td:eq(0)")[0].innerHTML + "").trim();
    GetParameters(commandName, function (parameters) {
        var i = 0;
        for (i = 0; i < parameters.length; i++) {
            if (parameters[i].Values.length > 0) {
                var inputField = $("#selectFieldTemplate").tmpl(parameters[i]);
                inputField.appendTo($interactionFields);
                var j = 0;
                var options = "";
                var parameter = parameters[i];
                for (j = 0; j < parameter.Values.length; j++) {
                    options = options + "<option value=\"" + parameter.Values[j] + "\">" + parameter.Values[j] + "</option>"
                }
                inputField.find(".select_field_input").append(options);
            } else {
                var inputField = $("#inputFieldTemplate").tmpl(parameters[i]);
                inputField.appendTo($interactionFields);
            }
        }
        $interactionPanel.find("#submit").bind("click", function (e) {
            var inputFields = "ParameterList:[";
            $interactionFields.find(".inputField").each(function () {
                $ths = $(this);

                var key = $ths.find("p")[0].innerHTML;
                var value = $ths.find("input").val();

                inputFields += "{Name:\"" + key + "\",Type:\"string\",Value:\"" + value + "\"},";

            });
            inputFields = inputFields.substr(0, inputFields.length - 1) + "]";

            var commandInfoStr = "{WFName:\"" + workFlowName + "\",CommandName:\"" + commandName + "\"," + inputFields + "}";
            var commandInfo = eval("(" + commandInfoStr + ")");
            startWorkFlow(commandInfo, function (data) {
                alert(data);
            });
        });

        $interactionPanel.find("#cancel").bind("click", function (e) {

        });
        showInLightbox($interactionPanel);
    });
}

function initializeInteraction(commandName) {
   
        $interactionPanel = $("#interactionTemplate").tmpl(null);
        $interactionFields = $interactionPanel.find(".interactionfields");
        var selectedTR = $("#setrackingreport").find("tr.selected");
        var selectedIdTd = selectedTR.find("td:eq(0)");
        var instanceId = (selectedIdTd[0].innerHTML+"").trim();
        var workFlowName = (selectedTR.find("td:eq(1)")[0].innerHTML+"").trim();
        GetParameters(commandName, function (parameters) {
            var i = 0;
            for (i = 0; i < parameters.length; i++) {
                var inputField = $("#inputFieldTemplate").tmpl(parameters[i]);
                inputField.appendTo($interactionFields);
            }
            $interactionPanel.find("#submit").bind("click", function (e) {
                var inputFields = "ParameterList:[";
                $interactionFields.find(".inputField").each(function () {
                    $ths = $(this);

                    var key = $ths.find("p")[0].innerHTML;
                    var value = $ths.find("input").val();

                    inputFields += "{Name:\"" + key + "\",Type:\"string\",Value:\"" + value + "\"},";

                });
                inputFields = inputFields.substr(0, inputFields.length - 1) + "]";

                var commandInfoStr = "{WFName:\""+workFlowName+"\",InstanceId:\""+instanceId+"\",CommandName:\"" + commandName + "\"," + inputFields + "}";
                var commandInfo = eval("(" + commandInfoStr + ")");
                doCommand(commandInfo, function (data) {
                    
                });
            });

            $interactionPanel.find("#cancel").bind("click", function (e) {
                
            });
            showInLightbox($interactionPanel);
        });
}
function validateInteraction(commandName) {
    
}
//function commitInteraction(commandInfo) {
    
//}
