

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
    var billNo = url.searchParams.get("BillNo");
    console.log("billNo", billNo)

    loadingBar();
    $.get('/import/reports/ViewAICTInvoiceToLine?invoiceno=' + billNo  , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });
    });