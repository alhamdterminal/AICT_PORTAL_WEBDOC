
    var packageTypes = [];




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


        if (PermissionInputs.find(x => x.fieldName == "Code" && x.isChecked == false)) {

        checkColumn(e, "code");
        }
        if (PermissionInputs.find(x => x.fieldName == "PackageType" && x.isChecked == false)) {

        checkColumn(e, "packageType");
        }

        if (PermissionInputs.find(x => x.fieldName == "Materialtype" && x.isChecked == false)) {

        checkColumn(e, "materialType");
        } 


    }


    function loadGrid() {


        $.get('/Export/PackageTypeExport/GetPackageTypeExport', function (data) {

            packageTypes = data;



            var dataGrid = $("#packageTypeExport").dxDataGrid({
                dataSource: packageTypes,
                keyExpr: "packageTypeExportId",
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
                    "code",
                    "packageType",
                    "materialType"


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
                    $.post('/Export/PackageTypeExport/AddPackageTypeExport', { values, values }, function (data) {

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

                    $.post('/Export/PackageTypeExport/UpdatePackageTypeExport', { values, values }, function (data) {

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


                    var key = e.data.packageTypeExportId;
                    $.post('/Export/PackageTypeExport/Delete', { key, key }, function (data) {

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


