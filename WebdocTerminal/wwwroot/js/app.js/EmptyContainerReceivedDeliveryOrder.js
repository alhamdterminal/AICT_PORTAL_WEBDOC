﻿



    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var donum = url.searchParams.get("DeliveryOrderNumber");

    console.log("donum", donum)
    loadingBar();
    $.get('/import/reports/ViewEmptyContainerReceivedDeliveryOrder?donum=' + donum, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });
    });

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

    }