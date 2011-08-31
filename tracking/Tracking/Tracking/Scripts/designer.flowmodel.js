function flowmodel(name) {//界面所有信息存储
    this.name = name;
    this.states = new HashMap(); //记录节点信息的HashMap
    this.transitions = new HashMap(); //记录连线信息的HashMap
    this.events = new HashMap();

}
function asXml(model) {
    var result = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
    result += "<!DOCTYPE stateProcess PUBLIC \"\" \"http://cmseportal.redmond.corp.microsoft.com/SEPortalServices/Config/StateMachine.dtd\">";
    result += "<states>";
    var states = model.states.values();
    var i = 0;
    for (i = 0; i < states.length; i++) {
        result += "<state stateName=\"" + states[i].name + "\">";
        result += "</state>";
    }
    result += "</states>";
    return result;
}
function controller(name,type) {
    this.name = name;
    this.type = type;
    this.selected = false;
    this.width = 40;
    this.height = 40;
    for (i = 0; i < functionPoints.length; i++) {
        var fp = functionPoints[i];
        if (fp.Type == type)
            this.ImgUrl = fp.ImgUrl;
    }
    this.x = 0;
    this.y = 0;
}
function transition(from, to,acceptevent) {
    this.from = from;
    this.to = to;
    this.acceptevent = acceptevent;
    this.visible = "";
}
