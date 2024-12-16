

    var grid;

    var DocumentReceived = [
    {Name: "YES" },
    {Name: "NO" }
    ];

    var onOptionChanged = function (e) {
        console.log(e);
    };

    var containers = [];

    var shippingAgents = [];

    $(function () {



        //  formfiled();

    });



    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {


        checkColumn(e, "virNumber");
    checkColumn(e, "containerNumber");
    checkColumn(e, "containerCondition");
    checkColumn(e, "shippingLine");
    checkColumn(e, "vehicleNumber");
    checkColumn(e, "shiftedYard");
    checkColumn(e, "shiftedYardDate");
    checkColumn(e, "createdDate");
 
         
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

        $.get('/APICalls/GetReturnToyardContainers', function (data) {


            console.log("data", data);

            if (dataSource)
                containers = dataSource

            else
                containers = data;

            var dataGrid = $("#returntoyardContainers").dxDataGrid({
                dataSource: containers,
                keyExpr: "emptyGateOutContainerId",

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
                    "containerNumber",
                    "containerCondition",
                    "shippingLine",
                    "vehicleNumber",
                    "shiftedYard",

                    {
                        dataField: "shiftedYardDate",
                        caption: "Shifted Yard Date",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        width: 100
                    },
                    {
                        dataField: "createdDate",
                        caption: "Created Date",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        width: 100
                    },
                    {
                        dataField: "returnToYardDate",
                        caption: "Return Date",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        width: 100,
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "returnToYardName",
                        caption: "Return Yard Name",
                        validationRules: [{ type: "required" }]
                    },
                    {
                        dataField: "documentReceived",
                        validationRules: [{ type: "required" }],
                        caption: "Document Received",
                        lookup: {
                            dataSource: DocumentReceived,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },

                    {
                        dataField: "isEmptyOut",
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


    function returntoyard() {

        $(this).children('#btnSubmit').prop('disabled', true);

        var returntoyardcontainers = containers.filter(c => c.isEmptyOut == true);

    console.log(returntoyardcontainers)

    $.post('AddReturnToYardContainers', {model: returntoyardcontainers }, function (data) {

        showToast("Empty Gate-Out Created", "success");

            loadGrid(containers.filter(c => c.isEmptyOut == false));

        });
    }


    function formfiled() {

        loadGrid();

    }



