
    var containerno = "";

    function container_changed(data) {
        containerno = data.value;
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

    function printReport() {

        var egm = document.getElementById("egm").value;
    var shippingAgent = document.getElementById("shippingAgent").value;
    var orientation = document.getElementById("type").value;
    loadingBar();
    $.get('/Export/Reports/ViewStatementShowingExportPosition?containerno=' + containerno + '&vesselExport=' + vesselExportId
    + '&voyageExport=' + voyageExportId + '&egm=' + egm + '&shippingAgent=' + shippingAgent + '&' + 'orientation=' + orientation, function (data) {

        loadingBarHide();
    $('#repotdata').html(data);
            });

    }

    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "ReportType" && x.isChecked == false)) {

        document.getElementById('type').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "EGM" && x.isChecked == false)) {

        document.getElementById('egm').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {

        document.getElementById('shippingAgent').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "Container" && x.isChecked == false)) {

        document.getElementById('ContainerNumber').style.display = "none";
        }
    }

