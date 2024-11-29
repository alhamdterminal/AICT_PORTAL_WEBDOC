

    //function changeType() {
        //    var type = ChequeReceivedForm.elements["Type"].value;
        //    if (type == 'Cash') {
        //        ChequeReceivedForm.elements["Number"].disabled = true;
        //    }
        //    else {
        //        ChequeReceivedForm.elements["Number"].disabled = false;

        //    }
        //}

        function showToast(message, icon) {

            $.toast({
                heading: message,
                showHideTransition: 'slide',
                position: 'bottom-right',
                icon: icon
            });
        }

    function submitChequeReceived() {

        var f = document.getElementById('ChequeReceivedForm');

    if (f.checkValidity()) {

        console.log(f)
            console.log(f.checkValidity())
    var values = $('#ChequeReceivedForm').serialize();
    console.log(values);
    var postUrl = '/Import/PartyLedger/AddChequeReceive?' + values;

    $.post(postUrl, function (data) {

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
        if (PermissionInputs.find(x => x.fieldName == "PartyId" && x.isChecked == false)) {

        document.getElementById('party').disabled = true;

        }
        if (PermissionInputs.find(x => x.fieldName == "BankId" && x.isChecked == false)) {

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
        if (PermissionInputs.find(x => x.fieldName == "Number" && x.isChecked == false)) {

        document.getElementById('number').disabled = true;

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


    if (!$('#number').val()) {

        $('#chequeNumbererror').html('This is a required field');
        }
    else {
        $('#chequeNumbererror').html('');
        }
         
    }


