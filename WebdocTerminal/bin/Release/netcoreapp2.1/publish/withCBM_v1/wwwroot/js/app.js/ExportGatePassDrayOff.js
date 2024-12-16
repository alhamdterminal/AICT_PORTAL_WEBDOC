
    var gatePassExportId = 0;


    $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    if (url.searchParams.get("gatePassExportId")) {
        gatePassExportId = url.searchParams.get("gatePassExportId");
    myFunction(gatePassExportId)
        }


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


    function myFunction(gatePassExportId) {
        loadingBar();
    $.get('/Export/Reports/ViewExportGatePassDrayOff?gatePassID=' + gatePassExportId, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });
    }

