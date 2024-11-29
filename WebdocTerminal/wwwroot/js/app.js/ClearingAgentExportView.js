    var clearingAgentExports = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function loadGrid() {


        $.get('/Export/ClearingAgentExport/GetClearingAgentExport', function (data) {

            clearingAgentExports = data;

            var dataGrid = $("#clearingAgentExport").dxDataGrid({
                dataSource: clearingAgentExports,
                keyExpr: "clearingAgentExportId",
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
                    "challanNumber",
                    "clearingAgentName",
                    "address",
                    "phoneNumber",
                    "emailAddress",


                ],
                onRowInserted: function (e) {

                    var values = e.data;
                    $.post('/Export/ClearingAgentExport/AddClearingAgentExport', { values, values }, function (data) {

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

                    $.post('/Export/ClearingAgentExport/UpdateClearingAgentExport', { values, values }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                        }
                        else {
                            showToast(data.message, "success");
                        }
                        loadGrid();

                    });
                },

            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');






        });
    }

    function formfiled() {
        loadGrid();
    }
