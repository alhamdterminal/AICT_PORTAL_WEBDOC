

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
    var waiverno = url.searchParams.get("waiverno");
    var type = url.searchParams.get("type");
    var containerid = url.searchParams.get("containerid");

    console.log("waiverno", waiverno)
    console.log("type", type)
    console.log("containerid", containerid)

    loadingBar();
    $.get('/Import/Reports/ViewRequestForWaiver?containerId=' + containerid + '&type=' + type + '&waiverno=' + waiverno, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });
    });



    function formfiled() {

    }
