




    var consigneeList = [];

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

        

        var fromdate = $('#single_cal2').val();
    var todate = $('#single_cal3').val();
    var fromrange = $('#fromrange').val();
    var torange = $('#torange').val();
    var amountRange = $('#amountRange').val();

    var date = $('#single_cal4').val();
    var branch = $('#branch').val();
    var bankid = $('#bankid').val();
    var title = $('#title').val();
    var shippingagentId = $('#shippingagentId').select2().val();


    if (date && branch && bankid && title) {

        loadingBar();

    $.get('/Import/Reports/ViewPODetailBank?fromdate=' + fromdate + '&todate=' + todate + '&fromrange=' + fromrange + '&torange=' + torange + '&amountRange=' + amountRange
    + '&date=' + date + '&branch=' + branch + '&bankid=' + bankid + '&title=' + title + '&shippingagentId=' + shippingagentId, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
                });
        }

    else {
        alert("please select date , branch , bank and title")
    }

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