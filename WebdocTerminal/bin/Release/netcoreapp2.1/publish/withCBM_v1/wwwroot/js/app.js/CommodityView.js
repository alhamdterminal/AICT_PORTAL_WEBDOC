    var commoditys= [];









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
   
        if (PermissionInputs.find(x => x.fieldName == "CommodityCode" && x.isChecked == false)) {

        checkColumn(e, "commodityCode");
        }
        
        if (PermissionInputs.find(x => x.fieldName == "CommodityName" && x.isChecked == false)) {

        checkColumn(e, "commodityName");
        }  if (PermissionInputs.find(x => x.fieldName == "HsCode" && x.isChecked == false)) {

        checkColumn(e, "hsCode");
        }
    }

    function loadGrid() {


        $.get('/Export/Commodity/GetCommodityExport', function (data) {

            commoditys = data;




            var dataGrid = $("#commodity").dxDataGrid({
                dataSource: commoditys,
                keyExpr: "commodityId",
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
                    "commodityCode",
                    "commodityName",
                    "hsCode",

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
                    $.post('/Export/Commodity/AddCommodityExport', { values, values }, function (data) {

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

                    $.post('/Export/Commodity/UpdateCommodityExport', { values, values }, function (data) {

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


                    var key = e.data.commodityId;
                    $.post('/Export/Commodity/Delete', { key, key }, function (data) {

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