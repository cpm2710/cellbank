var authURL = "/AuthenticationService.svc/authentications";
function Dashboard365Login(authInstance, callback) {
    $.ajax(
    {
        url: authURL,

        data: authInstance,

        type: "POST",


        contentType: "application/json",
        dataType:"json",
        success: function (data) {
            $.cookie("Dashboard365TokenName",data,{expires: 7});
            callback(data); 
        } 
    });
}