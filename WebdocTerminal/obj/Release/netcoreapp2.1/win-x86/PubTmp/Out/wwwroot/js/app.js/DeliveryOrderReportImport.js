



    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var donum = url.searchParams.get("DeliveryOrderNumber");

    window.open('/import/reports/ViewDeliveryOrderAuto?donum=' + donum, "PrintingFrame");

    var frameElement = document.getElementById("FrameToPrint");
    frameElement.addEventListener("load", function (e) {
            if (frameElement.contentDocument.contentType !== "text/html") {
        frameElement.contentWindow.print();
            }


    setTimeout(function () {window.close(); }, 30000);
        });

    loadingBar();
    $.get('/import/reports/ViewDeliveryOrder?donum=' + donum, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });





    });

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

    function formfiled() {

    }
