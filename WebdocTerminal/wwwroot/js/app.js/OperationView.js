


    $(function () {
        Getoperationdprt();
    })

    var operationdprtData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function Getoperationdprt() {
        $.get('/Import/ServicesInformation/GetOperationDprt', function (data) {
            operationdprtData = data;
            console.log(operationdprtData);
            operationdprtgrid(operationdprtData);
        });
    }




    function operationdprtgrid(operationdprtData) {


        $("#gridContainer").dxDataGrid({
            dataSource: operationdprtData,
            keyExpr: "operationDprtId",
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
                    dataField: "operationDprtName",
                    validationRules: [{ type: "required" }],
                    caption: "OPs Name"
                },
                {
                    dataField: "operationDprtCode",
                    validationRules: [{ type: "required" }],
                    caption: "OPs Code"
                }
            ],

            onRowInserted: function (e) {
                console.log(e)
                console.log(e.data)
                var operationDprt = e.data;

                $.post('/Import/ServicesInformation/AddOperationDprt', { operationDprt, operationDprt }, function (data) {

                    console.log(data);
                    showToast("Created", "success");

                    GetnatureOfPayments();

                });

            },

            onRowUpdated: function (e) {
                console.log(e);
                var operationDprt = e.data;
                $.post('/Import/ServicesInformation/UpdateOperationDprt', { operationDprt, operationDprt }, function (data) {

                    console.log(data)
                    showToast("Updated", "success");
                    Getoperationdprt();

                });
            }

        });
    }



    function formfiled() {

    }
