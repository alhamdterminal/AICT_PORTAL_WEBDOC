

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
        var todate = document.getElementById("single_cal3").value;


    loadingBar();

    $.get('/Import/Reports/ViewDailySummaryReport?todate=' + todate, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });

    }




    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "Date" && x.isChecked == false)) {
        document.getElementById('single_cal3').disabled = true;
    document.getElementById('btnSubmitReport').style.display = "none";
        }

    }
