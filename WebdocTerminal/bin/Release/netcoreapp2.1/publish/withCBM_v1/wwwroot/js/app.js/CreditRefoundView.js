


    //var UnSettledCreditAllowsList = [];
    //var selecteddatas = [];
    //var paymentReceivedIds = "";

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    $(function () {
        getbanks();
    $('#natureOfAmount').on('change', function (data) {

        getnaturefoamountvalue()

    });

    $('#paymentReceivedId').on('change', function (data) {

        myFunction();
            
        });
    });



    function myFunction() {


        var paymentReceivedId = $('#paymentReceivedId option:selected').val();

    if (paymentReceivedId) {

        $.get('/Import/Setup/GetCreditAllowedAmount?paymentReceivedIds=' + paymentReceivedId, function (data) {
            $('#creditAllowedRsRefound').val(data);
        });

        } else {
        $('#creditAllowedRsRefound').val(0);
        } 

    }


    function getnaturefoamountvalue() {


        var res = $('#natureOfAmount').val();

    if (res == "KnockOff") {
        document.getElementById('invoicenodiv').style.display = "block"
            document.getElementById('KnockOffAmountDiv').style.display = "block"

    $("#receivedAmount").prop("readonly", true);
    $("#aictAccountNumber").prop("readonly", true);
        } else {
        document.getElementById('invoicenodiv').style.display = "none"
            document.getElementById('KnockOffAmountDiv').style.display = "none"

    $("#receivedAmount").prop("readonly", false);


        }


    if (res == "KnockOff" || res == "Cash" ||  res == "Online") {
        $("#InsturmentNo").prop("readonly", true);
        } else {
        $("#InsturmentNo").prop("readonly", false);

        }


    if (res == "PO" || res == "Cheque") {
        document.getElementById('bankdiv').style.display = "block"

    } else {
        document.getElementById('bankdiv').style.display = "none"
    }

    if (res == "PO" || res == "Cheque" || res == "Online" || res == "KnockOff") {
        document.getElementById('PaymentForDiv').style.display = "block"

    } else {
        document.getElementById('PaymentForDiv').style.display = "none"
    }


    $("#InoviceNo").val('');
    $("#KnockOffAmount").val('');
    $("#creditAllowed").val(0);
    $("#InsturmentNo").val('');
    $("#receivedAmount").val(0);
    $('#bank').select2().val('').trigger('change.select2');
    $('#PaymentFor').val('');



    }


    function resetOnlineReceiptCounterPayment() {
        $('#bank').select2().val('').trigger('change.select2');

    $('#receivedAmount').val('')
    $('#InsturmentNo').val('')
    $('#creditAllowed').val(0)
    var res = $('#natureOfAmount').val();
    if (res == "KnockOff") {
        $('#InoviceNo').val('')
            $('#KnockOffAmount').val('')
        }

    $('#PaymentFor').val('');

    }

    var PaymentNatureList = [];


    function PaymentReceivedGrid() {

        console.log("asd", PaymentNatureList)

        $("#PaymentReceiveGrid").dxDataGrid({
        dataSource: PaymentNatureList,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    paging: {
        pageSize: 10
            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."
            },

    editing: {
        mode: "row",
    allowDeleting: true,
            },
    columns: [
    {
        dataField: "aictBankName",
    caption: "Bank",
    width: 190,
    validationRules: [{type: "required" }],
    lookup: {
        dataSource: banksList,
    valueExpr: "bankId",
    displayExpr: "bankName"
                    }
                },
    {
        dataField: "receivedAmount",
    caption: "Received Amount",
    dataType: "number",
    format: "#,##0.##",
                },
    {
        dataField: "InsturmentNo",
    caption: "Insturment No",
    dataType: "number",
    format: "#,##0.##",
                },
    {
        dataField: "natureOfAmount",
    caption: "Nature Of Amount",
                },
    {
        dataField: "PaymentFor",
    caption: "Payment For",
                },
    ],


    onRowRemoved: function (e) {

    },
          

        });


    }

    function AddPaymentNatureList() {
         
            var res = $('#natureOfAmount').val();

    if (res == "Cash") {

                var receivedAmountres = $('#receivedAmount').val();

    console.log("receivedAmountres", receivedAmountres)
    if (!receivedAmountres) {
                    return alert("please add amount ");
                }
            }

    if (res == "PO" || res == "Cheque") {


                var bankres = $('#bank option:selected').val();

    console.log("bankres", bankres);

    if (!bankres) {
                    return alert("please select Bank");
                }


    var receivedAmountres = $('#receivedAmount').val();

    console.log("receivedAmountres", receivedAmountres);

    if (!receivedAmountres) {
                    return alert("please add amount ");
                }

    var InsturmentNores = $('#InsturmentNo').val();

    console.log("InsturmentNores", InsturmentNores);

    if (!InsturmentNores) {
                    return alert("please add Insturment No ");
                }

    var PaymentForres = $('#PaymentFor option:selected').val();

    console.log("PaymentForres", PaymentForres);

    if (!PaymentForres) {

                    return alert("please select Payment For");
                }


            }


    if (res == "Online") {

                var receivedAmountres = $('#receivedAmount').val();

    console.log("receivedAmountres", receivedAmountres);

    if (!receivedAmountres) {
                    return alert("please add amount ");
                }

    var PaymentForres = $('#PaymentFor option:selected').val();


    if (!PaymentForres) {
                    return alert("please select Payment For");
                }

            }


    if (res == "KnockOff") {


                var InoviceNores = $('#InoviceNo').val();

    console.log("InoviceNores", InoviceNores);

    if (!InoviceNores) {
                    return alert("please add Invoice No");
                }


    var KnockOffAmountres = $('#KnockOffAmount').val();

    console.log("KnockOffAmountres", KnockOffAmountres);

    if (!KnockOffAmountres) {
                    return alert("please Excess Amount Issue");
                }

    if (Number(KnockOffAmountres) <= 0) {
                    return alert("your Excess Amount <= 0");
                }

    var PaymentForres = $('#PaymentFor option:selected').val();


    if (!PaymentForres) {
                    return alert("please select Payment For");
                }

            }



    let x = {

        aictBankName: $("#bank option:selected").val(),
    bankId: $("#bank option:selected").val(),
    receivedAmount: Math.round($('#receivedAmount').val()),
    InsturmentNo: $('#InsturmentNo').val(),
    creditAllowed: $('#creditAllowed').val(),
    InoviceNo: $('#InoviceNo').val(),
    KnockOffAmount: $('#KnockOffAmount').val(),
    knockOffInvoiceNo: $('#InoviceNo').val(),
    natureOfAmount: $('#natureOfAmount').val(),
    PaymentFor: $('#PaymentFor').val(),

            }


    if (res == "KnockOff") {
        x.receivedAmount = x.KnockOffAmount
    }

    PaymentNatureList.push(x);

    resetOnlineReceiptCounterPayment();
    PaymentReceivedGrid();
 

    }


    var banksList = [];

    function getbanks() {

        $.get('/APICalls/GeBanks', function (res) {
            banksList = res;
        });

    }



    function formfiled() {

    }

    function savecreditrefund() {

        var paymentReceivedId = $('#paymentReceivedId option:selected').val();

    var creditAllowedRsRefound = $('#creditAllowedRsRefound').val();

    console.log("PaymentNatureList", PaymentNatureList);

    if (PaymentNatureList.length) {

            if (paymentReceivedId) {

                if (creditAllowedRsRefound > 0) {

        let sum = 0;
                    PaymentNatureList.filter(obj => {sum += obj.receivedAmount; });

    if (sum != creditAllowedRsRefound) {
        alert("refund amount and settlement amount are not match")

    }
    else {
        $.post('/Import/Setup/SaveCreditAllowRefoundSettlement?paymentReceivedIds=' + paymentReceivedId, { PaymentNatureList: PaymentNatureList }, function (data) {
            console.log("data", data)

            if (data.error == true) {
                showToast(data.message, "error");

            }
            else {
                showToast(data.message, "success");
                window.location.href = window.location.href;
            }
        });
                    }

                   



                } else {
        alert("no amount for refund")
    }
            }
    else {
        alert("please select invoice")
    }


        }
    else {
        alert("no data in list for post");
        }

  
    }



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }