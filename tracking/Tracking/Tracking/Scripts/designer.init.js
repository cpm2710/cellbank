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
    "Description": "Task",
    "ImgUrl": ".\/images\/flowdesigner\/task_empty.png",
    "Type": "task",
    "AlertCount": "0"
}, {
    "Description": "Decision",
    "ImgUrl": ".\/images\/flowdesigner\/ruledecision.png",
    "Type": "decision",
    "AlertCount": "0"
}, {
    "Description": "Transition",
    "ImgUrl": ".\/images\/flowdesigner\/transition.png",
    "Type": "transition",
    "AlertCount": "0"
}, {
    "Description": "Custom",
    "ImgUrl": ".\/images\/flowdesigner\/customize.png",
    "Type": "custom",
    "AlertCount": "0"
}];
var fmodel;
var canvas;
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
function handleClickEvent(e) {//处理鼠标事件
    var x = getRelativeX(e,canvas);
    var y = getRelativeY(e, canvas);
    //selectedNode = selectElement(x, y);
}

function mouseMove(e) {//对于鼠标移动时的处理
}
function mouseUp(e) {

}

function initialDesigner(){
    //fmodel = new flowmodel();
	//canvas = $("#designercanvas")[0];
	//canvas.width = $("#flowdesignerwrapper")[0].offsetWidth;
    //canvas.height = $("#flowdesignerwrapper")[0].offsetHeight;
    renderControllers();
    initialDesignerPanel();
}
function initialDesignerPanel() {
    $("#designercanvas").mousedown(handleClickEvent); //canvas面板的鼠标单击事件
    $("#designercanvas").mousemove(mouseMove); //canvas面板的拖拉事件
    $("#designercanvas").mouseup(mouseUp); //canvas面板的鼠标释放事件
}