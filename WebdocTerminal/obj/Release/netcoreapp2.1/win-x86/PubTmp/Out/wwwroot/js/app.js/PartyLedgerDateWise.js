﻿
    var IsnegativeBalance = "";
    var chequeNo = "";
    function handleClick(cb) {
        if (cb.checked == true) {
        IsnegativeBalance = '0'

    }
    else {
        IsnegativeBalance = ""
    }

    }

    function chqeueNo_changed(data) {
        chequeNo = data.value;

    }

    function myFunction() {
        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var invoiceNo = document.getElementById("invoiceNumber").value;
    var bank = document.getElementById("bnk").value;
    var party = document.getElementById("party").value;
    var balanceFrom = document.getElementById("fromRange").value;
    var balanceTo = document.getElementById("toRange").value;

    loadingBar();

    $.get('/Import/Reports/ViewPartyLedgerDateWise?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'party=' + party
    + '&' + 'invoiceNo=' + invoiceNo + '&' + 'balanceFrom=' + balanceFrom + '&' + 'balanceTo=' + balanceTo
    + '&' + 'bank=' + bank + '&' + 'chequeNo=' + chequeNo + '&' + 'IsnegativeBalance=' + IsnegativeBalance, function (data) {
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

        if (PermissionInputs.find(x => x.fieldName == "InvoiceNo" && x.isChecked == false)) {

        document.getElementById('invoiceNumber').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ChqNo" && x.isChecked == false)) {

        document.getElementById('IGM').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "NavBalance" && x.isChecked == false)) {

        document.getElementById('isnegative').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Bank" && x.isChecked == false)) {

        document.getElementById('bnk').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "FromRange" && x.isChecked == false)) {

        document.getElementById('fromRange').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToRange" && x.isChecked == false)) {

        document.getElementById('toRange').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Party" && x.isChecked == false)) {

        document.getElementById('party').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "FromDate" && x.isChecked == false)) {

        document.getElementById('single_cal2').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ToDate" && x.isChecked == false)) {

        document.getElementById('single_cal3').disabled = true;
        }

    }