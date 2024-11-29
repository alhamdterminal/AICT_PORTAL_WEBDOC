



    function refresh() {
        window.location.reload();
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


        //$("#invoice").change(function () {


        //    var invoicenumber = $("#invoice option:selected").text();
        //    $.get('/APICalls/GetReceivedAmount?invoicenumber=' + invoicenumber, function (data) {

        //        $('#balance').val(data.billAmount);
        //        $('#salestax').val(data.salesTax);
        //        $('#bill').val(data.billAmount);

        //    });

        //    $.get('/APICalls/GetConsigneeByInvoiceNo?invoicenumber=' + invoicenumber, function (res) {

        //        console.log("res", res)

        //        $('#consignee').val(res);
        //    });


        //});

    });



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function submitAmountReceived() {

        var party = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");

    if (party) {


            var balanceLedgerAmount = document.getElementById("balanceLedgerAmt").value;

    //var cash = parseInt(document.getElementById("cash").value != "" ? document.getElementById("cash").value : 0);

    var cheque = parseInt(document.getElementById("cheque").value != "" ? document.getElementById("cheque").value : 0);

    var invoiceAmount = parseInt($('#balance').val());

            //if (invoiceAmount == 0) {

            //    showToast('Invoice Already Settled!', 'error');

            //    return;

            //}

            if ((cheque - invoiceAmount) == 0) {

        $('#balance').val(parseInt(invoiceAmount - cheque));

    if ($('#adjustChk').is(":checked")) {

        $('#balanceLedgerAmt').val(balanceLedgerAmount - cheque);
                }

    var values = $('#AmountReceivedForm').serialize();

    $.post('/Import/PartyLedger/AddAmountReceived?' + values, function (res) {

                    if (res.error == true) {

        showToast(res.message, "error");

    window.setInterval('refresh()', 3000);

    return false;
                    }
    else {

        showToast(res.message, "success");
    window.setInterval('refresh()', 3000);

                    }

                });
                // }


            }
    else {

        showToast('Please settle the amount!', 'error');
            }
        }
    else {
        showToast("please select party", "error");


        }


    }

    function onChangeParty(partyId) {
        //var partyId = $("#party option:selected").val();

        $.get('/APICalls/GetBalanceLedgerPartyId?partyId=' + partyId, function (data) {

            console.log("data", data)

            $('#balanceLedgerAmt').val(data);
        })

    }

    function formfiled() {

        //if (PermissionInputs.find(x => x.fieldName == "InvoiceId" && x.isChecked == false)) {

        //    document.getElementById('invoice').disabled = true;

        //}
        //if (PermissionInputs.find(x => x.fieldName == "AmountReceivedDate" && x.isChecked == false)) {

        //    document.getElementById('single_cal1').disabled = true;

        //}
        ////if (PermissionInputs.find(x => x.fieldName == "PartyId" && x.isChecked == false)) {

        ////    document.getElementById('party').disabled = true;

        ////}
        //if (PermissionInputs.find(x => x.fieldName == "BalanceAmount" && x.isChecked == false)) {

        //    document.getElementById('balance').disabled = true;

        //}
        //if (PermissionInputs.find(x => x.fieldName == "SalesTax" && x.isChecked == false)) {

        //    document.getElementById('salestax').disabled = true;

        //}
        //if (PermissionInputs.find(x => x.fieldName == "BillAmount" && x.isChecked == false)) {

        //    document.getElementById('bill').disabled = true;

        //}
        //if (PermissionInputs.find(x => x.fieldName == "CashAmount" && x.isChecked == false)) {

        //    document.getElementById('cash').disabled = true;

        //}
        //if (PermissionInputs.find(x => x.fieldName == "ChequeAmount" && x.isChecked == false)) {

        //    document.getElementById('cheque').disabled = true;

        //}
        //if (PermissionInputs.find(x => x.fieldName == "Discount" && x.isChecked == false)) {

        //    document.getElementById('discount').disabled = true;

        //}
        //if (PermissionInputs.find(x => x.fieldName == "BalanceLedgerAmount" && x.isChecked == false)) {

        //    document.getElementById('balanceLedgerAmt').disabled = true;

        //}

        getpartyList();

    getpartyListcheckreceved();
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
                        onChangeParty(e.value);
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



    var partyListcheckreceved = [];
    function getpartyListcheckreceved() {
        $.get('/Import/PartyLedger/GetALlparties', function (res) {
            if (res) {
                partyListcheckreceved = res;
                $("#searchBoxForcheckreceived").dxSelectBox({
                    dataSource: {
                        store: partyListcheckreceved,
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
            else {
                consigneeList = []
                $("#searchBoxForcheckreceived").dxSelectBox({
                    dataSource: {
                        store: partyListcheckreceved,
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


    function checkPaymentValidations() {



        //if (!$('#paymnetbank').val()) {

        //    $('#bankerror').html('This is a required field');
        //}
        //else {
        //    $('#bankerror').html('');
        //}

        //if (!$('#paymentamount').val()) {

        //    $('#paymentamounterror').html('This is a required field');
        //}
        //else {
        //    $('#paymentamounterror').html('');
        //}

        //if (!$('#paymenttype').val()) {

        //    $('#paymenttypeerror').html('This is a required field');
        //}
        //else {
        //    $('#paymenttypeerror').html('');
        //}



    }


    function submitChequeReceived() {


        var party = $("#searchBoxForcheckreceived").dxSelectBox('instance').option("value");

    if (party) {
            
            var paymenttype = $("#paymenttype").val();

    if (!paymenttype) {
                return showToast("Type is not avaible", "error")
            }


    var paymnetbank = $("#paymnetbank").val();

    if (!paymnetbank && paymenttype != "Cash") {
                return showToast("Bank is not avaible", "error")
            }


    if (paymnetbank && paymenttype == "Cash") {
                return showToast(" please un select Bank", "error")
            }

    var paymentamount = $("#paymentamount").val();

    if (!paymentamount) {
                return showToast("Amount is not avaible", "error")
            }


    var paymentnumber = $("#paymentnumber").val();

    if (!paymentnumber && (paymenttype != "Cash" && paymenttype != "FundTransferOnline") ) {
                return showToast("Instrument Number is not avaible", "error")
            }

    if (paymentnumber && (paymenttype == "Cash" || paymenttype == "FundTransferOnline")) {
                return showToast("Instrument Number is not required for cash and FundTransferOnline", "error")
            }

    var shippingagentId = $('#shippingagentId').select2().val();


    $('#paymentchequeNumbererror').html('');
    var values = $('#ChequeReceivedForm').serialize();
    console.log(values);

    var postUrl = '/Import/PartyLedger/AddChequeReceive?' + values + '&shippingagentId=' + shippingagentId;

    console.log("ok")
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
    else {
        showToast("please select party", "error");


    }


      

    }