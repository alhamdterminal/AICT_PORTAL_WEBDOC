

    function myFunction() {

        var agentId = $("#agentId option:selected").val();
    console.log("agentId", agentId);
    var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    loadingBar();
    $.get('/Import/Reports/ViewExaminationMarkESCYContainer?fromdate=' + fromdate + '&' + 'todate=' + todate + '&shippingAgent=' + agentId, function (data) {
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

        if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {

        document.getElementById('single_cal2').disabled = true;
        }

        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {

        document.getElementById('single_cal3').disabled = true;
        }



    }