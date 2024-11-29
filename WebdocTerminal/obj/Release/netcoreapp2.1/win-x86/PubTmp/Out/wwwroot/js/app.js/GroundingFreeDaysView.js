

    var consigneeList = [];

    var shippingagentdataList = [];
    var consigneedataList = [];
    var clearningagentdataList = [];
    var goodheaddataList = [];
    $(function () {
        $('#groundingFreeDayId').val('');


    getconsigneelist();

        
    });

    var Type = [
    {Name: "CY" },
    {Name: "LCL" },
    {Name: "FCL" }
    ];


    function getconsigneelist() {
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
                })

            }
        });

    }

    function updateInfo() {

        var consid = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");

    var shippingAgent = $('#shippingAgents').val();
    var clearingAgent = $('#clearingAgent').val();
    var goodsHeadId = $('#goodsHeadId').val();
    var indexCargoTypeid = $('#indexCargoType').val();
    var groundingFreeDays = $('#groundingFreeDays').val();
    var storageFreeFreeDays = $('#storageFreeFreeDays').val();

    console.log("shippingAgent", shippingAgent);

    console.log("consid", consid);

    console.log("groundingFreeDays", groundingFreeDays);

    if (shippingAgent || consid || clearingAgent || goodsHeadId || indexCargoTypeid ) {

            var data = {
        shippingAgentId: shippingAgent,
    consigneId: consid,
    groundingFreeDays: groundingFreeDays,
    storageFreeFreeDays: storageFreeFreeDays,
    clearingAgentId : clearingAgent,
    goodsHeadId: goodsHeadId,
    cargoType : indexCargoTypeid
            }
    console.log("model", data);

    $.post('/Import/Setup/AddGroundingFreeDays', {data: data }, function (data) {

                if (data.error == true) {
        showToast(data.message, "error");
    getgroundingfreedayslist();
                }
    else {
        showToast(data.message, "success");
    getgroundingfreedayslist();
                }
            });

        } else {
        showToast("please select Atleast one Parameter", "error")
    }

    }

    function formfiled() {
        getgroundingfreedayslist();
    }


    function getgroundingfreedayslist() {


        $.get('/ShippingAgent/Get', function (ShippingAgentres) {

            shippingagentdataList = ShippingAgentres;

            $.get('/ClearingAgent/Get', function (ClearingAgentres) {

                clearningagentdataList = ClearingAgentres;


                $.get('/Import/Setup/GetGoodHeads', function (GoodHeadsres) {

                    goodheaddataList = GoodHeadsres;

                    $.get('/Import/Setup/GetConsignes', function (Consignesres) {

                        consigneedataList = Consignesres;



                        $.get('/Import/Setup/GetGroundingFreeDays', function (res) {


                            var dataGrid = $("#gridlist").dxDataGrid({
                                dataSource: res,
                                keyExpr: "groundingFreeDayId",
                                wordWrapEnabled: true,
                                showBorders: true,
                                allowColumnResizing: true,
                                columnAutoWidth: true,
                                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                                paging: {
                                    enabled: true
                                },
                                editing: {
                                    mode: "popup",
                                    allowDeleting: true,
                                    allowUpdating: false,
                                },
                                searchPanel: {
                                    visible: true,
                                    width: 240,
                                    placeholder: "Search..."

                                },
                                selection: {
                                    mode: 'single',
                                },
                                hoverStateEnabled: true,
                                columns: [
                                    {

                                        width: 300,
                                        dataField: "shippingAgentId",
                                        caption: "Agent",
                                        lookup: {
                                            dataSource: shippingagentdataList,
                                            valueExpr: "shippingAgentId",
                                            displayExpr: "name"
                                        }


                                    },
                                    {

                                        width: 300,
                                        dataField: "consigneId",
                                        caption: "Consigne",
                                        lookup: {
                                            dataSource: consigneedataList,
                                            valueExpr: "consigneId",
                                            displayExpr: "consigneName",
                                            requireTotalCount: true,
                                            pageSize: 4,
                                            paginate: true,

                                        },
                                        acceptCustomValue: true,
                                        searchEnabled: true,

                                    },
                                    {

                                        width: 300,
                                        dataField: "clearingAgentId",
                                        caption: "Clearing Agent",
                                        lookup: {
                                            dataSource: clearningagentdataList,
                                            valueExpr: "clearingAgentId",
                                            displayExpr: "name"
                                        }
                                    },
                                    {

                                        width: 300,
                                        dataField: "goodsHeadId",
                                        caption: "Goods",
                                        lookup: {
                                            dataSource: goodheaddataList,
                                            valueExpr: "goodsHeadId",
                                            displayExpr: "goodsHeadName"
                                        }


                                    },


                                    {
                                        dataField: "cargoType",
                                        caption: "Type",
                                        width: 150,
                                        validationRules: [{ type: "required" }],
                                        lookup: {
                                            dataSource: Type,
                                            valueExpr: "Name",
                                            displayExpr: "Name"
                                        }

                                    },
                                    {
                                        dataField: "storageFreeFreeDays",
                                        caption: "Storage Free Days",

                                    },
                                    {
                                        dataField: "groundingFreeDays",
                                        caption: "Grounding Free Days",

                                    },

                                ],


                                onSelectionChanged(selectedItems) {

                                    console.log("selectedItems", selectedItems);

                                    const data = selectedItems.selectedRowsData[0];

                                    console.log("data", data);
                                    if (data) {

                                        console.log("if");
                                        document.getElementById('btnSubmit').style.display = "none"
                                        document.getElementById('btnSubmitupdate').style.display = "block"

                                        $('#shippingAgents').select2().val(data.shippingAgentId).trigger('change.select2');
                                        $('#clearingAgent').select2().val(data.clearingAgentId).trigger('change.select2');
                                        $('#goodsHeadId').select2().val(data.goodsHeadId).trigger('change.select2');
                                        $('#indexCargoType').val(data.cargoType);
                                        $('#groundingFreeDayId').val(data.groundingFreeDayId);
                                        $('#storageFreeFreeDays').val(data.storageFreeFreeDays);
                                        $('#groundingFreeDays').val(data.groundingFreeDays);


                                        if (data.consigneId) {

                                            var ab = $("#searchBoxForConsigne").dxSelectBox('instance').option().dataSource.store.find(x => x.value == data.consigneId)

                                            console.log("AB", ab);
                                            $("#searchBoxForConsigne").dxSelectBox('instance').option("value", ab.value);
                                            $("#searchBoxForConsigne").dxSelectBox('instance').option("text", ab.text);


                                        }
                                        else {
                                            $("#searchBoxForConsigne").dxSelectBox('instance').option("value", '');
                                            $("#searchBoxForConsigne").dxSelectBox('instance').option("text", '');


                                        }


                                    }

                                    else {
                                        console.log("else");
                                        document.getElementById('btnSubmit').style.display = "block"
                                        document.getElementById('btnSubmitupdate').style.display = "none"



                                        $('#shippingAgents').select2().val('').trigger('change.select2');
                                        $('#clearingAgent').select2().val('').trigger('change.select2');
                                        $('#goodsHeadId').select2().val('').trigger('change.select2');
                                        $('#indexCargoType').val('');
                                        $('#groundingFreeDayId').val('');
                                        $('#storageFreeFreeDays').val('');
                                        $('#groundingFreeDays').val('');
                                        $("#searchBoxForConsigne").dxSelectBox('instance').option("value", '');
                                        $("#searchBoxForConsigne").dxSelectBox('instance').option("text", '');

                                    }
                                },



                                onRowRemoved: function (e) {

                                    console.log("e", e)

                                    $.post('/Import/Setup/DeleteGroundingFreeDays?key=' + e.data.groundingFreeDayId, function (data) {
                                        if (data.error == true) {
                                            showToast(data.message, "error");

                                        }
                                        else {

                                            showToast(data.message, "success");


                                        }
                                    });

                                }

                            }).dxDataGrid("instance");

                            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');



                        });
                    });
                });
            });
        });
    }



    function updnfo() {

        var shippingAgents =  $('#shippingAgents').val();
    var clearingAgent =  $('#clearingAgent').val();
    var goodsHeadId = $('#goodsHeadId').val();
    var consigne = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");
    var indexCargoType = $('#indexCargoType').val();
    var groundingFreeDayId = $('#groundingFreeDayId').val();
    var storageFreeFreeDays = $('#storageFreeFreeDays').val();
    var groundingFreeDays = $('#groundingFreeDays').val();

    if (groundingFreeDayId) {

        let model = {

        groundingFreeDayId: groundingFreeDayId,
    groundingFreeDays: groundingFreeDays,
    storageFreeFreeDays: storageFreeFreeDays,
    cargoType: indexCargoType,
    shippingAgentId: shippingAgents,
    consigneId: consigne,
    clearingAgentId: clearingAgent,
    goodsHeadId: goodsHeadId,

            }

    console.log("model", model);


    $.post('/Import/Setup/GroundingFreeDaysUpdate' ,{model: model}, function (data) {
                if (data.error == true) {
        showToast(data.message, "error");

                }
    else {

        showToast(data.message, "success");

    console.log("else");
    document.getElementById('btnSubmit').style.display = "block"
    document.getElementById('btnSubmitupdate').style.display = "none"



    $('#shippingAgents').select2().val('').trigger('change.select2');
    $('#clearingAgent').select2().val('').trigger('change.select2');
    $('#goodsHeadId').select2().val('').trigger('change.select2');
    $('#indexCargoType').val('');
    $('#groundingFreeDayId').val('');
    $('#storageFreeFreeDays').val('');
    $('#groundingFreeDays').val('');
    $("#searchBoxForConsigne").dxSelectBox('instance').option("value", '');
    $("#searchBoxForConsigne").dxSelectBox('instance').option("text", '');

    getgroundingfreedayslist();


                }
            });

        }
    else {
        showToast("please select row", "error");
        }



    }




    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }
