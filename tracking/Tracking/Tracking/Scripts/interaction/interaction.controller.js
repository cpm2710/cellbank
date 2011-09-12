/*
{"CommandName":"shit","InstanceId":"123","ParameterList":[{"Name":"pname","Type"
:"string","Value":"value1"}],"WFName":"wf1"}

*/
function initializeInteraction(commandName) {
    $.getTmplSync("./templates/interactionTemplate.html").done(function () {
        $interactionPanel = $("#interactionTemplate").tmpl(null);
        $interactionFields = $interactionPanel.find(".interactionfields");
        $.getTmplSync("./templates/inputFieldTemplate.html").done(function () {
            GetParameters(commandName, function (parameters) {
                var i = 0;
                for (i = 0; i < parameters.length; i++) {
                    var inputField = $("#inputFieldTemplate").tmpl(parameters[i]);
                    inputField.appendTo($interactionFields);
                }
                $interactionPanel.find("#submit").bind("click", function (e) {
                    var inputFields = "{";
                    $interactionFields.find(".inputField").each(function () {
                        $ths = $(this);
                        var key = $ths.find("p")[0].innerHTML;
                        var value = $ths.find("input").val();
                        inputFields += key + ":\"" + value + "\",";
                    });
                    inputFields = inputFields.substr(0, inputFields.length - 1) + "}";
                    var parameters = eval("(" + inputFields + ")");

                });

                $interactionPanel.find("#cancel").bind("click", function (e) {
                    alert("shit");
                });
                showInLightbox($interactionPanel);
            });
        });
    });
}
function validateInteraction(commandName) {
    
}
//function commitInteraction(commandInfo) {
    
//}
