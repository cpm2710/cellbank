var ctxall;
function repaint() { // 閲嶇粯
    ctxall = canvas.getContext("2d");
    ctxall.clearRect(0, 0, canvas.width, canvas.height);
    paint(fmodel, ctxall);
}
function paint(model, ctx) { // ctx is cavas's context	
    // canvas.width = canvas.width;
    //ctx.canvas.width = canvas.width;
    //ctx.canvas.height = canvas.height;
    var states = model.states.values();
    //ctx.translate(0, 0);
    for (i = 0; i < states.length; i++) {
        var c = states[i];
        var image = new Image();
        image.src = states[i].ImgUrl;
        ctx.drawImage(image, c.x, c.y,c.width,c.height);
    }
    ctx.stroke();
}