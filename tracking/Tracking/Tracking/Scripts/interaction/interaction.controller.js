function initializeInteraction(commandName) {
    $.getTmplSync("./templates/interactionTemplate.html").done(function () {
        $interactionPanel = $("#interactionTemplate").tmpl(null);

        $.getTmplSync("./templates/inputFieldTemplate.html").done(function () {
            GetParameters(commandName, function (parameters) {
                var i = 0;
                for (i = 0; i < parameters.length; i++) {
                    var inputField = $("#inputFieldTemplate").tmpl(parameters[i]);
                    inputField.appendTo($interactionPanel);
                }
                showInLightbox($interactionPanel);
            });
        });

        //$usersModelT.appendTo(parent.model);
        //self.hide();
    });
    
}
function validateInteraction(commandName) {
    
}
//function commitInteraction(commandInfo) {
    
//}
