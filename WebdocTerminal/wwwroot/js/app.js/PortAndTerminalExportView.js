    var shippers = [];



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function loadGrid() {


        $.get('/Export/ExportSetup/GetPortAndTerminalExport', function (data) {

            console.log("data", data)

            shippers = data;

            var dataGrid = $("#shipper").dxDataGrid({
                dataSource: shippers,
                keyExpr: "portAndTerminalExportId",
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

                    {
                        dataField: "portName",
                        validationRules: [{ type: "required" }],
                        caption: "Port Name"

                    },
                    {
                        dataField: "rateAmount20",
                        caption: "Rate 20",
                        dataType: "number",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "rateAmount40",
                        caption: "Rate 40",
                        dataType: "number",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },
                    {
                        dataField: "rateAmount45",
                        caption: "Rate 45",
                        dataType: "number",
                        format: "#,##0.##",
                        editorOptions: {
                            step: 0
                        }
                    },

                ],

                onRowInserted: function (e) {

                    var values = e.data;
                    $.post('/Export/ExportSetup/AddPortAndTerminalExport', { values, values }, function (data) {

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

                    $.post('/Export/ExportSetup/UpdatePortAndTerminalExport', { values, values }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                        }
                        else {
                            showToast(data.message, "success");
                        }

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