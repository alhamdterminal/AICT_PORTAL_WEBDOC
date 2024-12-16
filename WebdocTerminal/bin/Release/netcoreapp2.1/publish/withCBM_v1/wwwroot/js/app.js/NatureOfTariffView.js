
    var NatureOfTariffList = [];


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function formfiled() {
        loadGrid();

    }




    function loadGrid() {


        $.get('/Export/TariffExport/GetNatureOfTariff', function (data) {

            NatureOfTariffList = data;

            console.log(NatureOfTariffList)



            var dataGrid = $("#NatureOfTariffGrid").dxDataGrid({
                dataSource: NatureOfTariffList,
                keyExpr: "natureOfTariffId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                columnAutoWidth: true,
                allowColumnResizing: true,
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
                    allowAdding: true
                },
                columns: [
                    {
                        dataField: "natureOfTariffName",
                        caption: "Nature",
                        validationRules: [{ type: "required" }],

                    },

                    "tariffType",
                ],

                onRowInserted: function (e) {

                    var values = e.data;

                    $.post('/Export/TariffExport/AddNatureOfTariff', { values: values }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                        }
                        else {
                            showToast(data.message, "success");
                        }
                        loadGrid();

                    });

                },

                onRowUpdated: function (e) {
                    console.log(e);
                    var values = e.data;

                    $.post('/Export/TariffExport/UpdateNatureOfTariff', { values: values }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error");
                        }
                        else {
                            showToast(data.message, "success");
                        }

                    });
                },

            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });
    }




