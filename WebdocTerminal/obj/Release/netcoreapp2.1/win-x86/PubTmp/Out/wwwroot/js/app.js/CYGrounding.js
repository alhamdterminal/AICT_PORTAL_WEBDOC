

    var grid;

    var containers = [];

    //$(function () {
        //  //  formfiled();

        //});

        function checkColumn(e, field) {

            if (e.parentType === "dataRow" && e.dataField !== field) {
                return;
            }

            e.editorOptions.disabled = true;
        }

    function hideMenifestedColumns(e) {

        checkColumn(e, "virNumber");
    checkColumn(e, "containerNo");
    checkColumn(e, "weight");
         

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

        $.get('/APICalls/GetUngroundedCYContainers', function (data) {



            console.log("data", data);


            if (dataSource)
                containers = dataSource
            else
                containers = data;

            console.log(containers);

            var dataGrid = $("#groundedContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "containerId",
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
                    {

                        dataField: "count",
                        caption: "Count",
                        allowEditing: false,

                    },
                    "virNumber",
                    "containerNo",
                    {

                        dataField: "indexNo",
                        caption: "Index No",
                        allowEditing: false,

                    },
                    {

                        dataField: "goodName",
                        caption: "Commodity",
                        allowEditing: false,

                    },
                    {

                        dataField: "manifestedSealNumber",
                        caption: "M Seal Number",
                        allowEditing: false,

                    },
                    {

                        dataField: "description",
                        caption: "Description",
                        allowEditing: false,

                    },

                    "weight",
                    "location",
                    {

                        dataField: "handlingCode",
                        caption: "Handling Code",
                        allowEditing: false,

                    },
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
                        dataField: "status",
                        caption: "Status",
                        lookup: {
                            dataSource: [{ status: "F" }, { status: "E" }],
                            displayExpr: "status",
                            valueExpr: "status"
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

    $.post('SaveCYGroundedContainers', {containers: groundedContainers }, function (data) {


            if (data.error == true) {
        showToast(data.message, "error");
    window.location.href = window.location.href;

            }
    else {

        showToast(data.message, "success");
    window.location.href = window.location.href;

                loadGrid(containers.filter(c => c.activityType != null && c.isChecked == false));

            }
    window.location.href = window.location.href;


        });
    }


    function formfiled() {

        loadGrid();
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

        $.get('/APICalls/GetUngroundedCYContainersByGoodHeadId?goodheadId=' + goodsHeadId, function (data) {


            console.log("data", data);

            containers = data;

            console.log(containers);

            var dataGrid = $("#groundedContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "containerId",
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
                    {

                        dataField: "count",
                        caption: "Count",
                        allowEditing: false,

                    },
                    "virNumber",
                    "containerNo",
                    {

                        dataField: "indexNo",
                        caption: "Index No",
                        allowEditing: false,

                    },
                    {

                        dataField: "goodName",
                        caption: "Commodity",
                        allowEditing: false,

                    },
                    {

                        dataField: "manifestedSealNumber",
                        caption: "M Seal Number",
                        allowEditing: false,

                    },
                    {

                        dataField: "description",
                        caption: "Description",
                        allowEditing: false,

                    },

                    "weight",
                    "location",
                    {

                        dataField: "handlingCode",
                        caption: "Handling Code",
                        allowEditing: false,

                    },
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
                        dataField: "status",
                        caption: "Status",
                        lookup: {
                            dataSource: [{ status: "F" }, { status: "E" }],
                            displayExpr: "status",
                            valueExpr: "status"
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



