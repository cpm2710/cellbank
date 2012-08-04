// url should be format as /resources/orgid/resourcename/id
// or /resources/orgid/resourcename/id/children
// or /resources/orgid/resourcename/id/customrelation
// or /resources/orgid/resourcename/id/brother
exports.parse = function(url, callback) {
	console.log("##########url:"+url);
	var array = url.split("/");
	var organization, resource, id, relation;

	for (var x = 0; x < array.length; x++) {
		//console.log(array[x].toString());
		if (array[x].toString() === "resources") {
			organization = array[x + 1];
			resource = array[x + 2];
			id = array[x + 3]
			relation = array[x + 4];
			break;
		}
	}
	if (callback != undefined) {
		callback(new Object({
			"organization": organization != undefined ? organization.toString() : undefined,
			"resource": resource != undefined ? resource.toString() : undefined,
			"id": id != undefined ? id.toString() : undefined,
			"relation": relation != undefined ? relation.toString() : undefined
		}));
	}
};