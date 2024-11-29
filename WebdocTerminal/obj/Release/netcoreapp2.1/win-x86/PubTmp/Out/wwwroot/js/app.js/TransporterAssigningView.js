

    var grid;

    var containers = [];

    var transporters = [];

    var IPaosCFS = []


    $(document).ready(function () {



        loadGrid("", "");


    });


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }


    function hideMenifestedColumns(e) {

        // checkColumn(e, "containerNo");

    }



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function loadGrid(igm, blnumber) {

        console.log("igm", igm);

    console.log("blnumber", blnumber);

    if (igm == undefined || igm == null) {
        igm = "";
        }

    if (blnumber == undefined || blnumber == null) {
        blnumber = "";
        }


    $.get('/APICalls/GetTransportAssigningContainers?virno=' + igm + '&blnumber=' + blnumber, function (data) {

        $.get('/Import/Setup/GetTransporters', function (result) {

            containers = data;

            transporters = result;

            console.log("containers", containers)

            var dataGrid = $("#gateinContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "containerId",
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
                        dataField: "blNumber",
                        caption: "BL Number",
                        allowEditing: false,
                    },
                    {
                        dataField: "containerNo",
                        caption: "Container Number",
                        allowEditing: false,
                    },
                    {
                        dataField: "status",
                        caption: "Status",
                        allowEditing: false,
                    },
                    {
                        dataField: "containerSize",
                        caption: "Container Size",
                        allowEditing: false,

                    },
                    {
                        dataField: "transporterId",
                        width: 200,
                        caption: "Transporter",
                        validationRules: [{ type: "required" }],
                        lookup: {
                            dataSource: transporters,
                            displayExpr: "transporterName",
                            valueExpr: "transporterId"
                        }
                    },
                    {
                        caption: "",
                        dataField: "isGateIn",
                        dataType: "boolean"

                    }


                ],

                onRowUpdated: function (e) {

                },


            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


        });

        });

    }


    function saveInfo() {

        $("#btnSubmit").attr("disabled", true);

        //setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        //console.log("gate in", containers);


        var containerlist = containers.filter(c => c.isGateIn == true);


    console.log("ready to save", containerlist)

    $.post('AddTransporterAssignings', {containers: containerlist }, function (data) {

            if (data.error) {
        showToast(data.message, "error");
    window.location.href = window.location.href;
    return;
            }
    else {
        showToast("Gate In Created ", "success");
    window.location.href = window.location.href;

            }
    window.location.href = window.location.href;
 
        });

    }



    function formfiled() {


    }


    function showdetail() {



        var virNumber = $("#virnolist").dxAutocomplete("instance").option("value");

    console.log("virNumber", virNumber);

    var BlNumber = $("#blNumber").dxAutocomplete("instance").option("value");

    console.log("BlNumber", BlNumber);

    loadGrid(virNumber, BlNumber)
    }
