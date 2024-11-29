
    var gateOutGDs = [];



    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField == field) {
        e.editorOptions.disabled = true;
        }

    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "gdNumber");
    checkColumn(e, "numberOfPackages");
    checkColumn(e, "packageType");

        if (PermissionInputs.find(x => x.fieldName == "Weight" && x.isChecked == false)) {

        checkColumn(e, "weight");
        }
        if (PermissionInputs.find(x => x.fieldName == "Status" && x.isChecked == false)) {

        checkColumn(e, "status");
        }
        if (PermissionInputs.find(x => x.fieldName == "IsGateOut" && x.isChecked == false)) {

        checkColumn(e, "isGateOut");
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

    function loadGrid(dataSource) {

        $.get('/Export/GateInOut/GetGateOutGDs', function (data) {
            if (dataSource)
                gateOutGDs = dataSource

            else
                gateOutGDs = data;

            var dataGrid = $("#gateOutGD").dxDataGrid({
                dataSource: gateOutGDs,
                keyExpr: "gdNumber",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,

                paging: {
                    enabled: true
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },
                editing: {
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },


                columns: [
                    "gdNumber",
                    "numberOfPackages",
                    "packageType",
                    "weight",
                    {
                        dataField: "status",
                        caption: "Status",
                        lookup: {
                            dataSource: [{ activity: "F" }
                                , { activity: "p" }
                            ],
                            displayExpr: "activity",
                            valueExpr: "activity"
                        }
                    },
                    {
                        caption: "",
                        dataField: "isGateOut",
                        dataType: "boolean"
                    }

                ],
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {

                    $("#btnSubmit").attr("disabled", false);
                }

            }).dxDataGrid("instance");


        });

    }

    function ground() {

        $("#btnSubmit").attr("disabled", false);
        var gateOutGD = gateOutGDs.filter(c => c.isGateOut == true);
    $.post('SaveGateOutGds', {model: gateOutGD }, function (data) {

        loadGrid(gateOutGDs.filter(c => c.isGateOut == false));

    showToast("GateOut GDs Saved", "success");


        });
    }


    function formfiled() {
        loadGrid();
    }

