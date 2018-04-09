$(document).ready(function () {
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

   
});