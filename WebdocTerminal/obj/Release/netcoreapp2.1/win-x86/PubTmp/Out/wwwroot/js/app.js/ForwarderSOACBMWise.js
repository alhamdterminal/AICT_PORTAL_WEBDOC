
    var tax = 0;

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
    var percbm = document.getElementById("percbm").value;
    var pergd = document.getElementById("pergd").value;

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    tax = document.getElementById("tax").value;


    if (!$('#percbm').val() || !$('#pergd').val()) {
        alert("Please Enter CBM AND GD")
    }
    else {
        loadingBar();
    $.get('/Export/Reports/ViewForwarderSOACBMWise?year=' + year + '&' + 'month=' + month
    + '&fromDate' + '=' + fromdate + '&' + 'toDate=' + todate + '&' + 'shippingagent=' + shippingAgent
    + '&Vessel' + '=' + vesselExportId + '&' + 'voyage=' + voyageExportId + '&' + 'tax=' + tax
    + '&usercbm' + '=' + percbm + '&' + 'usergd=' + pergd, function (data) {

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
        } if (PermissionInputs.find(x => x.fieldName == "PerCBM" && x.isChecked == false)) {

        document.getElementById('percbm').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "PerGD" && x.isChecked == false)) {

        document.getElementById('pergd').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {

        document.getElementById('fromdate').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {

        document.getElementById('todate').disabled = true;
        }

        if (PermissionInputs.find(x => x.fieldName == "Tax" && x.isChecked == false)) {

        document.getElementById('tax').disabled = true;
        }


    }


