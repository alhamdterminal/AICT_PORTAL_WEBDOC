

    function myFunction() {

        var fromdate = document.getElementById("fromdate").value;

    var todate = document.getElementById("todate").value;

    var ShippingAgent = $("#agentId option:selected").val();

    loadingBar();

    $.get('/Import/Reports/ViewDestuffReportWithoutCBM?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'ShippingAgent=' + ShippingAgent, function (data) {

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

        } if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {
        document.getElementById('fromdate').disabled = true;

        } if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {
        document.getElementById('todate').disabled = true;

        }


    }