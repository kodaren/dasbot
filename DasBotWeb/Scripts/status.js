$(function () {
    $.getJSON("/api/status/")
     .done(function (data) {
         var items = [];

         items.push("<ul>");
         $.each(data, function () {
             items.push("<li id='" + this.Name + "'>" + this.Description + "</li>");
         });
         items.push("</ul>");
         $("#activities").html(items.join(""));
     });
});