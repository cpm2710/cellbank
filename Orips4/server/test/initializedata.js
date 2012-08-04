var databaseUrl = "mydb"; // "username:password@example.com/mydb"
var collections = ["users", "reports"]
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
db.users.save(user, function(error, saved) {
	console.log(saved);
});

