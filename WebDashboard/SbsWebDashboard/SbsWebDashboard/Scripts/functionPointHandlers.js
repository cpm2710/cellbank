var functionPoints;
var GetFunctionPointsURl = "http://localhost:5659/FunctionPointService.svc/mainfpbyuser/";

function queryFunctionPointsForUser(userPrincipal, callback) {
    $.getJSON(GetFunctionPointsURl+userPrincipal,
        {
           
        },
        function (data) {
            //alert(data.length);
            if (data != undefined) {
                //alert(data.length)
                callback(data);
            }
        });
}