

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
        var ShippingAgent = $("#agentId option:selected").val();


    loadingBar();
    $.get('/Import/Reports/ViewContainerAvailableToYard?ShippingAgent=' + ShippingAgent, function (data) {

        loadingBarHide();
    $('#repotdata').html(data);
        });

    }
    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {
        document.getElementById('agentId').disabled = true;

        }


    }
