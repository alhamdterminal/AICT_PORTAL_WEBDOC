
    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function submitRefundAmount() {


        var party = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");

    if (party) {
            var f = document.getElementById('RefoundAmountForm');


    console.log("type", $('#type').val())
    console.log("chequenumber", $('#chequenumber').val())




    if (f.checkValidity()) {
        console.log("f", f)

                if ($('#type').val() != "Cash" && !$('#chequenumber').val()) {

        $('#chequenumbererror').html('This is a required field');
                }
    else {
        $('#chequenumbererror').html('');


    var values = $('#RefoundAmountForm').serialize();

    console.log("values", values)
    var KnockOf = $('#knockofinvoice').val();

    $.post("/Import/PartyLedger/AddRefundAmount?values" + values + '&KnockOf=' + KnockOf, function (data) {

                        if (data.error == true) {
        showToast(data.message, "error");
                        }
    else {
        showToast(data.message, "success");

    window.location.href = window.location.href;
                        }


                    });

                }




            }

    checkValidations();
        }
    else {
        showToast("please select party", "error");

          
        }

        

    }

    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Date" && x.isChecked == false)) {

        document.getElementById('single_cal1').disabled = true;

        }
        //if (PermissionInputs.find(x => x.fieldName == "PartyId" && x.isChecked == false)) {

        //    document.getElementById('party').disabled = true;

        //}
        if (PermissionInputs.find(x => x.fieldName == "BankId" && x.isChecked == false)) {

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

    getpartyList();
    }




    var partyList = [];
    function getpartyList() {
        $.get('/Import/PartyLedger/GetALlparties', function (res) {
            if (res) {
                partyList = res;
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: partyList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'PartyId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                    onValueChanged: function (e) {
                        console.log("e", e);

                        //LoadAgentTariffGrid();
                        //getsharerates();
                    }
                })
            }
            else {
                consigneeList = []
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: partyList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'PartyId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,

                    onValueChanged: function (e) {
                        console.log("e", e);
                    }

                })
            }
        });
    }





    function checkValidations() {


        //if (!$('#party').val()) {

        //    $('#partyerror').html('This is a required field');
        //}
        //else {
        //    $('#partyerror').html('');
        //}  

       
        if (!$('#bank').val() ) {

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

    if (!$('#type').val()  ) {

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



    }

