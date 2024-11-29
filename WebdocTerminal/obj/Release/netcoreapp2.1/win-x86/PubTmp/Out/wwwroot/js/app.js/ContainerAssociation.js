
    var grid;

    var containers = [];

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

        checkColumn(e, "containerNumber");
    checkColumn(e, "vehicleNumber");
    checkColumn(e, "numberofPackages");
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

        $.get('/APICalls/GetUnContainerAssociationContainers', function (data) {

            if (dataSource)

                containers = dataSource
            else
                containers = data;
            var dataGrid = $("#containerAssociation").dxDataGrid({
                dataSource: containers,
                keyExpr: "exportContainerId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                paging: {
                    enabled: false
                },
                editing: {
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [

                    "containerNumber",
                    "vehicleNumber",
                    "numberofPackages",
                    "gdNumber",
                    "shippingLineCode",
                    "shippingLineName",
                    "shippingLineNTN",
                    {
                        dataField: "consolidationStatus",
                        caption: "Status",
                        lookup: {
                            dataSource: [{ activity: "F" }, { activity: "P" }],
                            displayExpr: "Status",
                            valueExpr: "Status"
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

    function save() {

        $("#btnSubmit").attr("disabled", true);
         var containerAssociations = containers.filter(c => c.isChecked == true);

    $.post('SaveContainerAssociation', {containers: containerAssociations }, function (data) {
        showToast("Container Association Created", "success");

            loadGrid(containers.filter(c =>   c.isChecked == false));

        });
    }
