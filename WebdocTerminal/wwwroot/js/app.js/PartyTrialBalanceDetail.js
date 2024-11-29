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

    var fromdate = url.searchParams.get("fromdate");
    var todate = url.searchParams.get("todate");
    var customerid = url.searchParams.get("customerid")
    var accountid = url.searchParams.get("accountid")
    var iscredit = url.searchParams.get("iscredit");
    var type = url.searchParams.get("type");


    console.log(customerid, accountid , iscredit, type, fromdate, todate);

    loadingBar();

    $.post('/Account/Reports/ViewPartyTrialBalanceDetail?customerid=' + customerid + '&' + 'accountid=' + accountid +
    '&' + 'IsCredit=' + iscredit + '&' + 'Type=' + type + '&' + 'fromdate=' + fromdate + '&' + 'ToDate=' + todate, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });
    });

    function formfiled() {

    }
