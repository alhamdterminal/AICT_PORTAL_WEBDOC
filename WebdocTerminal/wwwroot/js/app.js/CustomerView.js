

    var ChartOfAccounts = [];
    var countries = [];
    var cities = [];
    var customers = [];


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



    function getcustomers() {
        $.get('/Setup/GetCustomer', function (data) {
            customers = data;
            console.log(customers);
            loadgrid();
        });
    }


    function getchartofaccounts() {
        $.get('/Account/ChartOfAccount/GetChartOfAccount', function (data) {
            ChartOfAccounts = data;
            console.log(ChartOfAccounts);

            if (ChartOfAccounts) {
                getcustomers()
            }

        });
    }

    function loadgrid() {

        $.get('/Setup/GetCountries', function (data) {
            countries = data;
            $.get('/Setup/GetCity', function (datacity) {
                cities = datacity;
                $("#gridContainer").dxDataGrid({
                    dataSource: customers,
                    keyExpr: "customerId",
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
                        mode: "form",
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

                        const worksheet = workbook.addWorksheet('Customer');

                        DevExpress.excelExporter.exportDataGrid({
                            component: e.component,
                            worksheet,
                            autoFilterEnabled: true,
                        }).then(() => {
                            workbook.xlsx.writeBuffer().then((buffer) => {

                                saveAs(new Blob([buffer], { type: 'application/octet-stream' }), "Customer.xlsx");

                            });
                        });
                        e.cancel = true;
                    },
                    columns: [
                        {
                            dataField: "code",
                            validationRules: [{ type: "required" }],
                            caption: "Code"
                        },
                        {
                            dataField: "name",
                            validationRules: [{ type: "required" }],
                            caption: "Name"
                        },
                        {
                            dataField: "address1",
                            validationRules: [{ type: "required" }],
                            caption: "First Address"

                        },
                        {
                            dataField: "address2",
                            caption: "Sec Address"
                        },
                        {
                            dataField: "zipCode",
                            caption: "ZIP Code"
                        },
                        {
                            dataField: "ntn",
                            caption: "NTN"
                        },
                        {
                            dataField: "salesTaxRegNumber",
                            caption: "SalesTax Reg No"
                        },
                        {
                            dataField: "phone1",
                            caption: "Phone No",
                            validationRules: [{ type: "required" }]

                        },
                        {
                            dataField: "email",
                            caption: "Email",
                            validationRules: [{ type: "required" }]

                        },
                        {
                            dataField: "countryId",
                            caption: "Country",
                            lookup: {
                                dataSource: countries,
                                displayExpr: "countryName",
                                valueExpr: "countryId"
                            },
                            validationRules: [{ type: "required" }]

                        },
                        {
                            dataField: "cityId",
                            caption: "City",
                            lookup: {
                                dataSource: cities,
                                displayExpr: "cityName",
                                valueExpr: "cityId"
                            },
                            validationRules: [{ type: "required" }]

                        },
                        {
                            dataField: "chartOfAccountId",
                            caption: "Control Account",
                            lookup: {
                                dataSource: ChartOfAccounts,
                                displayExpr: function (item) {
                                    return item && item.accCode + "-" + item.accountName;
                                },
                                valueExpr: "chartOfAccountId"
                            },
                            validationRules: [{ type: "required" }]

                        },
                        {
                            dataField: "isActive",
                            dataType: "boolean",
                            caption: "Status",
                        },

                    ],

                    onRowInserted: function (e) {

                        console.log(e.data)

                        var model = e.data;
                        $.post('/Setup/AddCustomer', { model, model }, function (data) {


                            if (data.error == true) {
                                showToast(data.message, "error")
                                getchartofaccounts();

                            } else {
                                showToast(data.message, "success")
                                getchartofaccounts();


                            }


                        });


                    },

                    onRowUpdated: function (e) {
                        console.log(e.data)
                        var model = e.data;
                        $.post('/Setup/UpdateCustomer', { model, model }, function (data) {

                            if (data.error == true) {
                                showToast(data.message, "error")
                                getchartofaccounts();

                            } else {
                                showToast(data.message, "success")
                                getchartofaccounts();

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