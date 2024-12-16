

    var AccountCategories = [];
    var AccountNatures = [];


    $(function () {

        getAccountCategories();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getAccountCategories() {
        $.get('/Setup/GetAccountCategory', function (data) {
            AccountCategories = data;
            console.log(AccountCategories);
            loadgrid();
        });
    }

    function loadgrid() {

        $.get('/Setup/GetAccountNature', function (data) {
            AccountNatures = data;

            $("#gridContainer").dxDataGrid({
                dataSource: AccountCategories,
                keyExpr: "accountCategoryId",
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
                        dataField: "accountNatureId",
                        caption: "Account Nature",
                        lookup: {
                            dataSource: AccountNatures,
                            displayExpr: "name",
                            valueExpr: "accountNatureId"
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
                    $.post('/Setup/AddAccountCategory', { model, model }, function (data) {


                        if (data.error == true) {
                            showToast(data.message, "error")
                            getAccountCategories();
                        } else {
                            showToast(data.message, "success")
                            getAccountCategories();


                        }


                    });


                },

                onRowUpdated: function (e) {
                    console.log(e.data)
                    var model = e.data;
                    $.post('/Setup/UpdateAccountCategory', { model, model }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error")
                            getAccountCategories();
                        } else {
                            showToast(data.message, "success")
                            getAccountCategories();


                        }
                    });
                },

            });

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });
    }



    function formfiled() {
        //getcities();

    }