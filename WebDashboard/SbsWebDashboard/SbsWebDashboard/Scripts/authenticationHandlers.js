var authURL = "http://localhost:5659/AuthenticationService.svc/authentications";
function authenticate(authInstance, callback) {
    $.ajax({
        type: "POST",
        url: authURL,
        data: authInstance,
        dataType: "json",
        contentType: "application/json",
        success: callback
    })
}