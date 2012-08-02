var abc="\r\npath";
console.log(abc.trim());

var cfg = require("./ini-file-loader.js").load("./app.ini")["resource_server"];
console.log(cfg);


var express = require('express');

var server = express.createServer();

console.log(__dirname);
server.use(express.static(__dirname + '/demo'));


server.use(express.errorHandler({
    showStack: true,
    dumpExceptions: true
}));
//启动express web服务，监听8080端口
server.listen(801);

/*var app = express();
app.get('/', function(req, res){
  res.send('Hello World');
});
app.listen(80);
console.log('Listening on port 3000');*/