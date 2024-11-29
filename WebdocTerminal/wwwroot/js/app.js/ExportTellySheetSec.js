


    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var containerNumber = url.searchParams.get("containerNumber");
    var vesid = url.searchParams.get("vesid");
    var voyId = url.searchParams.get("voyId");

    if (url.search == "?containerNumber=&vesid=&voyId=" || containerNumber) {

        loadingBar();

    $.get('/Export/Reports/ViewExportTellySheetSec?containerno=' + containerNumber + '&vesselExport=' + vesid
    + '&voyageExport=' + voyId, function (data) {

        loadingBarHide();
    $('#repotdata').html(data);
                });
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

    var containerno = "";

    function container_changed(data) {
        containerno = data.value;
    }

    function printReport() {
        loadingBar();

    $.get('/Export/Reports/ViewExportTellySheetSec?containerno=' + containerno + '&vesselExport=' + vesselExportId
    + '&voyageExport=' + voyageExportId, function (data) {

        loadingBarHide();
    $('#repotdata').html(data);
            });

    }


    function formfiled() {
    }


