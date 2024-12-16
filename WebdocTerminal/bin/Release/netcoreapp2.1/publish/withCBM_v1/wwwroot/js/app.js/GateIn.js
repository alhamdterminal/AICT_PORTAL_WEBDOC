
    var grid;

    var containers = [];

    var clearingAgents = [];


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

        checkColumn(e, "congisneeName");
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
        $.get('/APICalls/GetGateInExportContainers', function (data) {

            $.get('/APICalls/GetClearingAgents', function (agents) {
                containers = data;
                clearingAgents = agents;

                if (dataSource)
                    containers = dataSource
                else
                    containers = data;

                var dataGrid = $("#gateinContainer").dxDataGrid({
                    dataSource: containers,
                    keyExpr: "exportContainerId",
                    wordWrapEnabled: true,
                    showBorders: true,
                    showBorders: true,
                    columnAutoWidth: true,
                    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                    paging: {
                        enabled: false
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
                        "congisneeName",
                        "gdNumber",
                        "grossWeight",
                        "noOfPackages",
                        "packageType",
                        "vehicleNo",
                        "containerNo",

                        {
                            dataField: "clearingAgentName",
                            width: 200,
                            caption: "Clearing Agent",
                            lookup: {
                                dataSource: clearingAgents,
                                displayExpr: "name",
                                valueExpr: "name"
                            }
                        },
                        {
                            caption: "",
                            dataField: "isGateIn",
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


                $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


            });

        });

         
    }


    function gateIn() {

        $("#btnSubmit").attr("disabled", true);

        var gateInContainers = containers.filter(c => c.isGateIn == true);

    $.post('SaveGateInContainers', {containers: gateInContainers }, function (data) {

        showToast("Gate In Created ", "success");

            loadGrid(containers.filter(c => c.isGateIn == false));

        });
    }