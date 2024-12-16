


    $(function () {
        GetServicesTypes();
    })

    var servicetypeData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function GetServicesTypes() {
        $.get('/Import/ServicesInformation/GetServicesType', function (data) {
            servicetypeData = data;
            console.log(servicetypeData);
            servicetypegrid(servicetypeData);
        });
    }


    function servicetypegrid(servicetypeData) {


        $("#gridContainer").dxDataGrid({
            dataSource: servicetypeData,
            keyExpr: "servicesTypeId",
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
                    dataField: "servicesTypeCode",
                    validationRules: [{ type: "required" }],
                    caption: "Code"
                },
                {
                    dataField: "servicesTypeName",
                    validationRules: [{ type: "required" }],
                    caption: "Name"
                },
            ],

            onRowInserted: function (e) {
                console.log(e)
                console.log(e.data)
                var servicesType = e.data;

                $.post('/Import/ServicesInformation/AddServicesType', { servicesType, servicesType }, function (data) {

                    console.log(data);
                    showToast("Created", "success");

                    GetServicesTypes();

                });

            },

            onRowUpdated: function (e) {
                console.log(e);
                var servicesType = e.data;
                $.post('/Import/ServicesInformation/updateServicesType', { servicesType, servicesType }, function (data) {

                    console.log(data)
                    showToast("Updated", "success");
                    GetServicesTypes();

                });
            }

        });
    }



    function formfiled() {

    }
