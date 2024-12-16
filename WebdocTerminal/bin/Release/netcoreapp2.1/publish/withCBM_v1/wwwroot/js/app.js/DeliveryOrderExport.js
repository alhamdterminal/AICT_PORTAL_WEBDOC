
    var gdnumber = "";
    $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    if (url.searchParams.get("gdNumber")) {
        gdnumber = url.searchParams.get("gdNumber");
    printReport(gdnumber)
        }


    });

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




    function gdNumber_changed(data) {

        gdnumber = data.value;
    }


    function myFunction() {

        printReport(gdnumber)

    }

    function printReport(gdnumber) {
        loadingBar();
    $.get('/Export/Reports/ViewDeliveryOrderExport?gdNumber=' + gdnumber, function (data) {
        loadingBarHide();

    $('#repotdata').html(data);
        });
    }

    function formfiled() {


        if (PermissionInputs.find(x => x.fieldName == "GDNumber" && x.isChecked == false)) {

        document.getElementById('GDNumber').style.display = "none";
        }

    }

