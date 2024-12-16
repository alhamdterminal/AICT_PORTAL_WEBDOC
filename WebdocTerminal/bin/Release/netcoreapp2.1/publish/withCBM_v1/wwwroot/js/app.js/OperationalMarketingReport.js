




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

    var type = document.getElementById("type").value;

    var status = document.getElementById("status").value;

    var clearingagent = document.getElementById("clearingagent").value;


    loadingBar();

    $.get('/Import/Reports/ViewOperationalMarketingReport?clearingagent=' + clearingagent + '&type=' + type + '&consignee=' + consignee + '&fromdate=' + fromdate + '&todate='
    + todate + '&status=' + status, function (data) {

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
