(function ($) {
    var tmpls = {};
    $.getTmplAsync = function (url) {
        var tmpl = tmpls[url];
        if (!tmpl) {
            tmpl = $.get(url).done(function (data) {
                $(data).appendTo("body");
            });
        }
        return tmpl;
    };
    $.getTmplSync = function (url) {
        var tmpl = tmpls[url];
        if (!tmpl) {
            tmpl = $.ajax(
        {
            type: "GET",
            url: url,
            cache: true,
            async: false
        }).success(function (data) {
            $(data).appendTo("body");
            tmpl = $(data);
        });
        }
        return tmpl;
    };
} (jQuery));