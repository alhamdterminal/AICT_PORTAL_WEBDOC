

    var grid;

    var onOptionChanged = function (e) {
        console.log(e);
    };

    var containers = [];




    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "blNumber");
    checkColumn(e, "containerCondition");
    checkColumn(e, "containerNumber");
    checkColumn(e, "shiftedYard");
    checkColumn(e, "size");
    checkColumn(e, "virNumber");
         
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

        $.get('/Import/Delivery/GetEmptyContainerGatePasses', function (data) {

            console.log("data", data)
            if (dataSource)
                containers = dataSource

            else
                containers = data;

            var dataGrid = $("#emptygatepassContainers").dxDataGrid({
                dataSource: containers,
                keyExpr: "containerIndexId",

                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    pageSize: 10
                },
                editing: {
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [

                    "virNumber",
                    "blNumber",
                    "containerNumber",
                    "containerCondition",
                    "shiftedYard",
                    "size",
                    {
                        dataField: "isChecked",
                        caption: "",
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


    function emptygatePass() {

        $(this).children('#btnSubmit').prop('disabled', true);

        var emptyGatepass = containers.filter(c => c.isChecked == true);

    console.log(emptyGatepass)

    $.post('AddEmptyGatePass', {model: emptyGatepass }, function (data) {

        showToast("Empty Gate-Out Created", "success");

    window.location.href = window.location.href;

        });
    }


    function formfiled() {

        loadGrid();

    }



