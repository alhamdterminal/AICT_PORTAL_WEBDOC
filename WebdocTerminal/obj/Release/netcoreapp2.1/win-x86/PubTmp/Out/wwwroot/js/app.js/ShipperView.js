
    var shippers = [];







    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function disableColumns(e) {


        if (PermissionInputs.find(x => x.fieldName == "ShippeName" && x.isChecked == false)) {

        checkColumn(e, "shipperName");
        }
        if (PermissionInputs.find(x => x.fieldName == "ContactPerson" && x.isChecked == false)) {

        checkColumn(e, "contactPerson");
        }

        if (PermissionInputs.find(x => x.fieldName == "Address" && x.isChecked == false)) {

        checkColumn(e, "address");
        }
        if (PermissionInputs.find(x => x.fieldName == "PhoneNumber" && x.isChecked == false)) {

        checkColumn(e, "telephoneNumber");
        }
        if (PermissionInputs.find(x => x.fieldName == "Email" && x.isChecked == false)) {

        checkColumn(e, "emailAddress");
        }


    }

    function loadGrid() {


        $.get('/Export/Shipper/GetShipperExport', function (data) {

            console.log("data", data)

            shippers = data;




            var dataGrid = $("#shipper").dxDataGrid({
                dataSource: shippers,
                keyExpr: "shipperId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
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
                    allowUpdating: true,
                    allowDeleting: false,
                    allowAdding: true
                },
                columns: [
                    "",
                    {
                        dataField: "shipperName",
                        validationRules: [{ type: "required" }],
                        caption: "Shipper Name"

                    },
                    {
                        dataField: "ntnNumber",
                        validationRules: [{ type: "required" }],
                        caption: "NTN"

                    },
                    "contactPerson",
                    "address",
                    "telephoneNumber",
                    "emailAddress",


                ],
                onEditorPreparing: function (e) {
                    disableColumns(e);
                },

                onEditingStart: function (e) {
                },
                onInitNewRow: function (e) {
                },
                onRowInserting: function (e) {
                    console.log(e)


                },
                onRowInserted: function (e) {

                    var values = e.data;
                    $.post('/Export/Shipper/AddShipperExport', { values, values }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                        }
                        else {
                            showToast(data.message, "success");
                        }
                        loadGrid();

                    });
                },
                onRowUpdating: function (e) {

                },
                onRowUpdated: function (e) {
                    var values = e.data;

                    $.post('/Export/Shipper/UpdateShipperExport', { values, values }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                        }
                        else {
                            showToast(data.message, "success");
                        }

                    });
                },
                onRowRemoving: function (e) {

                },
                onRowRemoved: function (e) {


                    var key = e.data.shipperId;
                    $.post('/Export/Shipper/Delete', { key, key }, function (data) {

                        showToast("Deleted", "success");

                    });

                }
            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');






        });
    }

    function formfiled() {

        loadGrid();

    }