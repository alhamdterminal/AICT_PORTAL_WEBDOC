

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


    var igmno = "";

    function igm_changed(data) {
        igmno = "";
    igmno = data.value;
    }
    function myFunction() {

        loadingBar();

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    console.log("fromdate", fromdate);
    console.log("todate", todate);
    console.log("igmno", igmno);

    if (igmno == null || igmno == undefined) {
        igmno = "";
        }

    console.log("igmigmigm", igmno)
    $.get('/import/reports/ViewMaerskArrival?fromdate=' + fromdate + '&todate=' + todate + '&igmno=' + igmno, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    igm = "";

    }

    function formfiled() {

    }

