

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

    var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;
    var shippingagentId = $('#shippingagentId').select2().val();

    console.log("fromdate", fromdate);
    console.log("todate", todate);
    console.log("shippingagentId", shippingagentId);


    $.get('/Import/Reports/ViewDailySalesReport?fromdate=' + fromdate + '&' + 'todate=' + todate + '&shippingagentId=' + shippingagentId, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
        });

    }

    function formfiled() {

    }
    $(function () {

        $('#shippingagentId').attr("disabled", true);

    $.get('/Import/PartyLedger/GetLoginUserInfo', function (data) {
        console.log(data);

    if (data != 0) {
        $('#shippingagentId').val(data).trigger('change.select2');


            }
    else {
        $('#shippingagentId').attr("disabled", false);

            }
        }); 

    });
