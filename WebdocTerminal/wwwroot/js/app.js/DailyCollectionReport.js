

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

        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var party = document.getElementById("partyId").value;
    var invoiceNo = document.getElementById("invoiceNo").value;
    var fromRange = document.getElementById("fromRange").value;
    var toRange = document.getElementById("toRange").value;


    loadingBar();
    $.get('/Import/Reports/ViewDailyCollectionReport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'party=' + party
    + '&' + 'invoiceNo=' + invoiceNo + '&' + 'fromRange=' + fromRange + '&' + 'toRange=' + toRange, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });

    }


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "InvoiceNo" && x.isChecked == false)) {

        document.getElementById('invoiceNo').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "FromRange" && x.isChecked == false)) {

        document.getElementById('fromRange').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToRange" && x.isChecked == false)) {

        document.getElementById('toRange').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Party" && x.isChecked == false)) {

        document.getElementById('partyId').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {

        document.getElementById('single_cal2').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {

        document.getElementById('single_cal3').disabled = true;
        }


    }
