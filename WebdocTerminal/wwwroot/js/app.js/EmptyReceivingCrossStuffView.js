

    var grid;

    var containers = [];

    var shippinglines = [];

    var ListOfAllCsEmtypcontainers = [];

    $(function () {

        $.get('/APICalls/GetUnAssignedCScontainersList', function (data) {

            ListOfAllCsEmtypcontainers = data;

        });
    })


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function loadGrid(dataSource) {

        $.get('/APICalls/GetEmptyReveivingCrossStuffing', function (data) {

            console.log(data)

            $.get('/ShippingLine/GetShippingLines', function (shippinglinesdata) {

                $.get('/APICalls/GetUnAssignedCScontainers', function (containerres) {
                    if (dataSource)
                        containers = dataSource
                    else
                        containers = data;

                    shippinglines = shippinglinesdata;

                    console.log(containers)

                    var dataGrid = $("#gateinContainer").dxDataGrid({
                        dataSource: containers,
                        keyExpr: "containerNo",
                        allowColumnResizing: true,
                        columnAutoWidth: true,
                        wordWrapEnabled: true,
                        showBorders: true,
                        showBorders: true,


                        dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                        paging: {
                            enabled: true
                        },
                        searchPanel: {
                            visible: true,
                            width: 240,
                            placeholder: "Search..."
                        },
                        editing: {

                            allowUpdating: function (e) {

                                //console.log("e", )
                                if (e.row.data.emptyReceivedShippingLineId == null) {
                                    return true;
                                }
                                else {
                                    return false;
                                }
                            },

                            //allowUpdating: true,
                            allowDeleting: true,
                            selectTextOnEditStart: true,
                            mode: "row"
                        },
                        columns: [
                            {
                                dataField: "virNo",
                                caption: "VIR No",
                                allowEditing: false,
                                validationRules: [{ type: "required" }],
                            },

                            {
                                dataField: "containerNo",
                                caption: "Container No",
                                allowEditing: false,
                                validationRules: [{ type: "required" }],
                            },
                            {
                                dataField: "size",
                                caption: "Size",
                                allowEditing: false,
                                validationRules: [{ type: "required" }],
                            },
                            {
                                dataField: "shippingLineName",
                                caption: "ShippingLine",
                                allowEditing: false,
                                validationRules: [{ type: "required" }],
                            },
                            {
                                dataField: "blNo",
                                caption: "BL No",
                                allowEditing: false,
                                validationRules: [{ type: "required" }],
                            },
                            {
                                dataField: "csEmptyContainerReceiveId",
                                caption: "Container",
                                width: 200,
                                lookup: {
                                    dataSource: containerres,
                                    displayExpr: "text",
                                    valueExpr: "value"
                                },
                                setCellValue(rowData, value, e) {
                                    console.log("calling")
                                    var resofvlaue = ListOfAllCsEmtypcontainers.find(x => x.csEmptyContainerReceiveId == value);
                                    if (resofvlaue) {
                                        rowData.csEmptyContainerReceiveId = resofvlaue.csEmptyContainerReceiveId;
                                        rowData.csContainerNumber = resofvlaue.containerNo;
                                        rowData.csSize = resofvlaue.size;
                                        rowData.csTireWeight = resofvlaue.tireWeight;
                                        rowData.csCondition = resofvlaue.otherRemarks;
                                        rowData.csVehicleNumer = resofvlaue.trukNo;
                                    }
                                    else {

                                        rowData.csEmptyContainerReceiveId = "";
                                        rowData.csContainerNumber = "";
                                        rowData.csSize = "";
                                        rowData.csTireWeight = "";
                                        rowData.csCondition = "";
                                        rowData.csVehicleNumer = "";

                                    }
                                },


                            },

                            {
                                dataField: "csContainerNumber",
                                caption: "New Container No",
                                allowEditing: false,
                                validationRules: [{ type: "required" }],
                            },
                            {
                                dataField: "csSize",
                                caption: "Size",
                                validationRules: [{ type: "required" }],
                                allowEditing: false,

                                //lookup: {
                                //    dataSource: [{ containerSize: 20 }, { containerSize: 40 }, { containerSize: 45 }],
                                //    displayExpr: "containerSize",
                                //    valueExpr: "containerSize"
                                //}
                            },

                            {
                                dataField: "emptyReceivedShippingLineId",
                                caption: "Shipping Line",
                                width: 200,
                                validationRules: [{ type: "required" }],

                                lookup: {
                                    dataSource: shippinglines,
                                    displayExpr: "shippingLineName",
                                    valueExpr: "shippingLineId"
                                }
                            },

                            {
                                caption: "Tire Weight",
                                dataField: "csTireWeight",
                                dataType: "number",
                                allowEditing: false,
                                validationRules: [{ type: "required" }]
                            },
                            {
                                dataField: "csCondition",
                                caption: "Condition",
                                width: 200,
                                allowEditing: false,
                                validationRules: [{ type: "required" }],
                            },
                            {
                                dataField: "csVehicleNumer",
                                caption: "Vehicle",
                                width: 200,
                                allowEditing: false,
                                validationRules: [{ type: "required" }],
                            },

                        ],
                        onRowUpdated: function (e) {

                            console.log(e);
                            var model = e.data;

                            $.post('/Import/GateInOut/UpdateCyContainer?virNo=' + model.virNo + '&containerNo=' + model.containerNo, { model, model }, function (data) {

                                if (data.error == true) {
                                    showToast(data.message, "error")
                                } else {
                                    showToast(data.message, "success")
                                }

                                window.location.href = window.location.href;
                            })

                        },
                        onRowRemoved: function (e) {

                            console.log(e);
                            var model = e.data;

                            $.post('/Import/GateInOut/DeleteCSContainerAssociation?virNo=' + model.virNo + '&containerNo=' + model.containerNo, { model, model }, function (data) {

                                if (data.error == true) {
                                    showToast(data.message, "error")
                                } else {
                                    showToast(data.message, "success")
                                }

                                window.location.href = window.location.href;
                            })

                        },


                    }).dxDataGrid("instance");

                    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                    $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


                });

            });

        });

    }








    function formfiled() {
        loadGrid();
    }

