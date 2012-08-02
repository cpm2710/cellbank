exports.load = function(filename) {
    var r = [],
        q = require("querystring"),
        fileContent = require("fs").readFileSync(filename, "utf8"),
        v = q.parse(fileContent, '[', ']'),
        t;

        //console.log(fileContent);
    for (var i in v) {
        if (i != '' && v[i] != '') {
            r[i] = [];
            t = q.parse(v[i], '\r\n', '=');
            //console.log(t);
            for (var j in t) {
                //console.log(t[j].trim());
                if (j != '' && t[j] != '') r[i][j] = t[j].trim();
            }
        }
    }
    return r;
};