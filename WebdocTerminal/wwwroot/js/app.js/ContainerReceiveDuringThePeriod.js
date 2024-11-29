
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
    var type = document.getElementById("type").value;
    var ShippingAgent = $("#agentId option:selected").val();
    loadingBar();
    $.get('/Import/Reports/ViewContainerReceiveDuringThePeriod?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'type=' + type + '&' + 'ShippingAgent=' + ShippingAgent, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });

    }


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Type" && x.isChecked == false)) {

        document.getElementById('type').disabled = true;

        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {
        document.getElementById('agentId').disabled = true;

        } if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {
        document.getElementById('fromdate').disabled = true;

        } if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {
        document.getElementById('todate').disabled = true;

        }


    }