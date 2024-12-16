
    var containerNumber = null;


    function container_changed(data) {
        containerNumber = data.value;
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

        console.log(containerNumber)


        loadingBar();
    $.get('/Export/Reports/ViewExportAdvice?containerNumber=' + containerNumber + '&' + 'vesselExport=' + vesselExportId
    + '&' + 'voyageExport=' + voyageExportId, function (data) {
        loadingBarHide();

    $('#repotdata').html(data);
    containerNumber = null;


            });



    }



    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Container" && x.isChecked == false)) {

        document.getElementById('ContainerNumber').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').style.display = "none";
        }

    }

