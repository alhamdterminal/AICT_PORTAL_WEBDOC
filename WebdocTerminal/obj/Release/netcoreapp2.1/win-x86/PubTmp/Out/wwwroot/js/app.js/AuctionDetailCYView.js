

    var grid;

    var containers = [];

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }



    function hideMenifestedColumns(e) {

        checkColumn(e, "virNo");
    checkColumn(e, "blNo");
    checkColumn(e, "containerNo");
    checkColumn(e, "indexNo");
    checkColumn(e, "size");



    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
     }





    function updateIndexes() {
        console.log("dddd");
    $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        var resuktContainers = containers.filter(c =>  c.isAuction == true);
    console.log(resuktContainers, "RC");
    if (resuktContainers.length) {
        console.log("resuktContainers", resuktContainers)

            sendgroundingcontainers(resuktContainers);


        }


    }



    function sendgroundingcontainers(resuktContainers) {


        console.log(resuktContainers, "");
    $.post('AlotAuctionNumberCY', {model: resuktContainers }, function (data) {
        console.log(data);
    if (data.error == true) {
        showToast(data.message, "error");
    window.location.href = window.location.href;

                }
    else {

        showToast(data.message, "success");

    window.location.href = window.location.href;

                }

        });

    }

    function formfiled() {

    }



    function ShowDetail() {

         var fromdate = document.getElementById("single_cal2").value;

    var todate = document.getElementById("single_cal3").value;


    console.log("fromdate", fromdate);

    console.log("todate", todate);




    $.get('/APICalls/GetCargodetailCYUnDeliveredCargo?frmodate=' + fromdate + '&todate=' + todate, function (data) {

        console.log("data", data);

    containers = data;

    var dataGrid = $("#groundedContainer").dxDataGrid({
        dataSource: containers,
    keyExpr: "containerNo",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    paging: {
        enabled: false
                         },
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
    "virNo",
    "indexNo",
    {
        caption: "Aging Days",
    dataField: "size",
                             },

    {
        caption: "",
    dataField: "isAuction",
    dataType: "boolean"
                             }
    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
                         },
    onRowUpdated: function (e) {

    }

                     }).dxDataGrid("instance");


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
             $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

                 });
          


     }



