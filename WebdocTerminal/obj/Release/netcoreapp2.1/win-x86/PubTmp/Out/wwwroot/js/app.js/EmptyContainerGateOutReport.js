

    var type;


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


    function submitEmptyContainer() {

        if (type == "CFS") {
        containernumber = $("#containerdb option:selected").text();

        }
    else {
        containernumber = $("#containerCYdb option:selected").text();

        }

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    loadingBar();
    $.get('/Import/Reports/ViewEmptyContainerGateOutReport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'containernumber=' + containernumber, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });


    }





    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "ContainerSelection" && x.isChecked == false)) {


        document.getElementById('containerSelection').style.display = "none";
        }

        if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {


        document.getElementById('fromdate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {


        document.getElementById('todate').disabled = true;
        }

    }

