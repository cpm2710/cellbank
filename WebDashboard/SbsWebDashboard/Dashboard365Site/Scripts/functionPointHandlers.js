var functionPoints;
var GetFunctionPointsURl = "/FunctionPointService.svc/fpbyuserandaddin/";
function modifyHeader(XMLHttpRequest) {
    var token = $.cookie("Dashboard365TokenName");
    XMLHttpRequest.setRequestHeader("Dashboard365TokenName", token);
};
function queryFunctionPointsForUser(userPrincipal, callback) {
    $.ajax({
        type: "GET",
        url: GetFunctionPointsURl + userPrincipal,
        beforeSend:modifyHeader,
        success: function (data) {
            callback(data);
        }
        
    });
//    $.getJSON(GetFunctionPointsURl+userPrincipal,
//        function (data) {
//            //alert(data.length);
//            if (data != undefined) {
//                //alert(data.length)
//                callback(data);
//            }
//        });
}
function queryFunctionPointsForUserAndAddIn(userPrincipal, addIn, callback) {
    $.ajax({
        type: "GET",
        url: GetFunctionPointsURl + userPrincipal + "/" + addIn,
        beforeSend: modifyHeader,
        success: function (data) {
            callback(data);
        }

    });
//    $.getJSON(GetFunctionPointsURl + userPrincipal+"/"+addIn,
//        function (data) {
//            //alert(data.length);
//            if (data != undefined) {
//                //alert(data.length)
//                callback(data);
//            }
//        });
}