


    function formfiled() {
    }

    function myFunction() {
        var fromdate = document.getElementById("fromdate").value;

    var todate = document.getElementById("todate").value;

    console.log("fromdate", fromdate);
    console.log("todate", todate);


    var agent = document.getElementById("clearingagentid").value;


    loadingBar();




    $.get('/Import/Reports/ViewInvoiceSummaryAgentWise?fromdate=' + fromdate + '&todate=' + todate + '&agent=' + agent, function (data) {

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


