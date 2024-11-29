
    var AccountType = [];
    var AccountSubCategories = [];


    $(function () {

        getAccountType();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getAccountType() {
        $.get('/Setup/GetAccountType', function (data) {
            AccountType = data;
            console.log(AccountType);
            loadgrid();
        });
    }

    function loadgrid() {

        $.get('/Setup/GetAccountSubCategory', function (data) {
            AccountSubCategories = data;

            $("#gridContainer").dxDataGrid({
                dataSource: AccountType,
                keyExpr: "accountTypeId",
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
                        dataField: "accountSubCategoryId",
                        caption: "Account Sub Category",
                        //setCellValue: function (rowData, value) {

                        //    var t = AccountCategories.find(d => d.accountCategoryId == value);

                        //    if (t != null || t != undefined) {
                        //        console.log(t, "t");
                        //        rowData.AccountCategoryRange = t.fromAccount + " - " + t.toAccount;
                        //        rowData.accountCategoryId = t.accountCategoryId;
                        //     }
                        //},

                        lookup: {
                            dataSource: AccountSubCategories,
                            displayExpr: function (item) {
                                return item && item.fromAccount + "-" + item.toAccount + "-" + item.name;
                            },
                            valueExpr: "accountSubCategoryId"
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
                    $.post('/Setup/AddAccountType', { model, model }, function (data) {


                        if (data.error == true) {
                            showToast(data.message, "error")
                            getAccountType();
                        } else {
                            showToast(data.message, "success")
                            getAccountType();


                        }


                    });


                },

                onRowUpdated: function (e) {
                    console.log(e.data)
                    var model = e.data;
                    $.post('/Setup/UpdateAccountType', { model, model }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error")
                            getAccountType();
                        } else {
                            showToast(data.message, "success")
                            getAccountType();
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
