
    $(function () {

        $("#cnic").inputmask("99999-9999999-9");

    $("#consigneeNTN").inputmask("9999999-9");
    var dtToday = new Date();

    advancebilldate(dtToday)

    });

    var gdnumberres = "";

    function gdNumber_changed(data) {

        console.log("data2", data.value)


        var gdno = data.value;
    gdnumberres = gdno;
    if (gdno) {
            var buttn = document.getElementById("hidebutton");

    buttn.style.display = 'block';

    getInvoiceExport(gdno);

    getInvoicesList(gdno);
        }
    else {

            var buttn = document.getElementById("hidebutton");

    buttn.style.display = 'none';

    resetAllValues();
        }


    }


    function advancebilldate(dtToday) {

        console.log("dtToday", dtToday)
        var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
    month = '0' + month.toString();
    if (day < 10)
    day = '0' + day.toString();

    var minDate = year + '-' + month + '-' + day;


    $('#advanceBillDate').attr('min', minDate);
    }


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    //function cargoCallback(id) {

    //    var buttn = document.getElementById("hidebutton");

    //    buttn.style.display = 'block';

    //    cargoId = id;

    //    getInvoiceExport();


    //    getInvoicesList(cargoId);
    //}


    var itemsDetail = [];

    function getInvoicesList(gdnumber) {

        console.log("GetGDInvoices", gdnumber);

    $.get('/APICalls/GetGDInvoicesExprot?gdnumber=' + gdnumber, function (data) {

            if (data) {
        itemsDetail = data
                printcreatedinvoices(itemsDetail)
            }
    else {
        itemsDetail = [];
    printcreatedinvoices(itemsDetail)
            }

    console.log("itemsDetail", itemsDetail)


        });
    }

    function printcreatedinvoices(itemsDetail) {
        console.log("itemsDetail", itemsDetail)

        var dataGrid = $("#invoiceNositemsGrid").dxDataGrid({
        dataSource: itemsDetail,
    keyExpr: "invoiceExportId",
    wordWrapEnabled: true,
    showBorders: true,
    columnAutoWidth: true,

    paging: {
        enabled: false
            },

    editing: {
        mode: "row",
            },
    columns: [
    "invoiceNo",
    "isSubBill"
    ],

        }).dxDataGrid("instance");
    }
    function resetAllValues() {

        var dtToday = new Date();
    advancebilldate(dtToday)

    LoadGrid('#tariffGrid', []);
    printcreatedinvoices([]);

    $('#CBM').val(0);
    $('#Weight').val(0);
    $('#storageDays').val(0);
    $('#gateindate').val('');
    $('#vessel').val('');
    $('#voyage').val('');
    $('#etd').val('');
    $('#advanceBillDate').val('');

    $('#shipping_agent').val('');
    $('#packageType').val('');
    $('#numberOfPackages').val('');
    $('#dischargePort').val('');
    $('#consignee').val('');
    $('#consigneeNTN').val('');
    //$('#agents').select2().val('').trigger('change.select2');
    $('#clearingAgent').val('');
    $('#repname').val('');
    $('#phonenumber').val('');
    $('#cnic').val('');

    $('#waiverAmount').val(0);
    $('#additionalFreeDays').val(0);

    $('#storageTotal').val(0);
    $('#previousBillAmount').val(0);
    $('#afterSalesTax').val(0);
    $('#totalamount').val(0);
    $('#grandTotal').val(0);
    $('#totalCharges').val(0);
    $('#balanceAmountTotal').val(0);
    $('#salesTax').val(0);
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function getInvoiceExport(gdno) {

        if (gdno) {

        $.get('/APICAlls/GetExportInvoiceByGdnumber?gdnumber=' + gdno, function (data) {


            console.log("datadatadata", data)

            if (data) {



                $('#consignee').val(data.consignee);
                $('#consigneeNTN').val(data.consigneeNTN);

                // $('#agents').val(data.clearingAgentId).trigger('change.select2');

                $('#clearingAgent').val(data.clearingAgentName);


                $('#shipping_agent').val(data.shippingAgnet);

                if (data.weight) {

                    $('#Weight').val(data.weight);
                }
                else {

                    $('#Weight').val(0);
                }


                if (data.cbm) {

                    $('#CBM').val(data.cbm);
                }
                else {

                    $('#CBM').val(0);
                }



                $("#Weight").attr({
                    "min": $('#Weight').val()
                });

                $("#CBM").attr({
                    "min": $('#CBM').val()
                });


                if (data.gateInDate != null) {

                    $('#gateindate').val(new Date(data.gateInDate.split("T")[0]).toISOString().substr(0, 10));

                } else {

                    $('#gateindate').val('');
                }

                if (data.advanceDate != null) {
                    advancebilldate(new Date(data.advanceDate))
                    $('#advanceBillDate').val(new Date(data.advanceDate.split("T")[0]).toISOString().substr(0, 10));
                }
                else {
                    var dtToday = new Date();

                    advancebilldate(dtToday)
                    $('#advanceBillDate').val('');
                }

                if (data.etd != null) {

                    $('#etd').val(new Date(data.etd.split("T")[0]).toISOString().substr(0, 10));

                } else {

                    $('#etd').val('');
                }

                $('#vessel').val(data.vessel);
                $('#voyage').val(data.voyage);

                $('#packageType').val(data.packageType);
                $('#numberOfPackages').val(data.noOfPackages);
                $('#dischargePort').val(data.dishargePort);
                $('#cnic').val(data.cnic);
                $('#repname').val(data.agentRepName);
                $('#phonenumber').val(data.phoneNumber);

                $('#waiverAmount').val(data.waiverAmount);
                $('#additionalFreeDays').val(data.additionalFreeDays);
                $('#previousBillAmount').val(data.previousBillAmount);
                $('#salesTax').val(data.salesTax);

            } else {
                resetAllValues();
            }
        });
        }

    }

    function getCbmTotal() {

        if (gdnumberres) {

            var gateInDate = document.getElementById("gateindate").value;

    if (gateInDate) {

        weight = $('#Weight').val();
    cbm = $('#CBM').val();

                //$.get('/APICAlls/GetExportCBMTariffsForInvoice?gdnumber=' + gdnumberres + "&Weight=" + weight + "&CBM=" + cbm, function (data) {
        $.get('/APICAlls/GetExportTariffList?gdnumber=' + gdnumberres + "&Weight=" + weight + "&CBM=" + cbm, function (data) {


            console.log(data)
            var newarraydata = [];

            if (data) {
                data.forEach(c => {


                    var result = newarraydata.find(t => t.description == c.description);

                    if (result != null) {
                        result.amount += c.amount;

                    }
                    else {
                        newarraydata.push(c);

                    }


                });
            }

            console.log("before new array", newarraydata);

            LoadGrid('#tariffGrid', newarraydata);


            getStorageTotal();

        })
    }
    else {
        showToast("Please select gatein date", "warning");

            }

        }

    else {
        showToast("please select gd number", "error")
    }


    }



    function getStorageTotal() {
        //if (gdnumberres) {
        //    var gateInDate = document.getElementById("gateindate").value;

        //    if (gateInDate) {
        //        var advanceDate = invoiceForm.elements["AdvanceDate"].value;

        //        var etddate = document.getElementById("etd").value;


        //        if (advanceDate != "") {

        //            BillDate = advanceDate;
        //        } else {
        //            BillDate = etddate;
        //        }

        //        weight = $('#Weight').val();
        //        cbm = $('#CBM').val();

        //        $.post('/APICAlls/GetStorageTotalExportForInvoice?gdnumber=' + gdnumberres + '&gateInDate=' + gateInDate + '&Weight=' + weight + '&CBM=' + cbm, { BillDate: BillDate }, function (data) {
        //            console.log("data", data)
        //            if (data) {

        //                console.log("return storage total CY containerCount", data);

        //                $('#storageDays').val(data.storageDays);
        //                $('#storageTotal').val(data.storageTotal);

        //            }
        //            else {
        //                $('#storageDays').val('');
        //                $('#storageTotal').val('');

        //            }

        calculateTotal();


        //        });
        //    }

        //    else {
        //        showToast("Please select gatein date", "warning");

        //    }
        //}
        //else {
        //    showToast("please select gd number", "error")
        //}



    }



    function calculateTotal() {




        var PreviousBillAmount = $('#previousBillAmount').val();

    console.log("PreviousBillAmount", PreviousBillAmount)

    var TariffAmountTotal = $('#tariffAmountTotal').val();

    var StorageTotal = $('#storageTotal').val();




    console.log("TariffAmountTotal", Number(TariffAmountTotal))

    console.log("StorageTotal", Number(StorageTotal))


    var storageTariffTotal = Number(TariffAmountTotal) + Number(StorageTotal)

    console.log("storageTariffTotal", storageTariffTotal);



    $("#totalCharges").val(storageTariffTotal)

    var balanceAmt = (storageTariffTotal - Number(PreviousBillAmount))

    console.log("balanceAmt", balanceAmt);

    $("#balanceAmountTotal").val(balanceAmt)


    calculateSalesTax(balanceAmt)


    }
    function calculateSalesTax(balanceAmt) {


        var salesTax = $('#salesTax').val();

    console.log("balanceAmt", balanceAmt);
    console.log("salesTax", Number(salesTax));
    console.log("salesTax", (Number(salesTax) / 100));
    console.log("salesTax", (balanceAmt * (Number(salesTax) / 100)));

    var salesTaxAmount = Math.round(balanceAmt * (Number(salesTax) / 100));

    console.log("salesTaxAmount", salesTaxAmount);

    $('#afterSalesTax').val(salesTaxAmount);


    var salesTaxAmountbalanceAmt = (salesTaxAmount + balanceAmt)

    $('#grandTotal').val(Number(Math.round(salesTaxAmountbalanceAmt)));


    }

    function generateBill() {


        //if (cargoId == 0) {

        //    showToast("Please select GD!", "warning");

        //    return;
        //}

        getCbmTotal();


    }




    function formatDate(date) {

        return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
    }

    function LoadGrid(id, data) {

        var dataGrid = $(id).dxDataGrid({
        dataSource: data,
    keyExpr: "tariffExportId",
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
        dataField: "tariffExportId",
    caption: "Code",

                },
    {
        dataField: "natureOfTariff",
    caption: "Type",

                },
    {
        dataField: "description",
    caption: "",

                },
    {
        dataField: "amount",
    caption: "Rs.",
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

        if (gdnumberres) {

            var consignNTN = $('#consigneeNTN').val().replace(/[^0-9]/g, "");

    if (consignNTN.length != 8) {
                return showToast("please add consignee ntn", "error");
            }


    var form = document.getElementById('invoiceForm');

    form.reportValidity();


    if (form.checkValidity()) {

                var tariffdata = $("#tariffGrid").dxDataGrid("option", "dataSource");

    if (tariffdata.length) {



        postData = $('#invoiceForm').serialize();


    $.post('/Export/InvoiceExport/SaveBill?' + postData, {allItems: tariffdata, gdnumber: gdnumberres }, function (data) {


        billNo = data.invoiceNo;


    if (data.error == true) {
        showToast(data.message, "error");

                        }
    else {

                            if (data != "null") {

        window.open('/Export/Reports/ExportBillInvoice?BillNo=' + billNo, "_blank");
    window.location.href = window.location.href;


                            }


                        }

                    });
                }

    else {
        showToast("Please Generate Bill", "error");


                }



            }

        }
    else {
        showToast("please select GD-Number")
    }


    }



    function routetoBillInvoiceReport() {

        window.open('/Export/Reports/ExportBillInvoice?BillNo=' + billNo, "_blank");

    }




    function limitWeightCBMValues(id) {

        $("#" + id).change(function () {

            var min = parseInt($(this).attr('min'));
            if ($(this).val() < min) {
                $(this).val(min);
            }
        });
    }







    function formfiled() {
        document.getElementById('ischeckAdvanceBill').onchange = function () {
            document.getElementById('advanceBillDate').disabled = !this.checked;
        }
    }


    function enterCNICNumebr(val) {
        console.log("val", val)
        FindData(val);
    }


    function FindData(val) {
        var val = val;
    console.log("val", val)
    if (val) {
        $.get('/APICalls/GetExportAgentNameAndPhoneNo?val=' + val, function (data) {
            if (data) {

                $('#phonenumber').val(data.phoneNumber);
                $('#repname').val(data.clearingAgentRep);
            }
            else {

                $('#phonenumber').val('');
                $('#repname').val('');
            }

        });
        }
    else {
        alert("Please Enter Valid CNIC");
        }
    }




