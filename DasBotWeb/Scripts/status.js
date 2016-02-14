$(function () {
    
    function getData (data) {
        var items = [];

        $("#activities").empty();
        items.push("<ul class='list-group'>");
        $.each(data, function () {

            if (this.Status) {
                items.push("<li class='list-group-item text-center' id='" + this.Name + "'>");

                items.push("<h3><span class='label label-success'>" + this.Description + "</span></h3>");
                items.push("<span> " + this.Data + "</span>");
                items.push("</li>");
            } else {
                items.push("<li class='list-group-item text-center' id='" + this.Name + "'>");
                items.push("<h3><span class='label label-danger'>" + this.Description +"</span></h3>");
                items.push("<span> " + this.Data + "</span>");
                items.push("</li>");
            }

        });
        items.push("</ul>");
        $("#activities").html(items.join(""));
    }

    $.getJSON("/api/status/")
     .done(getData);

    
    
    
});