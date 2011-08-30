function getTrueOffsetLeft(ele) {
    var n = 0;
    var flag = 1;
    while (ele) {
        n += (ele.offsetLeft) || 0;
        ele = ele.offsetParent;
        // alert("scrollLeft is: "+ele.scrollLeft+" scrollWidth
        // is:"+ele.scrollWidth+" offsetWidth is:"+ele.offsetWidth+"offsetLeft
        // is"+ele.offsetLeft);
    }
    return n;
}

function getTrueOffsetTop(ele) {
    var n = 0;
    var flag = 1;
    while (ele) {
        n += (ele.offsetTop) || 0;
        ele = ele.offsetParent;
        // alert("scrollTop is: "+ele.scrollTop+" scrollHeight
        // is:"+ele.scrollHeight+" offsetHeight is:"+ele.offsetHeight+"offsetTop
        // is"+ele.offsetTop);
    }
    return n;
}
 function getRelativeX(e, element) {//鼠标在canvas实际面板坐标的计算

    // alert("clientX is:"+e.clientX+"canvasX
    // is:"+getTrueOffsetLeft(canvas)+"pageXOffset is:"+window.pageXOffset);
    var x = e.clientX - getTrueOffsetLeft(element) + 0.95 * window.pageXOffset;
    return x;
}
function getRelativeY(e,element) {
    // alert("clientY is:"+e.clientY+"canvasY
    // is:"+getTrueOffsetTop(canvas)+"pageYOffset is:"+window.pageYOffset);
    var y = e.clientY - getTrueOffsetTop(element) + 0.95 * window.pageYOffset;
    return y;
} // e.clientX,e.clientY鼠标位置.