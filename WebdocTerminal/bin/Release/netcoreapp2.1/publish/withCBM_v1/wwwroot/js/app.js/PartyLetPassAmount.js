
    $(function () {
 
         var url_string = window.location.href
    var url = new URL(url_string);
    var partyId = url.searchParams.get("PartyId");
    console.log("partyId", partyId);
    $('#partyId').val(partyId).trigger('change.select2');

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
                  
    })

    function find() {

        var partyId = $('#partyId').select2().val();
    var shippingagentId = $('#shippingagentId').select2().val();

    var fromdate = $('#single_cal2').val();
    var todate = $('#single_cal3').val();

    loadingBar();

    $.get('/Import/Reports/ViewPartyLetPassAmountFromAgent?partyId=' + partyId + '&shippingAgent=' + shippingagentId + '&fromdate=' + fromdate + '&todate=' + todate , function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
        });
    }


    //function callChangefunc() {
        //    var partyId = $("#partyId option:selected").val();
        //    loadingBar();
        //    $.get('/Import/Reports/ViewPartyLetPassAmount?partyId=' + partyId, function (data) {
        //        loadingBarHide();
        //        $('#repotdata').html(data);
        //    });
        //}

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