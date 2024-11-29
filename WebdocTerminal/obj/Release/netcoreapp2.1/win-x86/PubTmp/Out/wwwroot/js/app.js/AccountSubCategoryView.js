

    var AccountSubCategories = [];
    var AccountCategories = [];


    $(function () {

        getAccountSubCategories();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getAccountSubCategories() {
        $.get('/Setup/GetAccountSubCategory', function (data) {
            AccountSubCategories = data;
            console.log(AccountSubCategories);
            loadgrid();
        });
    }

    function loadgrid() {

        $.get('/Setup/GetAccountCategory', function (data) {
            AccountCategories = data;

            $("#gridContainer").dxDataGrid({
                dataSource: AccountSubCategories,
                keyExpr: "accountSubCategoryId",
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
                columns: [
                    {
                        dataField: "accountCategoryId",
                        caption: "Account Category",
                        //setCellValue: function (rowData, value) {
                        //    console.log("dasd", rowData)
                        //    console.log("dasd", value)
                        //    var t = AccountCategories.find(d => d.accountCategoryId == value);

                        //    if (t != null || t != undefined) {
                        //        console.log(t, "t");
                        //         rowData.AccountCategoryRange = t.fromAccount + " - " + t.toAccount;
                        //         rowData.accountCategoryId = t.accountCategoryId;
                        //     }
                        //},

                        lookup: {
                            dataSource: AccountCategories,
                            displayExpr: function (item) {
                                return item && item.fromAccount + "-" + item.toAccount + "-" + item.name;
                            },
                            valueExpr: "accountCategoryId"
                        },
                        validationRules: [{ type: "required" }]

                    },
                    //{
                    //    dataField: "AccountCategoryRange",
                    //    allowEditing: false,
                    //    validationRules: [{ type: "required" }],
                    //    caption: "Account Category Range"
                    //},
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
                        dataField: "fromAccount",
                        validationRules: [{ type: "required" }],
                        caption: "From Account"

                    },
                    {
                        dataField: "toAccount",
                        validationRules: [{ type: "required" }],
                        caption: "To Account"

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
                    $.post('/Setup/AddAccountSubCategory', { model, model }, function (data) {


                        if (data.error == true) {
                            showToast(data.message, "error")
                            getAccountSubCategories();
                        } else {
                            showToast(data.message, "success")
                            getAccountSubCategories();


                        }


                    });


                },

                onRowUpdated: function (e) {
                    console.log(e.data)
                    var model = e.data;
                    $.post('/Setup/UpdateAccountSubCategory', { model, model }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error")
                            getAccountSubCategories();
                        } else {
                            showToast(data.message, "success")
                            getAccountSubCategories();
                        }
                    });
                },

            });

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });
    }



    function formfiled() {


    }
