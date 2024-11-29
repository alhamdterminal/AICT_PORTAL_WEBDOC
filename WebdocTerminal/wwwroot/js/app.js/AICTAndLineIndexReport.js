

    function formfiled() {
    }

    function myFunction() {


        var Shippingline = document.getElementById("Shippinglineid").value;
    var Goodshead = document.getElementById("GoodsHeadId").value;

    if (Goodshead || Shippingline) {



        loadingBar();

    $.get('/Import/Reports/ViewAICTAndLineIndexReport?goodsheadid=' + Goodshead + '&shippingagentid=' + Shippingline, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

        }
    else {
        alert("please Select One FF OR Good")
    }





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