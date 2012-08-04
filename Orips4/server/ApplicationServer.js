var cfg = require("./ini-file-loader.js").load("./app.ini")["resource_server"];
var app_run_path = cfg["path"];
var authutil = require("./resourcepool/authenticationutil.js");
var express = require('express');
var urlparser = require("./urlparser.js");

var server = express.createServer();



server.use(express.static(app_run_path));
server.use(express.errorHandler({
	showStack: true,
	dumpExceptions: true
}));

server.get("/resources/*", function(req, res) {
	var xx = urlparser.parse(req.url, function(resource_meta) {
		console.log("############" + (resource_meta.organization));
		var username = req.headers["username"];
		var password = req.headers["password"];

		username = username == null ? "andy" : username;
		password = password == null ? "andy" : password;
		console.log(username);
		console.log(password);
		authutil.auth_user(username, password, function(user) {
			//console.log("@@@@@@@" + JSON.stringify(user));
			res.writeHead(200, {
				'Content-Type': 'text/plain'
			});
			res.write(JSON.stringify(user));
			res.end();
		});
		//for (var item in req.headers) {
		//	console.log(item + ": " + req.headers[item]);
		//}
	});


});

server.post("/resources/*", function(req, res) {

	
	req.on("data", function(data) {
		//console.log(data);
		var xx = urlparser.parse(req.url);
		var resource = JSON.parse(data);

	});
	//console.log("shit");
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