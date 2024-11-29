

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
    $.get('/import/reports/ViewTariffSummaryReport', function (data) {
        loadingBarHide();
    $('#repotdata').html(data);

    window.print();
        });
    });

    function formfiled() {

    }