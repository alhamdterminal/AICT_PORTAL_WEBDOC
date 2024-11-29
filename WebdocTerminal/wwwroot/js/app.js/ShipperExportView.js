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


        $.get('/Export/ExportSetup/GetShipperExport', function (data) {

            console.log("data", data)

            shippers = data;

            var dataGrid = $("#shipper").dxDataGrid({
                dataSource: shippers,
                keyExpr: "shipperExportId",
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

                onRowInserted: function (e) {

                    var values = e.data;
                    $.post('/Export/ExportSetup/AddShipperExport', { values, values }, function (data) {

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

                    $.post('/Export/ExportSetup/UpdateShipperExport', { values, values }, function (data) {

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