


    $(function () {
        getBidderData();
    });

    var BidderData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getBidderData() {
        $.get('/Import/Setup/GetBidders', function (data) {
            BidderData = data;
            console.log("BidderData", BidderData);
            Biddergrid();
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
    function Biddergrid() {


        console.log("BidderData", BidderData)

        $("#gridContainer").dxDataGrid({
        dataSource: BidderData,
    keyExpr: "bidderId",
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
        dataField: "bidderName",
    validationRules: [{type: "required" }],
    caption: "Name"
                },
    {
        dataField: "bidderNic",
    validationRules: [{type: "required" }],
    caption: "CNIC"
                } ,
    ],


    onRowInserted: function (e) {

        console.log(e)
                 console.log(e.data)
    var data = e.data;

    $.post('/Import/Setup/AddBidder', {data, data}, function (result) {

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
    $.post('/Import/Setup/UpdateBidder', {data, data}, function (result) {
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
