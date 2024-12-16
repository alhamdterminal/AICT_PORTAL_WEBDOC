

    var loadingProgramId = 0;
    var TPCargoDetailsId = 0;
    var TPReceiveVehicleId = 0
    var voyageExportId = 0;
    var CargoDetails = [];
    var StartTimedateBox;
    var StartTimedateBoxValue;
    var EndTimedateBox;
    var EndTimedateBoxValue;
    var url_string
    var url


    $(function () {
        var cont = document.getElementById("dgclass");
    cont.style.display = 'none';


    var cont = document.getElementById("Temperature");
    cont.style.display = 'none';

    url_string = window.location.href
    url = new URL(url_string);
    if (url.searchParams.get("TPReceiveVehicleId")) {
        TPReceiveVehicleId = url.searchParams.get("TPReceiveVehicleId");
    console.log("url ka ", TPReceiveVehicleId)
        }

    loadGrid();
    });

    function lpnumber_change(data) {

        loadingProgramId = data.value;
    console.log("loadingProgramId",loadingProgramId)
    findCargoDetail(loadingProgramId)
    changed_VesselExport(0, "")
    }

    function formatDateDev(date) {
        var d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();
    hours = d.getHours();
    minutes = d.getMinutes();
        ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    strTime = hours + ':' + minutes + ' ' + ampm;


    if (month.length < 2)
    month = '0' + month;
    if (day.length < 2)
    day = '0' + day;

    return [month, day, year].join('/') + ', ' + strTime;
    }

    function findCargoDetail(LoadingProgramId) {

        $.get('GetTPCargoDetailByLP?LoadingProgramId=' + LoadingProgramId, function (data) {
            if (data) {
                if (data.receiveStartDate) {
                    $("#receiveStartDate").dxDateBox("instance").option({ value: new Date(formatDateDev(data.receiveStartDate)) })
                }
                else {
                    $("#receiveStartDate").dxDateBox("instance").option({ value: new Date(formatDateDev(new Date())) })
                }
                if (data.receiveEndDate) {
                    $("#receiveEndDate").dxDateBox("instance").option({ value: new Date(formatDateDev(data.receiveEndDate)) })
                }
                else {
                    $("#receiveEndDate").dxDateBox("instance").option({ value: new Date(formatDateDev(new Date())) })
                }
                voyageExportId = data.voyageExportId;
                $('#cbm').val(data.cbm);
                $('#colorCode').val(data.colorCode);
                $('#DGClass').val(data.dgClass);
                $('#dischargePort').val(data.dischargePort);
                $('#finalDestination').val(data.finalDestination);
                $('#isdg').val(data.isDG.toString());
                $('#isPIFFA').val(data.isPIFFA.toString());
                $('#isReefer').val(data.isReefer.toString());
                $('#location').val(data.location);
                $('#noOfPackagesReceived').val(data.noOfPackagesReceived);
                $('#packageType').val(data.packageType);
                $('#plidNumber').val(data.plidNumber);
                $('#receivedFor').val(data.receivedFor);
                $('#remarks').val(data.remarks);
                $('#vesselExport').val(data.vesselExportId);
                // $('#voyageExport').val(data.voyageExportId);
                changed_VesselExport(data.vesselExportId, voyageExportId)
                $('#warehouseLocation').val(data.warehouseLocation);
                $('#weight').val(data.weight);
                $('#weightDecalred').val(data.weightDecalred);
                $('#temperature').val(data.temperature);
                $('#cargoCondition').val(data.cargoCondition);
                loadingProgramId = data.loadingProgramId;
                TPCargoDetailsId = data.tpCargoDetailsId;
                TPReceiveVehicleId = data.tpReceiveVehicleId;
                console.log("lp ka ", TPReceiveVehicleId)

                updateBtnShowHide(true);
                submitBtnShowHide(false);

                if (data.isDG.toString() == "true") {
                    var cont = document.getElementById("dgclass");
                    cont.style.display = 'block';
                }
                else {
                    var cont = document.getElementById("dgclass");
                    cont.style.display = 'none';
                }

                if (data.isReefer.toString() == "true") {
                    var cont = document.getElementById("Temperature");
                    cont.style.display = 'block';
                }
                else {
                    var cont = document.getElementById("Temperature");
                    cont.style.display = 'none';
                }

            }
            else {
                $("#receiveStartDate").dxDateBox("instance").option({ value: new Date(formatDateDev(new Date())) })
                $("#receiveEndDate").dxDateBox("instance").option({ value: new Date(formatDateDev(new Date())) })
                $('#cbm').val('');
                $('#colorCode').val('');
                $('#DGClass').val('');
                $('#dischargePort').val('');
                $('#finalDestination').val('');
                $('#location').val('');
                $('#noOfPackagesReceived').val('');
                $('#packageType').val('');
                $('#plidNumber').val('');
                $('#receivedFor').val('');
                $('#remarks').val('');
                $('#vesselExport').val('');
                $('#voyageExport').val('');
                $('#warehouseLocation').val('');
                $('#weight').val('');
                $('#weightDecalred').val('');
                $('#isdg').val("false");
                $('#isPIFFA').val("false");
                $('#isReefer').val("false");
                $('#temperature').val('');
                $('#cargoCondition').val('');
                loadingProgramId = loadingProgramId;
                TPCargoDetailsId = 0;
                TPReceiveVehicleId = url.searchParams.get("TPReceiveVehicleId");
                console.log("none ", TPReceiveVehicleId)
                updateBtnShowHide(false);
                submitBtnShowHide(true);

                var cont = document.getElementById("dgclass");
                cont.style.display = 'none';
            }

        })
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

    function AddCargoDetail() {
        console.log("Add ky time ", TPReceiveVehicleId)
        if (!(voyageExportId > 0)) {

        $('#VoyageExporterror').html('This is a required field');
        }
    else {
        $('#VoyageExporterror').html('');


    var f = document.getElementById('CargoDetailForm');

    if (f.checkValidity()) {

            var values = $('#CargoDetailForm').serialize();
    $.post('AddTPCargoDetail?' + values, {TPReceiveVehicleId: TPReceiveVehicleId, loadingProgramId: loadingProgramId  ,  voyageExportId : voyageExportId}, function (data) {
                if (data.error) {
        showToast(data.message, "warning");
                }

    else {
        TPCargoDetailsId = data.id;
    showToast(data.message, "success");
    updateBtnShowHide(true);
    submitBtnShowHide(false);
    loadGrid();
                }
            })

        }


    checkValidations();
        }
       


       



    }

    function checkValidations() {


        if (!$('#noOfPackagesReceived').val()) {

        $('#noOfPackagesReceivederror').html('This is a required field');
        }
    else {
        $('#noOfPackagesReceivederror').html('');
        }


    if (!$('#packageType').val()) {

        $('#packageTypeerror').html('This is a required field');
        }
    else {
        $('#packageTypeerror').html('');
        }


    if (!$('#weightDecalred').val()) {

        $('#weightDecalrederror').html('This is a required field');
        }
    else {
        $('#weightDecalrederror').html('');
        }


    if (!$('#colorCode').val()) {

        $('#colorCodeerror').html('This is a required field');
        }
    else {
        $('#colorCodeerror').html('');
        }


    if (!$('#finalDestination').val()) {

        $('#finalDestinationerror').html('This is a required field');
        }
    else {
        $('#finalDestinationerror').html('');
        }


    if (!$('#dischargePort').val()) {

        $('#dischargePorterror').html('This is a required field');
        }
    else {
        $('#dischargePorterror').html('');
        }


    if (!$('#plidNumber').val()) {

        $('#plidNumbererror').html('This is a required field');
        }
    else {
        $('#plidNumbererror').html('');
        }

    if (!$('#warehouseLocation').val()) {

        $('#warehouseLocationerror').html('This is a required field');
        }
    else {
        $('#warehouseLocationerror').html('');
        }

    if (!$('#cbm').val()) {

        $('#cbmerror').html('This is a required field');
        }
    else {
        $('#cbmerror').html('');
        }
    if (!$('#weight').val()) {

        $('#weighterror').html('This is a required field');
        }
    else {
        $('#weighterror').html('');
        }
    if (!$('#vesselExport').val()) {

        $('#vesselExporterror').html('This is a required field');
        }
    else {
        $('#vesselExporterror').html('');
        }

    if (!$('#receivedFor').val()) {

        $('#receivedForerror').html('This is a required field');
        }
    else {
        $('#receivedForerror').html('');
        }

    if (!$('#location').val()) {

        $('#locationerror').html('This is a required field');
        }
    else {
        $('#locationerror').html('');
        }
    if (!$('#remarks').val()) {

        $('#remarkserror').html('This is a required field');
        }
    else {
        $('#remarkserror').html('');
        }

    if (!$('#cargoCondition').val()) {

        $('#cargoConditionerror').html('This is a required field');
        }
    else {
        $('#cargoConditionerror').html('');
        }
    }

    function formatDate(date) {
        var d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();

    if (month.length < 2)
    month = '0' + month;
    if (day.length < 2)
    day = '0' + day;

    return [year, month, day].join('-');
    }

    function routetoCIRrepost() {

        var lpNumber = lpnumber;

    window.open('/Export/Reports/CargoInterchangeReceipt?lpNumber=' + lpNumber, "_blank");


    }

    function updateCargoDetail() {
        console.log("update ky time ", TPReceiveVehicleId)
        if (!(voyageExportId > 0)) {

        $('#VoyageExporterror').html('This is a required field');
        }
    else {
        $('#VoyageExporterror').html('');
    var f = document.getElementById('CargoDetailForm');

    if (TPCargoDetailsId != 0) {

            if (f.checkValidity()) {

                var values = $('#CargoDetailForm').serialize();
    $.post('updatePCargoDetail?' + values + '&TPReceiveVehicleId=' + TPReceiveVehicleId + '&LoadingProgramId=' + loadingProgramId + "&TPCargoDetailsId=" + TPCargoDetailsId + "&voyageExportId="+voyageExportId, function (data) {

        showToast("Updated", "success");

                })
            }


    checkValidations();

        }
        }


    }

    function restform() {
        var r = confirm("Are you sure ");
    if (r == true) {
        window.location.href = window.location.href
    }

    }

    function chageIsDG() {

        if ($('#isdg').val() == "true") {
            var cont = document.getElementById("dgclass");
    cont.style.display = 'block';
        }
    else {
            var cont = document.getElementById("dgclass");
    cont.style.display = 'none';
        }

    }
    function chageIsReefer() {

        if ($('#isReefer').val() == "true") {
            var cont = document.getElementById("Temperature");
    cont.style.display = 'block';
        }
    else {
            var cont = document.getElementById("Temperature");
    cont.style.display = 'none';
        }
    }

    function loadGrid() {
        console.log("sbt ky bad ", TPReceiveVehicleId)
        $.get("/Export/Transshipment/GetCargoDetailBYTPReceive?TPReceiveVehicleId=" + TPReceiveVehicleId, function (data) {

        CargoDetails = data;
    if (CargoDetails) {
                var dataGrid = $("#CargoDetails").dxDataGrid({
        dataSource: CargoDetails,
    keyExpr: "tpCargoDetailsId",
    wordWrapEnabled: true,
    showBorders: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
                    },

    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

                    },


    columns: [
    "cargoCondition",
    "cbm",
    "colorCode",
    "dgClass",
    "dischargePort",
    "finalDestination",
    "location",
    "receivedFor",
    "remarks",
    "weight",
    "weightDecalred"


    ],
    onRowRemoved: function (e) {

    }

                }).dxDataGrid("instance");

            }


        });

    }


    function changed_VesselExport(arg, voyageid) {

        vesselExportId = arg;

    $.get('/APICalls/GetVoyagesFormvesselExportId?vesselExportId=' + vesselExportId, function (data) {

        VoyageExports = data
            
            


            if (VoyageExports) {

        $("#voyageSelectBox").dxSelectBox({

            id: "voyageSelectBox",
            dataSource: VoyageExports,
            value: voyageid,
            displayExpr: "voyageNumber",
            valueExpr: 'voyageExportId',
            acceptCustomValue: true,
            onValueChanged: function (data) {
                voyageExportId = data.value;

            },
        })
    }

        });


    }

