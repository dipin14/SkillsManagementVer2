﻿$(document).ready(function () {
    $('.dropdown-toggle').dropdown();
    //$(".menu-list ul li").on("click", function () {
    //    $('li.active').removeClass("active");
    //    $(this).addClass("active");
    //});
    //$(".menu-list ul li a").on("click", function () {
    //    $(".menu-list ul li").find(".active").removeClass("active");
    //    $(this).parent().addClass("active");
    //});

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