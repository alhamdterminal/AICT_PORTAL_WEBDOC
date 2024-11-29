
    var portAndTerminals = [];




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
   
        if (PermissionInputs.find(x => x.fieldName == "PortName" && x.isChecked == false)) {

        checkColumn(e, "portName");
        }
        if (PermissionInputs.find(x => x.fieldName == "TerminalCode" && x.isChecked == false)) {

        checkColumn(e, "terminalCode");
        }
        if (PermissionInputs.find(x => x.fieldName == "TerminalName" && x.isChecked == false)) {

        checkColumn(e, "terminalName");
        }
        if (PermissionInputs.find(x => x.fieldName == "CityOfDischarge" && x.isChecked == false)) {

        checkColumn(e, "portOfDischarge");
        }
        if (PermissionInputs.find(x => x.fieldName == "CountryOfDischarge" && x.isChecked == false)) {

        checkColumn(e, "destination");
        }
    }

    function loadGrid() {


        $.get('/Export/PortAndTerminal/GetPortAndTerminals', function (data) {

            portAndTerminals = data;


            var grid = $("#portAndTerminal").dxDataGrid(this.gridSettings).dxDataGrid('instance');


            var dataGrid = $("#portAndTerminal").dxDataGrid({
                dataSource: portAndTerminals,
                keyExpr: "portAndTerminalId",
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
                    "portName",
                    "terminalCode",
                    "terminalName",
                    {
                        dataField: "portOfDischarge",
                        caption: "City Of Discharge",
                    },
                    {
                        dataField: "destination",
                        caption: "Country Of Discharge",
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
                onEditorPreparing: function (e) {
                    disableColumns(e);
                },

                onEditingStart: function (e) {
                },
                onInitNewRow: function (e) {
                },
                onRowInserting: function (e) {


                },
                onRowInserted: function (e) {

                    var values = e.data;
                    $.post('/Export/PortAndTerminal/AddPortAndTerminal', { values, values }, function (data) {

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

                    $.post('/Export/PortAndTerminal/UpdatePortAndTerminal', { values, values }, function (data) {

                        showToast("Updated", "success");

                    });
                },
                onRowRemoving: function (e) {

                },
                onRowRemoved: function (e) {

                    var key = e.data.portAndTerminalId;
                    $.post('/Export/PortAndTerminal/Delete', { key, key }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                        }
                        else {
                            showToast(data.message, "success");
                        }

                    });

                }
            }).dxDataGrid("instance");

            grid.on('editorPrepared', function (e) {
                if (e.parentType !== 'dataRow') {
                    return;
                }
                e.editorElement[e.editorName]('instance').on('keyDown', function (arg) {

                    var gridComponent = e.component;

                    var event = arg.jQueryEvent;

                    if (event.keyCode === 38) {
                        if (e.editorName == "dxNumberBox") {
                            event.stopPropagation();
                            event.preventDefault();
                        }

                    } else if (event.keyCode === 40) {
                        if (e.editorName == "dxNumberBox") {
                            event.stopPropagation();
                            event.preventDefault();
                        }

                    }

                    else if (event.keyCode === 189) {
                        if (e.editorName == "dxNumberBox") {
                            event.stopPropagation();
                            event.preventDefault();
                        }

                    }
                });


            });
            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });
    }

    function formfiled() {

        loadGrid();

    }
