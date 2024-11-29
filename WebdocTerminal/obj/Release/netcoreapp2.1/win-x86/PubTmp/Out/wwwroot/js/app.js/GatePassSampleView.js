

    var type;




    function showFilters() {

        type = $("input[name='type']:checked").val();
    console.log("typetypetypetypetype", type);


    $.get('/APICalls/GetFiltersForDeliveryOrder?Type=' + type, function (partial) {

        $('#filters').html(partial);

        })


    gatePassesGrid([])

    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function containerChangeCallback(ornt) {
        console.log("ContainerIndex")
 
        reportGrid($("#indexdb option:selected").val() , "CFS")
    }





    function containerCallback() {

        console.log("Container")
         
        reportGrid($("#containerCYdb option:selected").val() , "CY")

    }


    function reportGrid(containerid, containertype) {

        console.log("containerid", containerid)
 
        console.log("containertype", containertype)

    getcontainergatePasses(containerid ,  containertype);
         



    }


    function getcontainergatePasses(containerid, containertype) {

        $.get('/APICalls/getcontainergatePasses?containerid=' + containerid + '&containertype=' + containertype, function (res) {

            console.log("res", res);

            gatePassesGrid(res)

        });

    }


    function gatePassesGrid(res) {

        console.log("grid res", res)

        var dataGrid = $("#gridContainer2").dxDataGrid({
        dataSource: res,
    keyExpr: "gatePassSampleId",
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
        dataField: "gatePassNumber",
    allowEditing: false,
    caption: "GatePass Number"
                },
    {
        dataField: "detailOfSample",
    allowEditing: false,
    caption: "Detail Of Sample"
                },

    {
        dataField: "agentRepresentative",
    allowEditing: false,
    caption: "Agent Representative"
                },
    {
        dataField: "receivedBy",
    allowEditing: false,
    caption: "Received By"
                },
    {
        dataField: "examinedBy",
    allowEditing: false,
    caption: "Examined By"
                },


    {
        dataField: "gatePassSampleId",
    caption: 'S No.',
    cellTemplate: function (container, options) {
        $("<button type='button' class='btn btn-success' onClick='GatePassReport(" + options.value + ")'>Print</button>")
            .appendTo(container);
                    }, allowEditing: false
                },

    ], onRowUpdating: function (e) {
    }

        }).dxDataGrid("instance");
    }

    function GatePassReport(id) {

        window.open('/import/reports/GatePassSampleReport?type=' + type + "&gatePassSampleid=" + id, "_blank");

    }




    function submitGatePassSample() {


        var form = document.getElementById('GatePassSampleForm');

    var model = $('#GatePassSampleForm').serialize();


    console.log("model", model)
    console.log("form.reportValidity", form.reportValidity)

    form.reportValidity();


    if (form.checkValidity()) {

        console.log("ok")
            console.log("model", model);

    $.post("/Import/Setup/CreateGatePassSample?" + model, function (res) {

            if (res.error == true) {

        showToast(res.message, "error");
                 
            }
    else {

        showToast(res.message, "success");

    if (type == "CFS") {
        reportGrid($("#indexdb option:selected").val(), "CFS")
    }
    if (type == "CY") {
        reportGrid($("#containerCYdb option:selected").val(), "CY")

    }
               
            } 
                 

        });


        }
            //showToast("Saved", "success");
       

        


    }




    //function routetoGatepass() {

        //    var dono = returnDONumber;
        //    window.open('/import/Delivery/OrderDetail?doNumber=' + dono, "_blank");

        //    window.location.href = window.location.href;

        //   // top.location.href = '/import/Delivery/OrderDetail?doNumber=' + dono;
        //}





        function formfiled() {


        }

    /**/