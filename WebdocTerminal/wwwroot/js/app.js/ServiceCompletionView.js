

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

        checkColumn(e, "containerNo");
    checkColumn(e, "vehicleNo");
    checkColumn(e, "category");
    checkColumn(e, "gdNumber");
    checkColumn(e, "noOfPackages");
    checkColumn(e, "packageType");
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

        $.get('/APICalls/GetUnServiceCompletionContainers', function (data) {

            if (dataSource)

                containers = dataSource
            else
                containers = data;
            var dataGrid = $("#serviceCompletionContainer").dxDataGrid({
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

                    "containerNo",
                    "vehicleNo",
                    "category",
                    "gdNumber",
                    "location",
                    "noOfPackages",
                    "packageType",
                    {
                        dataField: "activityType",
                        caption: "Activity",
                        lookup: {
                            dataSource: [{ activity: "GROUNDED" }, { activity: "COMPLETED" }],
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

    function save() {

        $("#btnSubmit").attr("disabled", true);

        var serviceCompletionContainers = containers.filter(c => c.activityType != null && c.isChecked == true);

    $.post('SaveServiceCompletionContainers', {containers: serviceCompletionContainers }, function (data) {
        showToast("Service Completion Created", "success");

            loadGrid(containers.filter(c => c.activityType != null && c.isChecked == false));

        });
    }
