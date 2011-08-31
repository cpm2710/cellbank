function HashMap() {
    /** Map 大小 * */
    this.size = 0;
    /** 对象 * */
    this.entry = new Object();

    /** 存 * */
    this.put = function (key, value) {
        if (!this.containsKey(key)) {
            this.size++;
        }
        this.entry[key] = value;
    };

    /** 取 * */
    this.get = function (key) {
        return this.containsKey(key) ? this.entry[key] : null;
    };

    /** 删除 * */
    this.remove = function (key) {
        if (this.containsKey(key) && (delete this.entry[key])) {
            this.size--;
        }
    };

    /** 是否包含 Key * */
    this.containsKey = function (key) {
        return (key in this.entry);
    };

    /** 是否包含 Value * */
    this.containsValue = function (value) {
        for (var prop in this.entry) {
            if (this.entry[prop] == value) {
                return true;
            }
        }
        return false;
    };

    /** 所有 Value * */
    this.values = function () {
        var values = new Array();
        for (var prop in this.entry) {
            values.push(this.entry[prop]);
        }
        return values;
    };

    /** 所有 Key * */
    this.keys = function () {
        var keys = new Array();
        for (var prop in this.entry) {
            keys.push(prop);
        }
        return keys;
    };

    /** Map Size * */
    this.size = function () {
        return this.size;
    };

    /* 清空 */
    this.clear = function () {
        this.size = 0;
        this.entry = new Object();
    };
}