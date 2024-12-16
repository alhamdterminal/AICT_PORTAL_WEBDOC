

    $(function () {

        $.get('/APICalls/GetSection79CycontainersCFS', function (data) {
            console.log("responce", data);
            containers = data;
            dataGrid(containers);
        });
    });



    function dataGrid(containers) {
        var dataGrid = $("#CargoInformation").dxDataGrid({
        dataSource: containers,
    keyExpr: "cotnainerId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },
    columns: [
    {
        dataField: "agentName",
    caption: "Agent Name"
                },
    {
        dataField: "indexNo",
    caption: "Index No"
                },
    {
        dataField: "igm",
    caption: "IGM"
                },
    {
        dataField: "blNo",
    caption: "BL No"
                },
    {
        dataField: "size",
    caption: "Size"
                },
    {
        dataField: "manifestedWeight",
    caption: "M.WT"
                },
    {
        dataField: "manifestedQTY",
    caption: "M.QTY"
                }
    ]

        }).dxDataGrid("instance");

    }
