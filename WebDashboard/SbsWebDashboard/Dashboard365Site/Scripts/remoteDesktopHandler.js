//var desktopURL = "/RemoteDesktopService.svc/desktops";
var canvas = null;
var remotedMachineName = null;
function getTrueOffsetLeft(ele) {
    var n = 0;
    while (ele) {
        n += ele.offsetLeft || 0;
        ele = ele.offsetParent;
    }
    return n;
}

function getTrueOffsetTop(ele) {
    var n = 0;
    while (ele) {
        n += ele.offsetTop || 0;
        ele = ele.offsetParent;
    }
    return n;
}
function getRelativeX(e) {
    var x = e.clientX - getTrueOffsetLeft(canvas) + window.pageXOffset;
    return x;
}
function getRelativeY(e) {
    var y = e.clientY - getTrueOffsetTop(canvas) + window.pageYOffset;
    return y;
}


function refreshRemoteDesktop() {
    //指定图像源
    $.ajax({
        type: "GET",
        url: "http://" + "localhost" + ":3390/remotedesktops",
        beforeSend: modifyHeader,
        success: function (data) {
            var ctx = canvas.getContext("2d");
            //创建图像对象
            img = new Image();
            //图像被装入后触发
            img.onload = function () {
                ctx.drawImage(img, 0, 0);
            }
            img.onerror = function () {
                alert("imageerror");
            }
            var imageSrc = "data:image/png;base64," + data.DesktopBase64;
            img.src = imageSrc;
            //callback(data);
        },
        failed: function (data) {
            alert(data);
        }
    });
}

function handleClickEvent(e) {
    var x = getRelativeX(e);
    var y = getRelativeY(e);
    refreshRemoteDesktop();
}
function mouseMove(e) {
    {
        var x = getRelativeX(e);
        var y = getRelativeY(e);        
    }
}
function mouseUp(e) {
    var x = getRelativeX(e);
    var y = getRelativeY(e); 
}
function registerListener() {
    $("#remotedesktopcanvas").mousedown(handleClickEvent);
    $("#remotedesktopcanvas").mousemove(mouseMove);
    $("#remotedesktopcanvas").mouseup(mouseUp);
}
function RemoteMachine(machineName) {
    //获取画布对象
    canvas = $("#remotedesktopcanvas")[0];
    remotedMachineName = machineName;
    registerListener();    
}
function Clear() {
    //清除画布
    ctx = document.getElementById("canvas1").getContext("2d");
    ctx.clearRect(0, 0, 250, 300);
} 