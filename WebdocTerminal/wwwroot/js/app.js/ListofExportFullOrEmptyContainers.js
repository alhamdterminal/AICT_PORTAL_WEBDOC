
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
        loadingBar();
    var shippingAgent = document.getElementById("shippingAgent").value;
    var orientation = document.getElementById("type").value;

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    console.log("fromdate", fromdate)
    console.log("todate", todate)

    $.get('/Export/Reports/ViewListofExportFullOrEmptyContainers?containerno=' + containerno + '&vesselExport=' + vesselExportId
    + '&voyageExport=' + voyageExportId + '&shippingAgent=' + shippingAgent + '&' + 'orientation=' + orientation + '&fromDate=' + fromdate + '&toDate=' + todate, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });
    }

    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "ReportType" && x.isChecked == false)) {

        document.getElementById('type').style.display = "none";
        } if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {

        document.getElementById('shippingAgent').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "Container" && x.isChecked == false)) {

        document.getElementById('ContainerNumber').style.display = "none";
        }
    }



