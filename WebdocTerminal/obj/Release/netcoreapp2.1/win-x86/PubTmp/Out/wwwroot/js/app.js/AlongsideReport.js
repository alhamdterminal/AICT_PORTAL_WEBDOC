

    var virNumber;
    var virNoSouce = [];
    var type = "";
    var IGM = "";
    var indexNo = "";
    var Vessel = "";
    var Voyage = "";
    var Consignee = "";
    var typePerameter = "";


    function showFilters() {

        igm = "";
    type = $("input[name='type']:checked").val();


    $.get('/APICalls/GetFilters?Type=' + type, function (partial) {

        $('#filters').html(partial);

        })


    if (type == "CFS") {

        getGDIO();



        }
    if (type == "CY") {

        getGDI();
        }




    }
    function containerCallback() { }
    function containerChangeCallback() { }


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
        if (type) {

            if (igm) {
        IGM = igm

    }

    if (type == "CFS") {

        indexNo = $("#indexdb option:selected").text()
                if ($('#vessel').val()) {
        Vessel = $('#vessel').val()
                    Voyage = $("#searchBox").dxSelectBox('instance').option('text')

                }
            }
    if (type == "CY") {
        indexNo = $("#containerIndexCYdb option:selected").text()
                if ($('#vesel').val()) {
        Vessel = $('#vesel').val()

                    Voyage = $("#voyageBox").dxSelectBox('instance').option('value')
                }
            }



    if ($("#consignee").dxSelectBox('instance').option('value')) {
        Consignee = $("#consignee").dxSelectBox('instance').option('value').text

    }
        }

    var clearingAgent = document.getElementById("clearingAgent").value;
    var shippingAgent = document.getElementById("shippingAgent").value;
    var cargoType = document.getElementById("cargoType").value;

    console.log("type", type)
    console.log("cargoType", cargoType)
    loadingBar();

    $.get('/Import/Reports/ViewAlongsideReport?type=' + typePerameter + '&' + 'cargoType=' + cargoType + '&' + 'igm=' + IGM
    + '&' + 'index=' + indexNo + '&' + 'vessel=' + Vessel
    + '&' + 'voyage=' + Voyage + '&' + 'consignee=' + Consignee + '&' + 'shippingAgent=' + shippingAgent
    + '&' + 'clearingAgent=' + clearingAgent, function (data) {

        loadingBarHide();


    $('#repotdata').html(data);
            });

    IGM = "";
    Consignee = "";
    Vessel = "";
    Voyage = "";

    }

    function getGDIO() {
        typePerameter = 2
        $.get('/APICalls/GetGDIO', function (data) {

            var GDIO = data;
    virNo = "";
    $("#consignee").dxSelectBox({
        dataSource: GDIO,
    displayExpr: "text",
    searchEnabled: true,


            }).dxSelectBox("instance");
        })

    }
    function getGDI() {
        typePerameter = 3
        $.get('/APICalls/GetGDI', function (data) {

            var GDIO = data;

    $("#consignee").dxSelectBox({
        dataSource: GDIO,
    displayExpr: "text",
    searchEnabled: true,


            }).dxSelectBox("instance");
        })

    }



    function formfiled() {
        if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {
        document.getElementById('CFS').disabled = true;
    document.getElementById('CY').disabled = true;

        } if (PermissionInputs.find(x => x.fieldName == "Consignee" && x.isChecked == false)) {
        document.getElementById('consignee').style.display = "none";

        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {
        document.getElementById('shippingAgent').disabled = true;

        } if (PermissionInputs.find(x => x.fieldName == "ClearingAgent" && x.isChecked == false)) {
        document.getElementById('clearingAgent').disabled = true;

        }


    }