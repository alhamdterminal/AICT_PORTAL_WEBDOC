
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

    var TariffCode = document.getElementById("TariffCode").value;

    var FF = document.getElementById("FF").value;

    var ClearingAgent = document.getElementById("ClearingAgent").value;

    var Commodity = document.getElementById("Commodity").value;

    var Shipper = document.getElementById("Shipper").value;

    var ContainerType = document.getElementById("ContainerType").value;

    var CargoType = document.getElementById("CargoType").value;

    var Port = document.getElementById("Port").value;


    loadingBar();

    $.get('/Import/Reports/ViewAssignedTariffsDetailReport?TariffCode=' + TariffCode + '&FF=' + FF + '&ClearingAgent=' + ClearingAgent + '&Commodity=' + Commodity + '&Shipper=' + Shipper + '&ContainerType=' + ContainerType
    + '&CargoType=' + CargoType + '&Port=' + Port + '&consignee=' + consignee, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }


    function getConsignees() {

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
