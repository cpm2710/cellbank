var http = require('http');

// Create an HTTP tunneling proxy
var proxy = http.createServer(function(req, res) {
	req.on("data", function(data) {

		console.log(""+data);
		if (req.method === ("get")) {

		};
		if (req.method === ("post")) {

		};

		if (req.method === ("delete")) {

		};
		if (req.method === ("put")) {

		};
	});

	res.writeHead(200, {
		'Content-Type': 'text/plain'
	});
	res.end('okay');
}).listen("80");