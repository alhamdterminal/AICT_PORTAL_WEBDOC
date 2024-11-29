

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


    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    var billNo = url.searchParams.get("BillNo");




    loadingBar();

    $.get('/Export/Reports/ViewExportBillInvoice?BillNo=' + billNo, function (data) {

        loadingBarHide();

    console.log("data", data)

    $('#repotdata').html(data);


        });
    });


    function formfiled() {
        console.log(document.getElementsByClassName('dxrd-toolbar-item'))
    }

