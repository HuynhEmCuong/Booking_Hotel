
$(document).ready(function () {
    // Radio button
    $('.radio-group .radio').click(function () {
        $(this).parent().find('.radio').removeClass('selected');
        $(this).addClass('selected');
    });
    const pathname = location.pathname;
    if (pathname === '/' || pathname === '/gioi-thieu') {
        $(".navbar").toggleClass("navbar-white navbar-overlay")
        document.getElementById('logotype').src = "/assets/images/ui/logo-white.svg"
    } else {
        document.getElementById('logotype').src = "/assets/images/ui/logo-black.svg"
    }
});