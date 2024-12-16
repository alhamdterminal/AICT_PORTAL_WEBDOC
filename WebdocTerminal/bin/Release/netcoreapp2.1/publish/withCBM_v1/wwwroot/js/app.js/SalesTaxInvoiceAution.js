

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
    var billType = url.searchParams.get("BillType");
    var invoiceCategory = url.searchParams.get("InvoiceCategory");
    console.log("billNo", billNo)
    console.log("billType", billType)
    loadingBar();
    $.get('/import/reports/ViewSalesTaxInvoiceAution?BillNo=' + billNo + '&BillType=' + billType + '&invoiceCategory=' + invoiceCategory , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });
    });