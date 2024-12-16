
    function myFunction() {
        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;

    var ShippingAgent = $("#agentId option:selected").val();


    loadingBar();

    $.get('/Import/Reports/ViewDeliveryOrderReportCFSContainer?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'ShippingAgent=' + ShippingAgent, function (data) {
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

        if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {
        document.getElementById('agentId').disabled = true;

        }
        if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {
        document.getElementById('single_cal2').disabled = true;

        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {
        document.getElementById('single_cal3').disabled = true;

        }

        if ((PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) && (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false))
            && (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false))) {

        document.getElementById('btnSubmitReport').style.display = "none";
        }

    }
