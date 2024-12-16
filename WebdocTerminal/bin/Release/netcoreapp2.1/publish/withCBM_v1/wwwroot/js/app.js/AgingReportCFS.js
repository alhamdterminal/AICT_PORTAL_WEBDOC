

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

    var shippingagentId = document.getElementById("shippingagent").value;
    var descTtype = document.getElementById("descTtype").value;

    console.log("fromdate", fromdate);
    console.log("todate", todate);


    $.get('/Import/Reports/ViewAgingReportCFS?fromdate=' + fromdate + '&' + 'todate=' + todate + '&' + 'shippingagentId=' + shippingagentId
    + '&' + 'descTtype=' + descTtype
    , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }

    function formfiled() {

    }




