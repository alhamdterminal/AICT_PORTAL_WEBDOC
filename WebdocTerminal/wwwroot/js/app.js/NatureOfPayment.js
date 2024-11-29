


    $(function () {
        GetnatureOfPayments();
    })

    var natureOfPaymentData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function GetnatureOfPayments() {
        $.get('/Import/ServicesInformation/GetNatureOfPayment', function (data) {
            natureOfPaymentData = data;
            console.log(natureOfPaymentData);
            natureOfPaymentgrid(natureOfPaymentData);
        });
    }




    function natureOfPaymentgrid(natureOfPaymentData) {


        $("#gridContainer").dxDataGrid({
            dataSource: natureOfPaymentData,
            keyExpr: "natureOfPaymentId",
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
                    dataField: "natureOfPaymentCode",
                    validationRules: [{ type: "required" }],
                    caption: "Code"
                },
                {
                    dataField: "natureOfPaymentName",
                    validationRules: [{ type: "required" }],
                    caption: "Nature Of Payment"
                }
            ],

            onRowInserted: function (e) {
                console.log(e)
                console.log(e.data)
                var natureOfPayment = e.data;

                $.post('/Import/ServicesInformation/AddNatureOfPayment', { natureOfPayment, natureOfPayment }, function (data) {

                    console.log(data);
                    showToast("Created", "success");

                    GetnatureOfPayments();

                });

            },

            onRowUpdated: function (e) {
                console.log(e);
                var natureOfPayment = e.data;
                $.post('/Import/ServicesInformation/UpdateNatureOfPayment', { natureOfPayment, natureOfPayment }, function (data) {

                    console.log(data)
                    showToast("Updated", "success");
                    GetnatureOfPayments();

                });
            }

        });
    }



    function formfiled() {

    }
