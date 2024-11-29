
    var tax = "";

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
    var share20 = document.getElementById("share20").value;
    var share40 = document.getElementById("share40").value;

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    tax = document.getElementById("tax").value;

    if (!$('#share20').val() || !$('#share40').val()) {
        alert("Please Share of Bay West 20 and 40")
    }
    else {
        loadingBar();
    $.get('/Export/Reports/ViewSOADSVOtherThenPIFFA?year=' + year + '&' + 'month=' + month
    + '&fromDate' + '=' + fromdate + '&' + 'toDate=' + todate + '&' + 'shippingagent=' + shippingAgent
    + '&Vessel' + '=' + vesselExportId + '&' + 'voyage=' + voyageExportId + '&' + 'tax=' + tax
    + '&shareofBayWest20' + '=' + share20 + '&' + 'shareofBayWest40=' + share40, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
                });
        }

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
        } if (PermissionInputs.find(x => x.fieldName == "ShareofBayWest20" && x.isChecked == false)) {

        document.getElementById('share20').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ShareofBayWest40" && x.isChecked == false)) {

        document.getElementById('share40').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "Tax" && x.isChecked == false)) {

        document.getElementById('tax').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {

        document.getElementById('fromdate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {

        document.getElementById('todate').disabled = true;
        }


    }

