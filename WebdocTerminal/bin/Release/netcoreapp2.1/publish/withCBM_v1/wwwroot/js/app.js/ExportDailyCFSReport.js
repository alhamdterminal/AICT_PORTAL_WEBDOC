

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
    var agent = document.getElementById("agent").value;
    var orientation = document.getElementById("type").value;

    $.get('/Export/Reports/ViewExportDailyCFSReport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'vesselExport=' + vesselExportId
    + '&' + 'voyageExport=' + voyageExportId + '&' + 'egm=' + egm + '&' + 'agent=' + agent + '&' + 'orientation=' + orientation
    , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').style.display = "none";
        } if (PermissionInputs.find(x => x.fieldName == "ReportType" && x.isChecked == false)) {

        document.getElementById('type').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "EGM" && x.isChecked == false)) {

        document.getElementById('egm').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "FreightForwarder" && x.isChecked == false)) {

        document.getElementById('agent').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "From" && x.isChecked == false)) {

        document.getElementById('fromdate').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "To" && x.isChecked == false)) {

        document.getElementById('todate').disabled = true;
        }

    }


