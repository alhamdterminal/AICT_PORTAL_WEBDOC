﻿


    $(function () {
        myFunction();

    });
    function myFunction() {
        loadingBar();
    printReport();

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

    function printReport() {

        $.get('/Import/Reports/ViewIGMReceivedContainerNotArrive', function (data) {

            loadingBarHide();

            $('#repotdata').html(data);
        });

    }


    function formfiled() {

    }