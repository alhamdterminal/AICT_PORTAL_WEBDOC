

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
    var ShippingAgent = $("#agentId option:selected").val();


    loadingBar();

    $.get('/Import/Reports/ViewDestuffContainerDateWise?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'ShippingAgent=' + ShippingAgent, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }