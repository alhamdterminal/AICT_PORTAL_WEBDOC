
    var containerno = "";
    var gdno = "";

    function container_changed(data) {
        containerno = data.value;
    }

    function gd_changed(data) {
        gdno = data.value;
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

    $.get('/Export/Reports/ViewExportGatePasses?containerNumber=' + containerno + '&' + 'vessel=' + vesselExportId
    + '&' + 'voyage=' + voyageExportId + '&' + 'fromdate=' + fromdate + '&' + 'todate=' + todate + '&gdnumber=' + gdno
    , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }



    function formfiled() {

        //if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        //    document.getElementById('vesselExports').style.display = "none";
        //} if (PermissionInputs.find(x => x.fieldName == "Container" && x.isChecked == false)) {

        //    document.getElementById('ContainerNumber').style.display = "none";
        //}
        //if (PermissionInputs.find(x => x.fieldName == "From" && x.isChecked == false)) {

        //    document.getElementById('fromdate').disabled = true;
        //} if (PermissionInputs.find(x => x.fieldName == "To" && x.isChecked == false)) {

        //    document.getElementById('todate').disabled = true;
        //}

    }


