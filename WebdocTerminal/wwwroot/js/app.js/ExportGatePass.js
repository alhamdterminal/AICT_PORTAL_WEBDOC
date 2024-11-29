
    var containerNumber;

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

        loadingBar();

    var url_string = window.location.href
    var url = new URL(url_string);
    containerNumber = url.searchParams.get("containerNumber");

    $.get('/Export/Reports/ViewExportGatePass?containerNumber=' + containerNumber, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        })
    });



