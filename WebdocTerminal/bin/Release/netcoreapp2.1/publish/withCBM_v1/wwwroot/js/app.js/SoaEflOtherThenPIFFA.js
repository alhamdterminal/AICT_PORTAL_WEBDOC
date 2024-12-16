


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

        var shippingAgent = document.getElementById("shippingAgent").value;

    var year = document.getElementById("year").value;
    var month = document.getElementById("month").value;
    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;


    loadingBar();

    $.get('/Export/Reports/ViewSoaEflOtherThenPIFFA?year=' + year + '&' + 'month=' + month
    + '&fromDate' + '=' + fromdate + '&' + 'toDate=' + todate + '&' + 'shippingagent=' + shippingAgent
    + '&Vessel' + '=' + vesselExportId + '&' + 'voyage=' + voyageExportId, function (data) {
        loadingBarHide();

    $('#repotdata').html(data);
            });


    }
    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "Agent" && x.isChecked == false)) {

        document.getElementById('shippingAgent').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Year" && x.isChecked == false)) {

        document.getElementById('year').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "Month" && x.isChecked == false)) {

        document.getElementById('month').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "From" && x.isChecked == false)) {

        document.getElementById('fromdate').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "To" && x.isChecked == false)) {

        document.getElementById('todate').disabled = true;
        }


    }
