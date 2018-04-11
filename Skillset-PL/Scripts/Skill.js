function resetValidation() {

    $(".has-error").empty();
    $(".text-danger").empty();
}

$(document).ready(function () {
    $("ul li:eq(2) a").parent().addClass('active').parent().parent().addClass('active');
});