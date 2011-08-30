function flowmodel(name) {//界面所有信息存储
    this.name = name;
    this.controllers = new HashMap(); //记录节点信息的HashMap
    this.transitions = new HashMap(); //记录连线信息的HashMap
}
