

    var consigneeList = [];

    var InvoiceItemList = [];

    $(function () {


        $.get('/APICalls/GetALlConsignees', function (res) {
            if (res) {
                consigneeList = res;
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: consigneeList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ConsigneId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                })
            }
            else {
                consigneeList = []
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: consigneeList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ConsigneId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                })
            }
        });



    });




    function formfiled() {
        $('#emptyContainerReceivedId').on('change', function (data) {

            LoadEmptyreceveContainerData();

        });
    }


    function LoadEmptyreceveContainerData() {



        resetformvalue();

    var EmptyContainerReceivedId = $('#emptyContainerReceivedId').val();
    console.log("EmptyContainerReceivedId", EmptyContainerReceivedId)
    if (EmptyContainerReceivedId) {

        $.get('/Import/GateInOut/GetEmptyReceiveContainerData?EmptyContainerReceivedId=' + EmptyContainerReceivedId, function (res) {

            console.log("res", res)

            $('#containersize').val(res.size);
            $('#type').val(res.type);
            $('#vessel').val(res.vessel);
            $('#voyage').val(res.voyage);
            $('#line').val(res.line);
            $('#consigneeName').val(res.consigneeName);
            $('#clearingAgentName').val(res.clearningAgentName);
            $('#portOfLoading').val(res.portOfLoading);
            $('#principal').val(res.principal);
            $('#shippingAgentCode').val(res.shippinAgentId);

            if (res.actualArrive != null) {

                $('#actualarrive').val(new Date(res.actualArrive.split("T")[0]).toISOString().substr(0, 10));

            } else {
                $('#actualarrive').val('');
            }

            gettariffitemslist(res.shippinAgentId);


        });

        } else {
        resetformvalue();
        }


    }


    function resetformvalue() {
        $('#containersize').val('');
    $('#type').val('');
    $('#vessel').val('');
    $('#voyage').val('');
    $('#line').val('');
    $('#consigneeName').val('');
    $('#clearingAgentName').val('');
    $('#portOfLoading').val('');
    $('#principal').val('');
    $('#actualarrive').val('');
    $('#shippingAgentCode').val(0);
    $('#storageDays').val(0);
    tarifflistgrid([]);
    $('#salestax').val(0);
    $('#totalCharges').val(0);
    $('#afterSalesTax').val(0);
    $('#grandTotal').val(0);
        

    }


    function gettariffitemslist(shippingagentid) {



        console.log("shippingagentid", shippingagentid)

        if (shippingagentid) {


            var InDate = document.getElementById("actualarrive").value;

    var size = document.getElementById("containersize").value;



    $.post('/APICalls/GetEmptyContainertariff?shippingAgentId=' + shippingagentid + '&size=' + size + '&arrivedate=' + InDate , function (res) {

        InvoiceItemList = res;

    if (InvoiceItemList) {
        tarifflistgrid(InvoiceItemList);

    getstoragedays();

                }
    else {
        tarifflistgrid([]);
    getstoragedays();
                }
                
            });

           
             
        } else {
        tarifflistgrid([]);
    $('#storageDays').val(0);
    $('#salestax').val(0);
        }


      
    }


    function getstoragedays() {

        var shippingagentid = $('#shippingAgentCode').val();
    console.log("shippiczxczczcxzngagentid", shippingagentid)

    if (shippingagentid) {


            var InDate = document.getElementById("actualarrive").value;

    $.get('/APICalls/GetEmptyContainerdays?shippingAgentId=' + shippingagentid  + '&arrivedate=' + InDate, function (res) {

        console.log("resres", res)

                $('#storageDays').val(res.freeDays);

    $('#salestax').val(res.salesTax);


    createtotal();

            });

        } else {

        $('#storageDays').val(0);
    $('#salestax').val(0);
    $('#totalCharges').val(0);
    $('#afterSalesTax').val(0);
    $('#grandTotal').val(0);
        }

    }


    function tarifflistgrid(data) {


        console.log("data itms", data)
             

            var dataGrid = $("#gridTariff").dxDataGrid({
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
        dataField: "description",
    caption: "Name",

                    },
    {
        dataField: "amount",
    caption: "Rs #",
    format: "#,##0.##",
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

    $('#totalCharges').val(tariffAmountTotal);

    console.log("totalCharges grid", tariffAmountTotal)


    }




    function saveinfo() {


        var form = document.getElementById('EmptyContainerInvoice');

    var model = $('#EmptyContainerInvoice').serialize();


    console.log("model", model)
    console.log("form.reportValidity", form.reportValidity)

    form.reportValidity();
    if (form.checkValidity()) {


        console.log("form", form);


    var tariffdata = $("#gridTariff").dxDataGrid("option", "dataSource");

    console.log("tariffdata", tariffdata);

    postData = model


    $.post('/Invoice/SaveBillEmptyContainer?' + postData, {invoiceItems: tariffdata }, function (data) {

        billNo = data.invoiceNo;
    DeliveryOrderNumber = data.deliveryOrderNumber;


    if (data.error == true) {
        showToast(data.message, "error");

                }
    else {

                    if (data != "null") {

                        if (billNo) {
        window.open('/import/reports/EmptyContainerReceivedInvoice?BillNo=' + billNo + '&BillType=' + "Normal", "_blank");
                        }
    if (DeliveryOrderNumber) {
        window.open('/import/reports/EmptyContainerReceivedDeliveryOrder?DeliveryOrderNumber=' + DeliveryOrderNumber, "_blank");
                        }

    window.location.href = window.location.href;

                    }


                }
                

            });

             
        }
    else {
        console.log("else work")
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


    function createtotal() {

        var TariffAmountTotal = $('#totalCharges').val();
    var salestax = $('#salestax').val();



    var salesTaxAmount = Math.round(Number(TariffAmountTotal) * (Number(salestax) / 100));

    console.log("salestax", salesTaxAmount)


    $('#afterSalesTax').val(salesTaxAmount);
    $('#grandTotal').val(Number(salesTaxAmount) + Number(TariffAmountTotal));



    }




    function printSOC() {

        var containerno = $("#emptyContainerReceivedId option:selected").text();
    var containersize = $("#containersize").val();
    var actualarrive = $("#actualarrive").val();
    var type = $("#type").val();
    var line = $("#line").val();
    var principal = $("#principal").val();
    var consigneeName = $("#consigneeName").val();
    var clearingAgentName = $("#clearingAgentName").val();
    var portOfLoading = $("#portOfLoading").val();
    var vessel = $("#vessel").val();
    var voyage = $("#voyage").val();
    var shippingAgentCode = $("#shippingAgentCode").val();
    var storageDays = $("#storageDays").val();
    var salestax = $("#salestax").val();
    var totalCharges = $("#totalCharges").val();
    var afterSalesTax = $("#afterSalesTax").val();
    var grandTotal = $("#grandTotal").val();
    var tariffdata = $("#gridTariff").dxDataGrid("option", "dataSource");



    let model = {
        containerno: containerno,
    containersize: containersize,
    actualarrive: actualarrive,
    type: type,
    line: line,
    principal: principal,
    consigneeName: consigneeName,
    clearingAgentName: clearingAgentName,
    portOfLoading: portOfLoading,
    vessel: vessel,
    voyage: voyage,
    shippingAgentCode: shippingAgentCode,
    storageDays: storageDays,
    salestax: salestax,
    totalCharges: totalCharges,
    afterSalesTax: afterSalesTax,
    grandTotal: grandTotal,
    invoiceItems: tariffdata
        }

    console.log("model", model);


    $.post('/Import/Reports/ScheduleofChargesEmptycontainer', {invoice :  model }, function (data) {

        $('#excessRemarkModal').modal('toggle');
    $('#repotdata').html(data);




        });


    }
