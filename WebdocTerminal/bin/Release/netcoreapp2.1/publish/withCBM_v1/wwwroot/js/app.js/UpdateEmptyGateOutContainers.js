

    var grid;

    var containers = [];
    var transporters = [];

    var deliveredYards = [];


    $(function () {

        $.get('/APICalls/GetDeliveredYards', function (dYards) {
            console.log(dYards)
            deliveredYards = dYards;
        });


    $.get('/APICalls/GetAllVehicles', function (data) {

        transporters = data;

    console.log(transporters)

        });

    loadGrid("");
    })






    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function loadGrid(containerno) {

        console.log("containerno", containerno);

    if (containerno == undefined || containerno == null) {
        containerno = "";
        }

    $.get('/Import/GateInOut/GetEmptyGateOutContainers?containerno=' + containerno, function (data) {

        containers = data;

    console.log("containers", containers)

    var dataGrid = $("#gateinContainer").dxDataGrid({
        dataSource: containers,
    keyExpr: "emptyGateOutContainerId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: true,
    pageSize: 10
                    },

    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

                    },

    editing: {

        allowUpdating: true,
    mode: "row"
                    },
    columns: [
    {
        dataField: "virNo",
    caption: "Vir No",
    allowEditing: false,
                        },
    {
        dataField: "containerNo",
    caption: "Container Number",
    allowEditing: false,
                        },

    {
        dataField: "transporterId",
    caption: "Transporter",
    lookup: {
        dataSource: transporters,
    displayExpr: "transporterName",
    valueExpr: "transporterId"
                            },
    validationRules: [{type: "required" }]

                        },
    {
        dataField: "deliveredYard",
    caption: "Delivered Yard",
    width: 200,
    lookup: {
        dataSource: deliveredYards,
    displayExpr: "deliveredYardName",
    valueExpr: "deliveredYardName"
                            }
                        },
    {
        dataField: "vehicleNumber",
    caption: "Vehicle",
    width: 200,
    validationRules: [{type: "required" }]

                        },
    {
        dataField: "containerCondition",
    caption: "Container Condition",
    validationRules: [{type: "required" }]
                        },
    {
        dataField: "deliveredYardDate",
    caption: "Delivered  Date",
    dataType: 'date',
    format: 'dd/MM/yyyy',
    validationRules: [{type: "required" }]
                        },

    ],

    onRowUpdated: function (e) {
        console.log("e", e);
    var model = e.data;
    $.post('/Import/GateInOut/UpdateEmptyGateOutContainersInfo', {model: model }, function (data) {

                            if (data.error == true) {
        showToast(data.message, "error")

    } else {

        showToast(data.message, "success");
  
                            }

                        });
                    },


                }).dxDataGrid("instance");


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


     

        });

    }


    function formfiled() {


    }


    function showdetail() {
         
        var BlNumber = $("#containerNumber").dxAutocomplete("instance").option("value");

    console.log("BlNumber", BlNumber);

    loadGrid( BlNumber)
    }

