var functionPoints;
var GetFunctionPointsURl = "http://localhost:5659/FunctionPointService.svc/";

function queryFunctionPointsForUser(userPrincipal, callback) {
    $.getJSON(GetFunctionPointsURl,
        {
            userPrincipalName: userPrincipal
        },
        function (data) {
            //alert(data.length);
            if (data != undefined) {
                //alert(data.length)
                callback(data);
            }
        });
}