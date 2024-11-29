

    jQuery(document).ready(function () {
        jQuery('#hideshow').on('click', function (event) {
            jQuery('#VehcileExportDetailGrid').toggle('show');
        });
    });

    var lpnumber;
    var gdnumber;
    var loadingProgramDetail = [];
    var enteringCargoId = 0;
    var VehicleNumber = "";
    var shipperSeal = "";
    var VehicleDetail = [];



    //$(function () {

        //})
        function checkColumn(e, field) {

            if (e.parentType === "dataRow" && e.dataField !== field) {
                return;
            }

            e.editorOptions.disabled = true;
        }

    function hideMenifestedColumns(e) {


        if (PermissionInputs.find(x => x.fieldName == "VehicleNo" && x.isChecked == false)) {

        checkColumn(e, "vehicleNumber");
        }if (PermissionInputs.find(x => x.fieldName == "ShipperSeal" && x.isChecked == false)) {

        checkColumn(e, "shipperSeal");
        }
    
    }
    function loadGrid() {

        var dataGrid = $("#vehicleDetails").dxDataGrid({
        dataSource: VehicleDetail,
    keyExpr: "exportVehicleId",
    wordWrapEnabled: true,
    showBorders: true,
    columnAutoWidth: true,

    paging: {
        enabled: false
            },

    editing: {
        mode: "row",
    allowAdding: true,
    allowDeleting: true
            },
    columns: [
    "vehicleNumber",
    "shipperSeal"

    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
            },

        }).dxDataGrid("instance");


    }


    function gdNumber_changed(data) {

        gdnumber = data.value;

    getEnteringCargoByGDNo()

    }

    function getEnteringCargoByGDNo() {
        $.get('/APICalls/GetEnteringCargoByGD?gdnumber=' + gdnumber, function (data) {

            fieldsBind(data)

        })
    }

    function findEnteringCargo(gd) {

        $.get('/APICalls/FindEnteringCargo?gdnumber=' + gd + '&LPNumner=' + lpnumber, function (data) {

            fieldsBind(data)

        })
    }

    function loadingProgramNumber_changed(data) {

        lpnumber = "";
    if (data.selectedItem) {
            if (data.selectedItem.value) {
        lpnumber = data.selectedItem.value
                loadingProgramDetailGrid(lpnumber)

    findEnteringCargo(gdnumber, lpnumber);
            }
        }


    }

    function loadingProgramDetailGrid(lpNumber) {

        $.get('/APICalls/GetLoadingProgramDetailByLPNumber?lpNumber=' + lpNumber, function (data) {

            if (data) {
                loadingProgramDetail = data
                var dataGrid = $("#loadingProgramDetail").dxDataGrid({
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
                        {
                            dataField: "insDate",
                            dataType: "date"
                        },
                        {
                            dataField: "docsDate",
                            dataType: "date"
                        },
                        "remarks",

                    ],

                    onEditorPreparing: function (e) {
                    },

                    onRowUpdated: function (e) {

                        console.log(e);
                    },
                    onRowRemoved: function (e) {
                        console.log(e);
                    },
                    onRowInserted: function (e) {

                        console.log(e);

                    },

                }).dxDataGrid("instance");
            }
        });
    }

    function fieldsBind(data) {
 
        if (data) {
        $('#numberOfPackges').val(data.noOfPackages);
    $('#packgeType').val(data.packageType);
    $('#shipperName').val(data.shipperName);
    $('#consigneeName').val(data.congisneeName);
    $('#clearingAgentName').val(data.clearingAgent);
    $('#challanNumber').val(data.challanNumber);
    $('#grossWeight').val(data.grossWeight);
    //$('#vehicleNumber').val(data.vehicleNo);
    $('#remarks').val(data.remarks);
    if (data.exportVehicles) {
        VehicleDetail = data.exportVehicles;
                if (VehicleDetail.length > 0)
    $("#hideshow").click();
            }
    else {
        VehicleDetail = []

    }


    if (data.gateInDate) {
        $('#opiaReceived').val(formatDate(data.gateInDate));
            }
    if (data.gateInDate == null) {
        $('#opiaReceived').val('');
            }

    if (data.lpNumber) {
        lpnumber = data.lpNumber;
    $('#LPNumber').dxSelectBox("instance").option('value', data.lpNumber)
    loadingProgramDetailGrid(data.lpNumber);
            }

    enteringCargoId = data.enteringCargoId;
    loadGrid();

        }
    else {
        $('#numberOfPackges').val('');
    $('#packgeType').val('');
    $('#shipperName').val('');
    $('#consigneeName').val('');
    $('#clearingAgentName').val('');
    $('#challanNumber').val('');
    $('#grossWeight').val('');
    $('#opiaReceived').val('');
    $('#vehicleNumber').val('');
    $('#shipperSeal').val('');
    $('#remarks').val('');

    $('#LPNumber').dxSelectBox("instance").option('value', '')

    enteringCargoId = 0;
    VehicleDetail = [];
    loadGrid();
        }


    if (enteringCargoId != 0) {
        updateBtnShowHide(true);
    submitBtnShowHide(false);
        }
    else {
        updateBtnShowHide(false);
    submitBtnShowHide(true);
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

    function AddEnteringCargo() {
        if ((VehicleDetail.length == 0) && ($('#vehicleNumber').val() == "")) {
        alert('Please Enter one Vechile ')




    }
    else {
               if ((VehicleDetail.length == 0) && ($('#shipperSeal').val() == "")) {alert('Please Enter Shipper Seal ')}
    else {
                if (VehicleDetail.length) {
    }
    else {
        VehicleNumber = document.getElementById('vehicleNumber').value;

    shipperSeal = document.getElementById('shipperSeal').value;
    let x = {
        vehicleNumber: VehicleNumber,
    shipperSeal: shipperSeal,
    exportVehicleId: "0"

                    }
    VehicleDetail.push(x)
 
                }

    if (gdnumber) {

                    if (lpnumber) {

                        var f = document.getElementById('EnteringCargoForm');


    var GDNumber = gdnumber;
    var values = $('#EnteringCargoForm').serialize();
    $.post('addenteringcargo?' + values + '&GDNumber=' + GDNumber + '&lpnumber=' + lpnumber, {VehicleDetail: VehicleDetail }, function (data) {
                             if (data.error) {
        showToast(data.message, "warning");
                            }

    else {
        enteringCargoId = data.id;
    showToast(data.message, "success");
    updateBtnShowHide(true);
    submitBtnShowHide(false);
                            }


                        });

                    }
    else {
        alert("Please Select LP Number")
    }

                }
    else {
        alert("Please Select GD Number")
    }


            }

        }
         
       


    }

    function checkValidity() {


    }

    function UpdateEnteringCargo() {

        if ((VehicleDetail.length == 0) && ($('#vehicleNumber').val() == "") ) {
        alert('Please Enter one Vechile')
    }
    else {
             if ((VehicleDetail.length == 0) &&  ($('#shipperSeal').val() == "")) {
        alert('Please Enter one Shipper Seal')
    }
    else {
             if (VehicleDetail.length) {
    }
    else {
        VehicleNumber = document.getElementById('vehicleNumber').value;
    shipperSeal = document.getElementById('shipperSeal').value;
    let x = {
        vehicleNumber: VehicleNumber,
    shipperSeal: shipperSeal,
    exportVehicleId: "0" 
                
            }
    VehicleDetail.push(x)
 
            }

    if (gdnumber) {

            if (lpnumber) {

                var f = document.getElementById('EnteringCargoForm');


    var values = $('#EnteringCargoForm').serialize();


    $.post('updateEnteringCargo?' + values + '&gdnumber=' + gdnumber + '&lpnumber=' + lpnumber + '&enteringCargoId=' + enteringCargoId, {ExportVehicles: VehicleDetail }, function (data) {
                        if (data.error)
    showToast(data.message, "warning");

    else
    showToast(data.message, "success");


                    })
         

            }
    else {
        alert("Please Select LP Number")
    }

        }
    else {
        alert("Please Select GD Number")
    }

        }
      
        }
  

    }

    function printCargoDropOff() {

        window.open('/Export/Reports/CargoDropoffTicket?lpnumber=' + lpnumber, "_blank");
    }

    function printTellySheet() {

        window.open('/Export/Reports/ExrtTellySheet?lpNumber=' + lpnumber, "_blank");
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

    function restform() {
        var r = confirm("Are you sure");
    if (r == true) {
        window.location.href = window.location.href
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

    function toggleVehcileGrid() {
       
       
        if (document.getElementById('vehicleNumber').value  && document.getElementById('shipperSeal').value ) {

        VehicleNumber = document.getElementById('vehicleNumber').value;
    shipperSeal = document.getElementById('shipperSeal').value;
    let x = {
        vehicleNumber: VehicleNumber ,
    shipperSeal: shipperSeal ,
    exportVehicleId:"0"
               
            }
    if (VehicleDetail.length == 0) {

        VehicleDetail.push(x)
                 loadGrid();
    $('#vehicleNumber').val('')
    $('#shipperSeal').val('')
            }
    if (VehicleDetail.length) {

        $('#vehicleNumber').val('')
                $('#shipperSeal').val('')
            }

    document.getElementById('vehicleNumber').disabled = true;
    document.getElementById('shipperSeal').disabled = true;

        }

    }





    function formfiled() {

         if (PermissionInputs.find(x => x.fieldName == "GDNumber" && x.isChecked == false)) {

        document.getElementById('GDNumber').style.display = "none";
        }  if (PermissionInputs.find(x => x.fieldName == "LPNumber" && x.isChecked == false)) {

        document.getElementById('LPNumber').style.display = "none";
        }  if (PermissionInputs.find(x => x.fieldName == "VehicleNo" && x.isChecked == false)) {

        document.getElementById('vehicleNumber').disabled = true;
        }  if (PermissionInputs.find(x => x.fieldName == "ShipperSeal" && x.isChecked == false)) {

        document.getElementById('shipperSeal').disabled = true;
        }  if (PermissionInputs.find(x => x.fieldName == "MultipleVehicleSelection" && x.isChecked == false)) {

        document.getElementById('hideshow').disabled = true;
        }  if (PermissionInputs.find(x => x.fieldName == "Remarks" && x.isChecked == false)) {

        document.getElementById('remarks').disabled = true;
        }
    loadGrid()

    }


