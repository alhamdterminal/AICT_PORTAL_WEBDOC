

    var container = "";


    var containergatePasses = [];

    function container_changed(data) {
        console.log("data", data)
        if (data.container) {
        container = data.igm;
        }
    if (data.value) {
        container = data.value;
        }

    getemptygatePassContainers(container);

    }


    function getemptygatePassContainers(container) {
        $.get('/APICalls/GetDeliverEmptyGateOutContainer?container=' + container, function (data) {

            console.log("data", data);

            containergatePasses = data;


            var dataGrid = $("#emptyContainersgatePasses").dxDataGrid({
                dataSource: containergatePasses,
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

                    {
                        dataField: "emptyGateOutContainerId",
                        caption: "Gate Pass No",
                        allowEditing: false,
                    },

                    {
                        dataField: "virNumber",
                        caption: "Vir Number",
                        allowEditing: false,
                    },

                    {
                        dataField: "containerNumber",
                        caption: "Container Number",
                        allowEditing: false,
                    },

                    {
                        dataField: "createdDate",
                        caption: "Out Time",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        width: 100,
                        allowEditing: false
                    },
                    {
                        dataField: "deliveredYard",
                        caption: "Shifted Yard",
                        allowEditing: false
                    },

                    {
                        dataField: "containerCondition",
                        caption: "Container Condition",
                        allowEditing: false
                    },
                    {
                        dataField: "emptyGateOutContainerId",
                        caption: 'Print',
                        cellTemplate: function (container, options) {
                            $("<button type='button' class='btn btn-success' onClick='EmptyGatePassReport(" + options.value + ")'>Print</button>")
                                .appendTo(container);
                        }, allowEditing: false
                    }
                ],

                onRowUpdated: function (e) {

                    $("#btnSubmit").attr("disabled", false);
                }

            }).dxDataGrid("instance");


        });
    }

    function EmptyGatePassReport(id) {
        window.open('/import/reports/EmptyGatePassReport?id=' + id, "_blank");
    }


    function formfiled() { }

