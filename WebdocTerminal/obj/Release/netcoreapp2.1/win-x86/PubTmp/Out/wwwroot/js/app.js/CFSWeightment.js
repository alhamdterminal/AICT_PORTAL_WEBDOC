

    var grid;

    var containers = [];

    //$(function () {

        //    formfiled();

        //});



        function checkColumn(e, field) {

            if (e.parentType === "dataRow" && e.dataField !== field) {
                return;
            }

            e.editorOptions.disabled = true;
        }

    function hideMenifestedColumns(e) {

        checkColumn(e, "virNo");
    checkColumn(e, "containerNo");
    checkColumn(e, "indexNo");
    checkColumn(e, "blNo");
    checkColumn(e, "blGrossWeight");
    checkColumn(e, "handlingCode");

         if (PermissionInputs.find(x => x.fieldName == "FoundWeight" && x.isChecked == false)) {

        checkColumn(e, "foundWeight");
        }
         if (PermissionInputs.find(x => x.fieldName == "IsWeighment" && x.isChecked == false)) {

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

        $.get('/APICalls/GetCFSWeightmentContainers', function (data) {

            if (dataSource)
                containers = dataSource
            else
                containers = data;

            var dataGrid = $("#weightmentContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "containerIndexId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
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
                    "virNo",
                    "containerNo",
                    "indexNo",
                    "blNo",
                    "blGrossWeight",
                    "foundWeight",
                    "handlingCode",
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

    function weight() {

        $("#btnSubmit").attr("disabled", true);

        var weightmentContainers = containers.filter(c => c.isChecked == true);

    $.post('SaveCFSWeighmentContainers', {containers: weightmentContainers }, function (data) {

        showToast("CFS Weighment Created", "success");

            loadGrid(containers.filter(c => c.isChecked == false));

        });
    }

    function formfiled() {
        loadGrid();
    }

