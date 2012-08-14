var databaseUrl = "orips4"; // "username:password@example.com/mydb"
var usersCollectoin = ["users"]
var db = require("mongojs").connect(databaseUrl, usersCollectoin);

db.createCollection("users", function(err, collection) {
	if (err) console.log("not created");
	else console.log("create");
});


var user = {
	username: "andy",
	password: "andy",
	organization: "andyorg"
};

db.users.save(user, function(error, saved) {
	console.log(saved);
});


var resourceUrl = "andyorg"; // "username:password@example.com/mydb"
var collections = ["orders"]
var db2 = require("mongojs").connect(resourceUrl, collections);


var order = {
	name: "ordername",
	number: 1234,
	price: 1.2234
};

var order2 = {
	name: "ordername2",
	number: 123,
	price: 1.2234
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



//initialize resource_meta
var resourceMetasCollectoin = ["resource_metas"]
var db3 = require("mongojs").connect(databaseUrl, resourceMetasCollectoin);

db3.createCollection("resource_metas", function(err, collection) {
	if (err) console.log("not created");
	else console.log("create");
});

/*resource_meta
{
	_id:"11111",
	resource_name:"",
	resource_propery_metas:[
	{
		property_name:"abc",
		property_meta:"abv"
	},{
		property_name:"abc",
		property_meta:"abv"
	},{
		property_name:"abc",
		property_meta:"abv"
	}]
}

orders{
name:"ordername",
	number:1234,
	price:1.2234}
	*/

var one_resource_meta = {
	resource_name: "orders",
	resource_property_metas: [{
		property_name: "name",
		property_loc_name: "名字"
	}, {
		property_name: "number",
		property_loc_name: "数量",

	}, {
		property_name: "price",
		property_loc_name: "价格"
	}]
};

db3.resource_metas.save(one_resource_meta, function(error, saved) {
	console.log(saved);
});