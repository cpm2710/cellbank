var dashboarditems = [{
    "Name":"Current Projects"
}];

function initialDashboard() {
    $.getTmplAsync("./templates/dashboardItemTemplate.html").done(function () {
        var i = 0;
        for (i = 0; i < dashboarditems.length; i++) {
            var di = dashboarditems[i];
            $dItem = $("#dashboardItemTemplate").tmpl(di);
            $dItem.bind("click", function () {
                
             });
            $dItem.appendTo("#dashboarditems");
        }
    });
    refreshTrackingProcess();
}
function refreshTrackingProcess() {
    getWorkFlowInstances(function (data) {
        var i = 0;
        for (i = 0; i < data.length; i++) {
            $.getTmplAsync("./templates/trackingProcessTemplate.html").done(function () {
                var trackingItem = $("#trackingProcessTemplate").tmpl(data[i]);
                trackingItem.appendTo("#report");
            });
        }
    });
}