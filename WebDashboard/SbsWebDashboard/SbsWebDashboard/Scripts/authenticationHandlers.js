var authURL = "http:\/\/localhost:5659\/AuthenticationService.svc\/authentications";
function Dashboard365Login(authInstance, callback) {
    $.ajax(
    {
        type:"POST",
        url:authURL,
        dataType:"json",
        data: authInstance,
        contentType:"application/json",
        success:function (data) { alert(data); } 
    });
}