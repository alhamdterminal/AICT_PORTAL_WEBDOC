
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
                if (PermissionInputs.find(x => x.fieldName == "Voyage" && x.isChecked == false)) {

        document.getElementById('voyageExport').disabled = true;
                }
            });



        });
    });

    function checkColumn(e, field) {


        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;



    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "gdNumber");
    //checkColumn(e, "noOfPackages");
    checkColumn(e, "noOfPackagesPass");
    checkColumn(e, "destination");
    //checkColumn(e, "shipperId");
    checkColumn(e, "shipperName");
    checkColumn(e, "invoiceNumber");
    // checkColumn(e , "isAmountReceived");
    checkColumn(e, "messageTo");
    checkColumn(e, "anfDate");
    checkColumn(e, "paperReadyDate");
    checkColumn(e, "allowLoading");



    }

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

    $.get('/Export/ExportContainer/GetExportContainerItems?vesselid=' + vesselid + '&voyageId=' + voyageId + '&exportContainerId=' + exportContainerId, function (data) {

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
    keyExpr: "cargoReceivingInformationId",
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
    "gdNumber",
    "noOfPackages",
    //"noOfPackagesPass",
    "allowLoading",
    "destination",
    "shipperName",

    {
        dataField: "invoiceNumber",
    caption: "Invoice No",
    validationRules: [{type: "required" }],

                    },
    {
        dataField: "isAmountReceived",
    caption: "Is Amount Received",
    validationRules: [{type: "required" }],
                    },

    {
        dataField: "messageTo",
    caption: "Port",
    validationRules: [{type: "required" }],
                    },

    {
        caption: "ANF Date",
    dataField: "anfDate",
    dataType: "date",
    validationRules: [{type: "required" }],
    format: "dd/MM/yyyy"
                    },
    {
        caption: "Paper Ready",
    dataField: "paperReadyDate",
    dataType: "date",
    validationRules: [{type: "required" }],
    format: "dd/MM/yyyy"
                    },
                    //{
        //    width: 100,
        //    alignment: 'center',
        //    cellTemplate: function (container, options) {
        //        $('<a/>').addClass('dx-link')
        //            .text('Add Remarks')
        //            .on('dxclick', function () {
        //                addremarks(options.row.data)
        //            })
        //            .appendTo(container);
        //    }
        //},
        //{
        //    dataField: "shipperId",
        //    width: 200,
        //    caption: "Shipper",
        //    lookup: {
        //        dataSource: shipper,
        //        displayExpr: "shipperName",
        //        valueExpr: "shipperId"
        //    }
        //},
        //{
        //    dataField: "status",
        //    width: 200,
        //    lookup: {
        //        dataSource: ["P", "F"]
        //    }
        //},
        {
            dataField: "orderNumber",
            caption: "OrderNumber",
            validationRules: [{ type: "required" }],
        },


        {
            caption: "",
            dataField: "ischecked",
            dataType: "boolean"
        }


                ],

    onEditorPreparing: function (e) {

        //console.log(e.value)
        //if (e.dataField == "isAmountReceived" && e.value != null) {
        //    e.editorOptions.disabled = true
        //}
        hideMenifestedColumns(e);
                },
    onRowUpdated: function (e) {

    }

            }).dxDataGrid("instance");


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

            //});



        });

    }


    //function addremarks(data) {
        //    console.log("data", data);
        //    $('#cameraModal').modal('show');

        //    $('#gdNumber').val(data.gdNumber);


        //}


        //function savedata() {

        //    if ($('#remarks').val()) {

        //        var gdno = $('#gdNumber').val();

        //        var ExportContainerItemdata = $("#ExportContainerItem").dxDataGrid("option", "dataSource");

        //        if (ExportContainerItemdata.length) {
        //            var docList = ExportContainerItemdata.find(x => x.gdNumber === gdno);

        //            docList.isAmountReceived = $('#remarks').val();


        //        }
        //    }

        //    $('#cameraModal').modal('hide');


        //}

        function showToast(message, icon) {

            $.toast({
                heading: message,
                showHideTransition: 'slide',
                position: 'bottom-right',
                icon: icon
            });
        }


    function AddExportContainerItems(checked, submit) {

        if (checked == "false") {

        $("#btnSave").attr("disabled", true);
    setTimeout(function () {$("#btnSave").attr("disabled", false); }, 120000);

        }

    if (checked == "true") {

        $("#btnSubmit").attr("disabled", true);
    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        }


        var models = exportContainerItems.filter(c => c.ischecked == true);

    var exportContainerId = $('#exportContainer').val();

    $.post('/Export/ExportContainer/AddExportContainerItems', {models: models, exportContainerId: exportContainerId, ischecked: checked, vesselid: vesselid, voyageId: voyageId, submit: submit }, function (data) {

            if (data.error)
    showToast(data.message, "warning");
    else
    showToast(data.message, "success");

    if (submit === "true")
                loadGrid(exportContainerItems.filter(c => c.ischecked == false));
        });
    }

    function createExportStuffingTallySheet() {



        var skillsSelect = document.getElementById("exportContainer");
    var containerNumber = skillsSelect.options[skillsSelect.selectedIndex].text;
    vesid = document.getElementById('vesselExport').value;
    voyId = document.getElementById('voyageExport').value;


    if (containerNumber == "Select Container") {
        containerNumber = ""
    }

    window.open('/Export/Reports/ExportTellySheetSec?containerNumber=' + containerNumber + '&vesid=' + vesid + '&voyId=' + voyId, '_blank');

    }
    function restform() {
        var r = confirm("Are you sure ");
    if (r == true) {
        window.location.href = window.location.href
    }

    }

    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "Container" && x.isChecked == false)) {

        document.getElementById('exportContainer').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExport').disabled = true;
        }

        if (PermissionInputs.find(x => x.fieldName == "btnSubmit" && x.isChecked == false)) {

        document.getElementById('btnSubmit').style.display = "none";
        }

    }
