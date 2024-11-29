

    var containers = [];

    function igm_changed(data) {

        igm = data.value;
    igmnumber = data.value;


    igmnumber = igmnumber.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '_').length;

    console.log("igmnumber", igmnumber)


    if (igmnumber == 16) {
            if (igm) {

        $.get('/APICalls/GetIndexDropdownForCY?IGM=' + igm, function (indexdb) {

            $('#containerIndex').html(indexdb);

        });
            }
        }
    else {
        $('#containerIndex').html([]);


        }


    }


    function showcargoDetaildesc() {


        var virno = $("#virnolist").dxAutocomplete("instance").option("value");

    var containerno = $("#containerdb option:selected").val();

    console.log('virno', virno);

    console.log('containerno', containerno);

    if (virno && containerno) {
        $.get('/APICalls/GetUndeliveredSRC?virno=' + virno + '&containerno=' + containerno, function (data) {

            console.log("data", data);

            if (data.length) {

                griddata(data);

            }
            else {

                griddata([])
            }

        });
        }
    else {
        alert("You Must Have to Select Igm And Index")
    }


    }


    function griddata(data) {

        containers = data;

    var dataGrid = $("#groundedContainer").dxDataGrid({
        dataSource: containers,
    keyExpr: "srcId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    paging: {
        enabled: false
                },
    wordWrapEnabled: true,

    editing: {
        mode: "batch",
    allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click"
                },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

                },
    columns: [
    {
        dataField: "virNumber",
    caption: "VIR No",
    allowEditing: false,
                    },
    {
        dataField: "containerNumber",
    caption: "Container No",
    allowEditing: false,
                    },
    {
        dataField: "weight",
    caption: "Weight",

                    },
    {
        dataField: "location",
    caption: "location"
                    },

    "handlingCode",
    {
        dataField: "activityType",
    caption: "Activity",
    width: 150,
    lookup: {
        dataSource: [{activity: "LOAD" }, {activity: "DISCHARGE" }, {activity: "GROUNDED" }, {activity: "COMPLETED" }],
    displayExpr: "activity",
    valueExpr: "activity"
                        }
                    },
    {
        dataField: "status",
    caption: "Status",
    lookup: {
        dataSource: [{status: "F" }, {status: "E" }],
    displayExpr: "status",
    valueExpr: "status"
                        }
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



    }


    function ground() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        var groundedContainers = containers.filter(c => c.activityType != null && c.isProcessed == true);

    $.post('SaveReprocessSCR', {containers: groundedContainers }, function (data) {


            if (data.error == true) {
        showToast(data.message, "error");
    window.location.href = window.location.href;

            }
    else {

        showToast(data.message, "success");
    window.location.href = window.location.href;

                loadGrid(containers.filter(c => c.activityType != null && c.isProcessed == false));

            }
    window.location.href = window.location.href;


        });
    }

    function formfiled() {

    }

    function refresh() {
        window.location.reload();
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }
