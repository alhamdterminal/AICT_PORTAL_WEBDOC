

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

    $.get('/Import/Reports/ViewBoxesLyingAtPortContainerWise', function (data) {

        loadingBarHide();

    $('#repotdata').html(data);

        });


    });

    function formfiled() {

    }