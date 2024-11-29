




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
          
        var shippingAgent = document.getElementById("shippingAgent").value;
    var port = document.getElementById("port").value;

    loadingBar();

    $.get('/Import/Reports/ViewPendingContainer?line=' + shippingAgent + '&port=' + port  , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }




    function formfiled() {


    }