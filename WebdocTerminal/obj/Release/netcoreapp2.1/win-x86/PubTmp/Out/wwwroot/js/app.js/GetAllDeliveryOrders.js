
    $(function () {
        //$("#gridContainer").dxDataGrid({
        //    dataSource: deliveryOders,
        //    showBorders: true,
        //    columns: [{
        //        dataField: "Prefix",
        //        caption: "Title",
        //        width: 70
        //    },
        //        "consingee",
        //        "consigneeNTN",
        //        "status",
        //    {
        //        dataField: "date",
        //        dataType: "date"
        //    }
        //    ]
        //});

        $.get('/APICalls/GetAllDeliveryOrders', function (data) {
            var x = data;
            console.log(x);
            console.log(data);
            $("#gridContainer").dxDataGrid({
                dataSource: data,
                showBorders: true,
                columns: [
                    "consingee",
                    "consigneeNTN",
                    "status",
                    {
                        dataField: "date",
                        dataType: "date"
                    }, {
                        dataField: "deliveryOrderId",
                        width: 100,
                        cellTemplate: function (x, y) {
                            $("<div />").dxButton({
                                icon: 'arrowright',
                                onClick: function (e) {
                                    window.location.assign('/import/Delivery/OrderDetail?deliveryOrderId=' + y.text);
                                    console.log(y.text)
                                }
                            }).appendTo(x);
                        }
                    }
                ]
            });


        });

    });
