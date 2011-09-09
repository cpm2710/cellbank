function initializeInteraction(eventName) {
    
    $.getTmplSync("./templates/interactionTemplate.html").done(function () {
        $usersModelT = $("#interactionTemplate").tmpl(null);
        $usersModelT.appendTo(parent.model);
        //self.hide();
    });
}
function validateInteraction(eventName) {
    
}
function commitInteraction(eventName) {
    
}