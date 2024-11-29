

var type;



$(function () {


    //$("#cnic").inputmask("99999-9999999-9");

    //$("#phoneNumber").inputmask("9999-9999999");


    //    var dtToday = new Date();

    //    var month = dtToday.getMonth() + 1;
    //    var day = dtToday.getDate();
    //    var year = dtToday.getFullYear();
    //    if (month < 10)
    //        month = '0' + month.toString();
    //    if (day < 10)
    //        day = '0' + day.toString();

    //    var minDate = year + '-' + month + '-' + day;

    PaymentReceivedGrid();

    $('#natureOfAmount').on('change', function (data) {

        getnaturefoamountvalue()

    });



    $('#agents').on('change', function (data) {

        getpartybalanceamount()

    });


});



function getpartybalanceamount() {
    var clearingAgentdropdownid = $('#agents').val();

    console.log("clearingAgentdropdownid", clearingAgentdropdownid)

    if (clearingAgentdropdownid) {
        $.get('/APICalls/GePartyBalanceByPartyID?partyid=' + clearingAgentdropdownid, function (res) {
            console.log(res);
            $('#ledgerBalanceAmount').val(res);

        });
    }
    else {
        console.log("0");
        $('#ledgerBalanceAmount').val(0);

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


    if (res == "KnockOff" || res == "Cash" || res == "CashRefund" || res == "Online") {
        $("#InsturmentNo").prop("readonly", true);
    } else {
        $("#InsturmentNo").prop("readonly", false);

    }


    if (res == "PO" || res == "Cheque" || res == "Online") {
        document.getElementById('bankdiv').style.display = "block"

    } else {
        document.getElementById('bankdiv').style.display = "none"
    }

    if (res == "PO" || res == "Cheque") {
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


function AddPaymentNatureList() {


    var res = $('#natureOfAmount').val();

    if (res == "Cash") {

        var receivedAmountres = $('#receivedAmount').val();

        console.log("receivedAmountres", receivedAmountres)
        if (!receivedAmountres) {
            //return showToast("please add amount " ,  "error");
            return alert("please add amount ");
        }


    }


    if (res == "CashRefund") {

        var receivedAmountres = $('#receivedAmount').val();

        var excessAmountres = $('#excessAmount').val();

        console.log("receivedAmountres", receivedAmountres)
        if (!receivedAmountres || Number(receivedAmountres) <= 0) {
            //return showToast("please add amount ", "error");
            return alert("please add amount ");
        }
        if (!excessAmountres || Number(excessAmountres) <= 0) {
            //return showToast("there is no excess amount", "error");
            return alert("there is no excess amount");
        }

        if (Number(receivedAmountres) > Number(excessAmountres) && Number(excessAmountres) > 0) {
            //return showToast("your refound amount is greater then excess amount", "error");
            return alert("your refound amount is greater then excess amount");
        }

    }


    if (res == "PO" || res == "Cheque") {


        var bankres = $('#bank option:selected').val();

        console.log("bankres", bankres);

        if (!bankres) {
            //return showToast("please select Bank", "error");
            return alert("please select Bank");
        }


        var receivedAmountres = $('#receivedAmount').val();

        console.log("receivedAmountres", receivedAmountres);

        if (!receivedAmountres) {
            //return showToast("please add amount ", "error");
            return alert("please add amount ");
        }



        var InsturmentNores = $('#InsturmentNo').val();

        console.log("InsturmentNores", InsturmentNores);

        if (!InsturmentNores) {
            //return showToast("please add Insturment No ", "error");
            return alert("please add Insturment No ");
        }

        var PaymentForres = $('#PaymentFor option:selected').val();

        console.log("PaymentForres", PaymentForres);

        if (!PaymentForres) {
            //return showToast("please select Payment For", "error");
            return alert("please select Payment For");
        }


    }


    if (res == "Online") {


        var bankres = $('#bank option:selected').val();

        console.log("bankres", bankres);

        if (!bankres) {
            //return showToast("please select Bank", "error");
            return alert("please select Bank");
        }


        var receivedAmountres = $('#receivedAmount').val();

        console.log("receivedAmountres", receivedAmountres);

        if (!receivedAmountres) {
            //return showToast("please add amount ", "error");
            return alert("please add amount ");
        }



    }


    if (res == "KnockOff") {


        var InoviceNores = $('#InoviceNo').val();

        console.log("InoviceNores", InoviceNores);

        if (!InoviceNores) {
            //return showToast("please add Invoice No", "error");
            return alert("please add Invoice No");
        }


        var KnockOffAmountres = $('#KnockOffAmount').val();

        console.log("KnockOffAmountres", KnockOffAmountres);

        if (!KnockOffAmountres) {
            //return showToast("please Excess Amount Issue", "error");
            return alert("please Excess Amount Issue");
        }

        if (Number(KnockOffAmountres) <= 0) {
            //return showToast("your Excess Amount <= 0", "error");
            return alert("your Excess Amount <= 0");
        }


    }



    let x = {

        aictBankName: $("#bank option:selected").val(),
        receivedAmount: Math.round($('#receivedAmount').val()),
        InsturmentNo: $('#InsturmentNo').val(),
        creditAllowed: $('#creditAllowed').val(),
        InoviceNo: $('#InoviceNo').val(),
        KnockOffAmount: $('#KnockOffAmount').val(),
        natureOfAmount: $('#natureOfAmount').val(),
        PaymentFor: $('#PaymentFor').val(),

    }

    //if (res == "CashRefund") {
    //    x.receivedAmount = ( - x.receivedAmount)
    //}

    if (res == "KnockOff") {
        x.receivedAmount = x.KnockOffAmount
    }


    console.log("PaymentNatureList  ", PaymentNatureList)

    if (PaymentNatureList.length) {
        var result = PaymentNatureList.find(t => t.InoviceNo == x.InoviceNo && t.InoviceNo != "");

        console.log("result  ", result)
        if (result == null) {
            PaymentNatureList.push(x);

        }
    }
    else {
        PaymentNatureList.push(x);
    }


    // PaymentNatureList.push(x);


    resetOnlineReceiptCounterPayment();
    PaymentReceivedGrid();


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


}


function FindRecord() {

    if (type == "CFS") {
        var containerIndexId = $("#indexdb option:selected").val();

        var clearingAgentId = $("#agents option:selected").val();

        if (containerIndexId && clearingAgentId) {
            postData = '&containerindexid=' + containerIndexId + '&ClearingAgentId=' + clearingAgentId;
        }
        else {
            //return showToast("agent or cfs index not selected","error")
            return alert("agent or cfs index not selected");
        }

    }
    else {

        var indexnumber = $("#containerIndexCYdb option:selected").val();
        var clearingAgentId = $("#agents option:selected").val();

        console.log(igm)
        console.log(indexnumber)
        console.log(clearingAgentId)
        if (igm && indexnumber && clearingAgentId) {

            postData = "&igm=" + igm + "&index=" + indexnumber + '&ClearingAgentId=' + clearingAgentId;
        }
        else {
            //return showToast("agent or cy index not selected", "error")
            return alert("agent or cy index not selected");
        }


    }


    var res = $('#InoviceNo').val();

    if (res) {

        $.get('/APICalls/GetKnockOfInvoiceByInvoiceNo?invoiceno=' + res + postData, function (res) {


            if (res.error == true) {
                $("#KnockOffAmount").val(0)
                //return showToast(res.message, "error");
                return alert(res.message);
            } else {
                $("#KnockOffAmount").val(res.message)
            }

        });

    } else {
        //return   showToast("please add invoice no", "error")
        return alert("please add invoice no");
    }




}

function showFilters() {

    type = $("input[name='type']:checked").val();

    resetAllValues();

    restformValues();

    $('#tfoot').html('');
    $.get('/APICalls/GetFilterIndexWise?Type=' + type, function (partial) {

        $('#filters').html(partial);

    })

    if (type == "CFS") {
        //document.getElementById('ContainerType').style.display = "block"
    } else {
        document.getElementById('ContainerType').style.display = "none"
    }



}



var containerCount = 1;
var sumcontainerSizeAmount = 0;

var BillDate = formatDate(new Date());



function showToast(message, icon) {

    $.toast({
        heading: message,
        showHideTransition: 'slide',
        position: 'bottom-right',
        icon: icon
    });
}


//CFS Index Callback
function containerChangeCallback(ornt) {

    resetAllValues();
    restformValues();

    // var buttn = document.getElementById("hidebutton");
    // buttn.style.display = 'block';

    containerIndexId = parseInt($('#indexdb').val());

    $.get('/APICalls/GetCFSContainerSize?ContainerIndexId=' + containerIndexId, function (resp) {

        var serialno = 0;

        $('#tbody').html('');
        $('#tfoot').html('');
        resp.forEach(r => {

            serialno += 1;

            $('#tbody').append("<tr>" +
                "<td>" + serialno + "</td>" +
                "<td>" + r.containerNo + "</td>" +
                "<td>" + r.size + "</td>" +
                "</tr>");
        })

        if (resp.length) {


            $('#tfoot').append("<tr>" +
                "<td>" + '' + "</td>" +
                "<td >" + 'Total  Containers ' + resp.length + "</td>" +
                "</tr>");
        }
    });


    getInvoice();
    getperviouslogDetail();
}

//CY Container Callback
function containerCallback() {



    resetAllValues();
    restformValues();
    //var buttn = document.getElementById("hidebutton");
    //buttn.style.display = 'block';


    $.get('/APICalls/GetCYContainerListBYIGM?igm=' + igm + "&indexNo=" + $("#containerIndexCYdb option:selected").val(), function (resp) {

        var serialno = 0;

        containerCount = resp.length;

        $('#tbody').html('');
        $('#tfoot').html('');

        resp.forEach(r => {
            serialno += 1;
            $('#tbody').append("<tr>" +
                "<td>" + serialno + "</td>" +
                "<td>" + r.containerNo + "</td>" +
                "<td>" + r.containerSize + "</td>" +
                "<td>" + r.containerAmount + "</td>" +
                "<td>" + r.stauts + "</td>" +
                "</tr>");
        });

        if (resp.length) {


            resp.forEach(c => {
                if (c.stauts == "UnDelivered") {
                    sumcontainerSizeAmount += c.containerAmount;
                }

            });

            $('#tfoot').append("<tr>" +
                "<td>" + '' + "</td>" +
                "<td>" + '' + "</td>" +
                "<td>" + '' + "</td>" +
                "<td>" + '' + "</td>" +
                "<td >" + 'Total Un Delivered Containers ' + resp.filter(x => x.stauts == "UnDelivered").length + "</td>" +
                "</tr>");
        }


    });

    getInvoice();
    getperviouslogDetail();
}

function resetAllValues() {


    LoadGrid('#tariffGrid', []);
    loadGridhold([]);
    $('#tbody').html('');
    BillDate = formatDate(new Date());
    containerCount = 1;
    sumcontainerSizeAmount = 0;
    $('#storageTotal').val(0);
    $('#previousBillAmount').val(0);
    $('#afterSalesTax').val(0);
    $('#totalamount').val(0);
    $('#grandTotal').val(0);

    $('#excessAmount').val(0);
    $('#balanceInoviceAmount').val(0);
    $('#inoviceTotalAmount').val(0);

    $('#description').val('');
    //$('#balanceAmount').val(0);

    $('#totalCharges').val(0);
    $('#totalChargesAfterWaiver').val(0);

    $('#gst').val(0);
    $('#totalInvoiceAmountExWaiver').val(0);
    $('#invoiceAmount').val(0);
    $('#waiverDiscountAmounte').val(0);

    $('#creditAllowedAmount').val(0);

    $('#billTOLINEIncGSTD').val(0);

    $('#knockOffAmountg').val(0);
    $('#paymentReceivedh').val(0);

    $('#counterCollection').val(0);

    $('#gstonBILLTOLINE').val(0);
    $('#totalBTLAmount').val(0);


    $('#exBillToLine').val(0);
    $('#balanceAmountTotal').val(0);

    //working required

    $('#igmCBM').val(0);
    $('#destuffCBM').val(0);
    $('#examinationCBM').val(0);
    $('#ffCBM').val(0);

    $('#CBM').val(0);
    $('#Weight').val(0);
    $('#MainfestedWeight').val(0);
    $('#storageDays').val(0);
    $('#storageFreeDaysType').val('');
    $('#storageApplicableon').val('');
    $('#additionalFreeDays').val(0);
    $('#totalFreeDays').val(0);
    $('#totalBalanceCargo').val(0);
    $('#totalPartContainers').val(0);
    $('#weightInTon').val(0);
    $('#actualweightInTon').val(0);
    $('#billToLineAmount').val(0);
    $('#waiverDiscountAmount').val(0);
    $('#otherChargeAmount').val(0);
    // $('#portChargeAmount').val(0);
    $('#sealCharger').val(0);
    $('#cyStorageSizeAmount').val(0);
    $('#vehicleCharges').val(0);
    $('#weightmentCharges').val(0);
    $('#handingCharges').val(0);
    $('#auctionSalesTax').val(0);
    $('#auctionGrandTotal').val(0);
    $('#tariffCode').val('');
    $('#pass1').val('');

    PaymentNatureList = [];
    PaymentReceivedGrid();
    resetOnlineReceiptCounterPayment();

}

function showToast(message, icon) {

    $.toast({
        heading: message,
        showHideTransition: 'slide',
        position: 'bottom-right',
        icon: icon
    });
}

function getInvoice() {

    if (type == "CFS") {



        containerIndexId = $("#indexdb option:selected").val();


        $.get('/APICAlls/GetInvoice?ContainerIndexId=' + containerIndexId, function (data) {

            if (data) {

                if (data.destuffDate != null) {

                    $('#destuffdate').val(new Date(data.destuffDate.split("T")[0]).toISOString().substr(0, 10));
                } else {
                    $('#destuffdate').val('');
                }
                if (data.gateInDate != null) {

                    $('#gateindate').val(new Date(data.gateInDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#gateindate').val('');
                }
                if (data.invoiceDate != null) {

                    $('#deliveryDate').val(new Date(data.invoiceDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#deliveryDate').val('');
                }

                $('#shippingAgetName').val(data.shippingAgentName);
                $('#WaiveRemarks').val(data.lastWaiverRemarks);
                $('#previousWaiVerAmount').val(data.previousWaiVerAmount);
                $('#exchangeRateAmount').val(data.exchangeRateAmount);
                $('#creditAllowed').val(data.creditAmount);
                $('#creditAllowedAmount').val(data.creditAmount);



                if (Number(data.creditAmount) > 0) {

                    console.log("tes")

                    let x = {
                        aictBankName: "",
                        receivedAmount: data.creditAmount,
                        InsturmentNo: "",
                        creditAllowed: data.creditAmount,
                        InoviceNo: "",
                        KnockOffAmount: "",
                        natureOfAmount: "CreditAllowed",
                        PaymentFor: "",

                    }
                    PaymentNatureList = [];

                    PaymentNatureList.push(x);

                    resetOnlineReceiptCounterPayment();
                    PaymentReceivedGrid();
                }


                // $('#totalBalanceCargo').val(data.totalBalanceCargo);
                $('#vehcileAmountAllow').val(data.vehcileAmountAllow);
                $('#isoCodeDesc').val(data.isoCodeDesc);

                //NEW ADD
                //$('#agents').val(data.clearingAgentId);
                $('#agents').val(data.clearingAgentId).trigger('change.select2');


                getpartybalanceamount();


                $('#cnic').val(data.cnic);
                $('#clearingAgentRep').val(data.clearingAgentRep);
                $('#phoneNumber').val(data.phoneNumber);
                $('#shortweight').val(data.shortWeight);
                $('#excessweight').val(data.excesstWeight);




                $('#consigneeNameIGM').val(data.consigneeNameIGM);
                $('#good').val(data.good);
                $('#portAndTerminalId').val(data.portAndTerminal);
                $('#indexCargoType').val(data.indexCargoType);
                $('#shipper').val(data.shipper);
                $('#documentCode').val(data.documentCode);

                //$('#billToLineAmount').val(data.billToLineAmount.toFixed(2));
                $('#waiverDiscountAmount').val(data.waiVerAmount.toFixed(2));

                $("#waiverDiscountAmounte").val(data.waiVerAmount.toFixed(2));

                $('#otherChargeAmount').val(data.otherCharges.toFixed(2));
                //$('#portChargeAmount').val(data.portCharges.toFixed(2));
                $('#sealCharger').val(data.sealChargers.toFixed(2));
                $('#partContainer').val(data.isPartialDelivery);

                $('#description').val(data.description);


                $('#igmCBM').val(data.measurementCBM);
                $('#destuffCBM').val(data.cbm);
                $('#examinationCBM').val(data.examinationRequestCBM);
                $('#ffCBM').val(data.ffcbm);

                $('#consigneeNTNNumber').val(data.consigneeNTNNumber);
                $('#clearingAgentRegNumber').val(data.clearingAgentRegNumber);
                $('#actualQty').val(data.actualQty);
                $('#actualWeight').val(data.actualWeight);

                $('#actualweightInTon').val(Number(data.actualWeight) / 1000);
                $('#packages').val(data.packages);
                $('#blNumber').val(data.blNumber);

                if (data.previousBillAmount) {

                    $('#previousBillAmount').val(data.previousBillAmount);
                }
                else {

                    $('#previousBillAmount').val(0);
                }
                // $('#additionalFreeDays ').val(data.additionalFreeDays);
                if (data.weight) {

                    $('#MainfestedWeight').val(data.weight);


                    $('#weightInTon').val(Number(data.weight) / 1000);


                }
                else {

                    $('#MainfestedWeight').val(0);
                    $('#weightInTon').val(0);

                }


                let cbms = { cbm: data.cbm, measurementCBM: data.measurementCBM, examinationRequestCBM: data.examinationRequestCBM, ffcbm: data.ffcbm };

                let arr = Object.values(cbms);

                let max = Math.max(...arr);

                $('#CBM').val(max);



                let weightts = { weight: data.weight, actualWeight: data.actualWeight };

                let arrweight = Object.values(weightts);

                let maxweight = Math.max(...arrweight);

                $('#Weight').val(maxweight);


                $('#salesTax').val(data.salesTax);

                if (data.isDelivered == true) {
                    document.getElementById("ischeckAdvanceBill").disabled = true;

                } else {
                    document.getElementById("ischeckAdvanceBill").disabled = false;
                }

                if (data.deliveryStatus == "DeliVered") {
                    $('#deliveryStatus').val("Delivered");
                }
                else {
                    $('#deliveryStatus').val("Un-Delivered");
                }

            }
            else {

                restformValues();
            }



        });
    }
    else {

        $.get('/APICAlls/GetInvoiceCy?igm=' + igm + "&indexNo=" + $("#containerIndexCYdb option:selected").val(), function (data) {

            document.getElementById("ischeckAdvanceBill").disabled = false;
            if (data) {
                if (data.weight) {

                    $('#MainfestedWeight').val(data.weight);
                    $('#weightInTon').val(Number(data.weight) / 1000);

                }
                else {

                    $('#MainfestedWeight').val(0);
                    $('#weightInTon').val(0);

                }

                if (data.gateInDate == null) {
                    $('#gateindate').val('');

                }
                else {
                    $('#gateindate').val(new Date(data.gateInDate.split("T")[0]).toISOString().substr(0, 10));

                }

                if (data.invoiceDate != null) {

                    $('#deliveryDate').val(new Date(data.invoiceDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#deliveryDate').val('');
                }

                $('#shippingAgetName').val(data.shippingAgentName);
                $('#WaiveRemarks').val(data.lastWaiverRemarks);
                $('#previousWaiVerAmount').val(data.previousWaiVerAmount);
                $('#exchangeRateAmount').val(data.exchangeRateAmount);
                $('#creditAllowed').val(data.creditAmount);
                $('#creditAllowedAmount').val(data.creditAmount);

                if (Number(data.creditAmount) > 0) {

                    console.log("tes")

                    let x = {
                        aictBankName: "",
                        receivedAmount: data.creditAmount,
                        InsturmentNo: "",
                        creditAllowed: data.creditAmount,
                        InoviceNo: "",
                        KnockOffAmount: "",
                        natureOfAmount: "CreditAllowed",
                        PaymentFor: "",

                    }

                    PaymentNatureList = [];
                    PaymentNatureList.push(x);

                    resetOnlineReceiptCounterPayment();
                    PaymentReceivedGrid();
                }

                // $('#totalBalanceCargo').val('');



                $('#igmCBM').val('');
                $('#destuffCBM').val('');
                $('#examinationCBM').val('');
                $('#ffCBM').val('');
                $('#consigneeNTNNumber').val(data.consigneeNTNNumber);
                $('#clearingAgentRegNumber').val(data.clearingAgentRegNumber);
                $('#actualQty').val(data.actualQty);
                $('#actualWeight').val(data.actualWeight);
                $('#actualweightInTon').val(Number(data.actualWeight) / 1000);
                $('#packages').val(data.packages);
                $('#blNumber').val(data.blNumber);

                $('#description').val(data.description);

                //$('#additionalFreeDays ').val(data.additionalFreeDays);

                $('#vehcileAmountAllow').val(data.vehcileAmountAllow);
                $('#isoCodeDesc').val(data.isoCodeDesc);

                //NEW ADD
                $('#consigneeNameIGM').val(data.consigneeNameIGM);
                $('#good').val(data.good);
                $('#shipper').val(data.shipper);
                $('#documentCode').val(data.documentCode);

                $('#portAndTerminalId').val(data.portAndTerminal);
                $('#indexCargoType').val(data.indexCargoType);
                //$('#billToLineAmount').val(data.billToLineAmount);
                $('#waiverDiscountAmount').val(data.waiVerAmount);
                $("#waiverDiscountAmounte").val(data.waiVerAmount);

                $('#otherChargeAmount').val(data.otherCharges);
                // $('#portChargeAmount').val(data.portCharges);
                $('#sealCharger').val(data.sealChargers);
                //$('#agents').val(data.clearingAgentId);
                $('#agents').val(data.clearingAgentId).trigger('change.select2');

                getpartybalanceamount();

                $('#cnic').val(data.cnic);
                $('#clearingAgentRep').val(data.clearingAgentRep);
                $('#phoneNumber').val(data.phoneNumber);
                $('#shortweight').val(data.shortWeight);
                $('#excessweight').val(data.excesstWeight);




                let weightts = { weight: data.weight, actualWeight: data.actualWeight };

                let arrweight = Object.values(weightts);

                let maxweight = Math.max(...arrweight);

                $('#Weight').val(maxweight);



                $('#partContainer').val(data.isPartialDelivery);

                if (data.previousBillAmount) {

                    $('#previousBillAmount').val(data.previousBillAmount);
                }
                else {

                    $('#previousBillAmount').val(0);
                }

                if (data.destuffDate == null) {
                    $('#destuffdate').val('');

                }
                else {

                    $('#destuffDate').val(new Date(data.destuffDate.split("T")[0]).toISOString().substr(0, 10));

                }
                $('#salesTax').val(data.salesTax);

                if (data.deliveryStatus == "DeliVered") {
                    $('#deliveryStatus').val("Delivered");
                }
                else {
                    $('#deliveryStatus').val("Un-Delivered");
                }


            }
            else {
                restformValues();
            }



        });
    }

}


function restformValues() {

    $('#pass1').val('');
    $('#gateindate').val('');
    $('#deliveryDate').val('');
    $('#destuffdate').val('');
    $('#shippingAgetName').val('');
    $('#WaiveRemarks').val('');
    $('#previousWaiVerAmount').val(0);
    $('#exchangeRateAmount').val(0);
    $('#creditAllowed').val(0);
    $('#totalBalanceCargo').val(0);
    $('#vehcileAmountAllow').val('');
    $('#isoCodeDesc').val('');
    //$('#agents').val('');
    $('#agents').select2().val('').trigger('change.select2');
    $('#deliveryStatus').val('');
    $('#cnic').val('');
    $('#clearingAgentRep').val('');
    $('#shortweight').val('');
    $('#excessweight').val('');


    $('#good').val('');
    $('#partContainer').val('');
    $('#portAndTerminalId').val('');
    $('#indexCargoType').val('');

    $('#shipper').val('');
    $('#documentCode').val('');

    $('#igmCBM').val('');
    $('#destuffCBM').val('');
    $('#examinationCBM').val('');
    $('#ffCBM').val('');
    $('#consigneeNTNNumber').val('');
    $('#clearingAgentRegNumber').val('');
    $('#actualQty').val('');
    $('#actualWeight').val('');
    $('#packages').val('');
    $('#blNumber').val('');
    $('#tariffCode').val('');

    //NEW ADD

    $('#consigneeNameIGM').val('');

    $('#billToLineAmount').val(0);
    $('#waiverDiscountAmount').val(0);
    $('#otherChargeAmount').val(0);
    $('#weightmentCharges').val(0);
    $('#handingCharges').val(0);
    $('#auctionSalesTax').val(0);
    $('#auctionGrandTotal').val(0);
    // $('#portChargeAmount').val(0);
    $('#sealCharger').val(0);
    $('#cyStorageSizeAmount').val(0);
    $('#vehicleCharges').val(0);
    $('#previousBillAmount').val(0);
    $('#additionalFreeDays').val(0);
    $('#totalFreeDays').val(0);
    $('#totalPartContainers').val(0);
    $('#weightInTon').val(0);
    $('#actualweightInTon').val(0);
    $('#Weight').val(0);
    $('#MainfestedWeight').val(0);
    $('#CBM').val(0);
    $('#salesTax').val(0);

}


function getCFSTariffItems() {

    weight = $('#Weight').val();
    cbm = $('#CBM').val();

    var sectype = $("#orientation_db option:selected").text();

    if (sectype == "ContainerWise") {


        var indexcargotype = $('#indexCargoType').val();

        if (indexcargotype) {

            var gateInDate = document.getElementById("gateindate").value;
            var destuffdate = document.getElementById("destuffdate").value;
            if (gateInDate && destuffdate) {

                var advanceDate = invoiceForm.elements["AdvanceDate"].value;


                if (advanceDate != "") {

                    BillDate = advanceDate;
                }
                else {
                    BillDate = formatDate(new Date());
                }


                var clearingAgentId = $("#agents option:selected").val();


                if (clearingAgentId) {
                    $.get('/APICAlls/GetCBMTariffs?ContainerIndexId=' + containerIndexId + "&clearingAgentId=" + clearingAgentId + "&Weight=" + weight + "&CBM=" + cbm
                        + "&indexcargotype=" + indexcargotype + '&gateInDate=' + gateInDate, { BillDate: BillDate }, function (data) {

                            var newarraydata = [];

                            if (data) {
                                data.forEach(c => {


                                    var result = newarraydata.find(t => t.description == c.description && t.category == c.category);

                                    if (result != null) {
                                        result.amount += c.amount;

                                    }
                                    else {
                                        newarraydata.push(c);

                                    }


                                });
                            }


                            LoadGrid('#tariffGrid', newarraydata);


                            // LoadGrid('#tariffGrid', data);

                            getStorageTotal();


                            if (data.length) {

                                $('#tariffCode').val(data[0].tariffInformationId)
                            }

                        });
                }
                else {
                    //showToast("Please select clearing agent", "warning");
                    alert("Please select clearing agent");

                }


            }
            else {
                //showToast("must select gatein date && Destuff date", "warning");
                alert("must select gatein date && Destuff date");

            }


        } else {
            //showToast("Cargo Type not define", "warning");
            alert("must select gatein date && Destuff date");

        }




    }


}

function getcytariffitems() {

    var clearingAgentId = $("#agents option:selected").val();


    var gateInDate = document.getElementById("gateindate").value;


    var indexcargotype = $('#indexCargoType').val();


    if (indexcargotype) {
        if (clearingAgentId) {

            if (gateInDate) {
                indexcargotype = "CY";

                var advanceDate = invoiceForm.elements["AdvanceDate"].value;

                if (advanceDate != "") {

                    BillDate = advanceDate;
                } else {
                    BillDate = formatDate(new Date());
                }


                $.get('/APICalls/GetSizeTotal?igm=' + igm + "&indexNo=" + $("#containerIndexCYdb option:selected").val() + '&clearingAgentId=' + clearingAgentId + '&gateInDate=' + gateInDate
                    + '&containerCount=' + containerCount + '&indexcargotype=' + indexcargotype, { BillDate: BillDate }, function (data) {





                        var newarraydata = [];

                        if (data) {
                            data.forEach(c => {

                                //var result = newarraydata.find(t => t.description == c.description  );

                                //if (result != null) {
                                //    result.amount += c.amount;

                                //}
                                //else {
                                //    var resultperindex = newarraydata.find(t => t.amount == c.amount && t.description == c.description  );

                                //    console.log("resultperindex", resultperindex)
                                //    if (resultperindex == null) {
                                //        newarraydata.push(c);
                                //    }


                                //}

                                //var result = newarraydata.find(t => t.description == c.description && c.tariffType != "CYPerIndex");

                                //if (result != null) {
                                //    result.amount += c.amount;

                                //}
                                //else {
                                //    var resultperindex = newarraydata.find(t => t.amount == c.amount && t.description == c.description && c.tariffType == "CYPerIndex");

                                //    if (resultperindex == null) {
                                //        newarraydata.push(c);
                                //    }


                                //}


                                var result = newarraydata.find(t => t.description == c.description && t.category == c.category);

                                if (result != null) {
                                    result.amount += c.amount;

                                }
                                else {
                                    newarraydata.push(c);

                                }

                            });
                        }






                        LoadGrid('#tariffGrid', newarraydata);



                        getStorageTotalCY();

                        if (data.length) {

                            $('#tariffCode').val(data[0].tariffInformationId)
                        }

                    });
            } else {
                //showToast("must select gatein date", "warning");
                alert("must select gatein date");

            }




        }
        else {
            //showToast("Please select clearing agent", "warning");
            alert("Please select clearing agent");

        }
    }
    else {
        //showToast("cargo type not define", "warning");
        alert("cargo type not define");

    }



}



function getStorageTotal() {


    var indexcargotype = $('#indexCargoType').val();

    //   var additionalFreeDays = $('#additionalFreeDays').val();


    //  console.log("additionalFreeDays", additionalFreeDays);

    if (indexcargotype) {
        var sectype = $("#orientation_db option:selected").text();

        var gateInDate = document.getElementById("gateindate").value;

        var destuffdate = document.getElementById("destuffdate").value;

        if (gateInDate && destuffdate) {

            var clearingAgentId = $("#agents option:selected").val();

            if (clearingAgentId) {

                var advanceDate = invoiceForm.elements["AdvanceDate"].value;

                if (advanceDate != "") {

                    BillDate = advanceDate;
                }
                else {
                    BillDate = formatDate(new Date());
                }


                if (sectype == "ContainerWise") {
                    weight = $('#Weight').val();
                    cbm = $('#CBM').val();


                    $.post('/APICAlls/GetStorageTotal?ContainerIndexId=' + containerIndexId + '&clearingAgentId=' + clearingAgentId + '&gateInDate=' + gateInDate + '&destuffdate=' + destuffdate + '&Weight=' + weight
                        + '&CBM=' + cbm + '&indexcargotype=' + indexcargotype, { BillDate: BillDate }, function (data) {

                            console.log("cfs storage", data);
                            if (data) {
                                $('#storageDays').val(data.storageDays);
                                $('#storageTotal').val(data.storageTotal);
                                $('#additionalFreeDays').val(data.addtionalFreeDays);
                                $('#totalFreeDays').val(data.totalFreeDays);
                                $('#totalBalanceCargo').val(data.totalBalanceCargo);
                                $('#totalPartContainers').val(0);
                                $('#storageFreeDaysType').val(data.storageFreeDaysType);
                                $('#storageApplicableon').val(data.storageApplicableon);

                            }
                            else {
                                $('#storageDays').val('');
                                $('#storageTotal').val('');
                                $('#additionalFreeDays').val('');
                                $('#totalFreeDays').val('');
                                $('#totalBalanceCargo').val(0);
                                $('#totalPartContainers').val(0);
                                $('#storageFreeDaysType').val('');
                                $('#storageApplicableon').val('');
                            }


                            calculateTotal();



                        });

                }

            }
            else {
                //showToast("Please select clearing agent", "warning");

                alert("Please select clearing agent");

            }

        }
        else {
            //showToast("Please select gatein & Destuff date", "warning");
            alert("Please select gatein & Destuff date");

        }

    } else {
        //showToast("cargo type is not define", "warning");
        alert("cargo type is not define");
    }

}

function getStorageTotalCY() {


    var indexcargotype = $('#indexCargoType').val();

    //      var additionalFreeDays = $('#additionalFreeDays').val();


    // console.log("additionalFreeDays", additionalFreeDays);

    if (indexcargotype) {

        indexcargotype = "CY";
        var gateInDate = document.getElementById("gateindate").value;

        if (gateInDate) {

            var clearingAgentId = $("#agents option:selected").val();

            if (clearingAgentId) {

                var advanceDate = invoiceForm.elements["AdvanceDate"].value;

                if (advanceDate != "") {

                    BillDate = advanceDate;
                } else {
                    BillDate = formatDate(new Date());
                }

                $.post('/APICAlls/GetStorageTotalCY?igm=' + igm + "&indexNo=" + $("#containerIndexCYdb option:selected").val() + '&gateInDate=' + gateInDate
                    + "&clearingAgentId=" + clearingAgentId + "&indexcargotype=" + indexcargotype, { BillDate: BillDate }, function (data) {
                        console.log("cy storage", data);
                        if (data) {


                            $('#storageDays').val(data.storageDays);
                            $('#storageTotal').val(data.storageTotal);
                            $('#additionalFreeDays').val(data.addtionalFreeDays);
                            $('#totalFreeDays').val(data.totalFreeDays);
                            $('#totalBalanceCargo').val(data.totalBalanceCargo);
                            $('#totalPartContainers').val(data.partContainers);
                            $('#storageFreeDaysType').val(data.storageFreeDaysType);
                            $('#storageApplicableon').val(data.storageApplicableon);
                            //$('#storageTotal').val(data.storageTotal * containerCount);

                        }
                        else {
                            $('#storageDays').val('');
                            $('#storageTotal').val('');
                            $('#additionalFreeDays').val('');
                            $('#totalFreeDays').val('');
                            $('#totalBalanceCargo').val(0);
                            $('#totalPartContainers').val(0);
                            $('#storageFreeDaysType').val('');
                            $('#storageApplicableon').val('');

                        }
                        calculateTotal();

                    });
            }
            else {
                //showToast("Please select clearing agent", "warning");
                alert("Please select clearing agent");

            }
        }
        else {
            //showToast("Please select gatein date", "warning");
            alert("Please select gatein date");

        }
    } else {
        //showToast("cargo type not define", "warning");
        alert("cargo type not define");
    }




}

function generateBill() {

    console.log("type", type)
    var blnores = $("#blNumber").val();

    if (!blnores) {
        //return showToast("bl no is not avaible", "error")
        return alert("bl no is not avaible");
    }

    var indexCargoTyperes = $("#indexCargoType").val();

    if (!indexCargoTyperes) {
        //return showToast("Cargo Type no is not avaible", "error")
        return alert("Cargo Type no is not avaible");
    }

    var vehcileAmountAllowres = $("#vehcileAmountAllow").val();

    if (!vehcileAmountAllowres) {
        //return showToast("vechile allow not define", "error")
        return alert("vechile allow not define");
    }

    var partContainerres = $("#partContainer").val();

    if (!partContainerres) {
        //return showToast("part Container not define", "error")
        return alert("part Container not define");
    }

    var isoCodeDescres = $("#isoCodeDesc").val();

    if (!isoCodeDescres) {
        //return showToast("Container type not define", "error")
        return alert("Container type not define");
    }
    var portAndTerminalIdres = $("#portAndTerminalId").val();

    if (!portAndTerminalIdres) {
        //return showToast("PortAndTerminal not define", "error")
        return alert("PortAndTerminal not define");
    }

    var shipperres = $("#shipper").val();

    if (!shipperres) {
        /*return showToast("Shipper not define", "error");*/
        return alert("Shipper not define");
    }

    var goodres = $("#good").val();

    if (!goodres) {
        //return showToast("good not define", "error")
        return alert("good not define");
    }

    var consigneeNameIGMres = $("#consigneeNameIGM").val();

    if (!consigneeNameIGMres) {
        //return showToast("consignee not define", "error")
        return alert("Consignee Not define");
    }

    var shippingAgetNameres = $("#shippingAgetName").val();

    if (!shippingAgetNameres) {
        //return showToast("shippingAgent not define", "error")
        return alert("shippingAgent not define");
    }


    var gateindateres = document.getElementById("gateindate").value;

    if (!gateindateres) {
        //return showToast("gateindate not define", "error")
        return alert("gateindate not define");
    }

    var Weightres = $("#Weight").val();

    if (!Weightres) {
        //return showToast("weight not define", "error")
        return alert("weight not define");
    }


    var CBMres = $("#CBM").val();

    if (!CBMres) {
        //return showToast("CBM not define", "error")
        return alert("CBM not define");
    }


    var clearingAgentIdres = $("#agents option:selected").val();
    if (!clearingAgentIdres) {
        //return showToast("clearingagent not define", "error")
        return alert("clearingagent not define");
    }


    var packagesres = $("#packages").val();

    if (!packagesres) {
        //return showToast("packages not define", "error")
        return alert("packages not define");
    }

    var actualWeightres = $("#actualWeight").val();

    if (!actualWeightres) {
        //return showToast("actualWeight not define", "error")
        return alert("actualWeight not define");
    }

    var actualQtyres = $("#actualQty").val();

    if (!actualQtyres) {
        //return showToast("actualQty not define", "error")
        return alert("actualQty not define");
    }


    var actualQtyres = $("#actualQty").val();

    if (!actualQtyres) {
        //return showToast("actualQty not define", "error")
        return alert("actualQty not define");
    }


    //var clearingAgentRegNumberres = $("#clearingAgentRegNumber").val();

    //if (!clearingAgentRegNumberres) {
    //    //return showToast("clearingAgentRegNumberr not define", "error")
    //    return alert("clearingAgentRegNumberr not define");
    //}

    //var consigneeNTNNumberres = $("#consigneeNTNNumber").val();

    //if (!consigneeNTNNumberres) {
    //    //return showToast("consigneeNTNNumber not define", "error")
    //    return alert("consigneeNTNNumber not define");
    //}

    if (type == "CFS") {
        var ffCBMres = $("#ffCBM").val();

        if (!ffCBMres) {
            //return showToast("ffCBM not define", "error")
            return alert("ffCBM not define");
        }

        var examinationCBMres = $("#examinationCBM").val();

        if (!examinationCBMres) {
            //return showToast("examinationCBM not define", "error")
            return alert("examinationCBM not define");
        }

        var destuffCBMres = $("#destuffCBM").val();

        if (!destuffCBMres) {
            //return showToast("destuffCBM not define", "error")
            return alert("destuffCBM not define");
        }

        var igmCBMres = $("#igmCBM").val();

        if (!igmCBMres) {
            //return showToast("igmCBM not define", "error")
            return alert("igmCBM not define");
        }


    }









    if (!igm) {

        //showToast("Please select IGM!", "warning");
        alert("Please select IGM!");

        return;
    }

    if (type == "CFS") {

        var containerIndexId = $("#indexdb option:selected").val();


        if (containerIndexId) {



            var indexCargoType = $("#indexCargoType").val();


            if (indexCargoType) {


                if (indexCargoType == "LCL") {

                    //$.get('/APICalls/Getstatusofindexcargotype?ContainerIndexId=' + containerIndexId, function (res) {

                    //    if (res.error == false) {
                    var weigt = $('#Weight').val();



                    if (weigt > 0) {


                        $.get('/APICalls/CheckCFSContainerHold?ContainerIndexId=' + containerIndexId, function (res) {

                            if (res.length) {

                                //alert("Container is currently on hold. Invoice cannot be generated \n Lot No: " + res.lotNo + "\n Special Instructions: " + res.specialInstructions);
                                //alert(res.specialInstructions);

                                //res.forEach(c => {
                                //    alert(c.specialInstructions);
                                //});

                                loadGridhold(res);
                                $('#HoldStatusModal').modal('toggle');

                                CheckCFSContainerStatus();

                            }
                            else {
                                CheckCFSContainerStatus();
                                loadGridhold([]);
                            }

                        })




                    }
                    else {
                        //showToast("weight must be > 0 ", "error")
                        alert("weight must be > 0");

                    }
                    //} else {
                    //    alert(res.message);
                    //}

                    //});

                } else {


                    var weigt = $('#Weight').val();



                    if (weigt > 0) {

                        $.get('/APICalls/CheckCFSContainerHold?ContainerIndexId=' + containerIndexId, function (res) {
                            if (res.length) {

                                //alert("Container is currently on hold. Invoice cannot be generated \n Lot No: " + res.lotNo + "\n Special Instructions: " + res.specialInstructions);
                                //alert(res.specialInstructions);

                                //res.forEach(c => {

                                //    alert(c.specialInstructions);
                                //});


                                loadGridhold(res);
                                $('#HoldStatusModal').modal('toggle');

                                CheckCFSContainerStatus();
                            }
                            else {
                                CheckCFSContainerStatus();
                                loadGridhold([]);
                            }

                        })



                    }
                    else {
                        //showToast("weight must be > 0 ", "error")
                        alert("weight must be > 0 ");
                    }

                }




            }
            else {

                //showToast("Index Cargo Type Are Not Define", "warning");
                alert("Index Cargo Type Are Not Define");

                return;
            }





        }
        else {
            //showToast("Please select cfs Index ", "warning");
            alert("Please select cfs Index ");

            return;
        }



    }
    if (type == "CY") {

        var index = $("#containerIndexCYdb option:selected").val();

        if (index) {
            $.get('/APICalls/CheckCyContainerHold?&igm=' + igm + '&index=' + index, function (res) {


                if (res.length) {

                    //alert("Container is currently on hold. Invoice cannot be generated \n Lot No: " + res.lotNo + "\n Special Instructions: " + res.specialInstructions);
                    //alert(res.specialInstructions);

                    //res.forEach(c => {

                    //    alert(c.specialInstructions);
                    //});


                    loadGridhold(res);
                    $('#HoldStatusModal').modal('toggle');

                    CheckCYContainerStatus();
                }
                else {
                    CheckCYContainerStatus();
                    loadGridhold([]);
                }
            })
        }
        else {
            //showToast("Please select Index ", "warning");
            alert("Please select Index");


            return;
        }


    }



}

function loadGridhold(data) {

    $("#holdGrid").dxDataGrid({
        dataSource: data,
        keyExpr: "holdId",
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

        columns: [
            {
                dataField: "auctionLotNo",
                caption: "Auction Lot No",
                allowEditing: false,
            },
            {
                dataField: "holdDate",
                caption: "Hold Date",
                allowEditing: false,
                dataType: 'date',
                format: 'dd/MM/yyyy',
                allowEditing: false,
            },
            {
                dataField: "holdType",
                caption: "hold Type",
                allowEditing: false,
            },
            {
                dataField: "specialInstructions",
                caption: "special Instructions",
                allowEditing: false,
            },
            {
                dataField: "role",
                caption: "Role",
                allowEditing: false,
            },
            {
                dataField: "isHold",
                caption: "Hold Status",
                allowEditing: false,
            },
            {
                dataField: "ro",
                caption: "RO Number",

            },
            {
                dataField: "removeInstruction",
                caption: "Remove Instruction",
                validationRules: [{
                    type: "required",
                    message: "Remove Instruction Is Required"
                }],

            },
        ],

    });

}


function CheckCFSContainerStatus() {


    var indexnumber = $("#indexdb option:selected").text();

    console.log("igm", igm)
    console.log("indexnumber", indexnumber)


    $.get('/APICalls/CheckCFSContainerStatus?igm=' + igm + '&index=' + indexnumber, function (res) {

        console.log("res", res);

        if (res.error == true) {
            return alert(res.message)

        }
        else {

            getCFSTariffItems();

            getauctionamountInfo("CFS")

        }


    })



}

function CheckCYContainerStatus() {

    var index = $("#containerIndexCYdb option:selected").val();

    console.log("igm", igm)
    console.log("index", index)

    $.get('/APICalls/CheckCYContainerStatus?igm=' + igm + '&index=' + index, function (res) {

        console.log("res", res);

        if (res.error == true) {
            return alert(res.message)

        }
        else {

            getcytariffitems();

            getauctionamountInfo("CY")
        }


    })


}

function getstatusofindexcargotype() {


    $.get('/APICalls/Getstatusofindexcargotype?ContainerIndexId=' + containerIndexId, function (res) {

        return res;

    });
}



function getauctionamountInfo(type) {

    if (type == "CFS") {
        var weight = $('#Weight').val();
        var CBM = $('#CBM').val();


        var index = $("#indexdb option:selected").text();


        $.get('/APICalls/GetAuctionAmounts?CBM=' + CBM + '&weight=' + weight + '&type=' + type + '&igm=' + igm + '&index=' + index, function (res) {


            if (res) {

                $('#handingCharges').val(res.hanndlingWeight);
                $('#weightmentCharges').val(res.weight);

                var totalauctionamount = Number(res.hanndlingWeight) + Number(res.weight)


                if (totalauctionamount > 0) {
                    var st = $('#salesTax').val();

                    var AuctionsalesTaxAmount = Math.ceil(totalauctionamount * (Number(st) / 100));


                    $('#auctionSalesTax').val(AuctionsalesTaxAmount);



                    var grandtotalAuctionAmount = Number(totalauctionamount) + Number(AuctionsalesTaxAmount);


                    $('#auctionGrandTotal').val(grandtotalAuctionAmount);


                }

            }

        });
    }

    if (type == "CY") {

        var index = $("#containerIndexCYdb option:selected").val();


        $.get('/APICalls/GetAuctionAmounts?CBM=' + 0 + '&weight=' + 0 + '&type=' + type + '&igm=' + igm + '&index=' + index, function (res) {


            if (res) {

                $('#handingCharges').val(res.hanndlingWeight);
                $('#weightmentCharges').val(res.weight);

                var totalauctionamount = Number(res.hanndlingWeight) + Number(res.weight)


                if (totalauctionamount > 0) {
                    var st = $('#salesTax').val();

                    var AuctionsalesTaxAmount = Math.ceil(totalauctionamount * (Number(st) / 100));


                    $('#auctionSalesTax').val(AuctionsalesTaxAmount);



                    var grandtotalAuctionAmount = Number(totalauctionamount) + Number(AuctionsalesTaxAmount);



                    $('#auctionGrandTotal').val(grandtotalAuctionAmount);


                }

            }

        });
    }



}



function calculateTotal() {


    var PreviousBillAmount = $('#previousBillAmount').val();

    var TariffAmountTotal = $('#tariffAmountTotal').val();

    var StorageTotal = $('#storageTotal').val();

    if (type == "CFS") {
        $('#cyStorageSizeAmount').val(0);
    }
    if (type == "CY") {
        var stDays = $("#storageDays").val();
        if (stDays > 0) {
            var totalPartContainers = $("#totalPartContainers").val();

            console.log("totalPartContainers", totalPartContainers)

            var ContainerStorage = (sumcontainerSizeAmount * stDays);
            console.log("ContainerStorage", ContainerStorage)

            if (totalPartContainers > 0) {

                ContainerStorage = ContainerStorage / totalPartContainers;

                console.log("ContainerStorage", ContainerStorage)
            }

            $('#cyStorageSizeAmount').val(ContainerStorage);

        } else {
            $('#cyStorageSizeAmount').val(0);

        }


    }

    var containrstorageamt = $('#cyStorageSizeAmount').val();

    var otherchargAmt = $('#otherChargeAmount').val();

    //   var portChargeAmt = $('#portChargeAmount').val();

    var sealChargerAmt = $('#sealCharger').val();


    var storageTariffTotal = Number(TariffAmountTotal) + Number(StorageTotal)

    // var containrstorageamtotherchargAmt = Number(containrstorageamt) + Number(otherchargAmt) + Number(portChargeAmt) + Number(sealChargerAmt)
    var containrstorageamtotherchargAmt = Number(containrstorageamt) + Number(otherchargAmt) + Number(sealChargerAmt)

    var storageTariffTotalcontainrstorageamtotherchargAmt = (storageTariffTotal + containrstorageamtotherchargAmt)



    //vehicleCharges

    var deliveryStatus = $('#deliveryStatus').val();

    var vehcilAllow = $('#vehcileAmountAllow').val();


    if (vehcilAllow) {

        if (vehcilAllow == "Yes") {

            var goodheadname = $('#good').val();

            if (goodheadname) {
                if (goodheadname.includes("VEHICLE")) {

                    if (deliveryStatus != "Delivered") {
                        var vehicleChargeamt = (Number(storageTariffTotalcontainrstorageamtotherchargAmt) / 100 * 12);

                        $('#vehicleCharges').val(Math.ceil(vehicleChargeamt))
                    }

                }
            }

        }

    }




    var vehicleamt = $('#vehicleCharges').val();

    var waiverDiscountAmount = $('#waiverDiscountAmount').val();


    storageTariffTotalcontainrstorageamtotherchargAmt = Number(storageTariffTotalcontainrstorageamtotherchargAmt.toFixed(2)) + Number(vehicleamt)


    $("#totalCharges").val(storageTariffTotalcontainrstorageamtotherchargAmt)

    $("#totalChargesAfterWaiver").val(storageTariffTotalcontainrstorageamtotherchargAmt - Number(waiverDiscountAmount))

    var totlChargesAfterWaiver = $('#totalChargesAfterWaiver').val();

    var salesTax = $('#salesTax').val();


    var gstamount = Math.ceil(totlChargesAfterWaiver * (Number(salesTax) / 100));


    $('#gst').val(gstamount);


    $('#totalInvoiceAmountExWaiver').val(Math.ceil(Number($('#gst').val()) + Number(totlChargesAfterWaiver)));


    var InvoiceAmountExWaiverTotal = $('#totalInvoiceAmountExWaiver').val();


    $("#invoiceAmount").val(Number(InvoiceAmountExWaiverTotal) + Number(waiverDiscountAmount));


    GetBillToLineAmount();


    //var balanceAmt = (storageTariffTotalcontainrstorageamtotherchargAmt - Number(PreviousBillAmount))
    //$("#balanceAmountTotal").val(balanceAmt)
    //calculateSalesTax(balanceAmt)

    setTimeout(function () { calculateCounterCollection(); }, 2000);

}

function calculateCounterCollection() {






}

function calculateSalesTax(balanceAmt) {


    //var billtolineamt = $('#billToLineAmount').val();

    //var waiverDisAmount = $('#waiverDiscountAmount').val();

    var salesTax = $('#salesTax').val();


    var salesTaxAmount = Math.ceil(balanceAmt * (Number(salesTax) / 100));


    $('#afterSalesTax').val(salesTaxAmount);


    var salesTaxAmountbalanceAmt = (salesTaxAmount + balanceAmt)

    //var afterSalesTaxAmt = $('#afterSalesTax').val();

    //var totalamount = (Number(afterSalesTaxAmt) + total)


    //$('#totalamount').val(Number(totalamount) * containerCount);

    //var newtotalamount = $('#totalamount').val(  );

    //console.log("totalamount", totalamount);

    //console.log("newtotalamount", newtotalamount);

    //console.log("billtolineamt", billtolineamt);

    //console.log("waiverDisAmount", waiverDisAmount);

    //console.log("add waive and bill to line", billtolineamt + waiverDisAmount);

    //var sumwaiveandbillamount = (Number(billtolineamt) + Number(waiverDisAmount));

    //console.log("sumwaiveandbillamount", sumwaiveandbillamount)



    //var grandTotal = ( Number(newtotalamount) - Number(sumwaiveandbillamount))

    $('#grandTotal').val(Number(Math.ceil(salesTaxAmountbalanceAmt)));


    $('#inoviceTotalAmount').val(Number(Math.ceil(salesTaxAmountbalanceAmt)));


    //window.setInterval('calculatebilltolienamount()', 2000);

    setTimeout(function () { calculatebilltolienamount(); }, 2000);


}



function calculatebilltolienamount() {

    var btlamt = $('#billToLineAmount').val();
    if (btlamt) {
        var totalamt = $('#totalCharges').val();

        var exbtl = Number(totalamt) - Number(btlamt);

        $('#exBillToLine').val(exbtl);
    }






}

function formatDate(date) {

    return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
}


///// work end


function LoadGrid(id, data) {


    var dataGrid = $(id).dxDataGrid({
        dataSource: data,
        keyExpr: "tariffId",
        showBorders: false,
        showBorders: false,
        showColumnLines: false,
        dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",


        sorting: {
            mode: "none"
        },
        paging: {
            enabled: false
        },
        editing: {
            mode: "inline",
            allowUpdating: false,
            selectTextOnEditStart: false,
            startEditAction: "click"
        },
        columns: [
            {
                dataField: "category",
                caption: "Type",

            },
            {
                dataField: "description",
                caption: "Name",

            },
            {
                dataField: "amount",
                caption: "Rs #",
                format: {
                    type: "fixedPoint",
                    precision: 2
                },
                headerCellTemplate: function (header, info) {
                    $('<div>')
                        .html(info.column.caption)
                        .css('font-size', '18px')
                        .appendTo(header);
                }

            }

        ]


    }).dxDataGrid("instance");



    var tariffAmountTotal = 0;

    if (data) {
        data.forEach(c => {
            tariffAmountTotal += Number(c.amount);
        });
    }

    tariffAmountTotal = tariffAmountTotal.toFixed(2);

    $('#tariffAmountTotal').val(tariffAmountTotal);



}

function submitInvoice() {

    var letpassPartyBalance = $('#ledgerBalanceAmount').val();

    //var totlchrs = $('#totalCharges').val();

    //var btltotlchrs = $('#billToLineAmount').val();
    //var waivertotlchrs = $('#waiverDiscountAmount').val();
    //var totalbtlandwaiver = Number(btltotlchrs) + Number(waivertotlchrs);

    //if (Number(totalbtlandwaiver) > Number(totlchrs)) {
    //    //return showToast("your discount amount is greater then total charges", "error")
    //    return alert("your discount amount is greater then total charges")
    //}

    var inoviceTotalAmount = $('#inoviceTotalAmount').val();

    var islastPass = $("#islastPass").is(':checked', function () {
        $("#islastPass").prop('checked', true);
    });

    var PaymentReceiveGriddata = $("#PaymentReceiveGrid").dxDataGrid("option", "dataSource");

    var paymentreceivedtotalamount = 0;

    if (PaymentReceiveGriddata.length) {
        PaymentReceiveGriddata.forEach(c => {
            if (c.natureOfAmount != "CashRefund") {

                paymentreceivedtotalamount += Number(c.receivedAmount);
            }

        });
    }

    //console.log("inoviceTotalAmount", inoviceTotalAmount)
    //console.log("paymentreceivedtotalamount", paymentreceivedtotalamount)
    //console.log("islastPass", islastPass)


    if (Number(inoviceTotalAmount) < 0) {
        //return showToast("Your counter collection is less then zero please check your btl , waiver or pervious settelment amounts", "error")
        return alert("Your counter collection is less then zero please check your btl , waiver or pervious settelment amounts");
    }

    if (paymentreceivedtotalamount < inoviceTotalAmount && islastPass == false) {

        //return showToast("please settle payment or select letpass" , "error")
        return alert("please settle payment or select letpass");

    }

    if (Number(paymentreceivedtotalamount) > 0 && islastPass == true) {
        //return showToast("please settle payment or select letpass you are selecting both", "error");
        return alert("please settle payment or select letpass you are selecting both");
    }


    if (Number(letpassPartyBalance) < Number(inoviceTotalAmount) && islastPass == true) {
        //return showToast("Your Party Balance is less then invoice amount", "error")
        return alert("Your Party Balance is less then invoice amount");
    }

    var form = document.getElementById('invoiceForm');

    var model = $('#invoiceForm').serialize();


    form.reportValidity();


    if (form.checkValidity()) {

        var tariffdata = $("#tariffGrid").dxDataGrid("option", "dataSource");

        if (tariffdata.length) {

            var postData;

            if (type == "CFS") {

                postData = model;
            }
            else {

                postData = model + "&igm=" + igm + "&indexNo=" + $("#containerIndexCYdb option:selected").val();
            }

            $.post('/Invoice/SaveBill?' + postData + "&islastPass=" + islastPass, { invoiceItems: tariffdata, paymentReceivedList: PaymentReceiveGriddata }, function (data) {


                billNo = data.invoiceNo;
                DeliveryOrderNumber = data.deliveryOrderNumber;
                InvoiceNoBillToLine = data.invoiceNoBillToLine;
                AuctionInvoiveNUmber = data.auctionInvoiveNo;


                if (data.error == true) {
                    //showToast(data.message, "error");
                    alert(data.message);


                }
                else {

                    if (data != "null") {

                        if (InvoiceNoBillToLine) {
                            window.open('/import/reports/SalesTaxInvoice?BillNo=' + InvoiceNoBillToLine + '&BillType=' + "Bill To Line", "_blank");
                        }

                        if (billNo) {
                            window.open('/import/reports/SalesTaxInvoice?BillNo=' + billNo + '&BillType=' + "Normal", "_blank");
                        }
                        if (DeliveryOrderNumber) {
                            window.open('/import/reports/DeliveryOrder?DeliveryOrderNumber=' + DeliveryOrderNumber, "_blank");
                        }
                        if (AuctionInvoiveNUmber) {
                            window.open('/import/reports/SalesTaxInvoiceAution?BillNo=' + AuctionInvoiveNUmber + '&BillType=' + "Auction", "_blank");
                        }

                        // window.location.href = window.location.href;


                    }


                }

            });
        }
        else {
            //showToast("Please Generate Bill", "error");
            alert("Please Generate Bill");



        }




    }
    else {
        console.log("else work")
    }

}


function formfiled() {



    //if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {
    //    document.getElementById('CFS').disabled = true;
    //    document.getElementById('CY').disabled = true;

    //}
    if (PermissionInputs.find(x => x.fieldName == "ClearingAgent" && x.isChecked == false)) {
        document.getElementById('agents').disabled = true;

    }
    if (PermissionInputs.find(x => x.fieldName == "Consignee" && x.isChecked == false)) {
        document.getElementById('consignee').disabled = true;

    } if (PermissionInputs.find(x => x.fieldName == "CBM" && x.isChecked == false)) {
        document.getElementById('CBM').disabled = true;

    }
    if (PermissionInputs.find(x => x.fieldName == "Weight" && x.isChecked == false)) {
        document.getElementById('Weight').disabled = true;

    }
    if (PermissionInputs.find(x => x.fieldName == "AdvanceBill" && x.isChecked == false)) {
        document.getElementById('ischeckAdvanceBill').disabled = true;

    } if (PermissionInputs.find(x => x.fieldName == "GateInDate" && x.isChecked == false)) {
        document.getElementById('gateindate').disabled = true;

    } if (PermissionInputs.find(x => x.fieldName == "DestuffDate" && x.isChecked == false)) {
        document.getElementById('destuffdate').disabled = true;

    }
    if (PermissionInputs.find(x => x.fieldName == "SubBill" && x.isChecked == false)) {
        document.getElementById('ischeckSubBill').disabled = true;

    } if (PermissionInputs.find(x => x.fieldName == "AgentRepName" && x.isChecked == false)) {
        document.getElementById('repname').disabled = true;

    } if (PermissionInputs.find(x => x.fieldName == "PhoneNumber" && x.isChecked == false)) {
        document.getElementById('phonenumber').disabled = true;

    } if (PermissionInputs.find(x => x.fieldName == "CNIC" && x.isChecked == false)) {
        document.getElementById('cnic').disabled = true;

    } if (PermissionInputs.find(x => x.fieldName == "ImageName" && x.isChecked == false)) {
        document.getElementById('imageName').disabled = true;

    } if (PermissionInputs.find(x => x.fieldName == "Imagepicker" && x.isChecked == false)) {
        document.getElementById('files').disabled = true;

    }



    document.getElementById('ischeckAdvanceBill').onchange = function () {
        document.getElementById('pass1').disabled = !this.checked;
    }





}




function GetBillToLineAmount() {




    var perviouspaidamount = $('#previousBillAmount').val();


    var totalInvoiceAmountExWaiver = $('#totalInvoiceAmountExWaiver').val();

    if (type == "CFS") {

        var indexno = $("#indexdb option:selected").text();

        var storageAmount = $('#storageTotal').val();

        var sealChargr = $('#sealCharger').val();
        // var portChargAmount = $('#portChargeAmount').val();
        var otherChargAmount = $('#otherChargeAmount').val();
        var vehicleChargs = $('#vehicleCharges').val();
        var cyStoragSizeAmount = $('#cyStorageSizeAmount').val();

        var tariffdata = $("#tariffGrid").dxDataGrid("option", "dataSource");

        if (tariffdata.length) {


            $.post('/Invoice/CalculateBillToLineCFS?storageAmount=' + storageAmount + '&sealChargr=' + sealChargr + '&otherChargAmount=' + otherChargAmount
                + '&vehicleChargs=' + vehicleChargs + '&igm=' + igm + '&indexno=' + indexno + '&type=' + type, { tariffdata: tariffdata }, function (data) {


                    $('#billToLineAmount').val(data)



                    if (data > 0) {


                        var salesTax = $('#salesTax').val();


                        var salesTaxAmount = Math.ceil(data * (Number(salesTax) / 100));


                        $('#gstonBILLTOLINE').val(salesTaxAmount);


                        var totalbtlamt = (Number(data) + Number(salesTaxAmount));

                        $('#totalBTLAmount').val(totalbtlamt);

                        $('#billTOLINEIncGSTD').val(totalbtlamt);


                        var previousBillAmountandbtlwithgst = Number(perviouspaidamount) + Number($('#billTOLINEIncGSTD').val());

                        var aftertotalInvoiceAmountExWaiver = Number(totalInvoiceAmountExWaiver) - Number(previousBillAmountandbtlwithgst);

                        $('#counterCollection').val(aftertotalInvoiceAmountExWaiver);

                        alert("index on bill to line");



                        var counterCollectionres = $('#counterCollection').val();
                        $('#inoviceTotalAmount').val(counterCollectionres);

                        if (counterCollectionres < 0) {
                            $('#balanceInoviceAmount').val(0);

                        }
                        else {

                            var totalbalanceInoviceAmount = 0

                            totalbalanceInoviceAmount = ((aftertotalInvoiceAmountExWaiver) + (Number($("#excessAmount").val())));
                            if (totalbalanceInoviceAmount < 0) {
                                totalbalanceInoviceAmount = 0;
                            }
                            $("#balanceInoviceAmount").val(totalbalanceInoviceAmount);

                        }

                        if (counterCollectionres < 0) {

                            alert("your counterCollection is less then zero please check your waiver , btl and pervious paid amount");
                        }



                    }
                    else {


                        var previousBillAmountandbtlwithgst = Number(perviouspaidamount) + Number($('#billTOLINEIncGSTD').val());

                        var aftertotalInvoiceAmountExWaiver = Number(totalInvoiceAmountExWaiver) - Number(previousBillAmountandbtlwithgst);

                        $('#counterCollection').val(aftertotalInvoiceAmountExWaiver);


                        var counterCollectionres = $('#counterCollection').val();
                        $('#inoviceTotalAmount').val(counterCollectionres);

                        if (counterCollectionres < 0) {
                            $('#balanceInoviceAmount').val(0);

                        }
                        else {

                            var totalbalanceInoviceAmount = 0

                            totalbalanceInoviceAmount = ((aftertotalInvoiceAmountExWaiver) + (Number($("#excessAmount").val())));
                            if (totalbalanceInoviceAmount < 0) {
                                totalbalanceInoviceAmount = 0;
                            }
                            $("#balanceInoviceAmount").val(totalbalanceInoviceAmount);

                        }



                        if (counterCollectionres < 0) {

                            alert("your counterCollection is less then zero please check your waiver , btl and pervious paid amount");
                        }

                    }

                });


        }
    }

    if (type == "CY") {


        var indexno = $("#containerIndexCYdb option:selected").text();


        var tariffdata = $("#tariffGrid").dxDataGrid("option", "dataSource");

        if (tariffdata.length) {


            var storageAmount = $('#storageTotal').val();

            var sealChargr = $('#sealCharger').val();
            // var portChargAmount = $('#portChargeAmount').val();
            var otherChargAmount = $('#otherChargeAmount').val();
            var vehicleChargs = $('#vehicleCharges').val();
            var cyStoragSizeAmount = $('#cyStorageSizeAmount').val();


            $.post('/Invoice/CalculateBillToLinCY?storageAmount=' + storageAmount
                + '&sealChargr=' + sealChargr + '&otherChargAmount=' + otherChargAmount + '&vehicleChargs=' + vehicleChargs
                + '&cyStoragSizeAmount=' + cyStoragSizeAmount + '&igm=' + igm + '&indexno=' + indexno + '&type=' + type, { tariffdata: tariffdata }, function (data) {

                    $('#billToLineAmount').val(data);

                    if (data > 0) {
                        var salesTax = $('#salesTax').val();

                        var salesTaxAmount = Math.ceil(data * (Number(salesTax) / 100));

                        $('#gstonBILLTOLINE').val(salesTaxAmount);


                        var totalbtlamt = (Number(data) + Number(salesTaxAmount));

                        $('#totalBTLAmount').val(totalbtlamt);


                        var previousBillAmountandbtlwithgst = Number(perviouspaidamount) + Number($('#billTOLINEIncGSTD').val());

                        var aftertotalInvoiceAmountExWaiver = Number(totalInvoiceAmountExWaiver) - Number(previousBillAmountandbtlwithgst);

                        $('#counterCollection').val(aftertotalInvoiceAmountExWaiver);

                        alert("index on bill to line");

                        var counterCollectionres = $('#counterCollection').val();

                        $('#inoviceTotalAmount').val(counterCollectionres);

                        if (counterCollectionres < 0) {
                            $('#balanceInoviceAmount').val(0);

                        }
                        else {

                            var totalbalanceInoviceAmount = 0

                            totalbalanceInoviceAmount = ((aftertotalInvoiceAmountExWaiver) + (Number($("#excessAmount").val())));
                            if (totalbalanceInoviceAmount < 0) {
                                totalbalanceInoviceAmount = 0;
                            }
                            $("#balanceInoviceAmount").val(totalbalanceInoviceAmount);

                        }



                        if (counterCollectionres < 0) {

                            alert("your counterCollection is less then zero please check your waiver , btl and pervious paid amount");
                        }

                    }
                    else {


                        var previousBillAmountandbtlwithgst = Number(perviouspaidamount) + Number($('#billTOLINEIncGSTD').val());

                        var aftertotalInvoiceAmountExWaiver = Number(totalInvoiceAmountExWaiver) - Number(previousBillAmountandbtlwithgst);

                        $('#counterCollection').val(aftertotalInvoiceAmountExWaiver);


                        var counterCollectionres = $('#counterCollection').val();

                        $('#inoviceTotalAmount').val(counterCollectionres);

                        if (counterCollectionres < 0) {
                            $('#balanceInoviceAmount').val(0);

                        }
                        else {

                            var totalbalanceInoviceAmount = 0

                            totalbalanceInoviceAmount = ((aftertotalInvoiceAmountExWaiver) + (Number($("#excessAmount").val())));
                            if (totalbalanceInoviceAmount < 0) {
                                totalbalanceInoviceAmount = 0;
                            }
                            $("#balanceInoviceAmount").val(totalbalanceInoviceAmount);

                        }



                        if (counterCollectionres < 0) {

                            alert("your counterCollection is less then zero please check your waiver , btl and pervious paid amount");
                        }

                    }
                });

        }
    }


}

function refresh() {
    window.location.reload();
}





function printletpass() {

    var clearingAgentdropdownid = $('#agents').val();

    console.log("clearingAgentdropdownid", clearingAgentdropdownid)

    if (clearingAgentdropdownid) {
        window.open('/Import/Reports/PartyLetPassAmount?PartyId=' + clearingAgentdropdownid, "_blank");

    }
    else {
        //showToast("please select agent", "error");
        alert("please select agent");
    }


}


function getperviouslogDetail() {
    if (type == "CFS") {

        containerIndexId = $("#indexdb option:selected").val();


        $.get('/APICAlls/GetPerviousInvoicesLogCFS?ContainerIndexId=' + containerIndexId, function (data) {

            console.log("GetPerviousInvoicesLogCFS", data)
            if (data) {
                gridforlogs(data);

            }
            else {
                gridforlogs([]);
            }


        });
    }
    else {

        $.get('/APICAlls/GetPerviousInvoicesLogCY?igm=' + igm + "&indexNo=" + $("#containerIndexCYdb option:selected").val(), function (data) {

            console.log("GetPerviousInvoicesLogCY", data)
            if (data) {
                gridforlogs(data);

            }
            else {
                gridforlogs([]);
            }

        });
    }


}
function gridforlogs(data) {
    var dataGrid = $("#gridContainer2").dxDataGrid({
        dataSource: data,
        keyExpr: "invoiceInquiryId",
        wordWrapEnabled: true,
        showBorders: true,
        showBorders: true,
        allowColumnResizing: true,
        columnAutoWidth: true,
        dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
        paging: {
            enabled: false
        },
        editing: {
            mode: "row",
            allowAdding: true,
            allowUpdating: false,
            selectTextOnEditStart: true,
            startEditAction: "click"
        },
        columns: [
            {
                dataField: "callerName",
                caption: "Caller Name",
                validationRules: [{ type: "required" }],

            },
            {
                dataField: "inquiryDate",
                caption: "Inquiry Date",
                dataType: 'date',
                format: 'dd/MM/yyyy',
                validationRules: [{ type: "required" }],

            }, {
                dataField: "cbm",
                caption: "CBM",
                dataType: "number",
                validationRules: [{ type: "required" }],


            }, {
                dataField: "ammount",
                caption: "Amount",
                dataType: "number",
                format: "#,##0.##",
                validationRules: [{ type: "required" }],


            }, {
                dataField: "inquiryAbout",
                caption: "Inquiry About",
                validationRules: [{ type: "required" }],

            }, {
                dataField: "remarks",
                caption: "Remarks",
                validationRules: [{ type: "required" }],

            }

        ],
        onRowInserted: function (e) {

            console.log(e)
            console.log(e.data)
            var data = e.data;


            console.log("data", data)

            if (type == "CFS") {

                var containerindxid = $('#indexdb').val();


                if (containerindxid) {
                    console.log("containerindxid", containerindxid);

                    $.post('/Invoice/AddInvoiceInquiryCFS?containerindxid=' + containerindxid, { data, data }, function (result) {

                        console.log("result", result)
                        if (result.error) {
                            showToast(result.message, "warning");
                        }

                        else {

                            showToast(result.message, "success");

                        }

                        window.setInterval('refresh()', 3000);
                    });

                } else {
                    showToast("please select igm index", "error");

                }
            }

            else if (type == "CY") {

                var cyindexno = $('#containerIndexCYdb').val();

                if (igm && cyindexno) {

                    console.log("igm", igm);
                    console.log("cyindexno", cyindexno);



                    $.post('/Invoice/AddInvoiceInquiryCY?igm=' + igm + '&cyindexno=' + cyindexno, { data, data }, function (result) {

                        console.log("result", result)
                        if (result.error) {
                            showToast(result.message, "warning");
                        }

                        else {

                            showToast(result.message, "success");

                        }

                        window.setInterval('refresh()', 3000);
                    });


                } else {
                    showToast("please select igm index", "error");

                }
            }
            else {
                showToast("please select type", "error");
            }




        },
    }).dxDataGrid("instance");
}


