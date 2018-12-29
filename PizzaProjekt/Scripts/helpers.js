'use strict';

function getRequestVerificationToken() {
    return $("input[name=__RequestVerificationToken]").val();
}

function addErrors(errors) {
    var $summary = $("[data-valmsg-summary=true]");
    var $ul = $summary.find("ul");
    $ul.empty();

    if ($.isArray(errors)) {
        $.each(errors, function (index, message) {
            $("<li />").html(message).appendTo($ul);
        });
    } else {
        $("<li />").html(errors).appendTo($ul);
    }

    $summary.removeClass("validation-summary-valid").addClass("validation-summary-errors");
}