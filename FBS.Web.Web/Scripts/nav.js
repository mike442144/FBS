// JavaScript Document
$(document).ready(function() {
    $(".navlan div").each(function() {
            var a = $(this);
            a.click(function() {
            a.removeClass("navitem");
            a.removeClass("navitemCurrent");
                a.addClass("navitemCurrent");
                a.siblings().removeClass("navitemCurrent");
                a.siblings().addClass("navitem");
            });
            a.hover(function() {
            a.addClass("navitemhover");

        }, function() {
         a.removeClass("navitemhover"); });
        });
    });
