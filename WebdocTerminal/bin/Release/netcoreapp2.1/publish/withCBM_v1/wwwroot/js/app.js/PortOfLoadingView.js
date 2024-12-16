




    var PortOfLoadaingData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getPortOfLoadaing() {
        $.get('/Import/Setup/GetPortOfLoading', function (data) {
            PortOfLoadaingData = data;
            console.log(PortOfLoadaingData);
            PortOfLoadainggrid();
        });
    }



    function PortOfLoadainggrid() {

        $("#gridContainer").dxDataGrid({
            dataSource: PortOfLoadaingData,
            keyExpr: "portOfLoadingId",
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
                    dataField: "portOfLoadingName",
                    validationRules: [{ type: "required" }],
                    caption: "Port Of Loading"
                },

            ],

            onRowInserted: function (e) {


                var model = e.data;

                $.post('/Import/Setup/AddtPortOfLoading', { model, model }, function (data) {
                    showToast("Created", "success");
                    getPortOfLoadaing();
                });

            },

            onRowUpdated: function (e) {

                var model = e.data;

                $.post('/Import/Setup/UpdatePortOfLoading', { model, model }, function (data) {
                    showToast("Updated", "success");
                    getPortOfLoadaing();
                });
            }

        });
    }



    function formfiled() {
        getPortOfLoadaing();

    }
