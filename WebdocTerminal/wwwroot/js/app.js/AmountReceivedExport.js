



    var type = '';
    var ctoc;

    $(function () {



        $("#invoice").change(function () {
            var invoiceExportId = this.value;

            $.get('/APICalls/GetReceivedAmountExport?invoiceExportId=' + invoiceExportId, function (data) {

                $('#balance').val(data.billAmount);
                $('#salestax').val(data.salesTax);
                $('#bill').val(data.billAmount);

            });


            //  getCreditToCustomer(invoiceExportId);


        });

    });

    function getCreditToCustomer(id) {

        console.log("id", id);

    $.get('/Export/PartyLedgerExport/GetCreditToCustomer?Id=' + id, function (data) {
        ctoc = data;
    console.log("ctoc", ctoc);
        });


    }

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


        if (!$('#party').val()) {

        showToast("please select party", "error");
        }
    else {
        $('#partyerror').html('');

    var balanceLedgerAmount = document.getElementById("balanceLedgerAmt").value;

    var cash = parseInt(document.getElementById("cash").value != "" ? document.getElementById("cash").value : 0);

    var cheque = parseInt(document.getElementById("cheque").value != "" ? document.getElementById("cheque").value : 0);

    var invoiceAmount = parseInt($('#balance').val());


    if (((cash + cheque) - invoiceAmount) == 0) {

        $('#balance').val(parseInt(invoiceAmount - (cash + cheque)));

    if ($('#adjustChk').is(":checked")) {

        $('#balanceLedgerAmt').val(balanceLedgerAmount - (cash + cheque));
                }

    var values = $('#AmountReceivedForm').serialize();

    $.post('/Export/PartyLedgerExport/AddAmountReceivedExport?' + values + '&type=' + type, function (res) {

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


    }


    function refresh() {
        window.location.reload();
    }

    function onChangeParty() {
        var partyExportId = $("#party option:selected").val();

    $.get('/APICalls/GetExportPartyBalanceLedgerPartyId?partyId=' + partyExportId, function (data) {


        console.log("data", data)
            $('#balanceLedgerAmt').val(data);


        })

    }


    function getInvoices() {

        window.location.reload();
    }


    function formfiled() {

    }

