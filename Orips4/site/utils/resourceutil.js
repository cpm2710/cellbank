//organization/resources/resourcename/id
// {mainresource:"name1",subresource:"name2",relation:"parent",subsummary:"summary"
// ,mainid:"id1",subid:"id2"}
//especially /organization/resources/relations/$query={}
//query should be in {organization:asssdfd,resourcename:reere,query:query}

function get_resource(query_def, callback) {

	/*
var query_def = new Object({
					"organization": resource_meta.organization,
					"resourcename": resource_meta.resourcename,
					"query": query
				});*/
	//query_def could be $query="{adfadsdsa}";
	// query_def organization_id="";
	var url = "./" + query_def.organization + "/" + query_def.resourcename + query == null ? "" : ? ("$query=" + query_def.query);
	$.ajax({
		contentType: "application/json",
		url: url,
		type: "GET",
		cache: false,
		dataType: "json",
		error: callback,
		success: callback
	});
};

//resource should bein {organization:"sdf",resourcename:"sdf",resource:{}}

function post_resource(resource_def, callback) {

	/*
	var resource_def = new Object({
				"organization": resource_meta.organization,
				"resourcename": resource_meta.resourcename,
				"resource": resource
			});*/

	var url = "./" + resource_def.organization + "/" + resource_def.resourcename;
	$.ajax({
		contentType: "application/json",
		url: url,
		type: "POST",
		cache: false,
		dataType: "json",
		data: resource_def.resource,
		error: callback,
		success: callback
	});
};

//seems there would be one id in resource_def


function update_resource(resource_def, callback) {

	var url = "./" + resource_def.organization + "/" + resource_def.resourcename + "/" + resource_def.id;
	$.ajax({
		contentType: "application/json",
		url: url,
		type: "PUT",
		cache: false,
		dataType: "json",
		data: resource_def.resource,
		error: callback,
		success: callback
	});
};
};

//remove_def should be in {organization:"adsf",resourcename:"asdf",query:"deletequery{id:1}}

function delete_resource(remove_def, callback) {

};


/*
var adminManagementUrl="/rest/admin"
function modifyPasswordRest(orip, newp) {
	resultJson = $.ajax({
		contentType : "application/json",
		url : adminManagementUrl + "/modifyPassword/" + orip +"/"+newp,
		cache : false,
		async : false,
		type: 'POST',
		statusCode: {
			401: function(){
				jumpToLogin();
			},
		    403: function() {
		    	jumpToLogin();
		    }
		  }
	}).responseText;
	return resultJson;
}

function addAdmin(adminJson){
	resultJson = $.ajax({
		dataType: "json",
		contentType:"application/json",
		type:'POST',
		url: adminManagementUrl + "/",
		data: adminJson,
		cache:false,
		aysnc:false,
		statusCode: {
			401:function(){
				jumpToLogin();
			},
			403:function(){
				jumpToLogin();
			}
			
		}
	}).responseText;
	if (resultJson!=null && resultJson!="")
		resultJson =  eval("(" + resultJson + ")");
	return resultJson;
}

*/