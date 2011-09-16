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

    $("#setrackingreport tr:odd").addClass("odd");
    $("#setrackingreport tr:not(.odd)").hide();
    $("#setrackingreport tr:first-child").show();

    $("#setrackingreport tr.odd").click(function () {
        $(this).next("tr").toggle();
        $(this).find(".arrow").toggleClass("up");
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