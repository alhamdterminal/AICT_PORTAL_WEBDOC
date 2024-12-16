
    var OECMsdata = [];



    $(function () {

        loadGrid();

    });

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "gdNumber");



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


        $.get('/Export/OECM/GetOECMs', function (data) {
            OECMsdata = data;

            var dataGrid = $("#OECMsdata").dxDataGrid({
                dataSource: OECMsdata,
                keyExpr: "oecmId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    enabled: true
                },

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },
                editing: {
                    mode: "popup",
                    allowUpdating: true,
                    allowDeleting: false,
                    allowAdding: false
                },
                columns: [
                    "gdNumber",
                    {
                        dataField: "handlingCode",
                        caption: "Handling Code",
                        lookup: {
                            dataSource: [{ activity: "ECM" }
                            ],
                            displayExpr: "activity",
                            valueExpr: "activity"
                        }
                    },
                    "serviceStatus",
                    "remarks",
                    "messageTo",


                ],



                onInitNewRow: function (e) {
                },
                onRowInserting: function (e) {
                    console.log(e)


                },
                onRowInserted: function (e) {

                    var values = e.data;
                    $.post('/Export/OECM/Post', { values, values }, function (data) {
                        if (data.error == true) {
                            showToast(data.message, "error");
                        }
                        else {
                            showToast(data.message, "success");
                            loadGrid();

                        }
                    });

                },
                onRowUpdating: function (e) {

                },
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {

                    var values = e.data;
                    $.post('/Export/OECM/Put', { values, values }, function (data) {

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



