


    $(function () {
        getISOCodeHeadData();
    });

    var ISOCodeHeadsData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getISOCodeHeadData() {
        $.get('/Import/Setup/GetISOCodeHeads', function (data) {
            ISOCodeHeadsData = data;
            console.log("ISOCodeHeadsData", ISOCodeHeadsData);
            ISOCodeHeadgrid();
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
    function ISOCodeHeadgrid() {


        $("#gridContainer").dxDataGrid({
            dataSource: ISOCodeHeadsData,
            keyExpr: "isoCodeHeadId",
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
                    dataField: "isoCodeHeadDescription",
                    validationRules: [{ type: "required" }],
                    caption: "Name"
                }
            ],


            onRowInserted: function (e) {

                console.log(e)
                console.log(e.data)
                var data = e.data;

                $.post('/Import/Setup/AddISOCodeHead', { data, data }, function (result) {

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
                $.post('/Import/Setup/UpdateISOCodeHeads', { data, data }, function (result) {
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
