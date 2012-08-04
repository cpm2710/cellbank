var cfg = require("./util/ini-file-loader.js").load("./configfiles/app.ini")["resource_server"];
var app_run_path = cfg["path"];
var authutil = require("./resourcepool/authenticationutil.js");
var express = require('express');
var urlparser = require("./util/urlparser.js");
var resourceutil = require("./resourcepool/resourceutil.js");
var server = express.createServer();



server.use(express.static(app_run_path));
server.use(express.errorHandler({
	showStack: true,
	dumpExceptions: true
}));

server.get("/resources/*", function(req, res) {
	var xx = urlparser.parse(req.url, function(resource_meta) {
		var username = req.headers["username"];
		var password = req.headers["password"];
		username = username == null ? "andy" : username;
		password = password == null ? "andy" : password;
		console.log(username);
		console.log(password);
		authutil.auth_user(username, password, function(user) {
			if (user != undefined) {
				//console.dir(req);
				var query=eval(req.param("$query"));

				//{organization:asssdfd,resourcename:reere,query:query}
				urlparser.parse(req.url, function(resource_meta) {
					//$filter=CategoryName eq 'Produce'
					var query_def=new Object({
						"organization": resource_meta.organization,
						"resourcename": resource_meta.resourcename,
						"query":query
					});
					resourceutil.get(query_def, function(err, resources) {
						
						res.write(JSON.stringify(resources));
						res.writeHead(200, {
							'Content-Type': 'text/plain'
						});
						res.end();
					});
				});
			}
		});
	});
});

server.post("/resources/*", function(req, res) {
	req.on("data", function(data) {
		//console.log(data);
		urlparser.parse(req.url, function(resource_meta) {
			var resource = JSON.parse(data);
			// {organization:"sdf",resourcename:"sdf",resource:{}}
			var resource_def = new Object({
				"organization": resource_meta.organization,
				"resourcename": resource_meta.resourcename,
				"resource": resource
			});
			resourceutil.post(resource_def, function(err, saved) {
				console.log("here1");
			});
		});
	});
	//console.log("shit");
	res.writeHead(200, {
		'Content-Type': 'text/plain'
	});
	res.end('okay');
});

server.put("/resources/*", function(req, res) {
	req.on("data", function(data) {
		//console.log(data);
		urlparser.parse(req.url, function(resource_meta) {
			var resource = JSON.parse(data);

		});


	});
	//console.log("shit");
	res.writeHead(200, {
		'Content-Type': 'text/plain'
	});
	res.end('okay');
});

server.delete("/resources/*", function(req, res) {
	var xx = urlparser.parse(req.url);
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