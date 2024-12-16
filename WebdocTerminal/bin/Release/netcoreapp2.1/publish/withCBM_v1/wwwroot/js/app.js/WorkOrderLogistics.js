
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
    var workordno = $('#workorderno').val();
    var port = $('#port').val();

    console.log(fromdate)
    console.log(todate)
    console.log(workordno)
    console.log(port)


    $.post('/import/Reports/ViewWorkOrderLogistics?workordno=' + workordno + '&' + 'port=' + port + '&' +
    'fromdate=' + fromdate + '&' + 'todate=' + todate, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });

    }
