

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

    function printReport() {
        loadingBar();
    var destinationId = document.getElementById("destinationId").value;
    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    console.log("destinationId", destinationId);


    $.get('/Export/Reports/ViewExportfullout?Port=' + destinationId + '&fromdate=' + fromdate + '&todate=' + todate , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });
    }


    function formfiled() {

    }

