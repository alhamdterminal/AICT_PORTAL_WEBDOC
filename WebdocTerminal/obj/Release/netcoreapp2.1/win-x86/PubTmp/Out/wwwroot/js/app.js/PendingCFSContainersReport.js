
    $(function () {
        loadingBar();
    $.get('/Import/Reports/ViewPendingCFSContainersReport', function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });
    })

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