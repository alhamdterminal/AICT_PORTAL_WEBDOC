

    $(document).ready(function () {
        $('.fav_clr').select2({
            placeholder: ' please select Pre Alters',
            width: '100%',
            border: '1px solid #e4e5e7',
        });
    });

    function refresh() {
        window.location.reload();
    }

    var PaymentUpdateId = 0;
    var PaymentUpdateDetail = [];
    var ShippingAgentList = [];
    var ShippingLineList = [];


    $(function () {
        GetShippingAgents();
    GetShippingLines();
    getPaymentUpdateNumber();
    })

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }


    function formfiled() {

    }


    function hideMenifestedColumns(e) {

        checkColumn(e, "shippingLineName");
    checkColumn(e, "containerNumber");
    checkColumn(e, "size");
    checkColumn(e, "cargoType");
    checkColumn(e, "type");
    checkColumn(e, "soccoc");
    //checkColumn(e, "totalTHCCharges");
    checkColumn(e, "totalCharges");
    }

    function submitPaymentUpdate() {

        var number = "";

    number = $("#perAlertNumber").val();
    console.log("number", number);



    $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

    var f = document.getElementById('PaymentUpdateForm');
    if (f.checkValidity()) {
            var values = $('#PaymentUpdateForm').serialize();

    console.log("values", values)
    console.log("PaymentUpdateDetail", PaymentUpdateDetail)


            if (PaymentUpdateDetail.length > 0) {


        console.log("PaymentUpdateDetail", PaymentUpdateDetail);
    var checkres = false;
 
                PaymentUpdateDetail.filter(t => {

                    if ((t.thc != null && t.thc > 0) && (t.lolo != null && t.lolo > 0) && (t.loloIncInTHC != null && t.loloIncInTHC > 0)) {
        checkres = true;
    return alert("are added amount both in thc and lolo and loloIncInTHC in containerno " + t.containerNumber);
                    }

                    if ((t.thc != null && t.thc > 0) && (t.lolo == null || t.lolo == 0) && (t.loloIncInTHC == null || t.loloIncInTHC == 0)) {
        checkres = true;
    return alert("please add amount in   lolo or loloIncInTHC in containerno " + t.containerNumber);
                    }

                    if ((t.detention != null && t.detention > 0) && ((t.secDeposit != null && t.secDeposit > 0) || (t.thc != null && t.thc > 0) || (t.lolo != null && t.lolo > 0) || (t.loloIncInTHC != null && t.loloIncInTHC > 0))) {
        checkres = true;
    return alert("if you add amount in  detention can't add other amounts in containerno " + t.containerNumber);
                    }
                       
                });

    if (checkres == false) {
        $.post('/Import/PreAlert/AddPaymentUpdate?' + values + '&number=' + number, { Details: PaymentUpdateDetail }, function (result) {

            console.log("result", result)
            if (result.error) {
                //showToast(result.message, "warning");
                alert(result.message);
            }

            else {
                //showToast(result.message, "success");
                alert(result.message);
            }

            window.setInterval('refresh()', 3000);
        })
    }

                  

          
            }
    else {

        alert("Add at least 1 Detail");
            }

        }

    checkValidations();
    }

    function checkValidations() {

        //if (!$('#accNo').val()) {

        //    $('#accNoerror').html('This is a required field');
        //}
        //else {
        //    $('#accNoerror').html('');
        //}

    }


    function loadGrid(prealertNo) {

        console.log("perAlertNumber", prealertNo)

        $.get('/APICalls/GetMuliplePaymentDetails?prealertNo=' + prealertNo, function (data) {

        console.log("data", data);

    PaymentUpdateDetail = data;


    var grid = $("#paymentupdatedetails").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#paymentupdatedetails").dxDataGrid({
        dataSource: PaymentUpdateDetail,
    keyExpr: "preAlterDetailId",
    wordWrapEnabled: true,
    showBorders: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
                },

    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

                },

    editing: {
        mode: "cell",
    allowUpdating: true,
                },
    columns: [


    "containerNumber",
    {
        dataField: "size",
    width: 70,
    caption: "Size",
                    },
    {
        dataField: "cargoType",
    width: 80,
    caption: "Cargo Type",
                    },
    {
        dataField: "type",
    width: 80,
    caption: " Type",
                    },
    {
        dataField: "soccoc",
    width: 80,
    caption: "SOC / COC",
                    },
    {
        dataField: "secDeposit",
    dataType: "number",
    format: "#,##0.##",
    width: 120,
    caption: "Sec Deposit",
    cssClass: 'myClass',
    editorOptions: {
        step: 0
                        }
                    },

    {
        dataField: "thc",
    dataType: "number",
    format: "#,##0.##",
    width: 120,
    caption: "THC Charges",
    cssClass: 'myClass',
    editorOptions: {
        step: 0
                        }
                    },


                    //{
        //    caption: 'Delivery Order Charges',

        //    columns: [
        //        {
        //            dataField: "thcDoc",
        //            dataType: "number",
        //            format: "#,##0.##",
        //            width: 120,
        //            caption: "THC Doc",
        //            editorOptions: {
        //                step: 0
        //            }
        //        },
        //        {
        //            dataField: "thcInsurance",
        //            dataType: "number",
        //            format: "#,##0.##",
        //            width: 120,
        //            caption: "THC Insurance",
        //            editorOptions: {
        //                step: 0
        //            }
        //        },
        //        {
        //            dataField: "thc",
        //            dataType: "number",
        //            format: "#,##0.##",
        //            width: 120,
        //            caption: "THC",
        //            editorOptions: {
        //                step: 0
        //            }
        //        },
        //        {
        //            dataField: "thcOthers",
        //            dataType: "number",
        //            format: "#,##0.##",
        //            width: 120,
        //            caption: "THC Others",
        //            editorOptions: {
        //                step: 0
        //            }
        //        },

        //    ],
        //},


        {
            dataField: "lolo",
            dataType: "number",
            format: "#,##0.##",
            width: 120,
            caption: "LOLO (Line \ Yard Name)",
            cssClass: 'myClass',
            editorOptions: {
                step: 0
            }
        },
        {
            dataField: "detention",
            dataType: "number",
            format: "#,##0.##",
            width: 120,
            caption: "Detention",
            cssClass: 'myClass',
            editorOptions: {
                step: 0
            }
        },
        //{
        //    dataField: "totalTHCCharges",
        //    dataType: "number",
        //    format: "#,##0.##",
        //    width: 120,
        //    caption: "Total THC Charges",
        //    calculateCellValue: function (rowData) {
        //        console.log("rowData", rowData)
        //        var calculatedvalue = (rowData.thcDoc != null ? Number(rowData.thcDoc) : Number(0)) + (rowData.thcInsurance != null ? Number(rowData.thcInsurance) : Number(0)) + (rowData.thc != null ? Number(rowData.thc) : Number(0)) + (rowData.thcOthers != null ? Number(rowData.thcOthers) : Number(0));
        //        return rowData.totalTHCCharges = calculatedvalue;
        //    },
        //},

        {
            dataField: "totalCharges",
            dataType: "number",
            format: "#,##0.##",
            width: 120,
            cssClass: 'myClass',
            caption: "Total Charges",
            calculateCellValue: function (rowData) {


                //var calculatedvalue = (rowData.secDeposit != null ? Number(rowData.secDeposit) : Number(0))
                //    +
                //    (rowData.thcDoc != null ? Number(rowData.thcDoc) : Number(0))
                //    +
                //    (rowData.thcInsurance != null ? Number(rowData.thcInsurance) : Number(0))
                //    +
                //    (rowData.thc != null ? Number(rowData.thc) : Number(0))
                //    +
                //    (rowData.thcOthers != null ? Number(rowData.thcOthers) : Number(0))
                //    +
                //    (rowData.lolo != null ? Number(rowData.lolo) : Number(0));
                //return rowData.totalCharges = calculatedvalue;


                var calculatedvalue = (rowData.secDeposit != null ? Number(rowData.secDeposit) : Number(0))
                    +
                    (rowData.thc != null ? Number(rowData.thc) : Number(0))
                    +
                    (rowData.lolo != null ? Number(rowData.lolo) : Number(0))
                    +
                    (rowData.detention != null ? Number(rowData.detention) : Number(0));

                return rowData.totalCharges = calculatedvalue;


            },

        },
        {
            dataField: "loloIncInTHC",
            dataType: "number",
            format: "#,##0.##",
            width: 120,
            caption: "LOLO Inc.in THC",
            editorOptions: {
                step: 0
            }
        },

        {
            caption: 'Security Deposited / Detention',

            columns: [
                {
                    dataField: "shippingLineIdForSD",
                    caption: "Line Name",
                    width: 200,
                    lookup: {
                        dataSource: ShippingLineList,
                        valueExpr: "shippingLineId",
                        displayExpr: "shippingLineName",
                        allowClearing: true,
                    }

                },
                {
                    dataField: "shippingAgentIdForSD",
                    caption: "A/C Of",
                    width: 200,
                    lookup: {
                        dataSource: ShippingAgentList,
                        valueExpr: "shippingAgentId",
                        displayExpr: "name",
                        allowClearing: true,
                    }
                },

            ],
        },

        {
            caption: 'THC',

            columns: [
                {
                    dataField: "shippingLineIdForTHC",
                    caption: "Line Name",
                    width: 200,
                    lookup: {
                        dataSource: ShippingLineList,
                        valueExpr: "shippingLineId",
                        displayExpr: "shippingLineName",
                        allowClearing: true,
                    }
                },
                {
                    dataField: "shippingAgentIdForTHC",
                    caption: "A/C Of",
                    width: 200,
                    lookup: {
                        dataSource: ShippingAgentList,
                        valueExpr: "shippingAgentId",
                        displayExpr: "name",
                        allowClearing: true,
                    }
                },

            ],
        },

        {
            caption: 'LOLO',

            columns: [
                {
                    dataField: "shippingLineIdForLOLO",
                    caption: "Line Name",
                    width: 200,
                    lookup: {
                        dataSource: ShippingLineList,
                        valueExpr: "shippingLineId",
                        displayExpr: "shippingLineName",
                        allowClearing: true,
                    }
                },
                {
                    dataField: "shippingAgentIdForLoLo",
                    caption: "A/C Of",
                    width: 200,

                    lookup: {
                        dataSource: ShippingAgentList,
                        valueExpr: "shippingAgentId",
                        displayExpr: "name",
                        allowClearing: true,
                    }
                },

            ],
        },

                  
                  

                ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
                },
    onRowRemoved: function (e) {

    }

            }).dxDataGrid("instance");




    grid.on('editorPrepared', function (e) {
                if (e.parentType !== 'dataRow') {
                    return;
                }

                //$('input[type=text]').on('wheel', function (ad) {

        //});


        e.editorElement[e.editorName]('instance').on('keyDown', function (arg) {

            var gridComponent = e.component;

            var event = arg.jQueryEvent;

            console.log("event", event)
            if (event.keyCode === 38) {
                if (e.editorName == "dxNumberBox") {
                    event.stopPropagation();
                    event.preventDefault();
                }

            } else if (event.keyCode === 40) {
                if (e.editorName == "dxNumberBox") {
                    event.stopPropagation();
                    event.preventDefault();
                }

            }

            else if (event.keyCode === 189) {
                if (e.editorName == "dxNumberBox") {
                    event.stopPropagation();
                    event.preventDefault();
                }

            }
        });


            });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });



    }


    function showdetail() {
        var number = "";

    number = $("#perAlertNumber").val();
    console.log("number", number);

    getprealterDetailbyperAlertNumber(number)


    }


    function perAlertNumber_changed() {




        //var perAlertNumber = data.selectedItem;

        //getprealterDetailbyperAlertNumber(perAlertNumber)

    }

    function getprealterDetailbyperAlertNumber(perAlertNumber) {

        console.log("perAlertNumber", perAlertNumber)

        loadGrid(perAlertNumber);

    }


    function getPaymentUpdateNumber() {
        $.get('/Import/PreAlert/GenPaymentUpdateCode', function (data) {

            console.log("data", data);

            $('#requestNumber').val(data);

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

    function GetShippingAgents() {
        $.get('/ShippingAgent/Get', function (data) {
            ShippingAgentList = data;
            console.log("ShippingAgentList", ShippingAgentList);
        });
    }

    function GetShippingLines() {
        $.get('/ShippingLine/GetShippingLines', function (data) {
            ShippingLineList = data;
            console.log("ShippingLineList", ShippingLineList);
        });
    }

