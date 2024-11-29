
    var AllowUsersList = [];
    var AllUsersLists = [];


    $(function () {

        getAllowUsersList();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getAllowUsersList() {
        $.get('/Setup/GetAllowBackDateTransaction', function (data) {
            AllowUsersList = data;
            console.log(AllowUsersList);
            loadgrid();
        });
    }

    function loadgrid() {

        $.get('/APICalls/GetAllUser', function (data) {
            AllUsersLists = data;

            $("#gridContainer").dxDataGrid({
                dataSource: AllowUsersList,
                keyExpr: "allowBackDateTransactionId",
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
                    allowDeleting: true,
                    allowAdding: true
                },
                columns: [

                    {
                        dataField: "userName",
                        caption: "User Name",
                        lookup: {
                            dataSource: AllUsersLists,
                            displayExpr: "userName",
                            valueExpr: "userName"
                        },
                        validationRules: [{ type: "required" }]

                    },


                ],

                onRowInserted: function (e) {

                    console.log(e.data)

                    $.post('/Setup/AddBackDateTransaction?name=' + e.data.userName, function (data) {


                        if (data.error == true) {
                            showToast(data.message, "error")
                            getAllowUsersList();
                        } else {
                            showToast(data.message, "success")
                            getAllowUsersList();


                        }


                    });


                },

                onRowRemoved: function (e) {
                    console.log(e.data)

                    $.post('/Setup/DeleteBackDateTransaction?id=' + e.data.allowBackDateTransactionId, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error")
                            getAllowUsersList();
                        } else {
                            showToast(data.message, "success")
                            getAllowUsersList();
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
