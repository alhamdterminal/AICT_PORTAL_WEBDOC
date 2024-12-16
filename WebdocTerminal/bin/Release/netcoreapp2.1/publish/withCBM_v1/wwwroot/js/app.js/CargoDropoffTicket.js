


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
        loadingBar();

    var url_string = window.location.href
    var url = new URL(url_string);
    var lpNumber = url.searchParams.get("lpnumber");
    $.get('/Export/reports/ViewCargoDropoffTicket?lpNumber=' + lpNumber, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });
    });

