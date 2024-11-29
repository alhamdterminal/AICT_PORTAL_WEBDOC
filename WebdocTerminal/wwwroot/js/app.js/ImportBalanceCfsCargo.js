




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
    var shippingAgent = document.getElementById("shippingAgent").value;
    var cargoType = document.getElementById("cargoType").value;
    var goodsHeadId = document.getElementById("goodsHeadId").value;

    loadingBar();

    $.get('/Import/Reports/ViewImportBalanceCfsCargo?shippingAgent=' + shippingAgent + '&cargoType=' + cargoType + '&fromdate=' + fromdate + '&todate=' + todate + '&goodsHeadId=' + goodsHeadId, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }




    function formfiled() {


    }