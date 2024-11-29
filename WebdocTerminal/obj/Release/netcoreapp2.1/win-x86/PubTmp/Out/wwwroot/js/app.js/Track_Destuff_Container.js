

    function formfiled() {
    }

    function myFunction() {


        var Shippingline = document.getElementById("Shippinglineid").value;
    var Goodshead = document.getElementById("GoodsHeadId").value;

    loadingBar();




    $.get('/Import/Reports/ViewTrack_Destuff_Container?Shippingline=' + Shippingline + '&Goodshead=' + Goodshead, function (data) {

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
