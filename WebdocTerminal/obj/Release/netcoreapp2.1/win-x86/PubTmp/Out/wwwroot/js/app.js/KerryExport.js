



    var ContainerNumber = "";

    function container_changed(data) {
        ContainerNumber = data.value;
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

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    var shippingAgent = document.getElementById("shippingAgent").value;
    var orientation = document.getElementById("type").value;

    $.get('/Export/Reports/ViewKerryExport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'vesselExport=' + vesselExportId
    + '&' + 'voyageExport=' + voyageExportId + '&' + 'shippingAgent=' + shippingAgent + '&' + 'ContainerNumber=' + ContainerNumber + '&' + 'orientation=' + orientation, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });


    }

    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "ReportType" && x.isChecked == false)) {

        document.getElementById('type').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').style.display = "none";
        } if (PermissionInputs.find(x => x.fieldName == "Container" && x.isChecked == false)) {

        document.getElementById('ContainerNumber').style.display = "none";
        } if (PermissionInputs.find(x => x.fieldName == "ShippingAgnet" && x.isChecked == false)) {

        document.getElementById('shippingAgent').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "From" && x.isChecked == false)) {

        document.getElementById('fromdate').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "To" && x.isChecked == false)) {

        document.getElementById('todate').disabled = true;
        }

    }



