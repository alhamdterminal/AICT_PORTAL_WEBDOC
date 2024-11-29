
    var grid;

    var vessels = [];


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function loadGrid() {
        $.get('/Export/VesselExport/GetVesselExport', function (data) {

            vessels = data;

            var dataGrid = $("#vesselExportdata").dxDataGrid({
                dataSource: vessels,
                keyExpr: "vesselExportId",
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
                    "vesselName",
                ],

                onRowInserted: function (e) {

                    var VesselExport = e.data;
                    $.post('/Export/VesselExport/AddVesselExport', { VesselExport, VesselExport }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                            loadGrid();
                        }
                        else {
                            showToast(data.message, "success");
                            loadGrid();
                        }
                    });

                    loadGrid();
                },

                onRowUpdated: function (e) {

                    var VesselExport = e.data;
                    $.post('/Export/VesselExport/updateVesselExport', { VesselExport, VesselExport }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                            loadGrid();
                        }
                        else {
                            showToast(data.message, "success");
                            loadGrid();
                        }


                    });



                },
                onRowRemoving: function (e) {

                },
                onRowRemoved: function (e) {

                    var key = e.data.vesselExportId;
                    $.post('/Export/VesselExport/Delete', { key, key }, function (data) {
                        showToast("Vessel Export Deleted", "success");
                        loadGrid();
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
