

    function myFunction() {
         

        var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    var ShippingAgent = $("#agentId option:selected").val();
    var masterLineId = $("#masterLineId option:selected").val();

    loadingBar();

    $.get('/Import/Reports/ViewCMonthlyBillToLineAccount?line=' + ShippingAgent + '&fromdate=' + fromdate + '&todate=' + todate + '&masterline=' + masterLineId, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });
    }




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

