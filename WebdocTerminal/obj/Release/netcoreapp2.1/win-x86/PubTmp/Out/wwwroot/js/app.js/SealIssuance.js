


    $(function () {
        getsealPurchases();
    });

    var purchaseData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getsealPurchases() {
        $.get('/Import/Setup/GetSealPurchases', function (data) {
            purchaseData = data;
            console.log("Sealpurchase", purchaseData);
            sealPurchaseDatagrid();
        });
    }


    function refresh() {
        window.location.reload();
    }

    function sealPurchaseDatagrid() {



        $("#gridContainer").dxDataGrid({
            dataSource: purchaseData,
            keyExpr: "sealPurchaseId",
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
                allowAdding: false,
                allowDeleting: false,
                allowUpdating: true,

            },
            columns: [
                {
                    dataField: "sealPurchaseNo",
                    caption: "Purchase No",
                    allowEditing: false,
                },
                {
                    dataField: "purchaseDate",
                    caption: "Purchase Date",
                    dataType: 'date',
                    format: 'dd/MM/yyyy',
                    allowEditing: false,
                },
                {
                    dataField: "sealFrom",
                    caption: "From",
                    dataType: "number",
                    validationRules: [{ type: "required" }],
                    allowEditing: false,

                },

                {
                    dataField: "sealTo",
                    caption: "To",
                    dataType: "number",
                    validationRules: [{ type: "required" }],
                    allowEditing: false,
                },
                {
                    dataField: "rate",
                    validationRules: [{ type: "required" }],
                    dataType: "number",
                    format: "#,##0.##",
                    allowEditing: false,
                },
                {
                    dataField: "totalSeal",
                    validationRules: [{ type: "required" }],
                    dataType: "number",
                    allowEditing: false,
                },
                {
                    dataField: "remaingSeal",
                    validationRules: [{ type: "required" }],
                    dataType: "number",
                    allowEditing: false,
                },

                {
                    dataField: "assignStatus",

                }

            ],



            onRowUpdated: function (e) {
                console.log(e);
                console.log(e.data);
                $.post('/Import/Setup/UpdateSealPurchase', { data: e.data }, function (result) {
                    console.log("result", result)
                    if (result.error) {
                        showToast(result.message, "warning");
                    }

                    else {

                        showToast(result.message, "success");

                    }

                    window.setInterval('refresh()', 4000);
                });
            },

        });
    }



    function formfiled() {


    }
