

    var opias = [];



    $(function () {

        loadGrid();

    });


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function onEditorPrepare(e) {

        if (e.parentType === "dataRow" && e.dataField === "gdNumber") {

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

        $.get("/APICalls/GetAllExportPackages", function (packageTypes) {

            $.get('/Export/OPIA/GetOPIAs', function (data) {
                opias = data;

                var dataGrid = $("#opiasdata").dxDataGrid({
                    dataSource: opias,
                    keyExpr: "opiaId",
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
                        allowUpdating: false,
                        allowDeleting: false,
                        allowAdding: true
                    },
                    columns: [
                        "gdNumber",
                        "noOfPackages",
                        {
                            dataField: "packageType",
                            caption: "Package Type",
                            lookup: {
                                dataSource: packageTypes,
                                displayExpr: "packageType",
                                valueExpr: "packageType"
                            }
                        },
                        "consignorName",
                        "consignorNTN",
                        "consignorAddress",
                        "congisneeName",
                        "congisneeAddress",
                        "clearingAgentChallanNumber",
                        "clearingAgentName",
                        "grossWeight",
                        "messageTo",

                    ],

                    onEditingStart: function (e) {

                        onEditorPrepare(e);
                    },
                    onInitNewRow: function (e) {
                    },
                    onRowInserting: function (e) {
                        console.log(e)


                    },
                    onRowInserted: function (e) {

                        var values = e.data;
                        $.post('/Export/OPIA/Post', { values, values }, function (data) {
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
                    onRowUpdated: function (e) {

                        var values = e.data;
                        $.post('/Export/OPIA/updateOPIAS', { values, values }, function (data) {

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

        });

    }



