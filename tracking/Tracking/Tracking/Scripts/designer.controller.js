function containedIn(c, x, y) {
    if ((c.x < x) && (x < (c.x + c.width)) && (c.y < y)
			&& (y < (c.y + c.height))) {
        return true;
    }
    return false;
}
function selectElement(x, y) {
    var states = fmodel.states.values();
    var events = fmodel.events.values();
    var transitions = fmodel.transitions.values();        
        var i = 0;
        for (i = 0; i < states.length; i++) {
            if (containedIn(states[i], x, y)) {
                return states[i];
            }
        }
        for (i = 0; i < events.length; i++) {
            if (containedIn(events[i], x, y)) {
                return events[i];
            }
        }

    return null;
}

function handleClickEvent(e) {//处理鼠标事件
    var x = e.layerX;// getRelativeX(e, canvas);
    var y = e.layerY; // getRelativeY(e, canvas);
    if (selectedControllerType == null) {
        return;
    }
    var selectedNode = selectElement(x, y);
    if (selectedControllerType == "select") {
        if (selectedNode != null) {
            fmodel.clearSelection();
            selectedNode.selected = true;
        }
        
    } else {
    
    if (selectedControllerType == "event") {
        var name = "Event" + fmodel.events.values().length;
        var c = new controller(name, selectedControllerType)
        c.x = x - (c.width / 2);
        c.y = y - (c.height / 2);
        fmodel.events.put(c.name, c);
    } else if (selectedControllerType == "state") {
        var name = "State" + fmodel.states.values().length;
        var c = new controller(name, selectedControllerType)
        c.x = x - (c.width / 2);
        c.y = y - (c.height / 2);
        fmodel.states.put(c.name, c);
    }        
    }
    repaint();
   
}

function mouseMove(e) {//对于鼠标移动时的处理
}
function mouseUp(e) {

}
function doKeyDown(evt) {
    switch (evt.keyCode) {
        case 38:
            break;
    }
}