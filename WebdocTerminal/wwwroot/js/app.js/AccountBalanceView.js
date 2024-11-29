    var Accountbalances = [];
    var ChartOfAccounts = [];
    var Customers = [];
    var filterCustomers = false;


    $(function () {

        getchartofaccounts();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getAccountbalances() {
        $.get('/Account/ChartOfAccount/GetAccountBalance', function (data) {
            Accountbalances = data;
            console.log(Accountbalances);
            loadgrid();
        });
    }


    function getchartofaccounts() {
        $.get('/Account/ChartOfAccount/GetChartOfAccountNameAndCode', function (data) {
            ChartOfAccounts = data;
            console.log(ChartOfAccounts);

            if (ChartOfAccounts) {
                getAccountbalances()
            }

        });
    }


    function loadgrid() {

        $.get('/Setup/GetCustomersNameAndCode', function (data) {
            Customers = data;

            $("#gridContainer").dxDataGrid({
                dataSource: Accountbalances,
                keyExpr: "accountBalanceId",
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
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
                    allowUpdating: false,
                    allowDeleting: false,
                    allowAdding: true
                },
                export: {
                    enabled: true
                },
                onExporting(e) {

                    console.log("e", e)
                    const workbook = new ExcelJS.Workbook();

                    const worksheet = workbook.addWorksheet('AccountBalance');

                    DevExpress.excelExporter.exportDataGrid({
                        component: e.component,
                        worksheet,
                        autoFilterEnabled: true,
                    }).then(() => {
                        workbook.xlsx.writeBuffer().then((buffer) => {

                            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), "AccountBalance.xlsx");

                        });
                    });
                    e.cancel = true;
                },
                columns: [

                    {
                        dataField: "chartOfAccountId",
                        caption: "Control Account",
                        setCellValue(rowData, value) {
                            console.log("rowData", rowData)
                            console.log("value", value)
                            rowData.chartOfAccountId = value;
                            rowData.customerId = null;

                            if (Customers.filter(x => x.chartOfAccountId == value).length) {
                                filterCustomers = true;
                            }
                            else {
                                filterCustomers = false;
                            }

                        },
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
                        dataField: "customerId",
                        caption: "Customer",
                        width: 250,
                        lookup: {
                            dataSource(options) {
                                return {

                                    store: Customers,
                                    filter: options.data ? ['chartOfAccountId', '=', options.data.chartOfAccountId] : null,
                                    requireTotalCount: true,
                                    pageSize: 4,
                                    paginate: true,
                                };
                            },
                            displayExpr: "name",
                            valueExpr: "customerId",
                            allowClearing: true,
                        },
                        validationRules: [{
                            type: "custom",
                            validationCallback: function (e, s) {
                                if (filterCustomers == true && e.value == null) {
                                    return false;
                                }
                                else {
                                    return true;
                                }
                            },
                            message: "need to required customer"
                        }],
                    },
                    {
                        caption: "Tran Date",
                        dataField: "tranDate",
                        validationRules: [{ type: "required" }],
                        dataType: "date",
                    },
                    {
                        dataField: "isActive",
                        dataType: "boolean",
                        caption: "Status",
                    },

                ],
                onRowInserted: function (e) {

                    console.log(e.data)

                    if (e.data.debit > 0 && e.data.credit > 0) {
                        showToast("debit and credit both are > 0", "error")
                        getchartofaccounts();
                    }

                    else if (e.data.debit <= 0 && e.data.credit <= 0) {
                        showToast("debit and credit both are < 0", "error")
                        getchartofaccounts();
                    }
                    else {
                        var model = e.data;
                        $.post('/Account/ChartOfAccount/AddAccountBalance', { model, model }, function (data) {


                            if (data.error == true) {
                                showToast(data.message, "error")
                                getchartofaccounts();

                            } else {
                                showToast(data.message, "success")
                                getchartofaccounts();
                            }


                        });
                    }



                },

                onRowUpdated: function (e) {

                    console.log(e.data);

                    if (e.data.debit > 0 && e.data.credit > 0) {
                        showToast("debit and credit both are > 0", "error")
                        getchartofaccounts();
                    }

                    else if (e.data.debit <= 0 && e.data.credit <= 0) {
                        showToast("debit and credit both are < 0", "error")
                        getchartofaccounts();
                    }

                    else {
                        var model = e.data;
                        $.post('/Account/ChartOfAccount/UpdateAccountBalance', { model, model }, function (data) {

                            if (data.error == true) {
                                showToast(data.message, "error")
                                getchartofaccounts();

                            } else {
                                showToast(data.message, "success")
                                getchartofaccounts();

                            }
                        });
                    }

                },

            });

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });

    }

    function formfiled() {

    }