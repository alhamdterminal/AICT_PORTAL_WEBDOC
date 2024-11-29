

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

    console.log("fromdate", fromdate);
    console.log("todate", todate);




    $.get('/Import/Reports/ViewDoWithPendingGatepass?fromdate=' + fromdate + '&' + 'todate=' + todate, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }

    function formfiled() {

    }
