function getParameterByName(name) {
	var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);
	//alert(match);
	//alert(decodeURIComponent(match[1].replace(/\+/g, ' ')));
	return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
};