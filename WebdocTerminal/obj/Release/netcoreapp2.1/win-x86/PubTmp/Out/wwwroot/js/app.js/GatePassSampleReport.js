
    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var gatePassId = url.searchParams.get("gatePassSampleid");
    var type = url.searchParams.get("type");
    loadingBar();
    $.get('/import/reports/ViewGatePassSampleReport?gatePassSampleid=' + gatePassId + "&type=" + type, function (data) {
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

