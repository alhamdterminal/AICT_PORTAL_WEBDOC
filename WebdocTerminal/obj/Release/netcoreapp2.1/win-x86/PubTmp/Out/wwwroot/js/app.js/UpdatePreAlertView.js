


    var ShippingLineList = [];
    var ShippingagnetList = [];
    var PortAndTerminalList = [];



    $(function () {


        GetShippingLines();
    getPortAndTerminals();
    GetShippingAgents();
    });

    var ContainerType = [
    {Name: "CFS" },
    {Name: "CY" }
    ];

    var CargoType = [
    {Name: "G" },
    {Name: "DG" },
    {Name: "V" }

    ];

    var SOCCOC = [
    {Name: "SOC" },
    {Name: "COC" }
    ];



    function GetShippingLines() {

        $.get('/ShippingLine/GetShippingLines', function (data) {
            ShippingLineList = data;
            console.log("ShippingLineList", ShippingLineList);
        });

    }


    function GetShippingAgents() {

        $.get('/ShippingAgent/Get', function (data) {
            ShippingagnetList = data;
            console.log("ShippingagnetList", ShippingagnetList);
        });

    }



    function getPortAndTerminals() {

        $.get("/APICalls/GetPortAndTerminals", function (PortAndTerminal) {
            PortAndTerminalList = PortAndTerminal
            console.log("PortAndTerminalList", PortAndTerminalList)
        });
    }

    function GetPreAlertByDate(fromdate, todate) {


        $.get('/Import/PreAlert/GetPreAlterByDate?fromDate=' + fromdate + '&todate=' + todate, function (result) {
            console.log("result", result)
            var PreAlertList = result;


            $("#updatePreAlertdetails").dxDataGrid({
                dataSource: PreAlertList,
                keyExpr: "preAlertId",
                showBorders: true,
                selection: { mode: "single" },
                hoverStateEnabled: true,
                columnsAutoWidth: true,
                paging: {
                    pageSize: 15
                },
                scrolling: {
                    columnRenderingMode: "virtual"
                },
                filterRow: {
                    visible: true,
                    applyFilter: "auto"
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."
                },
                editing: {
                    mode: "row",
                    allowDeleting: true,
                    allowUpdating: true,
                    useIcons: true
                },
                headerFilter: {
                    visible: true
                },
                columns: [
                    {
                        dataField: "preAlertNumber",
                        caption: "Alert No",
                        allowEditing: false,
                    },

                    {
                        dataField: "preAlertDate",
                        caption: "Created Date",
                        dataType: "date",
                        format: "dd/MMM/yyyy",
                    },
                    {
                        dataField: "shippingAgentId",
                        caption: "Principal",
                        lookup: {
                            dataSource: ShippingagnetList,
                            displayExpr: "name",
                            valueExpr: "shippingAgentId"
                        }
                    },

                    {
                        dataField: "shippingAgentLineId",
                        caption: "Line / F.F",
                        lookup: {
                            dataSource: ShippingagnetList,
                            displayExpr: "name",
                            valueExpr: "shippingAgentId"
                        }
                    },
                    {
                        dataField: "vessel",
                        caption: "Vessel",
                    },
                    {
                        dataField: "voyage",
                        caption: "Voyage",
                    },
                    {
                        dataField: "portAndTerminalId",
                        caption: "Port",
                        lookup: {
                            dataSource: PortAndTerminalList,
                            displayExpr: "portName",
                            valueExpr: "portAndTerminalId"
                        }
                    },
                    {
                        dataField: "etaDate",
                        caption: "ETA Date",
                        dataType: "date",
                        format: "dd/MMM/yyyy",
                    },

                ],
                masterDetail: {
                    enabled: true,
                    template: function (container, options) {

                        $("<div>")
                            .dxDataGrid({
                                columnAutoWidth: true,
                                showBorders: true,
                                filterRow: {
                                    visible: true,
                                    applyFilter: "auto"
                                },
                                headerFilter: {
                                    visible: true
                                },
                                editing: {
                                    mode: "row",
                                    allowDeleting: true,
                                    allowUpdating: true,
                                    useIcons: true
                                },
                                columns: [

                                    {
                                        dataField: "containerNumber",
                                        validationRules: [{ type: "required" }],
                                        caption: "Container No",
                                    },
                                    {
                                        dataField: "masterBLNumber",
                                        validationRules: [{ type: "required" }],
                                        caption: "M BL No",
                                    },
                                    {
                                        dataField: "size",
                                        caption: "Container Size",
                                        width: 150,
                                        validationRules: [{ type: "required" }],
                                        lookup: {
                                            dataSource: [{ size: 20 }, { size: 40 }, { size: 45 }],
                                            displayExpr: "size",
                                            valueExpr: "size"
                                        }
                                    },
                                    {
                                        dataField: "type",
                                        caption: "Type",
                                        width: 150,
                                        validationRules: [{ type: "required" }],
                                        lookup: {
                                            dataSource: ContainerType,
                                            valueExpr: "Name",
                                            displayExpr: "Name"
                                        }
                                    },
                                    {
                                        dataField: "cargoType",
                                        caption: "Cargo Type",
                                        width: 150,
                                        validationRules: [{ type: "required" }],
                                        lookup: {
                                            dataSource: CargoType,
                                            valueExpr: "Name",
                                            displayExpr: "Name"
                                        }
                                    },
                                    {
                                        dataField: "shippingLineId",
                                        caption: "Shipping Agent",
                                        lookup: {
                                            dataSource: ShippingLineList,
                                            displayExpr: "shippingLineName",
                                            valueExpr: "shippingLineId"
                                        }
                                    },
                                    {
                                        dataField: "portAndTerminalName",
                                        caption: "P O L",
                                        //lookup: {
                                        //    dataSource: PortAndTerminalList,
                                        //    displayExpr: "portName",
                                        //    valueExpr: "portAndTerminalId"
                                        //}
                                    },
                                    {
                                        dataField: "soccoc",
                                        caption: "SOC / COC",
                                        width: 150,
                                        validationRules: [{ type: "required" }],
                                        lookup: {
                                            dataSource: SOCCOC,
                                            valueExpr: "Name",
                                            displayExpr: "Name"
                                        }
                                    }



                                ],
                                onRowUpdated: function (e) {
                                    console.log(e);
                                    var data = e.data;
                                    $.post('/Import/PreAlert/UpdatePreAlert', { data, data }, function (result) {


                                        if (result.error) {
                                            showToast(result.message, "warning");
                                        }
                                        else {
                                            showToast(result.message, "success");
                                        }

                                        window.setInterval('refresh()', 3000);
                                    });
                                },
                                onRowRemoved: function (e) {
                                    console.log(e.data)
                                    var key = e.data.preAlterDetailId;

                                    console.log("key", key);

                                    $.post('/Import/PreAlert/DeletePreAlertDetail?key=' + key, function (result) {

                                        if (result.error) {
                                            showToast(result.message, "warning");
                                        }
                                        else {
                                            showToast(result.message, "success");
                                        }

                                        window.setInterval('refresh()', 3000);
                                    })

                                },
                                dataSource: new DevExpress.data.DataSource({
                                    store: new DevExpress.data.ArrayStore({
                                        data: options.data.preAlterDetails
                                    }),
                                })
                            }).appendTo(container);

                    }
                },


                onEditorPreparing: function (e) {
                    // hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {
                    console.log(e);
                    var data = e.data;
                    $.post('/Import/PreAlert/UpdateAlert', { data, data }, function (result) {

                        //showToast(data.message, "success");


                        if (result.error) {
                            showToast(result.message, "warning");
                        }

                        else {

                            showToast(result.message, "success");

                        }

                        window.setInterval('refresh()', 3000);


                    });
                },
                onRowRemoved: function (e) {
                    console.log(e)
                    var key = e.data.preAlertId;

                    console.log("key", key);

                    $.post('/Import/PreAlert/DeletePreAlert?key=' + key, function (result) {

                        if (result.error) {
                            showToast(result.message, "warning");
                        }

                        else {

                            showToast(result.message, "success");

                        }

                        window.setInterval('refresh()', 3000);

                    })

                }

            });

        });


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

    function formfiled() {

    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {



        checkColumn(e, "preAlertPayOrderNumber");

    checkColumn(e, "requestNumber");
    checkColumn(e, "payOrderCreatedDate");

    }

    function ShowDetail() {


        var fromdate = document.getElementById("single_cal2").value;
    var todate = document.getElementById("single_cal3").value;

    GetPreAlertByDate(fromdate, todate);
    }



