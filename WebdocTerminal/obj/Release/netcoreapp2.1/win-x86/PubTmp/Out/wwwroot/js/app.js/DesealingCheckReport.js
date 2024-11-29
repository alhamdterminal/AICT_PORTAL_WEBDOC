


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


        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;



    console.log("fromdate", fromdate)
    console.log("todate", todate)

    loadingBar();
    $.get('/Import/Reports/ViewDesealingCheckReport?fromdate=' + fromdate + '&todate=' + todate, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });


    }




    function formfiled() {


    }
