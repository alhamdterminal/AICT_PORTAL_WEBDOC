﻿


    $(function () {
        getConsigneData();
    });

    var ConsigneData = [];

    var BillDateTypeLCL = [
    {Name: "GateIn" },
    {Name: "Destuffed" },
    {Name: "VesselArrival" }
    ];


    var BillDateTypeFCL = [
    {Name: "GateIn" },
    {Name: "Destuffed" },
    {Name: "VesselArrival" }
    ];


    var BillDateTypeCY = [
    {Name: "GateIn" },
    {Name: "VesselArrival" }
    ];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getConsigneData() {
        $.get('/Import/Setup/GetConsignes', function (data) {
            ConsigneData = data;
            console.log("ConsigneData", ConsigneData);
            Consignegrid();
        });
    }


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function refresh() {
        window.location.reload();
    }
    function Consignegrid() {


        $("#gridContainer").dxDataGrid({
            dataSource: ConsigneData,
            keyExpr: "consigneId",
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,
            paging: {
                pageSize: 10
            },
            searchPanel: {
                visible: true,
                width: 240,
                placeholder: "Search..."
            },

            editing: {
                mode: "row",
                allowUpdating: true,
                allowAdding: true
            },
            columns: [
                {
                    dataField: "consigneName",
                    validationRules: [{ type: "required" }],
                    caption: "Name"
                },
                {
                    dataField: "consigneNTN",
                    validationRules: [{ type: "required" }],
                    caption: "NTN"
                },
                {
                    dataField: "consigneCode",
                    validationRules: [{ type: "required" }],

                    caption: "Code"
                },
                {
                    dataField: "consigneePhoneNumber",
                    validationRules: [{ type: "required" }],
                    caption: "Phone Number"
                },
                {
                    dataField: "consigneeAddress",
                    validationRules: [{ type: "required" }],

                    caption: "Address"
                },
                {
                    dataField: "stRegistrationNo",
                    validationRules: [{ type: "required" }],

                    caption: "STN No"
                },

                {
                    dataField: "billDateTypeLCL",
                    caption: "Storage applicable CFS / LCL",
                    lookup: {
                        dataSource: BillDateTypeLCL,
                        valueExpr: "Name",
                        displayExpr: "Name"
                    }
                },

                {
                    dataField: "billDateTypeFCL",
                    caption: "Storage applicable CFS / FCL ",
                    lookup: {
                        dataSource: BillDateTypeFCL,
                        valueExpr: "Name",
                        displayExpr: "Name"
                    }
                },
                {
                    dataField: "billDateTypeCY",
                    caption: "Storage applicable CY",
                    lookup: {
                        dataSource: BillDateTypeCY,
                        valueExpr: "Name",
                        displayExpr: "Name"
                    }
                },
            ],


            onRowInserted: function (e) {

                console.log(e)
                console.log(e.data)
                var data = e.data;

                $.post('/Import/Setup/AddConsigne', { data, data }, function (result) {

                    console.log("result", result)
                    if (result.error) {
                        showToast(result.message, "warning");
                    }

                    else {

                        showToast(result.message, "success");
                        window.setInterval('refresh()', 3000);


                    }

                });



            },

            onRowUpdated: function (e) {
                console.log(e);
                var data = e.data;
                $.post('/Import/Setup/UpdateConsigne', { data, data }, function (result) {
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



    function formfiled() {


    }