



    var type = '';

    $(function () {

        console.log("Test")

        getChequeType();


    $("#invoice").change(function () {
            var invoiceid = this.value;

    $.get('/APICalls/GetReceivedAmount?InvoiceId=' + invoiceid, function (data) {

        $('#balance').val(data.billAmount);
    $('#salestax').val(data.salesTax);
    $('#bill').val(data.billAmount);

            });
        });

    });

    function getChequeType() {
        $.get('/Export/PartyLedgerExport/GetLastChequeReceiveType', function (data) {
            type = data;
        });
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function submitAmountReceived() {

        var balanceLedgerAmount = document.getElementById("balanceLedgerAmt").value;

    var cash = parseInt(document.getElementById("cash").value != "" ? document.getElementById("cash").value : 0);

    var cheque = parseInt(document.getElementById("cheque").value != "" ? document.getElementById("cheque").value : 0);

    var invoiceAmount = parseInt($('#balance').val());

    if (invoiceAmount == 0) {

        showToast('Invoice Already Settled!', 'error');

    return;

        }

    if (((cash + cheque) - invoiceAmount) == 0) {

        $('#balance').val(parseInt(invoiceAmount - (cash + cheque)));

    if ($('#adjustChk').is(":checked")) {

        $('#balanceLedgerAmt').val(balanceLedgerAmount - (cash + cheque));
            }

    var values = $('#AmountReceivedForm').serialize();

    $.post('/Export/PartyLedgerExport/AddAmountReceivedExport?' + values + '&type=' + type, function (res) {

        showToast('Amount Received!', 'success');

            });
        }
    else {

        showToast('Please settle the amount!', 'error');
        }



    }

    function onChangeParty() {
        var partyId = $("#party option:selected").val();

    $.get('/APICalls/GetBalanceLedgerAmountByPartyExportId?partyId=' + partyId, function (data) {

        $('#balanceLedgerAmt').val(data.balance);
        })

    }


    function getInvoices() {

        window.location.reload();
    }

