function trimText(text, length) {
    length = length || 20;
    if (!text) {
        return "";
    }
    text = $.trim(text);
    if (text.length <= length) {
        return kendo.htmlEncode(text);
    }

    var parsedText = "<span title='" + text.replace("'", "`") + "'> " + kendo.htmlEncode(text.substr(0, length)) + "...</span>";
    return parsedText;
}

function errorHandler(e) {

    var grid = $("#books-grid").data("kendoGrid");
    grid.one("dataBinding", function (e) {
        e.preventDefault();
    });

    var errorContainer = $("#error-message").text("");
    var message = "";
    if (e.errors) {
        for (var i in e.errors) {
            var propertyErrors = e.errors[i].errors;
            for (var p in propertyErrors) {
                message += "<div>" + propertyErrors[p] + "</div>";
            }
        }
    }
    else {
        message = "The entity couldn't be saved to Database."
    }
    errorContainer.html(message);
}