    var CargoReceivedId = 0;
    var clearingAgentExportId = 0;
    var loadingProgramDetail = [];
    var loadingProgramId = 0;
    var lpnumber;
    var StartTimedateBox;
    var StartTimedateBoxValue;
    var EndTimedateBox;
    var EndTimedateBoxValue;
    var TruckNumbers = [];
    var TruckNumber = 0;

    $(function () {

        $('#clearingAgentExport').css('pointer-events', 'none');
    $("#clearingAgentCNIC").inputmask("99999-9999999-9");
    $("#driverCNIC").inputmask("99999-9999999-9");

    })

    function lpnumber_change(data) {
        TruckNumbers = []
        lpnumber = data.value;
    GetTruckNumbers(lpnumber)

    lpnumberEnter(lpnumber);

    findCargoReceived(lpnumber, TruckNumber)
    }

    function GetTruckNumbers(lpnumber) {
        $.get('/APICalls/GetTurckNumbersBYLPNo?lpNumber=' + lpnumber, function (data) {
            TruckNumbers = data;
            if (TruckNumbers) {
                $("#TruckNumbererror").dxSelectBox({
                    dataSource: TruckNumbers,
                    displayExpr: "value",
                    valueExpr: 'value',
                    acceptCustomValue: true,
                    onValueChanged: function (data) {
                        TruckNumber = data.value;
                        findCargoReceived(lpnumber, TruckNumber);
                    },
                })
            }





        });
    }

    function gateInEDI() {


        $("#btnEDI").attr("disabled", true);
    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

    $.get('SendOGIE?CargoRecievedId=' + CargoReceivedId, function (resp) {

           // showToast("EDI message sent!", "success");
            if (resp.error) {
        showToast(resp.message, "warning");
            }

    else {

        showToast(resp.message, "success");
                
            }
        });
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

    function findCargoReceived(lpnumber, TruckNumber) {

        $.get('/APICalls/GetCargoReceive?lpnumber=' + lpnumber + '&TruckNo=' + TruckNumber, function (data) {

            $('#gdNumber').val(data.gdNumber);
            if (data.vehicleReceivedDate) {
                $('#vehicleReceivedDate').val(formatDate(data.vehicleReceivedDate));
            }
            if (data.vehicleReceivedDate == null) {
                $('#vehicleReceivedDate').val('');
            }

            $('#clearingAgent').val(data.clearingAgent);
            $('#cLearingAgentReprsentative').val(data.agentRepresentative);
            $('#clearingAgentCNIC').val(data.agentCNIC);
            $('#packageType').val(data.packageType);
            $('#driverName').val(data.driverName);
            $('#driverCNIC').val(data.driverCNIC);
            $('#weightDeclaredByDriver').val(data.weightDeclaredByDriver);
            $('#numberOfPackagesReceived').val(data.numberOfPackagesReceived);
            if (data.recevingStartTime) {
                $("#recevingStartTime").dxDateBox("instance").option({ value: new Date(formatDateDev(data.recevingStartTime)) })
                $("#recevingEndTime").dxDateBox("instance").option({ value: new Date(formatDateDev(data.recevingEndTime)) })
            }
            else {
                $("#recevingStartTime").dxDateBox("instance").option({ value: new Date(formatDateDev(new Date())) })
                $("#recevingEndTime").dxDateBox("instance").option({ value: new Date(formatDateDev(new Date())) })
            }

            $('#cargoRecevingCondition').val(data.cargoRecevingCondition);

            CargoReceivedId = data.cargoReceivedId;
            clearingAgentExportId = data.clearingAgentExportId
            loadingProgramId = data.loadingProgramId
            if (data.cargoReceivedId) {
                printBtnShowHide(true);
                updateBtnShowHide(true);
                submitBtnShowHide(false);
            }
            else {
                updateBtnShowHide(false);
                submitBtnShowHide(true);
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

    function lpnumberEnter(data) {
        lpnumber = data;
    $.get('/APICalls/GetLoadingProgramDetailByLPNumber?lpNumber=' + lpnumber, function (data) {
        loadingProgramDetail = data
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


        });

    }

    function printBtnShowHide(show) {
        var btn = document.getElementById("btn2");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }


    function AddCargoReceived() {
        if (TruckNumber) {
        StartTimedateBox = $("#recevingStartTime").dxDateBox("instance");

    StartTimedateBoxValue = StartTimedateBox.option('value')

    EndTimedateBox = $("#recevingEndTime").dxDateBox("instance");

    EndTimedateBoxValue = EndTimedateBox.option('value')

    var f = document.getElementById('CargoReceivedForm');

    if (f.checkValidity()) {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

    var values = $('#CargoReceivedForm').serialize();

    $.post('AddCargoReceived?' + values, {TruckNumber: TruckNumber, loadingProgramId: loadingProgramId, clearingAgentExportId: clearingAgentExportId }, function (data) {
                    if (data.error) {
        showToast(data.message, "warning");
                    }

    else {
        CargoReceivedId = data.id;
    showToast(data.message, "success");
    printBtnShowHide(true);
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
        alert('Please Select Truck Number')
    }

    }

    function checkValidity() {

        if (!$('#clearingAgent').val()) {

        $('#ClearingAgenterror').html('This is a required field');
        }
    else {
        $('#ClearingAgenterror').html('');
        }


    if (!$('#cLearingAgentReprsentative').val()) {

        $('#CLearingAgentReprsentativeerror').html('This is a required field');
        }
    else {
        $('#CLearingAgentReprsentativeerror').html('');
        }


    if (!$('#clearingAgentCNIC').val()) {

        $('#ClearingAgentCNICerror').html('This is a required field');
        }
    else {
        $('#ClearingAgentCNICerror').html('');
        }


    if (!$('#driverName').val()) {

        $('#DriverNameerror').html('This is a required field');
        }
    else {
        $('#DriverNameerror').html('');
        }


    if (!$('#driverCNIC').val()) {

        $('#DriverCNICerror').html('This is a required field');
        }
    else {
        $('#DriverCNICerror').html('');
        }


    if (!$('#packageType').val()) {

        $('#PackageTypeerror').html('This is a required field');
        }
    else {
        $('#PackageTypeerror').html('');
        }


    if (!$('#numberOfPackagesReceived').val()) {

        $('#NumberOfPackagesReceivederror').html('This is a required field');
        }
    else {
        $('#NumberOfPackagesReceivederror').html('');
        }


    if (StartTimedateBoxValue == null) {

        $('#RecevingStartTimeerror').html('This is a required field');
        }
    else {
        $('#RecevingStartTimeerror').html('');
        }


    if (EndTimedateBoxValue == null) {

        $('#RecevingEndTimeerror').html('This is a required field');
        }
    else {
        $('#RecevingEndTimeerror').html('');
        }


    if (!$('#cargoRecevingCondition').val()) {

        $('#CargoRecevingConditionerror').html('This is a required field');
        }
    else {
        $('#CargoRecevingConditionerror').html('');
        }

    if (!$('#weightDeclaredByDriver').val()) {

        $('#WeightDeclaredByDrivererror').html('This is a required field');
        }
    else {
        $('#WeightDeclaredByDrivererror').html('');
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

    function updateCargoReceived() {


        StartTimedateBox = $("#recevingStartTime").dxDateBox("instance");

    StartTimedateBoxValue = StartTimedateBox.option('value')

    EndTimedateBox = $("#recevingEndTime").dxDateBox("instance");

    EndTimedateBoxValue = EndTimedateBox.option('value')


    var f = document.getElementById('CargoReceivedForm');
    if (CargoReceivedId != 0) {

            if (f.checkValidity()) {



        $("#btnSubmitUpdate").attr("disabled", true);
    setTimeout(function () {$("#btnSubmitUpdate").attr("disabled", false); }, 120000);


    var values = $('#CargoReceivedForm').serialize();
    $.post('/Export/CargoReceived/UpdateCargoReceived?' + values + '&TruckNumber=' + TruckNumber + '&CargoReceivedId=' + CargoReceivedId + "&clearingAgentExportId=" + clearingAgentExportId, function (data) {

        showToast("Updated", "success");
                })
            }
    else {
        alert("Invalid form values");
            }

    checkValidity();

        }
    }

    function restform() {
        var r = confirm("Are you sure ");
    if (r == true) {
        window.location.href = window.location.href
    }

    }



    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "LPNumber" && x.isChecked == false)) {

        document.getElementById('loadingProgramDetail').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "TruckNumber" && x.isChecked == false)) {

        document.getElementById('truckNumberBox').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "AgentRepresentative" && x.isChecked == false)) {

        document.getElementById('cLearingAgentReprsentative').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "AgentCNIC" && x.isChecked == false)) {

        document.getElementById('clearingAgentCNIC').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "ShipmentStatus" && x.isChecked == false)) {

        document.getElementById('packageType').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "DriverName" && x.isChecked == false)) {

        document.getElementById('driverName').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "DriverCNIC" && x.isChecked == false)) {

        document.getElementById('driverCNIC').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "WeightDeclaredByDriver" && x.isChecked == false)) {

        document.getElementById('weightDeclaredByDriver').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "NoOfPackagesReceived" && x.isChecked == false)) {

        document.getElementById('numberOfPackagesReceived').disabled = true;
        }
        if (PermissionInputs.find(x => x.fieldName == "RecevingStart" && x.isChecked == false)) {

        document.getElementById('recevingStartTime').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "RecevingEnd" && x.isChecked == false)) {

        document.getElementById('recevingEndTime').style.display = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "CRCondition" && x.isChecked == false)) {

        document.getElementById('cargoRecevingCondition').disabled = true;
        }


        


            if (PermissionInputs.find(x => x.fieldName == "btnEDI" && x.isChecked == false)) {

        document.getElementById('btnEDI').style.display = "none";
     
        }
         
    }

