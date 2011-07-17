function Show() {
    //获取画布对象
    ctx = document.getElementById("canvas1").getContext("2d");
    //创建图像对象
    img = new Image();
    //图像被装入后触发
    img.onload = function () {
        ctx.drawImage(img, 0, 0);
    }
    //指定图像源
    img.src = IMG_SRC;
}
function Clear() {
    //清除画布
    ctx = document.getElementById("canvas1").getContext("2d");
    ctx.clearRect(0, 0, 250, 300);
} 