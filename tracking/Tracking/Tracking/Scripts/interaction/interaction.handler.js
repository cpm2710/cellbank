var trakingServiceURL = "./TrackingService.svc/";
function getWorkFlowInstances(callback) {
    $.ajax({
        type: "GET",
        url: trakingServiceURL+"workflowinstances",
        contentType: "application/json",
        dataType: "json",
        cache: false,
        success: function (data) {
            //$.cookie(data.TokenName, data.Token, { expires: 7 });
            //alert(data);
            callback(data);
        },
        error: function (data) {
            //callback(false, data);
        }
    });
}
function getWorkFlowInstance(instanceId,callback) {
    $.ajax({
        type: "GET",
        url: trakingServiceURL + "workflowinstances/" + instanceId,
        contentType: "application/json",
        dataType: "json",
        cache: false,
        success: function (data) {            
            //$.cookie(data.TokenName, data.Token, { expires: 7 });
            //alert(data);
            callback(data);
        },
        error: function (data) {
            //callback(false, data);
        }
    });
}
function GetParameters(commandName,callback) {
    $.ajax({
        type: "GET",
        url: trakingServiceURL + "parameters/" + commandName,
        contentType: "application/json",
        //data: jsonStr,
        dataType: "json",
        cache: false,
        success: function (data) {
            //$.cookie(data.TokenName, data.Token, { expires: 7 });
            //alert(data);
            callback(data);
        },
        error: function (data) {
            //callback(false, data);
        }
    });
}
function GetTrackingWorkFlowDefinition(callback) {
    $.ajax({
        type: "GET",
        url: trakingServiceURL + "workflowdefinitions",
        contentType: "application/json",
        //data: jsonStr,
        dataType: "json",
        cache: false,
        success: function (data) {
            //$.cookie(data.TokenName, data.Token, { expires: 7 });
            //alert(data);
            callback(data);
        },
        error: function (data) {
            //callback(false, data);
        }
    });
}
function startWorkFlow(commandInfo, callback) {
    var dataString = JSON.stringify(commandInfo);
    $.ajax({
        url: trakingServiceURL + "workflowinstances",
        data: dataString,
        type: "POST",        
        contentType: "application/json",        
        dataType: "json",
        cache: false,
        success: function (data) {
            //$.cookie(data.TokenName, data.Token, { expires: 7 });
            //alert(data);
            callback(data);
        },
        error: function (data) {
            //callback(false, data);
        }
    });
}
function doCommand(commandInfo, callback) {
    $.ajax({
        type: "POST",
        url: trakingServiceURL + "commands",
        contentType: "application/json",
        data: commandInfo,
        dataType: "json",
        cache: false,
        success: function (data) {
            //$.cookie(data.TokenName, data.Token, { expires: 7 });
            //alert(data);
            callback(data);
        },
        error: function (data) {
            //callback(false, data);
        }
    });
}