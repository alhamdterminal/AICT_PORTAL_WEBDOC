

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
    var Principle = document.getElementById("shippingagent").value;
    var port = document.getElementById("portAndTerminalId").value;
    var Transporter = document.getElementById("transporterId").value;
    var Goods = document.getElementById("goodsHeadId").value;
    var Cargo = document.getElementById("cargoType").value;
    var Type = document.getElementById("containerType").value;

    console.log("fromdate", fromdate);
    console.log("todate", todate);


    $.get('/Import/Reports/ViewArrivalBoxWise?fromdate=' + fromdate + '&' + 'toDate=' + todate + '&port=' + port + '&Principle=' + Principle + '&Transporter=' + Transporter  + '&Type=' + Type + '&Cargo=' + Cargo + '&Goods=' + Goods
    , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }

    function formfiled() {

    }




