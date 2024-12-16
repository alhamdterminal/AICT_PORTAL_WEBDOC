
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
    console.log("billNo", billNo)
    console.log("billType", billType)
    loadingBar();

    $.get('/import/reports/ViewSalesTaxInvoiceForLine?BillNo=' + billNo + '&BillType=' + billType, function (data) {

        loadingBarHide();
           //  setTimeout(function () {init(); }, 3000);
    $('#repotdata').html(data);

        });
         


    });

    function init() {
        console.log("test");
 

    }

    function formfiled() {

    }
