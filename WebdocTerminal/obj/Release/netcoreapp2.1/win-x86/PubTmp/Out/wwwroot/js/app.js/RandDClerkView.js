


    $(function () {
        getRandDClerkData();
    });

    var RandDClerkData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getRandDClerkData() {
        $.get('/Import/Setup/GetRandDClerks', function (data) {
            RandDClerkData = data;
            console.log("RandDClerkData", RandDClerkData);
            RandDClerkgrid();
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
    function RandDClerkgrid() {


        console.log("RandDClerkData", RandDClerkData)

        $("#gridContainer").dxDataGrid({
        dataSource: RandDClerkData,
    keyExpr: "randDClerkId",
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
        dataField: "randDClerkName",
    validationRules: [{type: "required" }],
    caption: "Name"
                }

    ],


    onRowInserted: function (e) {

        console.log(e)
                var data = e.data;

    $.post('/Import/Setup/AddRandDClerk', {data, data}, function (result) {

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
    $.post('/Import/Setup/UpdateRandDClerk', {data, data}, function (result) {
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
