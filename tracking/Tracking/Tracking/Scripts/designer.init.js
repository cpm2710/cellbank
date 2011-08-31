var functionPoints = [{
    "Description": "Select",
    "ImgUrl": ".\/images\/flowdesigner\/arrow.png",
    "Type": "select",
    "AlertCount": "0"
}, {
    "Description": "Start",
    "ImgUrl": ".\/images\/flowdesigner\/start_event_empty.png",
    "Type": "start",
    "AlertCount": "0"
}, {
    "Description": "End",
    "ImgUrl": ".\/images\/flowdesigner\/end_event_terminate.png",
    "Type": "end",
    "AlertCount": "0"
}, {
    "Description": "State",
    "ImgUrl": ".\/images\/flowdesigner\/task_empty.png",
    "Type": "state",
    "AlertCount": "0"
},{
    "Description": "Transition",
    "ImgUrl": ".\/images\/flowdesigner\/transition.png",
    "Type": "transition",
    "AlertCount": "0"
}, {
    "Description": "Event",
    "ImgUrl": ".\/images\/flowdesigner\/customize.png",
    "Type": "event",
    "AlertCount": "0"
}];
var fmodel;
var canvas;
var selectedControllerType="select";
function renderControllers() {//左边工具栏的一些初始化，以及对应的click更改当前操作状态
    $.getTmpl("./templates/functionTemplate.htm").done(function () {
        for (var i in functionPoints) {
            var fp = functionPoints[i];
            $ftmpl = $("#ftemplate").tmpl(fp);
            $ftmpl.bind("click", function () {
                $ftile = $(".ftile");
                $ftile.removeClass("selectedcontroller");
                $(this).addClass("selectedcontroller");
                var tmplItem = $.tmplItem($(this));
                //如果上一个状态是连线，清除连线的一些状态
                if (selectedControllerType != null && selectedControllerType == "transition") {
                    transitionFrom = null;
                    fmodel.tempTransition = null;
                    repaint();
                }
                selectedControllerType = tmplItem.data.Type;
            });
            $ftmpl.appendTo("#designercontrollers");
        }
    });
}

function resizeDesignerCanvas() {
    canvas.width = $("#designerpanel")[0].offsetWidth;
    canvas.height = $("#designerpanel")[0].offsetHeight;

}

function initialDesigner(){
    fmodel = new flowmodel();
	canvas = $("#designercanvas")[0];
	canvas.width = $("#designerpanel")[0].offsetWidth;
	canvas.height = $("#designerpanel")[0].offsetHeight;
    renderControllers();
    initialDesignerPanel();
    $(window).resize(function () {
        resizeDesignerCanvas();
    });
    window.addEventListener('keydown',doKeyDown,true);
}
function initialDesignerPanel() {
    $("#designercanvas").mousedown(handleClickEvent); //canvas面板的鼠标单击事件
    $("#designercanvas").mousemove(mouseMove); //canvas面板的拖拉事件
    $("#designercanvas").mouseup(mouseUp); //canvas面板的鼠标释放事件
}