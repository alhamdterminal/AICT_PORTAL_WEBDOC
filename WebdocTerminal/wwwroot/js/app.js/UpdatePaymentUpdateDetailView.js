


    var ShippingAgentList = [];
    var ShippingLineList = [];



    $(function () {


        GetShippingAgents();
    GetShippingLines();
    });

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


    function GetPaymentUpdatesByDate(fromdate, todate) {


        $.get('/Import/PreAlert/GetPaymentUpdatesByDate?fromDate=' + fromdate + '&todate=' + todate, function (result) {
            console.log("result", result)
            var PaymentUpdateList = result;


            $("#updatePaymentsdetails").dxDataGrid({
                dataSource: PaymentUpdateList,
                keyExpr: "paymentUpdateId",
                showBorders: true,
                selection: { mode: "single" },
                hoverStateEnabled: true,
                columnsAutoWidth: true,
                paging: {
                    pageSize: 15
                },
                scrolling: {
                    columnRenderingMode: "virtual"
                },
                filterRow: {
                    visible: true,
                    applyFilter: "auto"
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."
                },
                editing: {
                    mode: "row",
                    allowDeleting: true,
                    useIcons: true
                },
                headerFilter: {
                    visible: true
                },
                columns: [
                    {
                        dataField: "preAlertNumber",
                        caption: "Alert No",
                    },
                    {
                        dataField: "requestNumber",
                        caption: "Request No",
                    },

                    {
                        dataField: "paymentUpdateCreatedDate",
                        caption: "Created Date",
                        dataType: "date",
                        format: "dd/MMM/yyyy",
                    }
                ],
                masterDetail: {
                    enabled: true,
                    template: function (container, options) {

                        $("<div>")
                            .dxDataGrid({
                                columnAutoWidth: true,
                                showBorders: true,
                                filterRow: {
                                    visible: true,
                                    applyFilter: "auto"
                                },
                                headerFilter: {
                                    visible: true
                                },
                                editing: {
                                    mode: "row",
                                    allowDeleting: true,
                                    allowUpdating: true,
                                    useIcons: true
                                },
                                columns: [

                                    {
                                        dataField: "secDeposit",
                                        caption: "Sec Deposit",
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
                                    //    dataField: "thcDoc",
                                    //    validationRules: [{ type: "required" }],
                                    //    caption: "THC Doc",
                                    //},
                                    //{
                                    //    dataField: "thcInsurance",
                                    //    validationRules: [{ type: "required" }],
                                    //    caption: "THC Insurance",
                                    //},
                                    {
                                        dataField: "thc",
                                        caption: "THC",
                                        dataType: "number",
                                        format: "#,##0.##",
                                        width: 120,
                                        cssClass: 'myClass',
                                        caption: "THC Charges",
                                        editorOptions: {
                                            step: 0
                                        }
                                    },
                                    //{
                                    //    dataField: "thcOthers",
                                    //    validationRules: [{ type: "required" }],
                                    //    caption: "THC Others",

                                    //},
                                    //{
                                    //    dataField: "totalTHCCharges",
                                    //    validationRules: [{ type: "required" }],
                                    //    caption: "Total THC Charges",
                                    //    calculateCellValue: function (rowData) {
                                    //        console.log("rowData", rowData)
                                    //        var calculatedvalue = rowData.thcDoc + rowData.thcInsurance + rowData.thc + rowData.thcOthers;
                                    //        return rowData.totalTHCCharges = calculatedvalue;
                                    //    },

                                    //},
                                    {
                                        dataField: "lolo",
                                        caption: "Lolo",
                                        dataType: "number",
                                        format: "#,##0.##",
                                        width: 120,
                                        cssClass: 'myClass',
                                        caption: "LOLO Charges",
                                        editorOptions: {
                                            step: 0
                                        }
                                    },
                                    {
                                        dataField: "detention",
                                        dataType: "number",
                                        format: "#,##0.##",
                                        width: 120,
                                        cssClass: 'myClass',
                                        caption: "Detention",
                                        editorOptions: {
                                            step: 0
                                        }
                                    },
                                    {
                                        dataField: "totalCharges",
                                        caption: "Total Charges",
                                        dataType: "number",
                                        format: "#,##0.##",
                                        cssClass: 'myClass',
                                        allowEditing: false,
                                        calculateCellValue: function (rowData) {

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
                                                width: 300,
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
                                                width: 300,
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
                                                width: 300,
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
                                                width: 300,
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
                                                width: 300,
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
                                                width: 300,

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
                                onRowUpdated: function (e) {
                                    console.log(e);
                                    var data = e.data;

                                    if (data != null) {


                                        console.log("PaymentUpdateDetail", data);
                                        var checkres = false;


                                        if ((data.thc != null && data.thc > 0) && (data.lolo != null && data.lolo > 0) && (data.loloIncInTHC != null && data.loloIncInTHC > 0)) {
                                            checkres = true;
                                            return alert("are added amount both in thc and lolo and loloIncInTHC in containerno " + data.containerNumber);
                                        }

                                        if ((data.thc != null && data.thc > 0) && (data.lolo == null || data.lolo == 0) && (data.loloIncInTHC == null || data.loloIncInTHC == 0)) {
                                            checkres = true;
                                            return alert("please add amount in   lolo or loloIncInTHC in containerno " + data.containerNumber);
                                        }

                                        if ((data.detention != null && data.detention > 0) && ((data.secDeposit != null && data.secDeposit > 0) || (data.thc != null && data.thc > 0) || (data.lolo != null && data.lolo > 0) || (data.loloIncInTHC != null && data.loloIncInTHC > 0))) {
                                            checkres = true;
                                            return alert("if you add amount in  detention can't add other amounts in containerno " + data.containerNumber);
                                        }



                                        if (checkres == false) {

                                            $.post('/Import/PreAlert/UpdatePaymentUpdateDetail', { data, data }, function (result) {
                                                if (result.error) {
                                                    showToast(result.message, "warning");
                                                }

                                                else {

                                                    showToast(result.message, "success");

                                                }
                                            });
                                        }




                                    }
                                    //asd



                                },
                                onRowRemoved: function (e) {
                                    console.log(e.data)
                                    var key = e.data.preAlertPayOrderDetailId;

                                    console.log("key", key);

                                    $.post('/Import/PreAlert/DeletePaymentUpdateDetail?key=' + key, function (result) {

                                        if (result.error) {
                                            showToast(result.message, "warning");
                                        }
                                        else {
                                            showToast(result.message, "success");
                                        }

                                        window.setInterval('refresh()', 3000);
                                    })

                                },
                                dataSource: new DevExpress.data.DataSource({
                                    store: new DevExpress.data.ArrayStore({
                                        data: options.data.paymentUpdateDetails
                                    }),
                                })
                            }).appendTo(container);

                    }
                },


                onEditorPreparing: function (e) {
                    // hideMenifestedColumns(e);
                },
                onRowRemoved: function (e) {
                    console.log(e)
                    var key = e.data.paymentUpdateId;

                    console.log("key", key);

                    $.post('/Import/PreAlert/DeletePaymentUpdate?key=' + key, function (result) {

                        if (result.error) {
                            showToast(result.message, "warning");
                        }

                        else {

                            showToast(result.message, "success");

                        }

                        window.setInterval('refresh()', 3000);

                    })

                }

            });

        });


    }
    function refresh() {
        window.location.reload();
    }
    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function formfiled() {

    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {



        checkColumn(e, "preAlertPayOrderNumber");

    checkColumn(e, "requestNumber");
    checkColumn(e, "payOrderCreatedDate");

    }

    function ShowDetail() {


        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;

    GetPaymentUpdatesByDate(fromdate , todate);
    }

