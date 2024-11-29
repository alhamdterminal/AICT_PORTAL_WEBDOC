
    var voyages = [];

    var vesselExports = [];

    var portAndTerminals = [];








    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function disableColumns(e) {

        checkColumn(e, "congisneeName");
    checkColumn(e, "gdNumber");
    checkColumn(e, "noOfPackages");
    checkColumn(e, "packageType");
        if (PermissionInputs.find(x => x.fieldName == "VoyageNumber" && x.isChecked == false)) {

        checkColumn(e, "voyageNumber");
        }
        if (PermissionInputs.find(x => x.fieldName == "ETA" && x.isChecked == false)) {

        checkColumn(e, "eta");
        }
        if (PermissionInputs.find(x => x.fieldName == "ETD" && x.isChecked == false)) {

        checkColumn(e, "etd");
        }
        if (PermissionInputs.find(x => x.fieldName == "CutOff" && x.isChecked == false)) {

        checkColumn(e, "cutOff");
        }
        if (PermissionInputs.find(x => x.fieldName == "VirNumber" && x.isChecked == false)) {

        checkColumn(e, "virNumber");
        }
        if (PermissionInputs.find(x => x.fieldName == "EgmNumber" && x.isChecked == false)) {

        checkColumn(e, "egmNumber");
        }
        if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        checkColumn(e, "vesselExportId");
        }
        if (PermissionInputs.find(x => x.fieldName == "TerminalCode" && x.isChecked == false)) {

        checkColumn(e, "portAndTerminalId");
        }


    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function loadGrid() {


        $.get('/Export/VoyageExport/GetVoyageExport', function (voyage) {

            voyages = voyage;

            $.get('/Export/VesselExport/GetVesselExport', function (vesselExport) {

                vesselExports = vesselExport;

                $.get('/Export/ExportSetup/GetPortAndTerminalExport', function (portAndTerminal) {

                    portAndTerminals = portAndTerminal;

                    var dataGrid = $("#vogayeExportdata").dxDataGrid({
                        dataSource: voyages,
                        keyExpr: "voyageExportId",
                        wordWrapEnabled: true,
                        showBorders: true,
                        showBorders: true,
                        columnAutoWidth: true,
                        dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                        paging: {
                            pageSize: 5
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
                            "voyageNumber",
                            {

                                dataField: "eta",
                                caption: "ETA",
                                dataType: "date",
                                format: "dd/MM/yyyy",
                                width: 200,
                                validationRules: [{ type: "required" }]

                            },
                            {
                                dataField: "etd",
                                caption: "ETD",
                                dataType: "date",
                                width: 200,
                                format: "dd/MM/yyyy",
                                validationRules: [{ type: "required" }]

                            },
                            {
                                dataField: "cutOff",
                                caption: "Cut Off",
                                dataType: "date",
                                format: "dd/MM/yyyy",
                                width: 200,
                                validationRules: [{ type: "required" }]

                            },


                            "virNumber",
                            "egmNumber",

                            {
                                dataField: "vesselExportId",
                                width: 200,
                                caption: "Vessel Export",
                                lookup: {
                                    dataSource: vesselExports,
                                    displayExpr: "vesselName",
                                    valueExpr: "vesselExportId"
                                }
                            },
                            {
                                dataField: "portAndTerminalId",
                                width: 200,
                                caption: "Terminal Code",
                                lookup: {
                                    dataSource: portAndTerminals,
                                    displayExpr: "portName",
                                    valueExpr: "portAndTerminalId"
                                }
                            }
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

                            var VoyageExport = e.data;
                            $.post('/Export/VoyageExport/AddVoyageExport', { VoyageExport, VoyageExport }, function (data) {

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
                            var VoyageExport = e.data;
                            $.post('/Export/VoyageExport/updateVoyageExport', { VoyageExport, VoyageExport }, function (data) {
                                console.log(data)
                                if (data.error == true) {
                                    showToast(data.message, "error");
                                }
                                else {
                                    showToast(data.message, "success");

                                }
                                loadGrid()

                            });
                        },
                        onRowRemoving: function (e) {

                        },
                        onRowRemoved: function (e) {


                            var key = e.data.voyageExportId;

                            $.post('/Export/VoyageExport/Delete?key=' + key, function (data) {
                                showToast("Voyage Export Deleted", "success");
                            });
                        }
                    }).dxDataGrid("instance");


                    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                    $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');






                });


            });

        });
    }

    function formfiled() {
        loadGrid();
    }
