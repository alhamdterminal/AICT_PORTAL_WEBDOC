




    var consigneeList = [];

    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });
    }
    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }



    function myFunction() {

        var consignee = "";

    if ($("#searchBoxForConsigne").dxSelectBox('instance').option('value')) {
        consignee = $("#searchBoxForConsigne").dxSelectBox('instance').option('value')

    }


    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    var fromdatedelivered = document.getElementById("fromdatedelivered").value;

    var todatedelivered = document.getElementById("todatedelivered").value;

    var cargoType = document.getElementById("cargoType").value;
    var containerType = document.getElementById("containerType").value;
    var shippingAgent = document.getElementById("shippingAgent").value;
    var goodheadid = document.getElementById("goodheadid").value;


    loadingBar();

    $.get('/Import/Reports/ViewIndexDetailReport?shippingAgent=' + shippingAgent + '&Type=' + containerType + '&cargotype=' + cargoType
    + '&consignee=' + consignee + '&good=' + goodheadid + '&fromdate=' + fromdate + '&todate='
    + todate + '&fromdatedelivered=' + fromdatedelivered + '&todatedelivered=' + todatedelivered, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }


    function getConsignees() {


        //$.get('/APICalls/GetConsignees', function (data) {

        //    console.log(data);

        //    $("#consignee").dxSelectBox({
        //        dataSource: data,
        //        displayExpr: "text",
        //        searchEnabled: true,


        //    }).dxSelectBox("instance");
        //})

        $.get('/APICalls/GetALlConsignees', function (res) {
            if (res) {

                consigneeList = res;
                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: consigneeList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ConsigneId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                })

            }
            else {
                consigneeList = []

                $("#searchBoxForConsigne").dxSelectBox({
                    dataSource: {
                        store: consigneeList,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ConsigneId',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,

                });
            }
        });
    }



    function formfiled() {

        getConsignees();

    }