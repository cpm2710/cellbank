$(document).ready(function() {
	var page_id = (getParameterByName("page_id"));
	//first get the page definition
	//{page_id:"1",resources:["aaa","bbb","cccc"]};
	var query_def = new Object({
					"organization": "organization",
					"resourcename": "users",
					"query": null
				});
	function callback(d){

	};
	get_resource(query_def, callback);
});