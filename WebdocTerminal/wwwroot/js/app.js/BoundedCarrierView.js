




    var BoundedCarrierData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getBoundedCarrier() {
        $.get('/Import/Setup/GetBoundedCarrier', function (data) {
            BoundedCarrierData = data;
            console.log(BoundedCarrierData);
            BoundedCarriergrid();
        });
    }



    function BoundedCarriergrid() {

        $("#gridContainer").dxDataGrid({
            dataSource: BoundedCarrierData,
            keyExpr: "boundedTranspoterId",
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
                allowUpdating: false,
                allowAdding: true
            },
            columns: [
                {
                    dataField: "boundedNTN",
                    validationRules: [{ type: "required" }],

                    caption: "Bounded NTN"
                },
                {
                    dataField: "boundedCarrierName",
                    validationRules: [{ type: "required" }],

                    caption: "Carrier Name"
                },
            ],

            onRowInserted: function (e) {

                console.log(e.data);
                var model = e.data;
                console.log("model", model);
                console.log("model.boundedNTN.lenght", model.boundedNTN.length);



                if (model.boundedNTN.length < 7) {
                    getBoundedCarrier();
                    return showToast("bounded NTN lenght < 7", "error");

                }
                else {

                    $.post('/Import/Setup/AddtBoundedCarrier', { model, model }, function (data) {
                        showToast("Bank Created", "success");
                        getBoundedCarrier();
                    });
                }




            },


        });
    }



    function formfiled() {
        getBoundedCarrier();

    }
