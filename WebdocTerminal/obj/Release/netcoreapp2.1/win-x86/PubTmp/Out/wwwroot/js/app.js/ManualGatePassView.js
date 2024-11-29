

    var type;

    function showFilters() {

        type = $("input[name='type']:checked").val();

    restformValues();


    $.get('/APICalls/GetFilterIndexWise?Type=' + type, function (partial) {

        $('#filters').html(partial);

    console.log(" typetype ", type);


        })

    if (type == "CFS") {
        document.getElementById('ContainerType').style.display = "block"
    } else {
        document.getElementById('ContainerType').style.display = "none"
    }


    restformValues();
    getcontainerdeatil();
    gatePassesGrid([]);


    }


    function restformValues() {

        console.log("restformValues");

    $('#cashRegNoCBE').val('')
    $('#cashRegDateCBE').val('')
    $('#customHouse').val('')
    $('#clearingAgent').val('')
    $('#regNoConsignee').val('')
    $('#chlicenceNo').val('')
    $('#agentRep').val('')
    $('#nicno').val('')
    $('#customPerNo').val('')
    $('#vessel').val('')
    $('#aictDoNo').val('')
    $('#arrivalDate').val('')
    $('#voyage').val('')
    $('#marksNo').val('')
    $('#goodsDesc').val('')
    $('#containerNo').val('')
    $('#cycfs').val('')
    $('#size').val('')
    $('#packages').val('')



    $('#truckNumber').val('')
    $('#shift').val('')
    $('#randDClerkId').val('')
    $('#totalPackages').val('')
    $('#totalDelivered').val('')
    $('#delivered').val('')
    $('#packages').val('')

    }

    function getcontainerdeatil() {
        console.log("type", type)
        console.log("igm", igm)
    var indexnumber = 0
    if (type == "CFS") {
        indexnumber = $("#indexdb option:selected").text();
        }
    if (type == "CY") {
        indexnumber = $("#containerIndexCYdb option:selected").text();
        }

    $.get('/APICalls/GetContainerDeatilInfo?type=' + type + '&igm=' + igm + '&indexnumber=' + indexnumber, function (res) {

        console.log("res", res);

    $('#cashRegNoCBE').val(res.cashRegNoCBE)
    $('#cashRegDateCBE').val(res.cashRegDateCBE)
    $('#customHouse').val(res.customHouse)
    $('#clearingAgent').val(res.clearingAgent)
    $('#regNoConsignee').val(res.regNoConsignee)
    $('#chlicenceNo').val(res.chlicenceNo)
    $('#agentRep').val(res.agentRep)
    $('#nicno').val(res.nicno)
    $('#customPerNo').val(res.customPerNo)
    $('#vessel').val(res.vessel)
    $('#aictDoNo').val(res.aictDoNo)
    $('#arrivalDate').val(res.arrivalDate)
    $('#voyage').val(res.voyage)
    $('#marksNo').val(res.marksNo)
    $('#goodsDesc').val(res.goodsDesc)
    $('#containerNo').val(res.containerNo)
    $('#cycfs').val(res.cycfs)
    $('#size').val(res.size)
    $('#packages').val(res.packages)


        });
    }

    function containerChangeCallback(ornt) {
        console.log("test containerChangeCallback");
    restformValues();
    getcontainerdeatil();

    reportGrid(igm, $("#indexdb option:selected").val())
    }


    function containerCallback() {
        console.log("test containerCallback");
    restformValues();
    getcontainerdeatil();

    reportGrid(igm, $("#containerIndexCYdb option:selected").val())
    }


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function formfiled() {

    }

    function createManualGatePass() {
        console.log("createManualGatePass")

        var form = document.getElementById('ManualGatePassForm');

    var model = $('#ManualGatePassForm').serialize();


    console.log("form.reportValidity", form.reportValidity)

    form.reportValidity();



    if (form.checkValidity()) {

        console.log("model", model)
            console.log("ok")

    var postData;

    if (type == "CFS") {

        postData = model;
                 

            }
    else {

        postData = model + "&igm=" + igm + "&indexNo=" + $("#containerIndexCYdb option:selected").val();
            }

    console.log("postData", postData)
    $.post('/Import/Delivery/SaveManualGatePass?' + postData  , function (data) {


        console.log("data", data);

    if (data.error == true) {
        showToast(data.message, "error");

                }
    else {
        showToast(data.message, "success");

    if (type == "CFS") {
        console.log("test containerChangeCallback");
    restformValues();
    getcontainerdeatil();

    reportGrid(igm, $("#indexdb option:selected").val())
                    }
    else {
        console.log("test containerCallback");
    restformValues();
    getcontainerdeatil();

    reportGrid(igm, $("#containerIndexCYdb option:selected").val())
                    }


                }

               
            });

        }


    }



    function reportGrid(igm , index ) {

        console.log("igm", igm)
        console.log("index", index)
    console.log("type",type)


    if (type == "CFS") {
        getcfscontainergatePasses(index);
        }

    if (type == "CY") {
        getcycontainergatePasses(igm, index);
        }

        

    }



    function getcfscontainergatePasses(index) {

        console.log("Container Index Id", index)

        $.get('/APICalls/GetcfscontainergatePasses?containerindexId=' + index , function (res) {

        console.log("res", res);

    gatePassesGrid(res)

        });

    }

    function getcycontainergatePasses(igm, index) {

        console.log("igm", igm)
        console.log("CYContainer Index No", index)


    $.get('/APICalls/GetcycontainergatePasses?igm=' + igm + '&index=' + index, function (res) {

        console.log("res", res);

    gatePassesGrid(res)
        });
    }


    function DeleteGatePass(value) {
        console.log(value);
    var GatePassId = value;
    $.post('/Import/Delivery/DeleteManualGatePass?GatePassId=' + GatePassId, function (data) {

            if (data.error == true) {

        showToast(data.message, "error");

            }
    else {
        showToast(data.message, "success");

            }

    location.reload();


        })
    }

    function GatePassReport(id) {

        console.log(id);

    window.open('/import/reports/ManualGatepassReport?type=' + type + "&id=" + id, "_blank");

    }

    function gatePassesGrid(res) {

        console.log("grid res", res)

        var dataGrid = $("#gridContainer2").dxDataGrid({
        dataSource: res,
    keyExpr: "manualGatePassId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },
    editing: {
        mode: "batch",
    allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click"
            },
    columns: [
    {
        dataField: "truckNumber",
    allowEditing: false,
    caption: "Truck No"
                },
    {
        dataField: "totalPackages",
    allowEditing: false,
    caption: "Total Packages"
                },

    {
        dataField: "balancePackages",
    allowEditing: false,
    caption: "Balance Packages"
                },
    {
        dataField: "delivered",
    allowEditing: false,
    caption: "Delivered"
                },
    {
        dataField: "totalDelivered",
    allowEditing: false,
    caption: "Total Delivered"
                },
    {
        dataField: "type",
    allowEditing: false,
    caption: "Type"
                },
    {
        dataField: "remarks",
    allowEditing: false,
    caption: "Remarks"
                },

    {
        dataField: "manualGatePassId",
    caption: 'S No.',
    cellTemplate: function (container, options) {
        $("<button type='button' class='btn btn-success' onClick='GatePassReport(" + options.value + ")'>Print</button>")
            .appendTo(container);
                    }, allowEditing: false
                },
    {
        dataField: "manualGatePassId",
    caption: '#',
    cellTemplate: function (container, options) {
        $("<button type='button' class='btn btn-success' onClick='DeleteGatePass(" + options.value + ")'>Delete</button>")
            .appendTo(container);
                    }, allowEditing: false
                },
    ], onRowUpdating: function (e) {
    }

        }).dxDataGrid("instance");
    }




