


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

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    var egm = document.getElementById("egm").value;
    var shippingAgent = document.getElementById("shippingAgent").value;
    var clearingAgent = document.getElementById("clearingAgent").value;
    var shipper = document.getElementById("shipper").value;
    var portAndTerminal = document.getElementById("portAndTerminal").value;

    $.get('/Export/Reports/ViewAnfReport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'vesselExport=' + vesselExportId
    + '&' + 'voyageExport=' + voyageExportId + '&' + 'shippingAgent=' + shippingAgent
    + '&' + 'clearingAgent=' + clearingAgent + '&' + 'shipper=' + shipper + '&' + 'portAndTerminal=' + portAndTerminal + '&' + 'egm=' + egm
    , function (data) {

        loadingBarHide();
    $('#repotdata').html(data);

            });

    }


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "PortAndTerminal" && x.isChecked == false)) {

        document.getElementById('portAndTerminal').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "EGM" && x.isChecked == false)) {

        document.getElementById('egm').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ShippingAgnet" && x.isChecked == false)) {

        document.getElementById('shippingAgent').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ClearingAgent" && x.isChecked == false)) {

        document.getElementById('clearingAgent').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "Shipper" && x.isChecked == false)) {

        document.getElementById('shipper').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "From" && x.isChecked == false)) {

        document.getElementById('fromdate').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "To" && x.isChecked == false)) {

        document.getElementById('todate').disabled = true;
        }

    }

