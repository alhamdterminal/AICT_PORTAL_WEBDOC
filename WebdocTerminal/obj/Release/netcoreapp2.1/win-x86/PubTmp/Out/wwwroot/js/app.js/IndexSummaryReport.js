    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });
    var bar = document.getElementById("large-indicator");
    bar.style.display = 'block';
    }
    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }
    function myFunction() {

        loadingBar();

    var fromdate = $('#fromdate').val();
    var todate = $('#todate').val();
    var shippingagentid = $('#shippingagentid').val();

    console.log(fromdate)
    console.log(todate)
    console.log(shippingagentid)


    $.post('/import/Reports/ViewIndexSummaryReport?shippingagentid=' + shippingagentid + '&' +
    'fromdate=' + fromdate + '&' + 'todate=' + todate, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });

    }
