

    var grid;


    var containers = [];


    //$(function () {

        //    formfiled();

        //});



        function showToast(message, icon) {

            $.toast({
                heading: message,
                showHideTransition: 'slide',
                position: 'bottom-right',
                icon: icon
            });
        }

    function loadGrid(dataSource) {

        $.get('/APICalls/GetGateoutCFS', function (data) {
            if (dataSource)
                containers = dataSource

            else
                containers = data;

            console.log("containers", containers)

            var dataGrid = $("#gateoutContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "key",

                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    pageSize: 10
                },

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },

                editing: {

                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click",
                    mode: "cell"
                },
                columns: [
                    {
                        dataField: "virNo",
                        caption: "Vir No",
                        allowEditing: false,
                    },
                    {
                        dataField: "containerNo",
                        caption: "Container No",
                        allowEditing: false,
                    },

                    {
                        dataField: "blNo",
                        caption: "BL No",
                        allowEditing: false,
                    },
                    {
                        dataField: "indexNo",
                        caption: "Index No",
                        allowEditing: false,
                    },
                    {
                        dataField: "noOfPackages",
                        caption: "No Of Packages  / Weight ",
                        allowEditing: false,
                    },
                    {
                        dataField: "packageType",
                        caption: "Package Type",
                        allowEditing: false
                    },
                    {
                        dataField: "status",
                        caption: "Status",
                        allowEditing: false,
                    },

                    {
                        dataField: "gatePassNumber",
                        caption: "GatePass No",
                        allowEditing: false,
                    },

                    {
                        dataField: "gateOutTime",
                        caption: "Out Time",
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                        allowEditing: false,
                    },
                    {
                        dataField: "truckNo",
                        caption: "Truck No",
                        allowEditing: false,
                    },
                    //{
                    //    dataField: "pccssSealNo",
                    //    caption: "SealNo",
                    //    allowEditing: false,
                    //},
                    {
                        dataField: "portOfExit",
                        caption: "Port Of Exit",
                        allowEditing: false,
                    },
                    {
                        dataField: "countryOfExit",
                        caption: "Country Of Exit",
                        allowEditing: false,
                    },

                    {
                        caption: "",
                        dataField: "isGateOut",
                        dataType: "boolean"
                    }
                ],
                onEditorPreparing: function (e) {
                    console.log(e);
                },
                onRowUpdated: function (e) {

                }

            }).dxDataGrid("instance");

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


        })


    }


    function gateOutCFS() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        var gateOtCFS = containers.filter(c => c.isGateOut == true);


    console.log("gateOtCFS", gateOtCFS)
    console.log("gateOtCFS", gateOtCFS.length)



    $.post('AddGetOutCFS', {model: gateOtCFS }, function (data) {
        console.log('1');
    if (data.error == true) {
        loadGridhold(data.message);
    $('#HoldStatusModal').modal('toggle');
            }
    else {
        loadGridhold([]);
    showToast(data.message, "success");

                loadGrid(containers.filter(c => c.isGateOut == false));
            }
        });


    }


    function loadGridhold(data) {

        $("#holdGrid").dxDataGrid({
            dataSource: data,
            keyExpr: "holdId",
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,
            paging: {
                pageSize: 10
            },
            searchPanel: {
                visible: true,
                width: 240,
                placeholder: "Search..."
            },

            columns: [
                {
                    dataField: "virNo",
                    caption: "IGM",
                    allowEditing: false,
                },
                {
                    dataField: "indexNo",
                    caption: "Index No",
                    allowEditing: false,
                },
                {
                    dataField: "auctionLotNo",
                    caption: "Auction Lot No",
                    allowEditing: false,
                },
                {
                    dataField: "holdDate",
                    caption: "Hold Date",
                    allowEditing: false,
                    dataType: 'date',
                    format: 'dd/MM/yyyy',
                    allowEditing: false,
                },
                {
                    dataField: "holdType",
                    caption: "hold Type",
                    allowEditing: false,
                },
                {
                    dataField: "specialInstructions",
                    caption: "special Instructions",
                    allowEditing: false,
                },
                {
                    dataField: "role",
                    caption: "Role",
                    allowEditing: false,
                },
                {
                    dataField: "isHold",
                    caption: "Hold Status",
                    allowEditing: false,
                },
                {
                    dataField: "ro",
                    caption: "RO Number",

                },
                {
                    dataField: "removeInstruction",
                    caption: "Remove Instruction",
                    validationRules: [{
                        type: "required",
                        message: "Remove Instruction Is Required"
                    }],

                },
            ],

        });

    }



    function formfiled() {
        loadGrid();
    }


