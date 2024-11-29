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


        $.get('/Export/ExportSetup/GetShippingLineExport', function (data) {

            console.log("data", data)

            shippers = data;

            var dataGrid = $("#shipper").dxDataGrid({
                dataSource: shippers,
                keyExpr: "shippingLineExportId",
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
                        dataField: "shippingLineName",
                        validationRules: [{ type: "required" }],
                        caption: "Name"

                    },
                    {
                        dataField: "shippingLineCode",
                        validationRules: [{ type: "required" }],
                        caption: "Code"

                    },
                    {
                        dataField: "ntnNumber",
                        validationRules: [{ type: "required" }],
                        caption: "NTN"

                    }

                ],

                onRowInserted: function (e) {

                    var values = e.data;
                    $.post('/Export/ExportSetup/AddShippingLineExport', { values, values }, function (data) {

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

                    $.post('/Export/ExportSetup/UpdateShippingLineExport', { values, values }, function (data) {

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