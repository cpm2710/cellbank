function initializeInteraction(commandName) {
    $.getTmplSync("./templates/interactionTemplate.html").done(function () {
        $usersModelT = $("#interactionTemplate").tmpl(null);
        showInLightbox($usersModelT);
        //$usersModelT.appendTo(parent.model);
        //self.hide();
    });
    //GetParameters(commandName, function (parameters) {
        
    //});
}
function validateInteraction(commandName) {
    
}
//function commitInteraction(commandInfo) {
    
//}
