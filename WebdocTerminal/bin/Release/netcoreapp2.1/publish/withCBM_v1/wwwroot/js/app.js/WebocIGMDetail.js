

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


    var igm = "";

    function igm_changed(data) {
        igm = "";
    igm = data.value;
    }
    function myFunction() {

        loadingBar();

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    console.log("fromdate", fromdate);
    console.log("todate", todate);
    console.log("igm", igm);

    if (igm == null || igm == undefined) {
        igm = "";
        }

    console.log("igmigmigm", igm)
    $.get('/import/reports/viewwebocigmdetail?fromdate=' + fromdate + '&todate=' + todate + '&igmnumber=' + igm, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    igm = "";

    }

    function formfiled() {

    }



