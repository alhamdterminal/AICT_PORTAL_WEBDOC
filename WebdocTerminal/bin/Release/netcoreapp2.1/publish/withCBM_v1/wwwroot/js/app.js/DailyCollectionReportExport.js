
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


        var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    var party = document.getElementById("partyId").value;
    var invoiceNo = document.getElementById("invoiceNo").value;
    var fromRange = document.getElementById("fromRange").value;
    var toRange = document.getElementById("toRange").value;

    //loadingBar();
    $.get('/Export/Reports/ViewDailyCollectionReportExport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'party=' + party
    + '&' + 'invoiceNo=' + invoiceNo + '&' + 'fromRange=' + fromRange + '&' + 'toRange=' + toRange
    , function (data) {
        console.log(data)
                // loadingBarHide()
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
        } if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {

        document.getElementById('fromdate').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {

        document.getElementById('todate').disabled = true;
        }


    }

