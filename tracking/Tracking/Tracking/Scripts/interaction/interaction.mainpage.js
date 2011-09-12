var dashboarditems = [{
    "Name":"Current Projects"
}];
function initialDashboard() {

    $.getTmplAsync("./templates/dashboardItemTemplate.html").done(function () {
        var i = 0;
        for (i = 0; i < dashboarditems.length; i++) {
            var di = dashboarditems[i];
            $dItem = $("#dashboardItemTemplate").tmpl(di);
            $dItem.appendTo("#dashboarditems");
        }
    });
   
}