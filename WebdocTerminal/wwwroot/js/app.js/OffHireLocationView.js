


    $(function () {
        getoffhirelocationsData();
    });

    var offhirelocationsData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getoffhirelocationsData() {
        $.get('/Import/Setup/GetOffHireLocations', function (data) {
            offhirelocationsData = data;
            console.log("offhirelocationsData", offhirelocationsData);
            offhirelocationsgrid();
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
    function offhirelocationsgrid() {


        $("#gridContainer").dxDataGrid({
            dataSource: offhirelocationsData,
            keyExpr: "offHireLocationId",
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
                    dataField: "offHireLocationName",
                    validationRules: [{ type: "required" }],
                    caption: "Name"
                }
            ],


            onRowInserted: function (e) {

                console.log(e)
                console.log(e.data)
                var data = e.data;

                $.post('/Import/Setup/AddOffHireLocation', { data, data }, function (result) {

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
                $.post('/Import/Setup/UpdateOffHireLocation', { data, data }, function (result) {
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
