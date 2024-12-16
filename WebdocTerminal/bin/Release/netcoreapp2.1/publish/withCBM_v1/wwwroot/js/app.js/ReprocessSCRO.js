

    var containers = [];

    function igm_changed(data) {

        igm = data.value;
    igmnumber = data.value;


    igmnumber = igmnumber.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '_').length;

    console.log("igmnumber", igmnumber)


    if (igmnumber == 16) {
            if (igm) {

        $.get('/APICalls/GetIndexDropdownForCFS?IGM=' + igm, function (indexdb) {

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

    var indexno = $("#containerIndexCYdb option:selected").val();

    console.log('virno', virno);

    console.log('indexno', indexno);

    if (virno && indexno) {
        $.get('/APICalls/GetUndeliveredSRCO?virno=' + virno + '&indexno=' + indexno, function (data) {

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
    keyExpr: "srcoId",
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
        dataField: "indexNumber",
    caption: "Index No",
    allowEditing: false,
                    },

    {
        dataField: "blNumber",
    caption: "BL No", 
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

    if (groundedContainers.length) {
        console.log("groundedContainers", groundedContainers)

            sendgroundingcontainers(groundedContainers)

        } 

    }

    function checkHoldContainerIndexes(groundedContainers) {


        $.post('/APICalls/checkHoldContainerIndexes', { containers: groundedContainers }, function (data) {

            console.log("ResultForHold", data)

            if (data.holdStatus == true) {
                var r = confirm(data.specialInstructions);

                if (r == true) {
                    sendgroundingcontainers(groundedContainers);
                }


            }
            else {
                sendgroundingcontainers(groundedContainers);
            }

        });
    }

    function sendgroundingcontainers(groundedContainers) {


        $.post('SaveReprocessSCRO', { containers: groundedContainers }, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");
                window.setInterval('refresh()', 3000);

            }
            else {

                showToast(data.message, "success");

                window.setInterval('refresh()', 3000);

                griddata(containers.filter(c => c.activityType != null && c.isChecked == false));

            }

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
