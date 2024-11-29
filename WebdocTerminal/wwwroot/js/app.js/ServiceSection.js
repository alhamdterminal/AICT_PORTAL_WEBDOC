


    $(function () {
        Getservicesections();
    })

    var servicesectionData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function Getservicesections() {
        $.get('/Import/ServicesInformation/GetServiceSection', function (data) {
            servicesectionData = data;
            console.log(servicesectionData);
            servicesectiongrid(servicesectionData);
        });
    }




    function servicesectiongrid(servicesectionData) {


        $("#gridContainer").dxDataGrid({
            dataSource: servicesectionData,
            keyExpr: "servicesSectionId",
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
                    dataField: "servicesSectionCode",
                    validationRules: [{ type: "required" }],
                    caption: "Code"
                }
            ],

            onRowInserted: function (e) {
                console.log(e)
                console.log(e.data)
                var servicesSection = e.data;

                $.post('/Import/ServicesInformation/AddServiceSection', { servicesSection, servicesSection }, function (data) {

                    console.log(data);
                    showToast("Created", "success");

                    Getservicesections();

                });

            },

            onRowUpdated: function (e) {
                console.log(e);
                var servicesSection = e.data;
                $.post('/Import/ServicesInformation/UpdateServicesSection', { servicesSection, servicesSection }, function (data) {

                    console.log(data)
                    showToast("Updated", "success");
                    Getservicesections();

                });
            }

        });
    }



    function formfiled() {

    }
