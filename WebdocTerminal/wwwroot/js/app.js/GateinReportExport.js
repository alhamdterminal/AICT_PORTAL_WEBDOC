

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
        
        var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    if (fromdate && todate) {
        loadingBar();
    $.get('/Export/Reports/ViewGateinReportExport?fromdate=' + fromdate + '&todate=' + todate, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

        } else {window.alert("Plese Select From & To Date")};
        
    }


    function formfiled() {

    }

