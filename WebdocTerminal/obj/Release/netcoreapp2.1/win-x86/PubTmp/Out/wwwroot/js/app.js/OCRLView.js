

    var oclrs = [];

    $(function () {

        loadGrid();
    });

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField == field) {
        e.editorOptions.disabled = true;
        }

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

    function loadGrid(dataSource) {

        $.get('/Export/OCRL/GetOCRLsList', function (data) {

            if (dataSource)
                oclrs = dataSource

            else
                oclrs = data;


            var dataGrid = $("#ocrl").dxDataGrid({
                dataSource: oclrs,
                keyExpr: "ocrlId",
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
                    allowAdding: false
                },


                columns: [
                    "gdNumber",
                    "category",
                    "documentCodes",
                    "messageTo",

                    {
                        dataField: "status",
                        caption: "Status",
                        lookup: {
                            dataSource: [{ activity: "AL" }
                                , { activity: "OD" }
                            ],
                            displayExpr: "activity",
                            valueExpr: "activity"
                        }
                    }
                ],
                onEditingStart: function (e) {
                },
                onInitNewRow: function (e) {
                },
                onRowInserting: function (e) {
                    console.log(e)


                },
                onRowInserted: function (e) {

                    var values = e.data;
                    $.post('/Export/OCRL/Post', { values, values }, function (data) {

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
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {
                    var values = e.data;

                    $.post('/Export/OCRL/Put', { values, values }, function (data) {

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

                    var key = e.data.ocrlId;
                    $.post('/Export/OCRL/Delete', { key, key }, function (data) {

                        showToast("Deleted", "success");

                    });

                }
            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');






        });
    }

    function ground() {

        $("#btnSubmit").attr("disabled", false);

        var groundedCargos = cargos.filter(c => c.activityType != null && c.isChecked == true);

    $.post('SaveGroundedCargos', {cargos: groundedCargos }, function (data) {
        loadGrid(cargos.filter(c => c.isChecked == false));

    showToast("Grounded cargos Created", "success");


        });
    }


