var databaseUrl = "orips4"; // "username:password@example.com/mydb"
var usersCollectoin = ["users"]
var db = require("mongojs").connect(databaseUrl, usersCollectoin);

db.createCollection("users", function(err, collection) {
	if (err) console.log("not created");
	else console.log("create");
});


db.createCollection("users", function(err, collection) {
	if (err) console.log("not created");
	else console.log("create");
});

var user = {
	username: "andy",
	password: "andy",
	organization:"andyorg"
};

db.users.save(user, function(error, saved) {
	console.log(saved);
});


var resourceUrl = "andyorg"; // "username:password@example.com/mydb"
var collections = ["orders"]
var db2 = require("mongojs").connect(resourceUrl, collections);


var order={
	name:"ordername",
	number:1234,
	price:1.2234
};

var order2={
	name:"ordername2",
	number:123,
	price:1.2234
};



db2.orders.save(order, function(error, saved) {
	console.log(saved);
});

db2.orders.save(order2, function(error, saved) {
	console.log(saved);
});

db2.orders.find({
	"name": "ordername"
}, function(err, orders) {
	if (err || !orders) console.log("No female orders found");
	else console.log(JSON.stringify(orders));
});
