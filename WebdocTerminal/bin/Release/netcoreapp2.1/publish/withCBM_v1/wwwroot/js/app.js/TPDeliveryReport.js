


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

        var fromdate = document.getElementById("fromdate").value;

    var todate = document.getElementById("todate").value;

    var shippingAgent = document.getElementById("shippingAgent").value;

    var type = document.getElementById("type").value;


    console.log("shippingAgent", shippingAgent);
    console.log("fromdate", fromdate);
    console.log("todate", todate);
    console.log("type", type);

    loadingBar();

    $.get('/Import/Reports/ViewTPDeliveryReport?fromdate=' + fromdate + '&' + 'toDate=' + todate + '&' + 'type=' + type + '&' + 'shippingAgent=' + shippingAgent, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }
