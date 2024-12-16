



    var TariffInfodData = [];


    var ShippingAgentList = [];
    var consigneeList = [];
    var ClearingAgentList = [];
    var GoodsHeadList = [];
    var ShipperList = [];
    var ISOCodeHeadList = [];
    var PortAndTerminalList = [];




    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    $(function () {
        gettariffinfo();
    consigneedata();
    shippingagentdata();
    clearningagnetdata();
    GoodHeaddata();
    Shipperdata();
    PortAndTerminalsdata();
    ISOCodeHeadsdata();
         

    });

    function formfiled() {

    }

    function showinfo() {
        tariffinfogrid();
    }



    function shippingagentdata() {
        $.get('/APICalls/GetShippingAgentName', function (res) {


            if (res) {

                res.forEach(c => {
                    let x = {
                        "shippingAgentId": Number(c.value),
                        "name": c.text
                    };
                    ShippingAgentList.push(x);
                });

            }
            else {
                ShippingAgentList = []
            }
        });

    }

    function clearningagnetdata() {
        $.get('/APICalls/GetClearingAgentsImport', function (res) {

            console.log("ClearingAgents list ", res)


            if (res) {

                res.forEach(c => {
                    let x = {
                        "value": Number(c.value),
                        "text": c.text
                    };
                    ClearingAgentList.push(x);
                });

            }


            else {
                ClearingAgentList = []
            }
        });

    }
    function GoodHeaddata() {
        $.get('/APICalls/GetAllGoodHead', function (res) {

            console.log("GoodHead list ", res)

            if (res) {

                res.forEach(c => {
                    let x = {
                        "value": Number(c.value),
                        "text": c.text
                    };
                    GoodsHeadList.push(x);
                });

            }
            else {
                GoodsHeadList = []
            }
        });

    }
    function Shipperdata() {
        $.get('/APICalls/GetAllShippers', function (res) {

            console.log("Shippers list ", res)

            if (res) {

                res.forEach(c => {
                    let x = {
                        "value": Number(c.value),
                        "text": c.text
                    };
                    ShipperList.push(x);
                });

            }
            else {
                ShipperList = []
            }
        });

    }

    function PortAndTerminalsdata() {
        $.get('/APICalls/GetAllPortAndTerminals', function (res) {

            console.log("PortAndTerminal list ", res)

            if (res) {

                res.forEach(c => {
                    let x = {
                        "value": Number(c.value),
                        "text": c.text
                    };
                    PortAndTerminalList.push(x);
                });

            }
            else {
                PortAndTerminalList = []
            }
        });

    }

    function ISOCodeHeadsdata() {
        $.get('/APICalls/GetAllISOCodeHeads', function (res) {

            console.log("ISOCodeHead list ", res)

            if (res) {

                res.forEach(c => {
                    let x = {
                        "value": Number(c.value),
                        "text": c.text
                    };
                    ISOCodeHeadList.push(x);
                });

            }
            else {
                ISOCodeHeadList = []
            }
        });

    }


    function consigneedata() {
        $.get('/APICalls/GetALlConsignees', function (res) {

            console.log("consigne list ", res)

            if (res) {

                res.forEach(c => {
                    let x = {
                        "value": Number(c.value),
                        "text": c.text
                    };
                    consigneeList.push(x);
                });

            }
            else {
                consigneeList = []
            }
        });

    }

    function gettariffinfo() {
        $.get('/Tariff/GetTariffInformation', function (data) {

            console.log("test data", data)

            TariffInfodData = data;


        })
    }


    function refresh() {
        window.location.reload();
    }



    var IndexCargoType = [
    {Name: "FCL" },
    {Name: "LCL" },
    {Name: "CY" }
    ];


    function tariffinfogrid() {

        console.log("ShippingAgentList", ShippingAgentList)

        $("#gridContainer").dxDataGrid({
        dataSource: TariffInfodData,
    keyExpr: "tariffInformationId",
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    paging: {
        pageSize: 180
            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."
            },

    editing: {
        mode: "row",
    allowUpdating: true,
    allowAdding: false
            },
    columns: [
    {
        dataField: "tariffInformationId",
    caption: "Code",
    allowEditing: false,
                },

    {
        dataField: "indexCargoType",
    caption: "Type",
    lookup: {
        dataSource: IndexCargoType,
    displayExpr: "Name",
    valueExpr: "Name"
                    },
    validationRules: [{type: "required" }]
                },
    {
        dataField: "goodsHeadId",
    caption: "Good",
    width: 250,
    lookup: {
        dataSource: {
        store: GoodsHeadList,
    requireTotalCount: true,
    pageSize: 4,
    paginate: true,
                        },
    displayExpr: "text",
    valueExpr: "value"
                    }
                },
    {
        dataField: "shippingAgentId",
    caption: "ShippingAgent",
    lookup: {
        dataSource: {
        store: ShippingAgentList,
    requireTotalCount: true,
    pageSize: 4,
    paginate: true,
                        },
    displayExpr: "name",
    valueExpr: "shippingAgentId"
                    }
                },

    {
        dataField: "consigneId",
    caption: "Consignee",
    width: 250,
    lookup: {
        dataSource: {
        store: consigneeList,
    requireTotalCount: true,
    pageSize: 4,
    paginate: true,
                        },
    displayExpr: "text",
    valueExpr: "value"
                    }
                },
    {
        dataField: "clearingAgentId",
    caption: "ClearingAgent",
    width: 250,
    lookup: {
        dataSource: {
        store: ClearingAgentList,
    requireTotalCount: true,
    pageSize: 4,
    paginate: true,
                        },
    displayExpr: "text",
    valueExpr: "value"
                    }
                },
    {
        dataField: "isoCodeHeadId",
    caption: "Container Type",
    width: 250,
    lookup: {
        dataSource: {
        store: ISOCodeHeadList,
    requireTotalCount: true,
    pageSize: 4,
    paginate: true,
                        },
    displayExpr: "text",
    valueExpr: "value"
                    }
                },
    {
        dataField: "portAndTerminalId",
    caption: "PortAndTerminal",
    width: 250,
    lookup: {
        dataSource: {
        store: PortAndTerminalList,
    requireTotalCount: true,
    pageSize: 4,
    paginate: true,
                        },
    displayExpr: "text",
    valueExpr: "value"
                    }
                },
    {
        dataField: "shipperId",
    caption: "Shipper",
    width: 250,
    lookup: {
        dataSource: {
        store: ShipperList,
    requireTotalCount: true,
    pageSize: 4,
    paginate: true,
                        },
    displayExpr: "text",
    valueExpr: "value"
                    }
                },
    ],



    onRowUpdated: function (e) {
        console.log(e);
    var model = e.data;
    $.post('/Tariff/UpdateTariffContractData', {model, model}, function (result) {
        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");
                    }

    else {

        showToast(result.message, "success");

                    }

    window.setInterval('refresh()', 3000);
                });
            }
        });
    }





