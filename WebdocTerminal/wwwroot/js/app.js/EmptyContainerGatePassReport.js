

    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var emptyGatePass = url.searchParams.get("emptyGatePass");
    loadingBar();
    $.get('/import/reports/ViewEmptyContainerGatePassReport?emptyGatePass=' + emptyGatePass, function (data) {
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