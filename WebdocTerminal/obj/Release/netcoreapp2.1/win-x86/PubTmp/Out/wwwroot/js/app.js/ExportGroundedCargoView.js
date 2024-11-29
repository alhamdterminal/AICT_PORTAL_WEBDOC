

    var cargos = [];



    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField == field) {
        e.editorOptions.disabled = true;
        }

    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "gdNumber");

        if (PermissionInputs.find(x => x.fieldName == "Weight" && x.isChecked == false)) {
        checkColumn(e, "weight");

        }
        if (PermissionInputs.find(x => x.fieldName == "Location" && x.isChecked == false)) {
        checkColumn(e, "location");

        }
        if (PermissionInputs.find(x => x.fieldName == "Activity" && x.isChecked == false)) {
        checkColumn(e, "activityType");

        }
        if (PermissionInputs.find(x => x.fieldName == "Status" && x.isChecked == false)) {
        checkColumn(e, "isChecked");

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

        $.get('/APICalls/GetUngroundedCargos', function (data) {
            if (dataSource)
                cargos = dataSource

            else
                cargos = data;


            var dataGrid = $("#groundedCargo").dxDataGrid({
                dataSource: cargos,
                keyExpr: "enteringCargoId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                columnAutoWidth: true,
                allowColumnResizing: true,
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
                    {
                        dataField: "weight",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "location",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "activityType",
                        caption: "Activity",
                        lookup: {
                            dataSource: [{ activity: "GROUNDED" }
                                //, { activity: "COMPLETED" }
                            ],
                            displayExpr: "activity",
                            valueExpr: "activity"
                        }
                    },

                    {
                        caption: "",
                        dataField: "isChecked",
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

        $("#btnSubmit").attr("disabled", true);
    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        var groundedCargos = cargos.filter(c => c.activityType != null && c.isChecked == true);

    $.post('SaveGroundedCargos', {cargos: groundedCargos }, function (data) {


            if (data.error) {
        showToast(data.message, "warning");
            }

    else {

        loadGrid(cargos.filter(c => c.isChecked == false));
    showToast(data.message, "success");

            }


        });
    }

    function formfiled() {
        loadGrid();
    }

