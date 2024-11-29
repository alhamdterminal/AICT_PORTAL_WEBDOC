


    var container = "";

    function container_changed(data) {
        container = "";
    container = data.value;
    }




    function printreport() {

        var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    console.log("container", container);
    console.log("fromdate", fromdate);
    console.log("todate", todate);

    loadingBar();

    $.get('/Import/Reports/Viewinvoicelist?container=' + container, '&fromdate=' + fromdate + '&toDate=' + todate, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });
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

    function formfiled() {

    }
