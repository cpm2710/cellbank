(function ($) {

var tmpls = {};
$.getTmpl = function (url) {

var tmpl = tmpls[url];
if (!tmpl) {

tmpl = $.get(url).done(function (data) {

$(data).appendTo("body");

});
}
return tmpl;
};
}(jQuery));