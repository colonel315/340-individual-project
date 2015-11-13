/**
 * Created by Trey on 10/30/2015.
 */

$(document).ready(function() {
    var menuToggle = $('.menuToggle');

    menuToggle.click(function() {
        var menu = $(this).parent('.menuContainer').find('.menu');
        var container = $(this).parent('.menuContainer');

        if(container.hasClass("open")) {
            container.removeClass("open");
        }
        else {
            container.addClass("open");
        }

        menu.slideToggle(100);
        console.log('menus:' + menu.length);
    });
});
