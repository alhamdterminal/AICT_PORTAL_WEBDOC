

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function submitChequeReceived() {

        var f = document.getElementById('ChequeReceivedExportForm');

    if (f.checkValidity()) {



            var values = $('#ChequeReceivedExportForm').serialize();

    console.log("values", values)

    var postUrl = '/Export/PartyLedgerExport/AddChequeReceiveExport?' + values;

    $.post(postUrl, function (data) {

                if (data.error) {
        showToast(data.message, "warning");
                }

    else {

        showToast(data.message, "success");

    document.getElementById("ChequeReceivedExportForm").reset();
                }

            })
        }
    checkValidations();
    }


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Date" && x.isChecked == false)) {

        document.getElementById('single_cal1').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Party" && x.isChecked == false)) {

        document.getElementById('party').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Bank" && x.isChecked == false)) {

        document.getElementById('bank').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Amount" && x.isChecked == false)) {

        document.getElementById('amount').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ChequeDate" && x.isChecked == false)) {

        document.getElementById('single_cal2').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Type" && x.isChecked == false)) {

        document.getElementById('type').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ChequeNumber" && x.isChecked == false)) {

        document.getElementById('number').disabled = true;
        }

    }

    function checkValidations() {

        console.log("party", $('#party').val())
        console.log("bank", $('#bank').val())
    console.log("amount", $('#amount').val())
    console.log("type", $('#type').val())
    console.log("number", $('#number').val())

    if (!$('#party').val()) {

        $('#partyerror').html('This is a required field');
        }
    else {
        $('#partyerror').html('');
        }
    if (!$('#bank').val()) {

        $('#bankerror').html('This is a required field');
        }
    else {
        $('#bankerror').html('');
        }

    if (!$('#amount').val()) {

        $('#amounterror').html('This is a required field');
        }
    else {
        $('#amounterror').html('');
        }

    if (!$('#type').val()) {

        $('#typeerror').html('This is a required field');
        }
    else {
        $('#typeerror').html('');
        }


    if (!$('#number').val()) {

        $('#chequeNumbererror').html('This is a required field');
        }
    else {
        $('#chequeNumbererror').html('');
        }

    }
