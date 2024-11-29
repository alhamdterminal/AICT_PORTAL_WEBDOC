


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
        console.log("test 12312")
        var type = "CFS";
    $.get('/Import/Reports/ViewListofUpcomingContainer?type=' + type, function (data) {
        loadingBarHide();

    console.log("test 1")
    //console.log("test repotdata", data)

    $('#repotdata').html(data);
        });

    }


    function formfiled() {

    }

