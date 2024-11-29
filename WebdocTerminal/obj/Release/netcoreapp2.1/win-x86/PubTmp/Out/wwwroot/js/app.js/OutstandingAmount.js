
    var containerNumber = "";

    function container_changed(data) {

        containerNumber = data.value;


    }
    var gdnumber = "";

    function gdNumber_changed(data) {

        gdnumber = data.value;


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


    function myFunction() {

        loadingBar();

    var shippingAgent = document.getElementById("shippingAgent").value;
    var ClearingAgent = document.getElementById("ClearingAgent").value;
    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;


    $.get('/Export/Reports/ViewOutstandingAmount?fromDate=' + fromdate + '&' + 'toDate=' + todate
    + '&Vessel' + '=' + vesselExportId + '&' + 'voyage=' + voyageExportId + '&' + 'shippingagent=' + shippingAgent
    + '&clearingAgent' + '=' + ClearingAgent + '&' + 'containerNo=' + containerNumber + '&' + 'gdNumber=' + gdnumber, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });


    }


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "Agent" && x.isChecked == false)) {

        document.getElementById('shippingAgent').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ClearingAgent" && x.isChecked == false)) {

        document.getElementById('ClearingAgent').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "Container" && x.isChecked == false)) {

        document.getElementById('ContainerNumber').style.display = "none";
        } if (PermissionInputs.find(x => x.fieldName == "GDNumber" && x.isChecked == false)) {

        document.getElementById('GDNumber').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {

        document.getElementById('fromdate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {

        document.getElementById('todate').disabled = true;
        }


    }

