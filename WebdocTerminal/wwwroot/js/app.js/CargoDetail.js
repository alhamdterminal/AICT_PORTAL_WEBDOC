



    function containerChangeCallback() {
        console.log("containerChangeCallback", containerChangeCallback)
    }


    function formfiled() {

    }



    function showdetail() {

        var containerNumber = $("#containerdb option:selected").text();

    console.log("igm", igm);

    console.log("containerNumber", containerNumber);



    var containerno = $("#containerlist").dxAutocomplete("instance").option("value");




    console.log("containerno", containerno);

    $.get('/APICalls/GetCargoDetailCFSAndCYConatiner?igm=' + igm + '&containerNumber=' + containerNumber, function (data) {

        console.log("data", data)

            containers = data;
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
    editing: {
        mode: "form",
    allowUpdating: true
                },
    columns: [

    {
        dataField: "agentName",
    caption: "Shipping Agent",
    allowEditing: false

                    },
    {
        dataField: "virNo",
    caption: "Vir No",
    allowEditing: false

                    },
    {
        dataField: "containerNo",
    caption: "Container No",
    allowEditing: false

                    },
    {
        dataField: "blNo",
    caption: "BL Number",
    allowEditing: false

                    },
    {
        dataField: "indexNo",
    caption: "Index No",
    allowEditing: false

                    },
    {
        dataField: "status",
    caption: "Status",
    allowEditing: false

                    },

    {
        dataField: "foundSealNumber",
    caption: "FoundSealNumber",
    visible: false,
    allowEditing: false

                    },
    {
        dataField: "size",
    allowEditing: false,
    visible: false,
    caption: "Size"
                    },
    {
        dataField: "manifestedWeight",
    allowEditing: false,
    visible: false,
    caption: "M.WT"
                    },
    {
        dataField: "foundWeight",
    allowEditing: false,
    visible: false,
    caption: "F.WT"
                    },
    {
        dataField: "manifestedQTY",
    allowEditing: false,
    visible: false,
    caption: "M.QTY"
                    },
    {
        dataField: "manifestedUOP",
    allowEditing: false,
    visible: false,
    caption: "M.UOP"
                    },
    {
        dataField: "cbm",
    allowEditing: false,
    visible: false,
    caption: "CBM"
                    },
    {
        dataField: "foundQTY",
    allowEditing: false,
    visible: false,
    caption: "F.QTY"
                    },
    {
        dataField: "foundUOP",
    allowEditing: false,
    visible: false,
    caption: "F.UOP"
                    },
    {
        dataField: "description",
    allowEditing: false,
    visible: false,
    caption: "Description"
                    },
    {
        dataField: "marksAndNumber",
    allowEditing: false,
    visible: false,
    caption: "Marks & Number"
                    },
    {
        dataField: "location",
    caption: "Location"
                    },
    {
        dataField: "gateInDate",
    caption: "Gate IN",
    allowEditing: false,
    visible: false,
    dataType: 'date',
    format: 'dd/MM/yyyy'
                    },
    {
        dataField: "destuffDate",
    caption: "Destuff",
    allowEditing: false,
    visible: false,
    dataType: 'date',
    format: 'dd/MM/yyyy'
                    },
    {
        dataField: "destuffedRemark",
    allowEditing: false,
    visible: false,
    caption: "Destuffed Remark"
                    },
    {
        dataField: "invoiceNo",
    allowEditing: false,
    visible: false,
    caption: "Invoice No"
                    },
    {
        dataField: "invoiceDate",
    caption: "Invoice Date",
    allowEditing: false,
    visible: false,
    dataType: 'date',
    width: 170,
    format: 'dd/MM/yyyy'
                    },
    {
        dataField: "doNo",
    allowEditing: false,
    visible: false,
    width: 170,
    caption: "Do No"
                    },
    {
        dataField: "doDate",
    caption: "Do Date",
    allowEditing: false,
    visible: false,
    width: 170,
    dataType: 'date',
    format: 'dd/MM/yyyy'
                    },
    {
        dataField: "gatePassNo",
    allowEditing: false,
    visible: false,
    width: 170,
    caption: "GatePass No"
                    },
    {
        dataField: "gatePassDate",
    caption: "GatePass Date",
    allowEditing: false,
    width: 100,
    visible: false,
    dataType: 'date',
    format: 'dd/MM/yyyy'
                    }

    ],

    onRowUpdated: function (e) {

                    if (e.data.status == "CFS") {

        console.log("CFS", e);

    var containerId = e.data.cotnainerId;
    var location = e.data.location;

    $.post('/Import/Brt/UpdateCFSBrt?containerId=' + containerId + '&location=' + location, function (result) {

        console.log("result", result)
                            if (result.error) {
        showToast(result.message, "warning");
                            }

    else {

        showToast(result.message, "success");

                            }
                        });



                    }

    if (e.data.status == "CY") {
        console.log("CY", e);

    var containerId = e.data.cotnainerId;
    var location = e.data.location;

    $.post('/Import/Brt/UpdateCYBrt?containerId=' + containerId + '&location=' + location, function (result) {

        console.log("result", result)
                            if (result.error) {
        showToast(result.message, "warning");
                            }

    else {

        showToast(result.message, "success");

                            }


                        });


                    }

                 
                }
            }).dxDataGrid("instance");

        })

    }


    function showdetailSpecific() {

        var containerno = $("#containerlist").dxAutocomplete("instance").option("value");




    console.log("containerno", containerno);

    $.get('/APICalls/GetCargoDetailCFSAndCYConatiner?igm=' + "" + '&containerNumber=' + containerno, function (data) {

        console.log("data", data)

            containers = data;
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
    editing: {
        mode: "form",
    allowUpdating: true
                },
    columns: [

    {
        dataField: "agentName",
    caption: "Shipping Agent",
    allowEditing: false

                    },
    {
        dataField: "virNo",
    caption: "Vir No",
    allowEditing: false

                    },
    {
        dataField: "containerNo",
    caption: "Container No",
    allowEditing: false

                    },
    {
        dataField: "blNo",
    caption: "BL Number",
    allowEditing: false

                    },
    {
        dataField: "indexNo",
    caption: "Index No",
    allowEditing: false

                    },
    {
        dataField: "status",
    caption: "Status",
    allowEditing: false

                    },

    {
        dataField: "foundSealNumber",
    caption: "FoundSealNumber",
    visible: false,
    allowEditing: false

                    },
    {
        dataField: "size",
    allowEditing: false,
    visible: false,
    caption: "Size"
                    },
    {
        dataField: "manifestedWeight",
    allowEditing: false,
    visible: false,
    caption: "M.WT"
                    },
    {
        dataField: "foundWeight",
    allowEditing: false,
    visible: false,
    caption: "F.WT"
                    },
    {
        dataField: "manifestedQTY",
    allowEditing: false,
    visible: false,
    caption: "M.QTY"
                    },
    {
        dataField: "manifestedUOP",
    allowEditing: false,
    visible: false,
    caption: "M.UOP"
                    },
    {
        dataField: "cbm",
    allowEditing: false,
    visible: false,
    caption: "CBM"
                    },
    {
        dataField: "foundQTY",
    allowEditing: false,
    visible: false,
    caption: "F.QTY"
                    },
    {
        dataField: "foundUOP",
    allowEditing: false,
    visible: false,
    caption: "F.UOP"
                    },
    {
        dataField: "description",
    allowEditing: false,
    visible: false,
    caption: "Description"
                    },
    {
        dataField: "marksAndNumber",
    allowEditing: false,
    visible: false,
    caption: "Marks & Number"
                    },
    {
        dataField: "location",
    caption: "Location"
                    },
    {
        dataField: "gateInDate",
    caption: "Gate IN",
    allowEditing: false,
    visible: false,
    dataType: 'date',
    format: 'dd/MM/yyyy'
                    },
    {
        dataField: "destuffDate",
    caption: "Destuff",
    allowEditing: false,
    visible: false,
    dataType: 'date',
    format: 'dd/MM/yyyy'
                    },
    {
        dataField: "destuffedRemark",
    allowEditing: false,
    visible: false,
    caption: "Destuffed Remark"
                    },
    {
        dataField: "invoiceNo",
    allowEditing: false,
    visible: false,
    caption: "Invoice No"
                    },
    {
        dataField: "invoiceDate",
    caption: "Invoice Date",
    allowEditing: false,
    visible: false,
    dataType: 'date',
    width: 170,
    format: 'dd/MM/yyyy'
                    },
    {
        dataField: "doNo",
    allowEditing: false,
    visible: false,
    width: 170,
    caption: "Do No"
                    },
    {
        dataField: "doDate",
    caption: "Do Date",
    allowEditing: false,
    visible: false,
    width: 170,
    dataType: 'date',
    format: 'dd/MM/yyyy'
                    },
    {
        dataField: "gatePassNo",
    allowEditing: false,
    visible: false,
    width: 170,
    caption: "GatePass No"
                    },
    {
        dataField: "gatePassDate",
    caption: "GatePass Date",
    allowEditing: false,
    width: 100,
    visible: false,
    dataType: 'date',
    format: 'dd/MM/yyyy'
                    }

    ],

    onRowUpdated: function (e) {

                    if (e.data.status == "CFS") {

        console.log("CFS", e);

    var containerId = e.data.cotnainerId;
    var location = e.data.location;

    $.post('/Import/Brt/UpdateCFSBrt?containerId=' + containerId + '&location=' + location, function (result) {

        console.log("result", result)
                            if (result.error) {
        showToast(result.message, "warning");
                            }

    else {

        showToast(result.message, "success");

                            }
                        });



                    }

    if (e.data.status == "CY") {
        console.log("CY", e);

    var containerId = e.data.cotnainerId;
    var location = e.data.location;

    $.post('/Import/Brt/UpdateCYBrt?containerId=' + containerId + '&location=' + location, function (result) {

        console.log("result", result)
                            if (result.error) {
        showToast(result.message, "warning");
                            }

    else {

        showToast(result.message, "success");

                            }


                        });


                    }


                }
            }).dxDataGrid("instance");

        })
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }
