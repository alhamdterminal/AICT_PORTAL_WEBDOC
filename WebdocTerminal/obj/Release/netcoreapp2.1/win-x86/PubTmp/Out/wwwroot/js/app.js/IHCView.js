


    $(function () {
        getIHCData();
    });

    var IHCData = [];

    var HandlingCode = [
    {Name: "OC" },
    {Name: "DD" },
    {Name: "IB" },
    {Name: "RF" },
    {Name: "BR" }

    ];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getIHCData() {
        $.get('/Import/Setup/GetIHCs', function (data) {
            IHCData = data;
            console.log("IHCData", IHCData);
            IHCgrid();
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
    function IHCgrid() {


        $("#gridContainer").dxDataGrid({
            dataSource: IHCData,
            keyExpr: "ihcId",
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
                    dataField: "virNumber",
                    validationRules: [{ type: "required" }],
                    caption: "IGM"
                },
                {
                    dataField: "containerNumber",
                    validationRules: [{ type: "required" }],
                    caption: "Container No"
                },
                {
                    dataField: "handlingCode",
                    validationRules: [{ type: "required" }],
                    caption: "Handling Code",
                    lookup: {
                        dataSource: HandlingCode,
                        valueExpr: "Name",
                        displayExpr: "Name"
                    }
                },
            ],


            onRowInserted: function (e) {

                console.log(e)
                console.log(e.data)
                var data = e.data;

                $.post('/Import/Setup/AddIHC', { data, data }, function (result) {

                    console.log("result", result)
                    if (result.error) {
                        showToast(result.message, "warning");
                    }

                    else {

                        showToast(result.message, "success");

                    }

                    window.setInterval('refresh()', 3000);
                });



            },

            onRowUpdated: function (e) {
                console.log(e);
                var data = e.data;
                $.post('/Import/Setup/UpdateIHC', { data, data }, function (result) {
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
