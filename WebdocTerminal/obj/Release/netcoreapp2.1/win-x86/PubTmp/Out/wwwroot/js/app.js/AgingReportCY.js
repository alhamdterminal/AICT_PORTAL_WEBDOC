

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

        var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    var shippingagentId = document.getElementById("shippingagent").value;
    var descTtype = document.getElementById("descTtype").value;

    console.log("fromdate", fromdate);
    console.log("todate", todate);
    console.log("shippingagentId", shippingagentId);
    console.log("descTtype", descTtype);

    loadingBar();
    $.get('/Import/Reports/ViewAgingReportCY?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'shippingagentId=' + shippingagentId
    + '&' + 'descTtype=' + descTtype, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });


    }


    function formfiled() {



    }