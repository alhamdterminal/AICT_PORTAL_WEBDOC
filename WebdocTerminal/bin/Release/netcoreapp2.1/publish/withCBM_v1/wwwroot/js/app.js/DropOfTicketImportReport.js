

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

    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var igm = url.searchParams.get("igm");
    var ContainerNumber = url.searchParams.get("ContainerNumber");

    loadingBar();
    $.get('/import/reports/ViewDropOfTicketImportReport?igm=' + igm + '&ContainerNumber=' + ContainerNumber, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });
    });
