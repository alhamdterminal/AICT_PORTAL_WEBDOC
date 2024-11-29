

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

    var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var type = $("#type").val();


    console.log("fromdate", fromdate);
    console.log("todate", todate);
    console.log("type", type);


    $.get('/Import/Reports/ViewCashierDailySaleReport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'type=' + type , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }

    function formfiled() {

    }




