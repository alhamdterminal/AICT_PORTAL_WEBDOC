


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


        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var shippingAgent = $('#shippingLine').val();


    console.log("fromdate", fromdate)
    console.log("todate", todate)
    console.log("shippingAgent", shippingAgent)

    loadingBar();
    $.get('/Import/Reports/ViewCargoDeliveredAuction?fromdate=' + fromdate + '&todate=' + todate + '&shippingAgent=' + shippingAgent, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });


    }




    function formfiled() {


    }
