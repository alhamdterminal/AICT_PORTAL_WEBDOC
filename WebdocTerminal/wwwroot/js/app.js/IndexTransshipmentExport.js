
    var TPReceiveVehicleId = 0;
    var containers = [];

    var clearingAgents = [];

    $(function () {

        $("#agentCNIC").inputmask("99999-9999999-9");
    $("#driverCNIC").inputmask("99999-9999999-9");
    $("#cotnainerNo").inputmask("aaaa-999999-9");
    var cont = document.getElementById("containerNo");
    cont.style.display = 'none';

    $.get('/APICalls/GetClearingAgentExport', function (res) {
        console.log("clearingAgentres", res)
            clearingAgents = res;
    console.log("clearingAgents", clearingAgents)
        });

    })


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "createdAt");
    }
    function getTranshipmentByVehicleNo(VehicleNumber) {
        console.log("VehicleNumber", VehicleNumber);
    $.get('/Export/Transshipment/GetTransshipmentsByVehicleNumber?VehicleNumber=' + VehicleNumber, function (data) {
        console.log("data", data)
            containers = data;
    var dataGrid = $("#tpReceivedContainer").dxDataGrid({
        dataSource: containers,
    keyExpr: "tpReceiveVehicleId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    paging: {
        enabled: false
                },
    editing: {
        mode: "row",
    allowUpdating: true, 
                },
    columns: [
    "vehicleNo",
    "cotnainerNo",
    "sealNo",
    "containerGrossWeight",
    "driverName",
    "driverCNIC",
    "agentRepresentative",
    "agentCNIC",
    {
        dataField: "clearingAgentExportId",
    width: 200,
    caption: "Clearing Agent",
    lookup: {
        dataSource: clearingAgents,
    displayExpr: "text",
    valueExpr: "value"
                        }
                    },
    "remarks",
    {
        dataField: "createdAt",
    dataType: "date"
                    } ,
    {
        type: "buttons",
    buttons: ["edit", "delete", {
        text: "My Command",
    icon: "repeat",
    hint: "My Command",
    onClick: function (e) {
        routeToCargoInfo(e.row.data.tpReceiveVehicleId)

    }
                        }]
                    },

    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
                    
                },
    onRowUpdated: function (e) {
        console.log(e.data);
    var model = e.data;
    $.post('/Export/Transshipment/updateTransshipmentByModel', {model, model}, function(res){
        console.log(res);

    if (res.error == true) {
        showToast(res.message, "error");

                        }
    else {

        showToast(res.message, "success");
    getTranshipmentByVehicleNo(model.vehicleNo );

                        }
                    })
                }

            }).dxDataGrid("instance");



        });


    //     $.get('/Export/Transshipment/GetTransshipmentByVehicleNumber?VehicleNumber=' + VehicleNumber, function (data) {

        //        if (data) {

        //            $('#agentCNIC').val(data.agentCNIC);
        //            $('#agentRepresentative').val(data.agentRepresentative);
        //            $('#clearingAgentExport').val(data.clearingAgentExportId);
        //            $('#containerGrossWeight').val(data.containerGrossWeight);
        //            $('#containerPresent').val(data.containerPresent.toString());
        //            $('#cotnainerNo').val(data.cotnainerNo);
        //            $('#driverCNIC').val(data.driverCNIC);
        //            $('#driverName').val(data.driverName);
        //            $('#remarks').val(data.remarks);
        //            $('#sealNo').val(data.sealNo);
        //            TPReceiveVehicleId = data.tpReceiveVehicleId
        //            $('#vehicleNo').val(data.vehicleNo);
        //            if (data.containerPresent.toString() == "true") {
        //                var cont = document.getElementById("containerNo");
        //                cont.style.display = 'block';
        //            }
        //            if (data.containerPresent.toString() == "false") {
        //                var cont = document.getElementById("containerNo");
        //                cont.style.display = 'none';
        //            }
        //            updateBtnShowHide(true);
        //            submitBtnShowHide(false);
        //        }
        //        else {

        //            $('#agentCNIC').val(null)


        //            $('#agentRepresentative').val(null);
        //            $('#clearingAgentExport').val(null);
        //            $('#containerGrossWeight').val(null);
        //            $('#cotnainerNo').val(null);
        //            $('#driverCNIC').val(null);
        //            $('#driverName').val(null);
        //            $('#remarks').val(null);
        //            $('#sealNo').val(null);

        //            var cont = document.getElementById("containerNo");
        //            cont.style.display = 'none';

        //            TPReceiveVehicleId = 0

        //            updateBtnShowHide(false);
        //            submitBtnShowHide(true);
        //        }


        //    })
    }

    function chageContainContainer(value) {
        updateContainerShowHide(value)
    }

    function updateContainerShowHide(show) {

 
        var cont = document.getElementById("containerNo");
    if (show == "true") {
        cont.style.display = 'block';

        }

    else {
        cont.style.display = 'none';
    $('#cotnainerNo').val('');
        }
     }

    function updateBtnShowHide(show) {
        var btn = document.getElementById("btnSubmitUpdate");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }

    function submitBtnShowHide(show) {
        var btn = document.getElementById("btnSubmit");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function addTPReceiveVehicle() {

        var f = document.getElementById('TPReceiveVehicleForm');

    if (f.checkValidity()) {


            var model = $('#TPReceiveVehicleForm').serialize();

    $.post('/Export/Transshipment/AddTransshipment?' + model, function (data) {
        console.log("data", data)
                if (data.error) {
        showToast(data.message, "warning");
                }

    else {
        TPReceiveVehicleId = data.id;
    console.log("TPReceiveVehicleId", TPReceiveVehicleId)

    showToast(data.message, "success");
    console.log("model", model)
    var vehicle = $('#vehicleNo').val();
    console.log("adasd", vehicle);
    getTranshipmentByVehicleNo(vehicle);
                    //updateBtnShowHide(true);
                   // submitBtnShowHide(false);
                    
                }
            });

        }
    checkValidity();
    }


    function routeToCargoInfo(tpReceiveVehicleId) {
        console.log("tpReceiveVehicleId", tpReceiveVehicleId)
        if (tpReceiveVehicleId != 0) {
        top.location.href = '/Export/Transshipment/CargoDetails?TPReceiveVehicleId=' + tpReceiveVehicleId;
        }

    }

    //function submitTPReceiveVehicle() {
        //    if (TPReceiveVehicleId != 0) {

        //                top.location.href = '/Export/Transshipment/CargoDetails?TPReceiveVehicleId=' + TPReceiveVehicleId;

        //    } 
        //}


        function updateTPReceiveVehicle() {

            var f = document.getElementById('TPReceiveVehicleForm');
            if (f.checkValidity()) {

                if (TPReceiveVehicleId != 0) {

                    var model = $('#TPReceiveVehicleForm').serialize();
                    $.post('/Export/Transshipment/updateTransshipment?' + model + '&TPReceiveVehicleId=' + TPReceiveVehicleId, function (data) {

                        showToast("Updated", "success");
                    })



                }
            }
            checkValidity();
        }


    function checkValidity() {

         if (!$('#agentCNIC').val()) {

        $('#agentCNICerror').html('This is a required field');
        }
    else {
        $('#agentCNICerror').html('');
        }
    if (!$('#agentRepresentative').val()) {

        $('#agentRepresentativeerror').html('This is a required field');
        }
    else {
        $('#agentRepresentativeerror').html('');
        }
    if (!$('#clearingAgentExport').val()) {

        $('#clearingAgentExporterror').html('This is a required field');
        }
    else {
        $('#clearingAgentExporterror').html('');
        }
    if (!$('#containerGrossWeight').val()) {

        $('#containerGrossWeighterror').html('This is a required field');
        }
    else {
        $('#containerGrossWeighterror').html('');
        }

    if (!$('#driverCNIC').val()) {

        $('#driverCNICerror').html('This is a required field');
        }
    else {
        $('#driverCNICerror').html('');
        }
    if (!$('#driverName').val()) {

        $('#driverNameerror').html('This is a required field');
        }
    else {
        $('#driverNameerror').html('');
        }
    if (!$('#remarks').val()) {

        $('#remarkserror').html('This is a required field');
        }
    else {
        $('#remarkserror').html('');
        }
    if (!$('#sealNo').val()) {

        $('#sealNoerror').html('This is a required field');
        }
    else {
        $('#sealNoerror').html('');
        }
    if (!$('#vehicleNo').val()) {

        $('#vehicleNoerror').html('This is a required field');
        }
    else {
        $('#vehicleNoerror').html('');
        }
    }


    function formfiled() {


            if (PermissionInputs.find(x => x.fieldName == "VehicleNumber" && x.isChecked == false)) {

        document.getElementById('vehicleNo').disabled = true;
            }
            if (PermissionInputs.find(x => x.fieldName == "ContainsContainer" && x.isChecked == false)) {

        document.getElementById('containerPresent').disabled = true;
            }

            if (PermissionInputs.find(x => x.fieldName == "SealNumber" && x.isChecked == false)) {

        document.getElementById('sealNo').disabled = true;
            }
            if (PermissionInputs.find(x => x.fieldName == "ContainerGrossWeight" && x.isChecked == false)) {

        document.getElementById('containerGrossWeight').disabled = true;
                }
                if (PermissionInputs.find(x => x.fieldName == "DriverName" && x.isChecked == false)) {

        document.getElementById('driverName').disabled = true;
            }if (PermissionInputs.find(x => x.fieldName == "DriverCNIC" && x.isChecked == false)) {

        document.getElementById('driverCNIC').disabled = true;
            }if (PermissionInputs.find(x => x.fieldName == "ClearingAgent" && x.isChecked == false)) {

        document.getElementById('clearingAgentExport').disabled = true;
            }if (PermissionInputs.find(x => x.fieldName == "AgentRepresentative" && x.isChecked == false)) {

        document.getElementById('agentRepresentative').disabled = true;
            }if (PermissionInputs.find(x => x.fieldName == "RepresentativeCNIC" && x.isChecked == false)) {

        document.getElementById('agentCNIC').disabled = true;
            }if (PermissionInputs.find(x => x.fieldName == "Remarks" && x.isChecked == false)) {

        document.getElementById('remarks').disabled = true;
            }
             
    }
