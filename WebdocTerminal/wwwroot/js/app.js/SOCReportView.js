



    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var SOCId = url.searchParams.get("SOCId");

    console.log("SOCId", SOCId)
    loadingBar();
    $.get('/import/reports/SOCReport?SOCId=' + SOCId, function (data) {
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
