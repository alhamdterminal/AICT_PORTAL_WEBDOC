


    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var BLNumber = url.searchParams.get("BLNumber");
    if (BLNumber) {
        loadingBar();

    $.get('/Import/Reports/CrossStuffingReport?BLNumber=' + BLNumber, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });
        }
      
    });


    function myFunction() {

        var blno = $("#bllist").dxAutocomplete("instance").option("value");

    loadingBar();
    $.get('/Import/Reports/CrossStuffingReport?BLNumber=' + blno, function (data) {

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
