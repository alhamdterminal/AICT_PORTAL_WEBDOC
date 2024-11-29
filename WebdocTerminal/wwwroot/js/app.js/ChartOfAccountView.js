
    var chartOfaccounts = [];
    var accounttypes = [];
    var voucherservicetypes = [];


    $(function () {

        getchartOfaccounts();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getchartOfaccounts() {
        $.get('/Account/ChartOfAccount/GetChartOfAccount', function (data) {
            chartOfaccounts = data;
            console.log(chartOfaccounts);
            loadgrid();
        });
    }

    function loadgrid() {

        $.get('/Setup/GetVoucherServiceType', function (resvoucherservicetype) {
            voucherservicetypes = resvoucherservicetype;

            $.get('/Setup/GetAccountType', function (resaccounttype) {
                accounttypes = resaccounttype;

                $("#gridContainer").dxDataGrid({
                    dataSource: chartOfaccounts,
                    keyExpr: "chartOfAccountId",
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

                        const worksheet = workbook.addWorksheet('ChartOfAccount');

                        DevExpress.excelExporter.exportDataGrid({
                            component: e.component,
                            worksheet,
                            autoFilterEnabled: true,
                        }).then(() => {
                            workbook.xlsx.writeBuffer().then((buffer) => {

                                saveAs(new Blob([buffer], { type: 'application/octet-stream' }), "ChartOfAccount.xlsx");

                            });
                        });
                        e.cancel = true;
                    },
                    columns: [
                        {
                            dataField: "subCategory",
                            caption: "Sub Category",
                            allowEditing: false,
                        },
                        {
                            dataField: "accountTypeId",
                            caption: "Account Type",

                            lookup: {
                                dataSource: accounttypes,
                                displayExpr: function (item) {
                                    return item && item.fromAccount + "-" + item.toAccount + "-" + item.name;
                                },
                                valueExpr: "accountTypeId"
                            },
                            validationRules: [{ type: "required" }]

                        },
                        {
                            dataField: "accCode",
                            validationRules: [{ type: "required" }],
                            caption: "Code"
                        },
                        {
                            dataField: "accountName",
                            validationRules: [{ type: "required" }],
                            caption: "Name"
                        },

                        {
                            dataField: "voucherServiceTypeId",
                            caption: "Service Type",

                            lookup: {
                                dataSource: voucherservicetypes,
                                displayExpr: function (item) {
                                    return item && item.code + "-" + item.name;
                                },
                                valueExpr: "voucherServiceTypeId"
                            },
                            validationRules: [{ type: "required" }]

                        },
                        {
                            dataField: "status",
                            dataType: "boolean",
                            caption: "Status",
                        },

                    ],

                    onRowInserted: function (e) {

                        console.log(e.data)

                        var model = e.data;
                        $.post('/Account/ChartOfAccount/AddChartOfAccount', { model, model }, function (data) {


                            if (data.error == true) {
                                showToast(data.message, "error")
                                getchartOfaccounts();
                            } else {
                                showToast(data.message, "success")
                                getchartOfaccounts();


                            }


                        });


                    },

                    onRowUpdated: function (e) {
                        console.log(e.data)
                        var model = e.data;
                        $.post('/Account/ChartOfAccount/UpdateChartOfAccount', { model, model }, function (data) {

                            if (data.error == true) {
                                showToast(data.message, "error")
                                getchartOfaccounts();
                            } else {
                                showToast(data.message, "success")
                                getchartOfaccounts();
                            }
                        });
                    },

                });

                $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

            });

        });
    }



    function formfiled() {


    }
