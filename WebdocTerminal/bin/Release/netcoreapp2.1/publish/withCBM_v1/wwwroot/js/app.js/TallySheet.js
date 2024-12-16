



    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var containerNo = url.searchParams.get("containerNo");
    var virno = url.searchParams.get("virno");
    var orientation = url.searchParams.get("orientation");

    loadingBar();

    $.get('/Import/Reports/ViewTallySheet?containerNo=' + containerNo + "&virno=" + virno + "&orientation=" + orientation, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });
    });

    function submitTallySheet() {

    }

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

