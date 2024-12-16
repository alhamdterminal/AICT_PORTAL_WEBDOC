
    var locationCodes = [];


    $(function () {



    });

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


        if (PermissionInputs.find(x => x.fieldName == "Uncode" && x.isChecked == false)) {

        checkColumn(e, "uncode");
        }
        if (PermissionInputs.find(x => x.fieldName == "CountryCode" && x.isChecked == false)) {

        checkColumn(e, "countryCode");
        }

        if (PermissionInputs.find(x => x.fieldName == "PortCode" && x.isChecked == false)) {

        checkColumn(e, "portCode");
        }
        if (PermissionInputs.find(x => x.fieldName == "CountryName" && x.isChecked == false)) {

        checkColumn(e, "countryName");
        }
        if (PermissionInputs.find(x => x.fieldName == "PortName" && x.isChecked == false)) {

        checkColumn(e, "portName");
        } 


    }


    function loadGrid() {


        $.get('/Export/ExportLocationCodeList/GetExportLocationCodeList', function (data) {

            locationCodes = data;




            var dataGrid = $("#exportLocationCodeList").dxDataGrid({
                dataSource: locationCodes,
                keyExpr: "exportLocationCodeListId",
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
                    allowDeleting: true,
                    allowAdding: true
                },
                columns: [
                    "uncode",
                    "countryCode",
                    "portCode",
                    "countryName",
                    "portName"


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
                    $.post('/Export/ExportLocationCodeList/AddExportLocationCodeList', { values, values }, function (data) {

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

                    $.post('/Export/ExportLocationCodeList/UpdateExportLocationCodeList', { values, values }, function (data) {

                        showToast("Update Shipper", "success");

                    });
                },
                onRowRemoving: function (e) {

                },
                onRowRemoved: function (e) {


                    var key = e.data.exportLocationCodeListId;
                    $.post('/Export/ExportLocationCodeList/Delete', { key, key }, function (data) {

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

