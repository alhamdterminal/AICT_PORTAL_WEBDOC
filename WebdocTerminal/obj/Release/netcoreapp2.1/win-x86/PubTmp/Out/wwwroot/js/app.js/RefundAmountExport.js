
    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function submitRefundAmount() {


        var f = document.getElementById('RefoundAmountForm');

    if (f.checkValidity()) {


            var values = $('#RefoundAmountForm').serialize();

    var KnockOf = $('#knockofinvoice').val();

    $.post("/Export/PartyLedgerExport/AddRefundAmountExport?values" + values + '&KnockOf=' + KnockOf, function (data) {

                if (data.error == true) {
        showToast(data.message, "error");
                }
    else {
        showToast(data.message, "success");

    window.location.href = window.location.href;
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
        if (PermissionInputs.find(x => x.fieldName == "OrderNumber" && x.isChecked == false)) {

        document.getElementById('orderNumber').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ChequeDate" && x.isChecked == false)) {

        document.getElementById('single_cal2').disabled = true;
        }


    }



    function checkValidations() {


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

    if (!$('#knockofinvoice').val()) {

        $('#knockofinvoiceerror').html('This is a required field');
        }
    else {
        $('#knockofinvoiceerror').html('');
        }


    if (!$('#debitAmount').val()) {

        $('#debitAmounterror').html('This is a required field ');
        }
    else {
        $('#debitAmounterror').html('');
        }


    if (!$('#chequenumber').val()) {

        $('#chequenumbererror').html('This is a required field');
        }
    else {
        $('#chequenumbererror').html('');
        }



    }


