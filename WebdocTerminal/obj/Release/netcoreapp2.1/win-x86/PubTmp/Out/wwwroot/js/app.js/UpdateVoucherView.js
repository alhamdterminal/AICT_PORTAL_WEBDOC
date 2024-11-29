
    var VoucherServices = [];
    var ChartOfAccounts = [];
    var Departments = [];
    var Customers = [];
    var VoucherDetail = [];
    var filterServiceType = false;

    $(function () {

        $('#voucherid').val(0);
    $('#vouchercode').val('');
    GetServiceType();
    getchartofaccounts();
    getdepartments();
    getcustomers();

    });

    function GetServiceType() {
        $.get('/Setup/GetVoucherServiceType', function (data) {
            VoucherServices = data;
            console.log(VoucherServices);
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
            GridData(VoucherDetail)
        });
        }

    }



    function finddata() {

        var url_string = window.location.href
    var url = new URL(url_string);
    var VoucherId = url.searchParams.get("VoucherId");

    if (!VoucherServices.length) {
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
    else if (!VoucherId) {
            return showToast("Voucher VoucherCode Not Find", "error");
        }
    else {


        $.get('/Account/Voucher/GetVoucherAndVoucherDetailById?VoucherId=' + VoucherId, function (data) {

            if (data) {

                $('#voucherTypeId').val(data.voucherTypeId).trigger('change.select2');
                $('#voucherid').val(data.voucherId);
                $('#vouchercode').val(data.voucherNo);
                $('#status').val(data.status);
                if (data.voucherDate != null) {
                    $('#voucherDate').val(new Date(data.voucherDate.split("T")[0]).toISOString().substr(0, 10));
                } else {
                    $('#voucherDate').val('');
                }

                if (data.voucherDetails.length) {
                    GridData(data.voucherDetails)
                }
                else {
                    GridData([])
                }
            }
            else {
                resetvalues();
            }

        });
        }
         
    }

    function resetvalues() {

        $('#voucherDate').val('');
    $('#voucherTypeId').val('').trigger('change.select2');
    $('#voucherid').val('');
    $('#vouchercode').val('');
    $('#status').val('');
    GridData([]);

    }

    function GridData(VoucherDetailList) {


        $("#gridContainer").dxDataGrid({
            dataSource: VoucherDetailList,
            keyExpr: "voucherDetailId",
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
                allowDeleting: function (e) {

                    if ($('#status').val() != "InProgress") {
                        return false;
                    }
                    else {
                        return true;
                    }
                },
                allowUpdating: function (e) {

                    if ($('#status').val() != "InProgress") {
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

                console.log(e.data)
                var voucherid = $('#voucherid').val();

                if (e.data.debit > 0 && e.data.credit > 0) {

                    getVoucherDetails(voucherid);
                    return showToast("debit and credit both are > 0", "error");
                }

                else if (e.data.debit <= 0 && e.data.credit <= 0) {
                    getVoucherDetails(voucherid);

                    return showToast("debit and credit both are < 0", "error");
                }
                else {

                    var model = e.data;

                    $.post('/Account/Voucher/AddVoucherDetailOnly?VoucherId=' + voucherid, { model, model }, function (data) {
                        if (data.error == true) {
                            showToast(data.message, "error");
                            getVoucherDetails(voucherid);

                        } else {

                            showToast("Created", "success");

                            getVoucherDetails(voucherid)

                        }
                    });

                }
            },

            onRowUpdated: function (e) {

                var voucherid = $('#voucherid').val();

                console.log(e.data);

                if (e.data.debit > 0 && e.data.credit > 0) {
                    getVoucherDetails(voucherid);
                    return showToast("debit and credit both are > 0", "error")

                }

                else if (e.data.debit <= 0 && e.data.credit <= 0) {
                    getVoucherDetails(voucherid);
                    return showToast("debit and credit both are < 0", "error")

                }

                else {
                    var model = e.data;
                    $.post('/Account/Voucher/UpdateVoucherDetail', { model, model }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error")
                            getVoucherDetails(voucherid);

                        } else {
                            showToast(data.message, "success")
                            getVoucherDetails(voucherid);

                        }
                    });
                }

            },

            onRowRemoved: function (e) {

                var voucherid = $('#voucherid').val();

                var model = e.data;
                $.post('/Account/Voucher/DeleteVoucherDetail', { model, model }, function (data) {

                    if (data.error == true) {
                        showToast(data.message, "error")
                        getVoucherDetails(voucherid);
                    } else {
                        showToast(data.message, "success")
                        getVoucherDetails(voucherid);
                    }
                });
            },


        });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "chartOfAccountId"); 
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
      
    }

    function formfiled() {

    }

    function PrintDetail() {

        var voucherid = $('#voucherid').val();

    if (voucherid != 0 && voucherid != "") {
        window.open('/Account/Reports/SingleVoucherView?VoucherId=' + voucherid, "_blank");
        }
    else {
        showToast("no voucher found", "error");
        }

    }

