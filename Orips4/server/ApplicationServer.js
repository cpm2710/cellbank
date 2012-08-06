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

//organization/resources/resourcename/id

// {mainresource:"name1",subresource:"name2",relation:"parent",subsummary:"summary"
// ,mainid:"id1",subid:"id2"}

//especially /organization/resources/relations/$query={}
server.get("/*/resources/*", function(req, res) {
	urlparser.parse(req._parsedUrl.pathname, function(resource_meta) {
		var username = req.headers["username"];
		var password = req.headers["password"];
		username = username == null ? "andy" : username;
		password = password == null ? "andy" : password;

		authutil.auth_user(username, password, function(user) {
			if (user != undefined) {
				//console.log("@@@@@@@@@" + resource_meta.organization + "#########" + resource_meta.resourcename + req.param("$query"));
				var query;
				if(req.param("$query")!=undefined){
					query = JSON.parse(req.param("$query"));
				}
				

				//{organization:asssdfd,resourcename:reere,query:query}
				//$filter=CategoryName eq 'Produce'
				var query_def = new Object({
					"organization": resource_meta.organization,
					"resourcename": resource_meta.resourcename,
					"query": query
				});
				resourceutil.get(query_def, function(err, resources) {
					res.writeHead(200, {
						'Content-Type': 'text/plain'
					});
					res.write(JSON.stringify(resources));

					res.end();
				});

			}
		});
	});
});

//if we submit one resource related to one resource
// shold we let the resource cross organization?
//organization/resources/resourcename/
//if this is the relations then should be 
//organization/resources/relations
// relations should in format 
// {mainresource:"name1",subresource:"name2",relation:"parent",subsummary:"summary"
// ,mainid:"id1",subid:"id2"}
server.post("/*/resources/*", function(req, res) {
	urlparser.parse(req._parsedUrl.pathname, function(resource_meta) {
		req.on("data", function(data) {
			var resource = JSON.parse(data);
			// {organization:"sdf",resourcename:"sdf",resource:{}}
			var resource_def = new Object({
				"organization": resource_meta.organization,
				"resourcename": resource_meta.resourcename,
				"resource": resource
			});
			resourceutil.post(resource_def, function(err, saved) {

				res.writeHead(200, {
					'Content-Type': 'text/plain'
				});
				res.end('okay');
			});
		});
	});
});




server.put("/*/resources/*", function(req, res) {
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

server.delete("/*/resources/*", function(req, res) {
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