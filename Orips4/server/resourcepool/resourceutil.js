

//query should be in {organization:asssdfd,resourcename:reere,query:query}
exports.get = function(query_def, callback) {

	console.log("query=="+JSON.stringify(query_def.query));
	var databaseUrl = query_def.organization; // "username:password@example.com/mydb"
	//var resouceCollection = [resource.resourcename];
	var db = require("mongojs").connect(databaseUrl);
	var resource_collection = db.collection(query_def.resourcename);

	
	resource_collection.find(query_def.query, callback);
};

//resource should bein {organization:"sdf",resourcename:"sdf",resource:{}}
exports.post = function(resource_def, callback) {
	var databaseUrl = resource_def.organization; // "username:password@example.com/mydb"
	//var resouceCollection = [resource.resourcename];
	var db = require("mongojs").connect(databaseUrl);
	var resource_collection = db.collection(resource_def.resourcename);
	resource_collection.save(resource_def.resource, callback);
};

//remove_def should be in {organization:"adsf",resourcename:"asdf",query:"deletequery{id:1}}
exports.delete = function(remove_def, callback) {
	var databaseUrl = remove_def.organization; // "username:password@example.com/mydb"
	//var resouceCollection = [resource.resourcename];
	var db = require("mongojs").connect(databaseUrl);
	var resource_collection = db.collection(remove_def.resourcename);
	resource_collection.remove(remove_def.query, callback);
};

exports.update = function(resource_def, callback) {
	var databaseUrl = resource_def.organization; // "username:password@example.com/mydb"
	//var resouceCollection = [resource.resourcename];
	var db = require("mongojs").connect(databaseUrl);
	var resource_collection = db.collection(resource_def.resourcename);
	resource_collection.update(resource_def.query, resource_def.update, callback);
};
	/*
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
