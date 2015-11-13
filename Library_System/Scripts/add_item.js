/**
 * Created by Trey on 11/4/2015.
 */


$(document).ready(function() {
    var mySelect = $('#mySelect');
    var book = $('.book');
    var periodicals = $('.periodicals');
    var cd = $('.cd');
    var currentSelected = "";

    mySelect.change(function() {
        if(mySelect.val() == "Book") {
            if(currentSelected == "periodicals") {
                periodicals.slideToggle();
            }
            else if(currentSelected == 'cd') {
                cd.slideToggle();
            }

            book.slideToggle();

            currentSelected = 'book';
        }
        else if(mySelect.val() == "Periodicals") {
            if(currentSelected == "book") {
                book.slideToggle();
            }
            else if(currentSelected == 'cd') {
                cd.slideToggle();
            }

            periodicals.slideToggle();

            currentSelected = 'periodicals';
        }
        else if(mySelect.val() == "CD") {
            if(currentSelected == "periodicals") {
                periodicals.slideToggle();
            }
            else if(currentSelected == 'book') {
                book.slideToggle();
            }

            cd.slideToggle();

            currentSelected = 'cd';
        }
        else {
            if(currentSelected == "periodicals") {
                periodicals.slideToggle();
            }
            else if(currentSelected == 'cd') {
                cd.slideToggle();
            }
            else {
                book.slideToggle();
            }

            currentSelected = "";
        }
    })
});