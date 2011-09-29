var dashboarditems = [{ "Name": "Current Projects" }, { "Name": "Start Project" }, { "Name": "Statistics" }, {"Name":"Process Graph"}];

function formatTableStyle() {
    $(".reporttable tr:odd").addClass("odd");
    $(".reporttable tr:not(.odd)").hide();
    $(".reporttable tr:first-child").show();
    $(".reporttable tr.odd").click(function () {
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
function refreshStartProjects() {
    GetTrackingWorkFlowDefinition(function (workFlowDefinitions) {
        var i = 0;
        for (i = 0; i < workFlowDefinitions.length; i++) {
            var workFlowDefItem = $("#workFlowDefinitionItemTemplate").tmpl(workFlowDefinitions[i]);
            workFlowDefItem.bind("click", function (e) {
                var trs = $("#sestartprojectreport").find("tr");
                trs.removeClass("selected");
                $(this).addClass("selected");
                /*var abc=$(this).tmplItem();
                console.dir(abc);
                alert(abc.data.WFName);*/
            });
            workFlowDefItem.appendTo("#sestartprojectreport");
        }
    });
    $("#sestartprojectactions > .startprojectaction").bind("click", function (e) {
        //        var selectedWFD = $("#sestartprojectreport").find("tr.selected");
        //        var wfname = selectedWFD.tmplItem().data.WFName;
        //        var commandInfo = { "WFName": wfname };
        initializeProcessStartInteraction();
        //        startWorkFlow(commandInfo, function (data) {
        //            alert(data);
        //        });
    });
    $("#sestartprojectactions > .suspendprojectaction").bind("click", function (e) {

    });
    $("#sestartprojectactions > .attachbugtemplate").bind("click", function (e) {

    }); 
}
function showStartProject() {
    $("#showpanelmain").empty();
    $("#startProjectMainTemplate").tmpl(null).appendTo("#showpanelmain");
    refreshStartProjects();
    formatTableStyle();
}
function showStatistics() {
    $("#showpanelmain").empty();
    $("#statisticsMainTemplate").tmpl(null).appendTo("#showpanelmain");
}
function showProcessGraph() {
    $("#showpanelmain").empty();
    $("#processGraphMainTemplate").tmpl(null).appendTo("#showpanelmain"); 
}
function hookLeftDashboard() {
    $("#dashboarditems").find(".dashboardItem").each(function (i) {
        $(this).data("index", i);
        $(this).bind("click", function (e) {
            $(this).parent().find(".dashboardItem").removeClass("selected");
            $(this).addClass("selected");
            var index = $(this).data("index");
            if (index == 0) {
                showCurrentProject();
            }
            else if (index == 1) {
                showStartProject();
            } else if (index == 2) {
                showStatistics();
            } else if (index == 3) {
                showProcessGraph();
            }
        });
    });
}
function showCurrentProject() {
    $("#showpanelmain").empty();
    $("#seTrackingMainTemplate").tmpl(null).appendTo("#showpanelmain");
    
    refreshTrackingProcess();
}

function refreshTrackingProcess() {
    getWorkFlowInstances(function (data) {
        var i = 0;
        for (i = 0; i < data.length; i++) {
            var trackingItem = $("#trackingProcessItemTemplate").tmpl(data[i]);
            trackingItem.bind("click", function (e) {
                var trs = $("#setrackingreport").find("tr");
                trs.removeClass("selected");
                $(this).addClass("selected");
                var selectedTrackingProcess = $(this).tmplItem();
                var instanceId = selectedTrackingProcess.data.Id;
                $("#setrackingactions").empty();
                getWorkFlowInstance(instanceId, function (data) {
                    var candCommandList = data.CandidateCommandList;
                    for (i = 0; i < candCommandList.length; i++) {
                        var candidateCmd = { CommandName: candCommandList[i] };
                        var candiCmdJObj = $("#candidateCommandTemplate").tmpl(candidateCmd);
                        candiCmdJObj.appendTo("#setrackingactions");
                        candiCmdJObj.bind("click", function (e) {
                            initializeInteraction($(this).tmplItem().data.CommandName);
                        });
                    }
                });
            });
            trackingItem.appendTo("#setrackingreport");
        }
    });
}

function initialDashboard() {
    formatTableStyle();
    showDashboard();
    showCurrentProject();
}