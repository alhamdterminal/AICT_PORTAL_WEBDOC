

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

        loadingBar();

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;
    var party = $('#party').select2().val();


    console.log("fromdate", fromdate);
    console.log("todate", todate);
    console.log("party", party);


    $.get('/Export/Reports/ViewBreakupofCashierDailySaleReportExport?fromdate=' + fromdate + '&toDate=' + todate + '&party=' + party, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }

    function formfiled() {

    }

    $(function () {

        $('#party').attr("disabled", true);

    $.get('/Import/PartyLedger/GetLoginUserInfo', function (data) {
        console.log(data);

    if (data != 0) {
        $('#party').val(data).trigger('change.select2');


            }
    else {
        $('#party').attr("disabled", false);

            }
        });

    });


