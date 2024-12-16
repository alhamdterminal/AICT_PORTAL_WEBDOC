
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

        loadingBar();

    $.get('/Export/Reports/ViewTerminalReceipt?vesselId=' + vesselExportId + '&voyageId=' + voyageExportId + '&gdnumber=' + gdnumber, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }

    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').style.display = "none";
        } if (PermissionInputs.find(x => x.fieldName == "GDNumber" && x.isChecked == false)) {

        document.getElementById('GDNumber').style.display = "none";
        }

    }

