

    var grid;

    var containers = [];

    var bondedcarriers = [];



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function loadGrid(dataSource) {

        $.get('/APICalls/GetPreGateOutCrossStuffing', function (data) {

            console.log(data)

            $.get('/Import/Setup/GetBoundedCarrier', function (bondedcarriersdata) {

                if (dataSource)
                    containers = dataSource
                else
                    containers = data;

                bondedcarriers = bondedcarriersdata;

                console.log(containers);

                var dataGrid = $("#gateinContainer").dxDataGrid({
                    dataSource: containers,
                    keyExpr: "containerNumber",
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

                        allowUpdating: true,
                        //selectTextOnEditStart: true,
                        //startEditAction: "click",
                        mode: "cell"
                    },
                    columns: [
                        {
                            dataField: "virNumber",
                            caption: "VIR No",
                            allowEditing: false,
                            validationRules: [{ type: "required" }],
                        },

                        {
                            dataField: "containerNumber",
                            caption: "Container No",
                            allowEditing: false,
                            validationRules: [{ type: "required" }],
                        },
                        {
                            dataField: "vehicleNumber",
                            caption: "Vehicle Number",
                            validationRules: [{ type: "required" }],
                        },

                        {
                            dataField: "bondedCarrierId",
                            caption: "bonded Carrier NTN",
                            width: 200,
                            validationRules: [{ type: "required" }],

                            lookup: {
                                dataSource: bondedcarriers,
                                displayExpr: "boundedNTN",
                                valueExpr: "boundedNTN"
                            },

                            //setCellValue: function (rowData, value) {

                            //    rowData.bondedCarrierId = value;

                            //    var res = bondedcarriers.find(x => x.boundedNTN == value);

                            //    rowData.bondedCarrierName = res.boundedCarrierName;

                            //}
                        },

                        {
                            dataField: "bondedCarrierName",
                            caption: "Carrier Name",
                            width: 200,
                            validationRules: [{ type: "required" }],

                        },
                        {
                            caption: "",
                            dataField: "isProcessed",
                            dataType: "boolean"
                        }

                    ],

                    onEditorPreparing: function (e) {

                        console.log("e.row.data.bondedCarrierId", e);
                        if (e.parentType == 'dataRow' && e.dataField == "bondedCarrierId") {

                            //console.log( " asd", e.editorElement)
                            //e.editorElement.dxSelectBox('instance').option('onValueChanged', function (args) {
                            //    var res = bondedcarriers.find(x => x.boundedNTN == args.value);
                            //    console.log("res", res);
                            //    boundedCarrierName = res.boundedCarrierName;
                            //});
                            //console.log("e.row.data.bondedCarrierId", e)
                            //console.log("e.row.data.bondedCarrierId", e.row.data.bondedCarrierId)

                            e.editorOptions.onValueChanged = function (args) {
                                console.log("e", e);
                                console.log("args", args);
                                console.log("bondedcarriers", bondedcarriers);
                                var res = bondedcarriers.find(x => x.boundedNTN == args.value);

                                console.log("res", res)

                                e.component.cellValue(e.row.rowIndex, "bondedCarrierName", res.boundedCarrierName);
                                e.component.cellValue(e.row.rowIndex, "bondedCarrierId", args.value);
                            }
                        }
                    },



                }).dxDataGrid("instance");

                $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

            });

        });

    }











    function formfiled() {
        loadGrid();
    }


    function createedi() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        var modelist = containers.filter(c => c.isProcessed == true);

    console.log(modelist)
    $.post('/Import/Grounding/CreatePreGateOutCrossStuff', {modelist: modelist }, function (data) {

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
            loadGrid(containers.filter(c => c.isProcessed == false));
        });
    }

