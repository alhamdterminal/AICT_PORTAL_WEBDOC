    var pono = "";
    var gdnumber;
    var lpnumber;
    var loadingPrograms = [];
    var loadingProgramDetail = [];
    var loadingProgramId = 0;
    var loadingProgramDetailId = 0;
    var cargoReceivedId = 0;
    var CargoId = 0;

    $(function () {

        $('#loadingTeminal').css('pointer-events', 'none');
    $('#vesselExport').css('pointer-events', 'none');
    $('#voyageExport').css('pointer-events', 'none');
    });

    function gdNumber_changed(data) {

        gdnumber = data.value;
    ChangeGdNumber(gdnumber)
    }
    function ChangeGdNumber(data) {
        if (data) {
        $.get('/APICalls/GetLatestLPByGDNumber?GDNumber=' + data, function (res) {

            if (res.loadingProgramNumber) {

                $('#LPNumber').dxSelectBox("instance").option('value', res.loadingProgramNumber);
                $('#LPNumber').dxSelectBox("instance").option('disabled', true);
            }
            else {
                $('#LPNumber').dxSelectBox("instance").option('value', '');
                $('#LPNumber').dxSelectBox("instance").option('disabled', false);

            }

        });
        }

    }

    function change_LPNumber() {
        lpnumber = $('#LPNumber').dxSelectBox("instance").option('value')

        $.get('/APICalls/GetCargoInformation?gdNumber=' + gdnumber + '&lpNumber=' + lpnumber + '&lpDetailId=' + loadingProgramDetailId, function (data) {

             if (data) {
        $('#packageType').val(data.packageType);
    $('#vehicleNumber').val(data.vehicleNumber);
    $('#clearingAgent').val(data.clearingAgentName);
    $('#shipper').val(data.shipperName);
    $('#consignee').val(data.consigneeName);
    $('#packagesReceived').val(data.packagesReceived);
    if (data.cargoReceivedDate) {
        $('#cargoReceivedDate').val(formatDate(data.cargoReceivedDate));

                }
    else {
        $('#cargoReceivedDate').val('');
                }
    $('#loadingTeminal').val(data.portAndterminalId);
    $('#finalDestination').val(data.finalDestination);
    $('#dischargePort').val(data.dischargePort);
    $('#vesselExport').val(data.vesselExportId);
    $('#voyageExport').val(data.voyageExportId);
    $('#commodity').val(data.commodityId);
    $('#plidNumber').val(data.plidNumber);
    $('#warehouseLocation').val(data.warehouseLocation);
    $('#cbm').val(data.cbm);
    $('#weight').val(data.weight);
    $('#location').val(data.location);
    $('#colorcode').val(data.colorCode);
    $('#packageReceived').val(data.packageReceived);
    CargoId = data.cargoId;

    loadingProgramDetailId = data.loadingProgramDetailId;
    if (data.cargoId) {

        updateBtnShowHide(true);
    submitBtnShowHide(false);
                }
    else {
        updateBtnShowHide(false);
    submitBtnShowHide(true);
                }

            }


        });



    }



    function loadingProgramNumber_changed(arg) {

        lpnumber = arg.value;


    $.get('/APICalls/GetLoadingProgramDetailByLPNumber?lpNumber=' + lpnumber, function (data) {

        loadingProgramDetail = data

            if (loadingProgramDetail) {

        $("#searchBox").dxSelectBox({
            dataSource: loadingProgramDetail,
            displayExpr: "poNumber",
            acceptCustomValue: true,
            onValueChanged: function (data) {
                if (data.value) {
                    $('#pono').html(data.value.poNumber);

                }
                else {
                    $('#pono').html("");

                }

                loadingProgramDetailId = data.value.loadingProgramDetailId;
                change_LPNumber(loadingProgramDetailId)


            },
        })
    }






    var dataGrid = $("#loadingProgramDetal").dxDataGrid({
        dataSource: loadingProgramDetail,
    keyExpr: "loadingProgramDetailId",
    wordWrapEnabled: true,
    showBorders: true,
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

    editing: {
        mode: "row"

                },
    columns: [
    "poNumber",
    "style",
    "totalPackages",
    "packageType",
    "totalPieces",


    ],

    onEditorPreparing: function (e) {
    },


            }).dxDataGrid("instance");


        });

    }


    function updateCargo() {

        var f = document.getElementById('CargoForm');

    if (f.checkValidity()) {

            var model = $('#CargoForm').serialize();


    $.post('updateCargo?' + model, {loadingProgramDetailId: loadingProgramDetailId, gdnumber: gdnumber, CargoId: CargoId }, function (data) {

               // showToast("Saved", "success");

                if (data.error) {
        showToast(data.message, "warning");
                }

    else {

        showToast(data.message, "success");
                     
                }

            })

        }
    else {
        alert("Invalid form values");
        }

    checkValidity();
    }

    function checkValidity() {

        if (!$('#cbm').val()) {

        $('#CBMerror').html('This is a required field');
        }
    else {
        $('#CBMerror').html('');
        }
    if (!$('#weight').val()) {

        $('#Weighterror').html('This is a required field');
        }
    else {
        $('#Weighterror').html('');
        }
    if (!$('#location').val()) {

        $('#Locationerror').html('This is a required field');
        }
    else {
        $('#Locationerror').html('');
        }
    if (!$('#commodity').val()) {

        $('#Commodityerror').html('This is a required field');
        }
    else {
        $('#Commodityerror').html('');
        }
    if (!$('#packageReceived').val()) {

        $('#PackageReceivederror').html('This is a required field');
        }
    else {
        $('#PackageReceivederror').html('');
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

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function AddCargo() {




        if (gdnumber) {

            if (lpnumber) {

                var f = document.getElementById('CargoForm');

    if (f.checkValidity()) {
                    var model = $('#CargoForm').serialize();

    $.post('AddCargo?' + model, {loadingProgramDetailId: loadingProgramDetailId, gdnumber: gdnumber }, function (data) {
                         
                         if (data.error) {
        showToast(data.message, "warning");
                        }

    else {
        CargoId = data.id;
    showToast(data.message, "success");
    updateBtnShowHide(true);
    submitBtnShowHide(false);
                        }

                    })
                }
    else {
        alert("Invalid form values");
                }

    checkValidity();

            }
    else {
        alert("Please Select LP Number")
    }
        }
    else {
        alert("Please Select GD Number")
    }




    }


    function restform() {
        var r = confirm("Are you sure?");
    if (r == true) {
        window.location.href = window.location.href
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

    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "GDNumber" && x.isChecked == false)) {

        document.getElementById('GDNumber').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "LPNumber" && x.isChecked == false)) {

        document.getElementById('LPNumber').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "PONo" && x.isChecked == false)) {

        document.getElementById('searchBox').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "FinalDestination" && x.isChecked == false)) {

        document.getElementById('finalDestination').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "DischargePort" && x.isChecked == false)) {

        document.getElementById('dischargePort').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Commodity" && x.isChecked == false)) {

        document.getElementById('commodity').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "PLIDNumber" && x.isChecked == false)) {

        document.getElementById('plidNumber').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "WarehouseLocation" && x.isChecked == false)) {

        document.getElementById('warehouseLocation').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "CBM" && x.isChecked == false)) {

        document.getElementById('cbm').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Weight" && x.isChecked == false)) {

        document.getElementById('weight').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "Location" && x.isChecked == false)) {

        document.getElementById('location').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ColorCode" && x.isChecked == false)) {

        document.getElementById('colorcode').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "PackageReceived" && x.isChecked == false)) {

        document.getElementById('packageReceived').disabled = true;
        }
    }