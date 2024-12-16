


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
    var requestNumber = $('#requestNumber').val();
    var shippingLine = $('#shippingLine').val();
    var numberOfcontainers = $('#numberOfcontainers').val();
    var vesselName = $('#vesselName').val();
    var shippingAgent = $('#shippingAgent').val();


    console.log("fromdate", fromdate)
    console.log("todate", todate)
    console.log("requestNumber", requestNumber)
    console.log("shippingLine", shippingLine)
    console.log("numberOfcontainers", numberOfcontainers)
    console.log("vesselName", vesselName)
    console.log("shippingAgent", shippingAgent)

    loadingBar();
    $.get('/Import/Reports/ViewOutStandingPaymentReport?requestNumber=' + requestNumber + '&' + 'shippingLine=' + shippingLine + '&' + 'numberOfcontainers=' + numberOfcontainers
    + '&' + 'vesselName=' + vesselName + '&' + 'shippingAgent=' + shippingAgent + '&' + 'fromdate=' + fromdate
    + '&' + 'todate=' + todate, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });


    }




    function formfiled() {


    }
