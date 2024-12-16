


    $(function () {
        getGoodsHeadData();

    });

    var GoodData = [];
    var GoodsHeadData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getGoodData() {
        $.get('/Import/Setup/GetGoods', function (data) {
            GoodData = data;
            console.log("GoodData", GoodData);
            Goodsgrid();
        });
    }


    function getGoodsHeadData() {
        $.get('/Import/Setup/GetGoodHeads', function (data) {
            GoodsHeadData = data;
            console.log("GoodsHeadData", GoodsHeadData);

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
    function Goodsgrid() {


        $("#gridContainer").dxDataGrid({
            dataSource: GoodData,
            keyExpr: "goodId",
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
                    dataField: "goodsName",
                    validationRules: [{ type: "required" }],
                    caption: "Name"
                },
                {
                    dataField: "goodsDescription",
                    validationRules: [{ type: "required" }],
                    caption: "Description"
                },
                {
                    dataField: "goodsCode",
                    validationRules: [{ type: "required" }],

                    caption: "Code"
                },
                {
                    dataField: "goodsHeadId",
                    caption: "Goods Head",
                    lookup: {
                        dataSource: GoodsHeadData,
                        displayExpr: "goodsHeadName",
                        valueExpr: "goodsHeadId"
                    },
                    validationRules: [{ type: "required" }]

                }
            ],


            onRowInserted: function (e) {

                console.log(e)
                console.log(e.data)
                var data = e.data;

                $.post('/Import/Setup/AddGood', { data, data }, function (result) {

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
                $.post('/Import/Setup/UpdateGood', { data, data }, function (result) {
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


        getGoodData();
    }
