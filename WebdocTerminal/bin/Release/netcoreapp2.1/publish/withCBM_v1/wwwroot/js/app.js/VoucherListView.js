
    $(function () {

        $('#voucherid').val(0);
    $('#vouchercode').val('');
    GetVoucherTypes();
    GetServiceType();
    getchartofaccounts();
    getdepartments();
    getcustomers();

    });


    function showvoucherCode() {

        Grid();
    }


    function formfiled() {

    }

    var VoucherTypes = [];
    var VoucherServices = [];
    var ChartOfAccounts = [];
    var Departments = [];
    var Customers = [];
    var VoucherList = [];
    var VoucherDetail = [];
    var filterServiceType = false;

    function GetVoucherTypes() {
        $.get('/Setup/GetVoucherType', function (data) {
            VoucherTypes = data;
        });
    }

    function GetServiceType() {
        $.get('/Setup/GetVoucherServiceType', function (data) {
            VoucherServices = data;
        });
    }

    function getchartofaccounts() {
        $.get('/Account/ChartOfAccount/GetChartOfAccountNameAndCode', function (data) {
            ChartOfAccounts = data;
        });
    }

    function getdepartments() {
        $.get('/Setup/GetDepartment', function (data) {
            Departments = data;
        });
    }

    function getcustomers() {
        $.get('/Setup/GetCustomersNameAndCode', function (data) {
            Customers = data;
        });
    }

    function getVoucherDetails(voucherid) {

        if (voucherid) {

        $.post('/Account/Voucher/GetVoucherDetail?voucherid=' + voucherid, function (data) {
            VoucherDetail = data;
        });
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



    function PrintDetail() {

        if (!VoucherTypes.length) {
            return showToast("please wait Voucher type on loading", "error");
        }
    else if (!VoucherServices.length) {
            return showToast("please wait Voucher service type on loading", "error");
        }
    else if (!ChartOfAccounts.length) {
            return showToast("please wait Chart Of Account on loading", "error");
        }
    else if (!Departments.length) {
            return showToast("please wait Department on loading", "error");
        }
    else if (!Customers.length) {
            return showToast("please wait Customers on loading", "error");
        }
    else {

            var userId = $('#userId').val();
    var fromdate = $('#single_cal2').val();
    var todate = $('#single_cal3').val();
    var statusType = $('#statusType').val();
    var search = $('#search').val();
    var voucherTypeId = $('#voucherTypeId').val();

    $.get('/Account/Voucher/GetVoucherList?userId=' + userId + '&fromdate=' + fromdate + '&todate=' + todate + '&status=' + statusType + '&search=' + search + '&voucherTypeId=' + voucherTypeId, function (data) {
        console.log("data", data);
    if (data) {
        VoucherList = VoucherList.map(obj => ({ ...obj, isActive: null }))

                    VoucherList = data;
    GridData(VoucherList)
                }
    else {
        GridData([])
    }
            });
        }
    }

    function GridData(VoucherList) {


        $("#gridContainer").dxDataGrid({
            dataSource: VoucherList,
            keyExpr: "voucherId",
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
                mode: "cell",
                allowDeleting: false,
                allowUpdating: function (e) {

                    if (e.row.data.status != "InProgress") {
                        return false;
                    }
                    else {
                        return true;
                    }
                },
                useIcons: true
            },
            headerFilter: {
                visible: true
            },
            //export: {
            //    enabled: true
            //},
            //onExporting(e) {

            //    console.log("e", e)
            //    const workbook = new ExcelJS.Workbook();

            //    const worksheet = workbook.addWorksheet('VouchersList');

            //    DevExpress.excelExporter.exportDataGrid({
            //        component: e.component,
            //        worksheet,
            //        keepColumnWidths: false,
            //     }).then(() => {
            //        workbook.xlsx.writeBuffer().then((buffer) => {

            //            var resdata = "VouchersList.xlsx";

            //            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), resdata);

            //        });
            //    });
            //    e.cancel = true;


            //},
            columns: [
                {
                    dataField: "voucherTypeId",
                    caption: "Voucher Type",
                    lookup: {
                        dataSource: {
                            store: VoucherTypes,
                            requireTotalCount: true,
                            pageSize: 4,
                            paginate: true,
                        },
                        displayExpr: "name",
                        valueExpr: "voucherTypeId"
                    },
                    allowEditing: false,
                    validationRules: [{ type: "required" }]

                },
                {
                    dataField: "voucherNo",
                    caption: "Voucher No",
                    allowEditing: false,
                    validationRules: [{ type: "required" }]
                },
                {
                    dataField: "voucherDate",
                    caption: "Voucher Date",
                    dataType: "date",
                    format: "dd/MMM/yyyy",
                    allowEditing: false,
                    validationRules: [{ type: "required" }]
                },
                {
                    dataField: "status",
                    caption: "Status",
                    allowEditing: false,
                    validationRules: [{ type: "required" }]
                },
                {
                    dataField: "isActive",
                    dataType: "boolean",
                    caption: "",

                },
                {
                    width: 100,
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        $('<a/>').addClass('dx-link')
                            .text('route')
                            .on('dxclick', function () {
                                update_Voucher(options.row.data)
                            })
                            .appendTo(container);
                    }
                },

                {
                    width: 100,
                    alignment: 'center',
                    cellTemplate: function (container, options) {

                        if (options.data.status == "Posted") {
                            $('<a/>').addClass('dx-link')
                                .text('Duplicate')
                                .on('dxclick', function () {
                                    create_Dublicate(options.row.data)
                                })
                                .appendTo(container);
                        }

                    }
                },
                {
                    width: 100,
                    alignment: 'center',
                    cellTemplate: function (container, options) {

                        if (options.data.status == "Posted") {
                            $('<a/>').addClass('dx-link')
                                .text('Reversal')
                                .on('dxclick', function () {
                                    create_Reversal(options.row.data)
                                })
                                .appendTo(container);
                        }

                    }
                }
            ],
            masterDetail: {
                enabled: true,
                template: function (container, options) {
                    $("<div>")
                        .dxDataGrid({
                            dataSource: new DevExpress.data.DataSource({
                                store: new DevExpress.data.ArrayStore({
                                    data: options.data.voucherDetails
                                }),
                            }),
                            keyExpr: "voucherDetailId",

                            columnAutoWidth: true,
                            showBorders: true,
                            filterRow: {
                                visible: true,
                                applyFilter: "auto"
                            },
                            headerFilter: {
                                visible: true
                            },

                            //onContentReady: function (e) {
                            //    console.log(options.data.status)
                            //    e.element.find(".dx-datagrid-addrow-button").click(function (ee) {
                            //        console.log("E", ee)
                            //        if (options.data.status != "InProgress")
                            //            //ee.stopPropagation()
                            //            ee.currentTarget.jQuery2240340867160092495742.dxButton.visible = false
                            //    });
                            //},

                            editing: {
                                mode: "row",
                                allowDeleting: function (e) {
                                    if (options.data.status != "InProgress") {
                                        return false;
                                    }
                                    else {
                                        return true;
                                    }
                                },
                                allowUpdating: function (e) {
                                    if (options.row.data.status != "InProgress") {
                                        return false;
                                    }
                                    else {
                                        return true;
                                    }
                                },
                                allowAdding: true,
                                useIcons: true
                            },
                            columns: [

                                {
                                    dataField: "voucherServiceTypeId",
                                    caption: "Service Type",
                                    setCellValue(rowData, value, e) {
                                        rowData.voucherServiceTypeId = value;
                                        rowData.chartOfAccountId = null;
                                        if (VoucherServices.find(x => x.voucherServiceTypeId == value).code == "CD" || VoucherServices.find(x => x.voucherServiceTypeId == value).code == "P") {
                                            filterServiceType = true;
                                        }
                                        else {
                                            filterServiceType = false;
                                        }

                                    },
                                    lookup: {
                                        dataSource: {
                                            store: VoucherServices,
                                            requireTotalCount: true,
                                            pageSize: 4,
                                            paginate: true,
                                        },
                                        displayExpr: function (item) {
                                            return item && item.code + "-" + item.name;
                                        },
                                        valueExpr: "voucherServiceTypeId"
                                    },
                                    validationRules: [{ type: "required" }]

                                },
                                {
                                    dataField: "customerId",
                                    caption: "Customer",
                                    width: 250,
                                    lookup: {
                                        dataSource: {
                                            store: Customers,
                                            requireTotalCount: true,
                                            pageSize: 4,
                                            paginate: true,
                                        },
                                        displayExpr: "name",
                                        valueExpr: "customerId",
                                        allowClearing: true,
                                    },
                                    setCellValue(rowData, value, e) {

                                        if (e.voucherServiceTypeId) {
                                            if (VoucherServices.find(x => x.voucherServiceTypeId == e.voucherServiceTypeId).code == "CD" || VoucherServices.find(x => x.voucherServiceTypeId == e.voucherServiceTypeId).code == "P") {
                                                filterServiceType = true;
                                            }
                                            else {
                                                filterServiceType = false;
                                            }
                                        }
                                        if (filterServiceType == true) {
                                            var resdata = Customers.find(x => x.customerId == value);

                                            if (resdata) {
                                                rowData.chartOfAccountId = resdata.chartOfAccountId;
                                            }
                                            else {
                                                rowData.chartOfAccountId = null;
                                            }
                                        }
                                        else {
                                            rowData.chartOfAccountId = null;
                                        }
                                        rowData.customerId = value;


                                    },

                                },
                                {
                                    dataField: "chartOfAccountId",
                                    caption: "Control Account",
                                    lookup: {
                                        dataSource: {
                                            store: ChartOfAccounts,
                                            requireTotalCount: true,
                                            pageSize: 4,
                                            paginate: true,
                                        },
                                        displayExpr: "accountName",
                                        valueExpr: "chartOfAccountId"
                                    },
                                    validationRules: [{ type: "required" }]

                                },
                                {
                                    dataField: "departmentId",
                                    caption: "Department",
                                    lookup: {
                                        dataSource: {
                                            store: Departments,
                                            requireTotalCount: true,
                                            pageSize: 4,
                                            paginate: true,
                                        },
                                        displayExpr: function (item) {
                                            return item && item.departmentCode + "-" + item.departmentName;
                                        },
                                        valueExpr: "departmentId"
                                    },
                                    validationRules: [{ type: "required" }]

                                },

                                {
                                    dataField: "debit",
                                    validationRules: [{ type: "required" }],
                                    caption: "Debit",
                                    dataType: "number",
                                    format: "#,##0.##",
                                    width: 120,
                                    editorOptions: {
                                        step: 0
                                    },


                                },
                                {
                                    dataField: "credit",
                                    validationRules: [{ type: "required" }],
                                    caption: "Credit",
                                    dataType: "number",
                                    format: "#,##0.##",
                                    width: 120,
                                    editorOptions: {
                                        step: 0
                                    },

                                },
                                {
                                    dataField: "chequeOrReference",
                                    caption: "Ref/Cheq#",
                                },
                                {
                                    dataField: "narration",
                                    validationRules: [{ type: "required" }],
                                    caption: "Narration",
                                },

                            ],
                            summary: {
                                totalItems: [
                                    {
                                        column: "debit",
                                        summaryType: "sum",
                                        customizeText(data) {
                                            return `  ${data.value.toLocaleString()}`;
                                        },
                                    },
                                    {
                                        column: "credit",
                                        summaryType: "sum",
                                        customizeText(data) {
                                            return `  ${data.value.toLocaleString()}`;
                                        },
                                    },
                                ]
                            },

                            onEditorPreparing: function (e) {
                                hideMenifestedColumns(e);
                            },
                            onRowInserted: function (e) {

                                if (e.data.debit > 0 && e.data.credit > 0) {

                                    PrintDetail();
                                    return showToast("debit and credit both are > 0", "error");
                                }

                                else if (e.data.debit <= 0 && e.data.credit <= 0) {
                                    PrintDetail();
                                    return showToast("debit and credit both are < 0", "error");
                                }
                                else {
                                    var model = e.data;
                                    $.post('/Account/Voucher/AddVoucherDetailOnly?VoucherId=' + options.data.voucherId, { model, model }, function (data) {
                                        if (data.error == true) {
                                            showToast(data.message, "error");
                                            PrintDetail();
                                        } else {
                                            showToast("Created", "success");
                                            PrintDetail();

                                        }
                                    });
                                }
                            },
                            onRowUpdated: function (e) {
                                console.log(e);


                                if (e.data.debit > 0 && e.data.credit > 0) {
                                    PrintDetail();
                                    return showToast("debit and credit both are > 0", "error")
                                }

                                else if (e.data.debit <= 0 && e.data.credit <= 0) {
                                    PrintDetail();
                                    return showToast("debit and credit both are < 0", "error")
                                }

                                else {
                                    var model = e.data;
                                    $.post('/Account/Voucher/UpdateVoucherDetail', { model, model }, function (data) {

                                        if (data.error == true) {
                                            showToast(data.message, "error")
                                            PrintDetail();
                                        } else {
                                            showToast(data.message, "success")
                                            PrintDetail();
                                        }
                                    });
                                }
                            },
                            onRowRemoved: function (e) {
                                console.log(e.data)
                                var model = e.data;
                                $.post('/Account/Voucher/DeleteVoucherDetail', { model, model }, function (data) {

                                    if (data.error == true) {
                                        showToast(data.message, "error")
                                        PrintDetail();
                                    } else {
                                        showToast(data.message, "success")
                                        PrintDetail();
                                    }
                                });

                            },

                        }).appendTo(container);


                },
            },



        });

    }


    function create_Dublicate(data) {

        $.post('/Account/Voucher/CreateDublicateVoucher?voucherId=' + data.voucherId, function (res) {
            if (res.error == true) {
                showToast(res.message, "error");
            } else {


                alert("Voucher No  " + res.message.voucherNo + " Created ");

            }
        });
    }

    function create_Reversal(data) {

        $.post('/Account/Voucher/CreateReversalVoucher?voucherId=' + data.voucherId, function (res) {
            if (res.error == true) {
                showToast(res.message, "error");
            } else {


                alert("Voucher No  " + res.message.voucherNo + " Created ");

            }
        });
    }

    function update_Voucher(data) {

        window.open('/Account/Voucher/UpdateVoucherView?VoucherId=' + data.voucherId, "_blank");

    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "chartOfAccountId");
        //checkColumn(e, "isActive");

    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField == field) {

            if (filterServiceType == true) {

        e.editorOptions.disabled = true;
            }

    if (e.row.data.voucherServiceTypeId) {
                if (VoucherServices.find(x => x.voucherServiceTypeId == e.row.data.voucherServiceTypeId).code == "CD" || VoucherServices.find(x => x.voucherServiceTypeId == e.row.data.voucherServiceTypeId).code == "P") {
        e.editorOptions.disabled = true;
                }
            }

        }
        //if (e.parentType === "dataRow" && e.dataField == field) {

        //    return null;
        //}

    }

    function PostVouchers() {

        var listoferrorouchers = [];

        var datalist = VoucherList.filter(c => c.isActive == true);


        datalist.filter(c => {

            if (c.status != "InProgress") {
        c.narration = "it's not in progress ";
    listoferrorouchers.push(c);
            }

    if (c.voucherDetails.length == 0) {
        c.narration = "voucher detail is missing";
    listoferrorouchers.push(c);
            }


            var total = (c.voucherDetails.map(item => item.debit).reduce((prev, next) => prev + next, 0)) - (c.voucherDetails.map(item => item.credit).reduce((prev, next) => prev + next, 0));

    if (total != 0) {
        c.narration = "debit and credit are not equals";
    listoferrorouchers.push(c);
            }
        });


    console.log("listoferrorouchers", listoferrorouchers);

    if (listoferrorouchers.length) {
        loadGriderror(listoferrorouchers);
    $('#ErroeStatusModal').modal('toggle');
        }
    else {
        loadGriderror([]);
    if (window.confirm('are you show want to post vouchers?')) {


                if (datalist.length) {
        $.post('/Account/Voucher/PostVouchers', { model: datalist }, function (data) {
            if (data.error == true) {
                loadGriderror(data.message);
                $('#ErroeStatusModal').modal('toggle');
            }
            else {
                loadGriderror([]);
                showToast(data.message, "success");
                PrintDetail();
            }
        });
                }
    else {
        showToast("please select voucher", "error");
                }
                
            }
        }
    }

    function cancelvouchers() {

        var listoferrorouchers = [];

        var datalist = VoucherList.filter(c => c.isActive == true);


        datalist.filter(c => {

            if (c.status == "Posted") {
        c.narration = "it's  Posted voucher";
    listoferrorouchers.push(c);
            }

    if (c.status == "Canceled") {
        c.narration = "it's  already Canceled voucher";
    listoferrorouchers.push(c);
            }
    
        });


    console.log("listoferrorouchers", listoferrorouchers);

    if (listoferrorouchers.length) {
        loadGriderror(listoferrorouchers);
    $('#ErroeStatusModal').modal('toggle');
        }
    else {
        loadGriderror([]);
    if (window.confirm('are you show want to cancel vouchers?')) {
                if (datalist.length) {
        $.post('/Account/Voucher/CancelVouchers', { model: datalist }, function (data) {
            if (data.error == true) {
                loadGriderror(data.message);
                $('#ErroeStatusModal').modal('toggle');
            }
            else {
                loadGriderror([]);
                showToast(data.message, "success");
                PrintDetail();
            }
        });
                }
    else {
        showToast("please select voucher", "error");
                }
            
            }
        }
    }

    function selectAll() {

        // var PaymentReceiveGriddata = $("#gridContainer").dxDataGrid("option", "dataSource");
        // console.log("PaymentReceiveGriddata", PaymentReceiveGriddata)


        VoucherList.forEach(c => {

            if (c.status == "InProgress") {
                c.isActive = true
            }
        });

    GridData(VoucherList);

    }

    function unselectAll() {

        VoucherList.forEach(c => {
            if (c.status == "InProgress") {
                c.isActive = false
            }
        });

    GridData(VoucherList)
    }

    function loadGriderror(data) {

        console.log("data", data)
        $("#errorGrid").dxDataGrid({
        dataSource: data,
    keyExpr: "voucherId",
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    paging: {
        pageSize: 10
            },
    columns: [
    {
        dataField: "voucherNo",
    caption: "Voucher No",
    allowEditing: false,
                },
    {
        dataField: "narration",
    caption: "Narration",
    allowEditing: false,
                },
    ],

        });

    }

    function Printvouchers() {

         var Griddata = $("#gridContainer").dxDataGrid("option", "dataSource");
    console.log("Griddata", Griddata)

    if (Griddata.length) {

        voucherId = Griddata.map(function (item) { return item["voucherId"]; });
    voucherId = voucherId.toString();

    window.open('/Account/Reports/VoucherReport?VoucherId=' + voucherId , "_blank");
        }



        //var gridInstance = $("#gridContainer").dxDataGrid("instance");
        //console.log("gridInstance", gridInstance)


        //var VoucherNo = $('#search').val();
        //var userid = $('#userId').val();
        //var status = $('#statusType').val();
        //var fromdate = $('#single_cal2').val();
        //var toDate = $('#single_cal3').val();

        //if (voucherid != 0 && voucherid != "") {
        //      window.open('/Account/Reports/VoucherReport?VoucherId=' + voucherid , "_blank");

        //    //window.open('/Account/Reports/VoucherReport?VoucherId=' + voucherid + '&VoucherNo=' + VoucherNo + '&userid=' + userid + '&status=' + status + '&fromdate=' + fromdate
        //    //    + '&toDate=' + toDate   , "_blank");
        //}
        //else {
        //    showToast("no voucher found", "error");
        //}

    }

