
    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });
    }
    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }

    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var tokenNo = url.searchParams.get("tokenNo");

    loadingBar();
    $.get('/Import/Reports/ViewParkingTicketReport?tokenNo=' + tokenNo, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);


        });
    });