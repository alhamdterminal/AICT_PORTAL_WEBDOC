

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

    function myFunction() {

        loadingBar();


    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    var shippingAgent = document.getElementById("shippingAgent").value;
    var containerType = document.getElementById("containerType").value;



    $.get('/Import/Reports/ViewImportFullStockArriveMF?fromdate=' + fromdate + '&todate=' + todate + '&shippingAgent=' + shippingAgent + '&containerType=' + containerType  , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }

    function formfiled() {

    }



