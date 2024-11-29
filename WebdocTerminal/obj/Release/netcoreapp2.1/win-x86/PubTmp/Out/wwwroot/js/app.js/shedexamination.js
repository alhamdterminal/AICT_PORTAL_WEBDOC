

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

    function myFunction() {

        loadingBar();


    var fromdate = document.getElementById("fromdate").value;

    var todate = document.getElementById("todate").value;

    var commodity = document.getElementById("commodity").value;

    console.log("fromdate", fromdate);
    console.log("todate", todate);




    $.get('/Import/Reports/Viewshedexamination?fromdate=' + fromdate + '&' + 'todate=' + todate + '&commodity=' + commodity, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }

    function formfiled() {

    }

