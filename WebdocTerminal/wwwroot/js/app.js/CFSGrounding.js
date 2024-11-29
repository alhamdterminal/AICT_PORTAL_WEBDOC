

    var grid;

    var containers = [];

    //$(function () {

        //      formfiled()

        //   });

        function checkColumn(e, field) {

            if (e.parentType === "dataRow" && e.dataField !== field) {
                return;
            }

            e.editorOptions.disabled = true;
        }

    function hideMenifestedColumns(e) {

        checkColumn(e, "indexNo");
    checkColumn(e, "virNumber");
    checkColumn(e, "containerNo");
    checkColumn(e, "blNumber");
    //checkColumn(e, "weight");
    checkColumn(e, "handlingCode");

        if (PermissionInputs.find(x => x.fieldName == "Location" && x.isChecked == false)) {

        checkColumn(e, "location");
        }
        if (PermissionInputs.find(x => x.fieldName == "ActivityType" && x.isChecked == false)) {

        checkColumn(e, "activityType");
        }
        if (PermissionInputs.find(x => x.fieldName == "IsGrounded" && x.isChecked == false)) {

        checkColumn(e, "isChecked");
        }
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

        $.get('/APICalls/GetUngroundedCFSContainers', function (data) {

            console.log("data", data);

            if (dataSource)
                containers = dataSource
            else
                containers = data;

            var dataGrid = $("#groundedContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "containerIndexId",
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
                    "containerNo",
                    "indexNo",
                    "virNumber",
                    "blNumber",
                    {
                        dataField: "goodName",
                        caption: "Commodity",
                        allowEditing: false,

                    },
                    {
                        dataField: "igmoBLNumber",
                        formItem: { visible: false }
                    },
                    {
                        dataField: "weight",
                        caption: "Weight",
                        allowEditing: false,

                    },
                    {

                        dataField: "manifestedSealNumber",
                        caption: "Manifested Seal Number",
                        allowEditing: false,

                    },
                    {

                        dataField: "examinationAlertDate",
                        caption: "Examination Alert Date",
                        allowEditing: false,
                        dataType: 'date',
                        format: 'dd/MM/yyyy',
                    },
                    {

                        dataField: "description",
                        caption: "Description",
                        allowEditing: false,

                    },
                    {
                        dataField: "location",
                        caption: "location"
                    },
                    {

                        dataField: "count",
                        caption: "Count",
                        allowEditing: false,

                    },
                    "handlingCode",
                    {
                        dataField: "activityType",
                        caption: "Activity",
                        width: 150,
                        lookup: {
                            dataSource: [{ activity: "LOAD" }, { activity: "DISCHARGE" }, { activity: "GROUNDED" }, { activity: "COMPLETED" }],
                            displayExpr: "activity",
                            valueExpr: "activity"
                        }
                    },
                    {
                        caption: "",
                        dataField: "isChecked",
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

    function ground() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        var groundedContainers = containers.filter(c => c.activityType != null && c.isChecked == true);

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
                else {
                    console.log("else")
                }

            }
            else {
                sendgroundingcontainers(groundedContainers);
            }

        });
    }

    function sendgroundingcontainers(groundedContainers) {


        $.post('SaveCFSGroundedContainers', { containers: groundedContainers }, function (data) {

            if (data.error == true) {
                showToast(data.message, "error");
                window.setInterval('refresh()', 3000);

            }
            else {

                showToast(data.message, "success");

                window.setInterval('refresh()', 3000);

                loadGrid(containers.filter(c => c.activityType != null && c.isChecked == false));

            }

        });

    }

    function formfiled() {

        loadGrid();
    }

    function refresh() {
        window.location.reload();
    }

    function showgroundingdetail() {

        var goodsHeadId = $("#goodsHeadId option:selected").val();
    if (goodsHeadId) {
        loadGridbyid(goodsHeadId);
        }
    else {
        loadGrid();
        }

    }

    function loadGridbyid(goodsHeadId) {

        $.get('/APICalls/GetUngroundedCFSContainersByGoodHeadId?goodheadId=' + goodsHeadId, function (data) {


            console.log("data", data);

            containers = data;

            console.log(containers);

            var dataGrid = $("#groundedContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "containerIndexId",
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
                    "containerNo",
                    "indexNo",
                    "virNumber",
                    "blNumber",
                    {
                        dataField: "goodName",
                        caption: "Commodity",
                        allowEditing: false,

                    },
                    {
                        dataField: "igmoBLNumber",
                        formItem: { visible: false }
                    },
                    {
                        dataField: "weight",
                        caption: "Weight",
                        allowEditing: false,

                    },
                    {

                        dataField: "manifestedSealNumber",
                        caption: "Manifested Seal Number",
                        allowEditing: false,

                    },
                    {

                        dataField: "examinationAlertDate",
                        caption: "Examination Alert Date",
                        allowEditing: false,
                        dataType: 'date',
                        format: 'dd/MM/yyyy',

                    },
                    {

                        dataField: "description",
                        caption: "Description",
                        allowEditing: false,

                    },
                    {
                        dataField: "location",
                        caption: "location"
                    },
                    {

                        dataField: "count",
                        caption: "Count",
                        allowEditing: false,

                    },
                    "handlingCode",
                    {
                        dataField: "activityType",
                        caption: "Activity",
                        width: 150,
                        lookup: {
                            dataSource: [{ activity: "LOAD" }, { activity: "DISCHARGE" }, { activity: "GROUNDED" }, { activity: "COMPLETED" }],
                            displayExpr: "activity",
                            valueExpr: "activity"
                        }
                    },
                    {
                        caption: "",
                        dataField: "isChecked",
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


