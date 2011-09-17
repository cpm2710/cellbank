var dashboarditems = [{ "Name": "Current Projects" }, { "Name": "Start Project" }, { "Name": "Statistics"}];

function formatTableStyle() {
    $("#setrackingreport tr:odd").addClass("odd");
    $("#setrackingreport tr:not(.odd)").hide();
    $("#setrackingreport tr:first-child").show();
    $("#setrackingreport tr.odd").click(function () {
        $(this).next("tr").toggle();
        $(this).find(".arrow").toggleClass("up");
    });
}
function showDashboard() {
    var i = 0;
    for (i = 0; i < dashboarditems.length; i++) {
        var di = dashboarditems[i];
        var dItem = $("#dashboardItemTemplate").tmpl(di);

        dItem.appendTo("#dashboarditems");
    }
    hookLeftDashboard();
}
function showStartProject() {
    $("#showpanelmain").empty();
    alert("showStartProject");
}
function showStatistics() {
    $("#showpanelmain").empty();
    alert("showStatistics");
}
function hookLeftDashboard() {
    $("#dashboarditems").find(".dashboardItem").each(function (i) {
        $(this).data("index", i);
        $(this).bind("click", function (e) {
            var index = $(this).data("index");
            if (index == 0) {
                showCurrentProject();
            }
            else if (index == 1) {
                showStartProject();
            } else if (index == 2) {
                showStatistics();
            }
        });
    });
}
function showCurrentProject() {
    $("#showpanelmain").empty();
    $("#seTrackingMainTemplate").tmpl(null).appendTo("#showpanelmain");
    
    refreshTrackingProcess();
}
function initialDashboard() {
    formatTableStyle();
    showDashboard();
    showCurrentProject();
}
function refreshTrackingProcess() {
    getWorkFlowInstances(function (data) {
        var i = 0;
        for (i = 0; i < data.length; i++) {
            var trackingItem = $("#trackingProcessTemplate").tmpl(data[i]);
            trackingItem.appendTo("#report");
        }
    });
}