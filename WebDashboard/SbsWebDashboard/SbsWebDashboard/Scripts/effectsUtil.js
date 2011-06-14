var stage = [window.screenX, window.screenY, window.innerWidth, window.innerHeight];

function enableShakeWindow(callback) {
    setInterval(loop, 1000);

}

function loop() {
    if (isBrowserShaked()) {

    }
}

function isBrowserShaked() {
    var changed = false;
    if (stage[0] != window.screenX) {
        stage[0] = window.screenX;
        changed = true;
    }
    if (stage[1] != window.screenY) {
        stage[1] = window.screenY;
        changed = true;
    }
    return changed;
}