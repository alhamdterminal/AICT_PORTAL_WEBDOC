

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

    function printReport() {
        loadingBar();
    var shippingAgent = document.getElementById("shippingAgent").value;
    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    console.log("shippingAgent", shippingAgent);


    $.get('/Export/Reports/ViewDailySalesReportExport?ShippingAgent=' + shippingAgent + '&fromdate=' + fromdate + '&todate=' + todate, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });
    }


    function formfiled() {

    }

