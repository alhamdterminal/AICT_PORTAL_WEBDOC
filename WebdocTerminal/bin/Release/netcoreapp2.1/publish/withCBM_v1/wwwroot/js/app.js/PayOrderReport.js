


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

    var payorderNumber = "";
    document.onload = $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    payorderNumber = url.searchParams.get("payorderNumber");
    bankcode = url.searchParams.get("bankcode");

    if (bankcode) {

        loadingBar();
    $.get('/Import/Reports/ViewPayOrderReport?PayOrderNumber=' + payorderNumber + '&' + 'bankName=' + bankcode, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });
        }
    else {
        showToast("Please Select Bank", "warning");

        }


    });

    function myFunction() {



        var bank = $('#bank').val();



    console.log("bank", bank);

    if (bank) {

        loadingBar();
    $.get('/Import/Reports/ViewPayOrderReport?PayOrderNumber=' + payorderNumber + '&' + 'bankName=' + bank, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });
        }
    else {
        showToast("Please Select Bank", "warning");

        }




    }



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function formfiled() {


    }
