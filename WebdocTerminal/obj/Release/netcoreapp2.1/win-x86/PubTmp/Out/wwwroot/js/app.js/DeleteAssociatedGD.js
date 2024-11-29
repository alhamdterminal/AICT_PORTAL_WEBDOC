
    var exportContainerItems = [];
    var shipper = [];
    var vesselid = 0;
    var voyageId = 0;
    var exportContainerId = 0;


    function loadingBar() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'block';
    $("#large-indicator").dxLoadIndicator({
        height: 60,
    width: 60
        });
    }
    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }




    $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    exportContainerId = url.searchParams.get("exportContainerId");

    if (exportContainerId) {
        $('#exportContainer').val(exportContainerId);
        }

    $("#vesselExport").change(function () {
            var vesselId = this.value;
    console.log("vesselId", vesselId)




    $.get('/APICalls/GetVoyages?VesselExportId=' + vesselId, function (resp) {

        $('#voayges').html(resp);
                
            });



        });
    });




    function findExportContainerItems() {


        exportContainerId = $('#exportContainer').val();
    vesselid = document.getElementById('vesselExport').value;
    voyageId = document.getElementById('voyageExport').value;

    if (exportContainerId) {

            if (vesselid) {

                if (voyageId) {
        loadGrid()
    }
    else {
        alert('Please Select Voyage')
    }

            }

    else {
        alert('Please Select Vessel')
    }

        }
    else {
        alert('Please Select Container')
    }

    }

    function loadGrid(dataSource) {
        loadingBar();

    $.get('/Export/ExportContainer/GetExportContainerItemsAssociated?vesselid=' + vesselid + '&voyageId=' + voyageId + '&exportContainerId=' + exportContainerId, function (data) {

            //$.get('/Export/Shipper/GetShipperExport', function (Shipperdata) {
            //    shipper = Shipperdata;

                if (dataSource)
    exportContainerItems = dataSource
    else
    exportContainerItems = data;

    console.log(exportContainerItems)

    loadingBarHide();
    var dataGrid = $("#ExportContainerItem").dxDataGrid({
        dataSource: exportContainerItems,
    keyExpr: "exportContainerItemId",
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
        mode: "row",
    allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click"
                    },
    columns: [

    {
        dataField: "gdNumber",
    caption: "GD Number",
    validationRules: [{type: "required" }],
    allowEditing: false,

                        },
    {
        dataField: "numberOfPackages",
    caption: "Number of Packages",
    validationRules: [{type: "required" }],
    allowEditing: false,

                        },
    {
        dataField: "allowLoading",
    caption: "Allow Loading",
    validationRules: [{type: "required" }],
    allowEditing: false,

                        },
    {
        dataField: "destination",
    caption: "Destination",
    validationRules: [{type: "required" }],
    allowEditing: false,

                        },
    {
        dataField: "shipperName",
    caption: "Shipper Name",
    validationRules: [{type: "required" }],
    allowEditing: false,

                        },

    {
        dataField: "orderNumber",
    caption: "OrderNumber",
    validationRules: [{type: "required" }],
                        },


    {
        caption: "",
    dataField: "ischecked",
    allowEditing: false,
    dataType: "boolean"
                        }


    ],



                }).dxDataGrid("instance");


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

            //});



        });

    }



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function AddExportContainerItems( ) {

        var models = exportContainerItems.filter(c => c.ischecked == true);

        var resdata = Math.max(...models.map(o => o.orderNumber));

        var gddata = models.find(c => c.orderNumber == resdata);

    var r = confirm("Are you sure wan't To Send Delete message against this gd  " + gddata.gdNumber + " with status F");

    console.log("gddata", gddata);


    if (r == true) {



        $.post('/Export/ExportContainer/AddExportContainerItem', { gddata: gddata }, function (data) {

            if (data.error)
                showToast(data.message, "warning");
            else
                showToast(data.message, "success");

        });
        }
    }




    function formfiled() {


    }
