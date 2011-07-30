var desktopURL = "/RemoteDesktopService.svc/desktopslices";
var canvas = null;
var remotedMachineName = null;
var img = null;
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
        url: desktopURL + "/" + remotedMachineName,
        beforeSend: modifyHeader,
        success: function (data) {
            var imageSlices = eval(data);
            var i = 0;
            for (i = 0; i < imageSlices.length; i++) {
                var islice=imageSlices[i];
                var ctx = canvas.getContext("2d");
                //            //创建图像对象
                var img = new Image();
                //            //图像被装入后触发
                img.onload = function () {
                    ctx.drawImage(img, islice.X, islice.Y);
                }
                img.onerror = function () {
                    alert("imageerror");
                }
                var imageSrc = "data:image/png;base64," + islice.DesktopBase64;
                img.src = imageSrc;
            }

            setTimeout(refreshRemoteDesktop, 1);
            //            var ctx = canvas.getContext("2d");
            //            //创建图像对象
            //            img = new Image();
            //            //图像被装入后触发
            //            img.onload = function () {
            //                ctx.drawImage(img, 0, 0);
            //            }
            //            img.onerror = function () {
            //                alert("imageerror");
            //            }
            //            var imageSrc = "data:image/png;base64," + data.DesktopBase64;
            //            img.src = imageSrc;
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
    var height = img.height;
    var width = img.width;
    //refreshRemoteDesktop();
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
    refreshRemoteDesktop();
}
//function Clear() {
//    //清除画布
//    ctx = document.getElementById("canvas1").getContext("2d");
//    ctx.clearRect(0, 0, 250, 300);
//} 