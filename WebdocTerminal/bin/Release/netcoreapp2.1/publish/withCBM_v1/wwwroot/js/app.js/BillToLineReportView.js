﻿

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

        loadingBar();

    var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;

    var shippingagentId = document.getElementById("shippingagent").value;

    console.log("fromdate", fromdate);
    console.log("todate", todate);


    $.get('/Import/Reports/ViewBillToLineReport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'shippingagentId=' + shippingagentId
    , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }

    function formfiled() {

    }