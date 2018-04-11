$(document).ready(function () {
    $('.dropdown-toggle').dropdown();
    
    var url = window.location;

    $('.menu-list ul li a').filter(function () {
        return this.href == url;       

    }).parent().addClass('active').parent().parent().addClass('active');

    $(".toggle-button").click(function () {
        $("#show").toggle();
        $(".classi").toggleClass("fa fa-chevron-up fa fa-chevron-down")
    });

    $(".toggle-button1").click(function () {
        $("#show1").toggle();
        $(".classi1").toggleClass("fa fa-chevron-up fa fa-chevron-down")
    });

    $(".toggle-button2").click(function () {
        $("#show2").toggle();
        $(".classi2").toggleClass("fa fa-chevron-up fa fa-chevron-down")
    });

    $(".toggle-button3").click(function () {
        $("#show3").toggle();
        $(".classi3").toggleClass("fa fa-chevron-up fa fa-chevron-down")
    });

});