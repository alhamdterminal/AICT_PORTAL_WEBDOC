
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


    function printReport() {


        var egm = document.getElementById("egm").value;
    var shippingAgent = document.getElementById("shippingAgent").value;

    loadingBar();

    $.get('/Export/Reports/ViewTerminalReceiptSec?vessel=' + vesselExportId + '&voyage=' + voyageExportId
    + '&egm=' + egm + '&shippingAgent=' + shippingAgent + '&containerNumber=' + containerNumber + '&gdnumber=' + gdnumber
    , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });


    }


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').style.display = "none";
        } if (PermissionInputs.find(x => x.fieldName == "EGM" && x.isChecked == false)) {

        document.getElementById('egm').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {

        document.getElementById('shippingAgent').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "Container" && x.isChecked == false)) {

        document.getElementById('ContainerNumber').style.display = "none";
        } if (PermissionInputs.find(x => x.fieldName == "GDNumber" && x.isChecked == false)) {

        document.getElementById('GDNumber').style.display = "none";
        }
    }

