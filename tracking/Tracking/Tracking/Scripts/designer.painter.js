var ctxall;
function repaint() { // 閲嶇粯
    ctxall = canvas.getContext("2d");
    ctxall.clearRect(0, 0, canvas.width, canvas.height);
    paint(fmodel, ctxall);
}
function paintDashedLine(sx, sy, ex, ey, ctx) {//描绘选中状态后的线
    var linesection = 4;
    // var length=Math.sqrt((sx-ex)*(sx-ex)+(sy-ey)*(sy-ey));
    var len = Math.abs(ex - sx + ey - sy);
    var n = len / linesection;
    var xinc = (ex - sx) / n;
    var yinc = (ey - sy) / n;
    var px = sx;
    var py = sy;

    for (j = 0; j < n; j++) {
        px += xinc;
        py += yinc;
        if (j % 2 == 0) {
            ctx.moveTo(px, py);
        } else {
            ctx.lineTo(px, py);
        }
    }
}
function MarkCurNode(x, y,width,height, ctx) {// 每个节点周围描绘8个点
    // 绘制虚线框
    ctx.lineWidth = 1;
    paintDashedLine(x, y, x + width, y, ctx);
    paintDashedLine(x + width, y, x + width, y
					+ height, ctx);
    paintDashedLine(x, y, x, y + height, ctx);
    paintDashedLine(x, y + height, x + width, y
					+ height, ctx);
    ctx.stroke();
}

function paint(model, ctx) {
    ctx.canvas.width = canvas.width;
    ctx.canvas.height = canvas.height;
    var states = model.states.values();
    for (i = 0; i < states.length; i++) {
        var c = states[i];
        var image = new Image();
        image.src = states[i].ImgUrl;
        ctx.drawImage(image, c.x, c.y, c.width, c.height);
        ctx.font = "12pt Arial"; // 设置字体，大小
        ctx.textBaseline = "top"; // 设置文字垂直对齐方式
        ctx.textAlign = "center"; // 设置文字水平对齐方式
        var strtext = c.name;
        ctx.fillText(strtext, (c.x + c.width / 2), (c.y
						+ c.height + 5));
        if (c.selected == true) {
            MarkCurNode(c.x, c.y,c.width,c.height, ctx);
        }
    }
    ctx.stroke();
}