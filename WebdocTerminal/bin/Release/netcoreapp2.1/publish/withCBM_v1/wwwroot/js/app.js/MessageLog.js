
    function myFunction() {
        var fromdate = document.getElementById("single_cal2").value;
    loadingBar();
    $.get('/Import/Reports/ViewMessageLog?fromdate=' + fromdate, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });

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



    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "Date" && x.isChecked == false)) {
        document.getElementById('single_cal2').disabled = true;

        }

    }