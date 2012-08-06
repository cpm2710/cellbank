// url should be format as /resources/orgid/resourcename/id
// or /orgid/resources/resourcename/id/children
// or /orgid/resources/resourcename/id/customrelation
// or /orgid/resources/resourcename/id/brother
// or /orgid/resources/relations
exports.parse = function(url, callback) {
	console.log("##########url:"+url);
	var array = url.split("/");
	var organization, resourcename, id, relation;

	for (var x = 0; x < array.length; x++) {
		console.log("#####"+array[x].toString());
		if (array[x].toString() === "resources") {
			organization = array[x - 1];
			resourcename = array[x + 1];
			id = array[x + 2]
			relation = array[x + 3];
			break;
		}
	}
	if (callback != undefined) {
		callback(new Object({
			"organization": organization != undefined ? organization.toString() : undefined,
			"resourcename": resourcename != undefined ? resourcename.toString() : undefined,
			"id": id != undefined ? id.toString() : undefined,
			"relation": relation != undefined ? relation.toString() : undefined
		}));
	}
};