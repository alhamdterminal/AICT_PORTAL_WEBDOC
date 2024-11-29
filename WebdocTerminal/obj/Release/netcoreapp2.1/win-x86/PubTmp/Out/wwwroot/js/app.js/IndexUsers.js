

    var Users = [];
    var Status = [{"Id": true, "Name": "Active" }, {"Id": false, "Name": "InActive" }]



   // $(function () {


        // })


        function editUser(id) {

            window.location = '/Users/UpdateUser?userId=' + id;
        }

    function deleteUser(id) {
        console.log(id)
    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "firstName");
    checkColumn(e, "lastName");
    checkColumn(e, "contactNo");
    checkColumn(e, "username");
         if (PermissionInputs.find(x => x.fieldName == "Status" && x.isChecked == false)) {

        checkColumn(e, "status");
           
        }

    }


    function getUsers() {
        $.get('/Users/GetUsers', function (data) {
            console.log(data);


            Users = data

            var dataGrid = $("#User").dxDataGrid({
                dataSource: Users,
                keyExpr: "userId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    pageSize: 10
                },
                selection: {
                    mode: "single"
                },
                editing: {
                    mode: "row",
                    allowUpdating: true
                },

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },

                columns: [

                    "firstName",
                    "lastName",
                    "contactNo",
                    "username",
                    {
                        dataField: "status",
                        width: 125,
                        lookup: {
                            dataSource: Status,
                            displayExpr: "Name",
                            valueExpr: "Id"
                        }
                    },
                    {
                        type: "buttons",
                        buttons: ["edit", "delete", {
                            text: "My Command",
                            icon: "repeat",
                            hint: "My Command",
                            onClick: function (e) {
                                console.log(e)
                                var id = e.row.key
                                editUser(id)
                            }
                        }]
                    },

                ],
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },

                onEditingStart: function (e) {
                },

                onRowInserted: function (e) {
                    console.log("Hello");

                },
                onRowUpdating: function (e) {

                },
                onRowUpdated: function (e) {
                    console.log(e);
                    var model = e.data;
                    console.log(model);
                    $.post('/Users/EditStatus', { model, model }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                        }
                        else {
                            showToast(data.message, "success");
                        }

                    });
                },

            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


        })
    }

    function formfiled() {
        getUsers();
    }


    //function routeToRegistration() {
    //     window.location = '/Users/UserRegistration';
    //}
