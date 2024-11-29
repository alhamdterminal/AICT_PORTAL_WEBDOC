
    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });
    }

    function loadingBarHide() {
        $("#large-indicator").hide();
    }

    function myFunction() {
        var fromdate = $("#fromdate").val();
    var todate = $("#todate").val();
    var port = $("#port").val();
    var vessel = $("#vessel").val();

    loadingBar();

    $.get('/Import/Reports/ViewGetContainerTerminalReport', {
        port: port,
    vessel: vessel,
    fromdate: fromdate,
    todate: todate
        }, function (data) {
        loadingBarHide();
    $('#reportData').html(data);
        });
    }
