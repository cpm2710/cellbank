var cfg = require("./ini-file-loader.js").load("./app.ini")["resource_server"];
var app_run_path = cfg["path"];



var express = require('express');

var server = express.createServer();

server.use(express.static(app_run_path));


server.use(express.errorHandler({
	showStack: true,
	dumpExceptions: true
}));

server.get("/resources/*", function(req, res) {
	console.log(req.url);
	res.writeHead(200, {
		'Content-Type': 'text/plain'
	});
	res.end('okay');
});

server.post("/resources/", function(req, res) {
	console.log("shit");
	res.writeHead(200, {
		'Content-Type': 'text/plain'
	});
	res.end('okay');
});

//启动express web服务，监听8080端口
server.listen(80);



/*var app = express();
app.get('/', function(req, res){
  res.send('Hello World');
});
app.listen(80);
console.log('Listening on port 3000');*/