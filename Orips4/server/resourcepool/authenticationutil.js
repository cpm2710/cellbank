exports.auth_user = function auth_user(username, password,callback) {

	var databaseUrl = "mydb"; // "username:password@example.com/mydb"
	var collections = ["users"]
	var db = require("mongojs").connect(databaseUrl, collections);

	db.users.find({
		username: username,
		password: password
	}, function(err, users) {
		if (err || !users) console.log("Authentication Failed!");
		else callback(users[0]);
	})
};

exports.create_user = function create_user(user) {
	db.users.save(user, function(error, saved) {
		console.log(saved);
	});
};

exports.delete_user = function delete_user(user) {
	db.users.delete(user, function(error, count) {
		console.log(count);
	});
};
/*db.createCollection("users", function(err, collection) {
	if (err) console.log("not created");
	else console.log("create");
});

db.users.find({
	sex: "male"
}, function(err, users) {
	if (err || !users) console.log("No female users found");
	else users.forEach(function(femaleUser) {
		console.log(femaleUser);
	});
});


db.users.save({
	email: "srirangan@gmail.com",
	password: "iLoveMongo",
	sex: "male"
}, function(err, saved) {
	if (err || !saved) console.log("User not saved");
	else console.log("User saved");
})

db.users.update({
	email: "srirangan@gmail.com"
}, {
	$set: {
		password: "iReallyLoveMongo"
	}
}, function(err, updated) {
	if (err || !updated) console.log("User not updated");
	else console.log("User updated");
});*/