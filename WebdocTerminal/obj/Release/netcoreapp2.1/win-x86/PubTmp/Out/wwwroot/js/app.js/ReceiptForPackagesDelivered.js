
    function print() {

        var containerIndexId = $("#containerIndexdiv option:selected").val();

    loadingBar();

    $.get('/Import/Reports/ViewReceiptForPackagesDelivered?containerIndexId=' + containerIndexId, function (data) {
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


    function containerCallback() {

    }