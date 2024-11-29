

    var grid;

    var containers = [];


    $(document).ready(function () {

        loadGrid();
 
     });




    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function loadGrid( ) {

        $.get('/APICalls/GetReProcessGateIn', function (data) {

            console.log("containers", data);

            containers = data;

            var grid = $("#gateinContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');

            var dataGrid = $("#gateinContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "containerNumber",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
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
                    mode: "cell",
                    allowUpdating: true,
                },
                columns: [
                    {
                        dataField: "virNumber",
                        caption: "Vir Number",
                        allowEditing: false,
                    },
                    {
                        dataField: "containerNumber",
                        caption: "Container  Number",
                        allowEditing: false,
                    },

                    {
                        dataField: "pccssSealNumber",
                        caption: "PCCSS Seal No",
                        allowEditing: false,

                    },
                    {
                        dataField: "weight",
                        caption: "Manifest Weight",
                        allowEditing: false,
                    },
                    {
                        dataField: "vehicleNumber",
                        caption: "Vehicle No",
                        allowEditing: false,
                    },
                    {
                        caption: "",
                        dataField: "isProcessed",
                        dataType: "boolean"

                    }


                ],



            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');




        });

    }






    function gateIn() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

    console.log("gate in", containers);


        var gateInContainers = containers.filter(c => c.isProcessed == true);


    console.log("ready to gate in" ,gateInContainers)

    $.post('SaveReProcessGateIn', {containers: gateInContainers }, function (data) {

            if (data.error) {
        alert(data.message);
            }
    else {
        alert(data.message)
    }
    window.location.href = window.location.href;
             
        });

    }


    function formfiled() {


    }

