

    var gdnumber;

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

    var date = formatDate(Date.now());
    $.get('/Export/Reports/ViewCargoMEasurementCertificate?gdnumber=' + gdnumber + '&' + 'date=' + date, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });

    }




    function formatDate(date) {
        var d = new Date(date),

    day = '' + d.getDate(),
    month = '' + (d.getMonth() + 1),
    year = d.getFullYear();

    if (month.length < 2)
    month = '0' + month;
    if (day.length < 2)
    day = '0' + day;

    return [year, month, day].join('');
    }



    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "GDNumber" && x.isChecked == false)) {

        document.getElementById('GDNumber').style.display = "none";
        }

    }