


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function submitChequeReceived() {
        var values = $('#ChequeReceivedForm').serialize();
    console.log(values);
    var postUrl = '/Export/PartyLedgerExport/AddChequeReceiveExport?' + values;

    $.post(postUrl, function (data) {
        showToast("Cheque Receive Created ", "success");
    document.getElementById("ChequeReceivedForm").reset();
        })
    }
