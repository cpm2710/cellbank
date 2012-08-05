var databaseUrl = "organization"; // "username:password@example.com/mydb"
var collections = ["users", "orders"]
var db = require("mongojs").connect(databaseUrl, collections);

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

db.users.save(user, function(error, saved) {
	console.log(saved);
});

db.orders.save(order, function(error, saved) {
	console.log(saved);
});

db.orders.save(order2, function(error, saved) {
	console.log(saved);
});

db.orders.find({
	"name": "ordername"
}, function(err, orders) {
	if (err || !orders) console.log("No female orders found");
	else console.log(JSON.stringify(orders));
});
