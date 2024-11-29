

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
    var InvoiceCategory = url.searchParams.get("InvoiceCategory");
    console.log("billNo", billNo)
    console.log("billType", billType)
    loadingBar();

    $.get('/import/reports/ViewSalesTaxInvoice?BillNo=' + billNo + '&BillType=' + billType + '&InvoiceCategory=' + InvoiceCategory , function (data) {

        loadingBarHide();
           //  setTimeout(function () {init(); }, 3000);
    $('#repotdata').html(data);
             
        });


    window.open('/import/reports/ViewSalesTaxInvoiceAutoPrint?BillNo=' + billNo + '&BillType=' + billType + '&InvoiceCategory=' + InvoiceCategory , "PrintingFrame");

        /*@* window.open('@Url.Action("ViewSalesTaxInvoiceAutoPrint", "Reports" , new {billNo = BillNo, billType = "BillType"})', "PrintingFrame");*@*/

    var frameElement = document.getElementById("FrameToPrint");
    frameElement.addEventListener("load", function (e) {
            if (frameElement.contentDocument.contentType !== "text/html") {

        frameElement.contentWindow.print();
                  

            }
    setTimeout(function () {window.close(); }, 30000);
                
        });


    });

    function init() {
        console.log("test");

    //    var dasd = $("#repotdata").xtraReport("option", "dataSource");
    //    console.log("dasd", dasd);

        // var url = document.URL.split('?')[0];

        // console.log("url", url)
        // var printWindowWrapper = window.open(url + "?Print=true", "_blank");

        // console.log("printWindowWrapper", printWindowWrapper)

        //printWindowWrapper.addEventListener("load", function (e) {
        //    if (printWindowWrapper.document.contentType !== "text/html")
        //        printWindowWrapper.print();
        //});

    }

    function formfiled() {

    }
