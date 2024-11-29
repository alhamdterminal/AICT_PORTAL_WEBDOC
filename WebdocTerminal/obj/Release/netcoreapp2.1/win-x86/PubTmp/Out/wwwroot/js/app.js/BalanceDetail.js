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

    var customerid = url.searchParams.get("customerid");
    var Date = url.searchParams.get("date");

    console.log(customerid);
    console.log(Date);

    loadingBar();

    $.post('/Account/Reports/ViewBalanceDetail?tdate=' + Date + '&' + 'csid=' + customerid, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });
    });

