var ctxall;
var arrow_length = 8;
var beta = Math.PI / 9; // 初定箭头角度为40度，故一半为20度
function repaint() { //
    ctxall = canvas.getContext("2d");
    ctxall.clearRect(0, 0, canvas.width, canvas.height);
    paint(fmodel, ctxall);
}

function drawLine(startx, starty, endx, endy, width, height, ctx) { // 水平和竖直的情况下会出现tan_alpha无限大或者为0而不能画出线。
    var pianYiLiang; // 为了算出两中点之间连线没有被模型屏蔽的部分

    var tan_alpha;
    var flagx;
    var flagy;
    if (endx == startx) {
        tan_alpha = 10000;
        flagx = 0;
        flagy = (endy - starty) / Math.abs(endy - starty);
    } else if (endy == starty) {
        flagx = (endx - startx) / Math.abs(endx - startx);
        flagy = 0;
        tan_alpha = 0.0001;
    } else {
        tan_alpha = Math.abs(endy - starty) / Math.abs(endx - startx);
        flagx = (endx - startx) / Math.abs(endx - startx);
        flagy = (endy - starty) / Math.abs(endy - starty);
    }
    // var tran_alpha = Math.abs(endy - starty) / Math.abs(endx - startx);
    // var flagx = (endx - startx) / Math.abs(endx - startx);
    // var flagy = (endy - starty) / Math.abs(endy - starty);
    if (tan_alpha <= 1) {
        pianYiLiang = tan_alpha * width / 2; // 如果tanalpha为无穷大
        ctx.strokeStyle = "rgb(0, 0, 0)";
        ctx.lineWidth = 1;
        ctx.beginPath();
        ctx.moveTo(startx + flagx * width / 2, starty + flagy * pianYiLiang);
        ctx.lineTo(endx - flagx * width / 2, endy - flagy * pianYiLiang);
        ctx.stroke();
        drawArrow(startx, starty, endx - flagx * width / 2, endy - flagy
						* pianYiLiang, ctx); // 画箭头
    } else if (tan_alpha > 1) {
        pianYiLiang = height / (2 * tan_alpha); // 如果tan_alpha为0
        ctx.strokeStyle = "rgb(0, 0, 0)";
        ctx.lineWidth = 1;
        ctx.beginPath();
        ctx.moveTo(startx + flagx * pianYiLiang, starty + flagy * height / 2);
        ctx.lineTo(endx - flagx * pianYiLiang, endy - flagy * height / 2);
        ctx.stroke();
        drawArrow(startx, starty, endx - flagx * pianYiLiang, endy - flagy
						* height / 2, ctx);
    }
}

function drawArrow(startx, starty, endx, endy, ctx) {//绘制最后的箭头
    var x3, y3, x4, y4;
    var alpha;
    var flagx;
    var flagy;
    if (endx == startx) {
        alpha = Math.PI / 2
        flagx = 1;
        flagy = (endy - starty) / Math.abs(endy - starty);
    } else if (endy == starty) {
        alpha = 0;
        flagy = 1;
        flagx = (endx - startx) / Math.abs(endx - startx);
    } else {
        alpha = Math.atan(Math.abs(endy - starty) / Math.abs(endx - startx));
        flagx = (endx - startx) / Math.abs(endx - startx);
        flagy = (endy - starty) / Math.abs(endy - starty);
    }
    // var alpha = Math.atan(Math.abs(endy - starty) / Math.abs(endx - startx));
    // var flagx = (endx - startx) / Math.abs(endx - startx);
    // var flagy = (endy - starty) / Math.abs(endy - starty);
    x3 = endx - flagx * arrow_length * Math.cos(alpha - beta);
    y3 = endy - flagy * arrow_length * Math.sin(alpha - beta);
    x4 = endx - flagx * arrow_length * Math.cos(alpha + beta);
    y4 = endy - flagy * arrow_length * Math.sin(alpha + beta);
    ctx.strokeStyle = "rgb(0, 0, 0)";
    ctx.fillStyle = "#130c0e";
    ctx.lineWidth = 1;
    ctx.beginPath();
    ctx.moveTo(endx, endy);
    ctx.lineTo(x3, y3);
    ctx.lineTo(x4, y4);
    ctx.moveTo(endx, endy);
    ctx.fill();
    ctx.stroke();
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
    var events = model.events.values();
    for (i = 0; i < events.length; i++) {
        var e = events[i];
        var img = new Image();
        image.src = states[i].ImgUrl;
        ctx.drawImage(image, c.x, c.y, c.width, c.height);
        ctx.font = "12pt Arial"; // 设置字体，大小
        ctx.textBaseline = "top"; // 设置文字垂直对齐方式
        ctx.textAlign = "center"; // 设置文字水平对齐方式
        var strtext = c.name;
        ctx.fillText(strtext, (c.x + c.width / 2), (c.y
						+ c.height + 5));
        if (c.selected == true) {
            MarkCurNode(c.x, c.y, c.width, c.height, ctx);
        }
    }
    var transitions = model.transitions.values();
    for (i = 0; i < transitions.length; i++) {
        var t = transitions[i];
        var start_x = t.from.x + t.from.width / 2;
        var start_y = t.from.y + t.from.height / 2;
        var end_x = t.to.x + t.to.width / 2;
        var end_y = t.to.y + t.to.height / 2;
        drawLine(start_x, start_y, end_x, end_y, t.from.width, t.from.height, ctx);
        ctx.stroke();
        //TODO paint the transitions.
    }
    ctx.stroke();
}