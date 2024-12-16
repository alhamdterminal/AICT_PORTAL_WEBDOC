
    var items = [];

    //$(function () {

        //    //$('#DONumber').change(function () {

        //    //    gd = $(this).find("option:selected").val();

        //    //    getDeliveryOrderDisplayInfo(gd);
        //    //    displayGrid();

        //    //});

        //})


        function gdNumber_changed(value) {

            console.log("value", value);

            getDeliveryOrderDisplayInfo(value);

        }


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function getDeliveryOrderDisplayInfo(val) {

        $.get('/ApiCalls/GetExportGatePassDisplayInfo?gdnumber=' + val, function (resp) {

            console.log("resp", resp);
            displayInfo(resp);

        })

    }

    function displayInfo(info) {


        if (info) {
        $('#status').val(info.drayOffStatus);
    $("#gd-date").val(info.gdDate)
    $("#invoice-no").val(info.invoiceNo);
    $("#clearing-agent").val(info.clearingAgent);
    $("#challan-number").val(info.challanNumber);
    $("#shipper").val(info.shipper);
    $("#packages").val(info.noOfPackages);
    $("#agent-representative").val(info.agentRepresentative);
    $("#representative-cnic").val(info.agentRepresentativeCNIC);
    $("#total-delivered").val(info.totalDelivered);
    $("#remaining").val(info.balancePackages);
    $("#enteringCargoId").val(info.enteringCargoId);
    $("#exportDeliveryOrderId").val(info.exportDeliveryOrderId);

    if (info.doItemExports.length) {
        displayGrid(info.doItemExports);
            }
    else {
        displayGrid([]);
            }
        }
    else {

        $('#status').val('');
    $("#gd-date").val('')
    $("#invoice-no").val('');
    $("#clearing-agent").val('');
    $("#challan-number").val('');
    $("#shipper").val('');
    $("#packages").val('');
    $("#agent-representative").val('');
    $("#representative-cnic").val('');
    $("#total-delivered").val('');
    $("#remaining").val('');
    $("#enteringCargoId").val('');
    $("#exportDeliveryOrderId").val('');

    displayGrid([]);
        }






    }



    function displayGrid(result) {

        //   $.get('/Export/PackageTypeExport/GetPackageTypeExport', function (data) {
        // var PackageTypes = data;

        var dataGrid = $("#gridContainer").dxDataGrid({
        dataSource: result,
    keyExpr: "gatePassExportId",
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

    editing: {
        mode: "row",
    allowUpdating: false,
    allowDeleting: true,
    allowAdding: false
            },
    columns: [
    "truckNumber",
    "numberOfPackage",
    "status",
    {
        dataField: "gatePassExportId",
    caption: 'S No.',
    cellTemplate: function (container, options) {
        $("<button type='button' class='btn btn-success' onClick='routeToReport(" + options.value + ")'>Print</button>")
            .appendTo(container);
                    }, allowEditing: false
                },
    ],
    onEditorPreparing: function (e) {

    },
    onRowRemoved: function (e) {
        console.log(e)
                var key = e.data.doItemExportId;
    $.post('/Export/Delivery/DeleteGatePass?key=' + key, function (data) {
        showToast(data.message, data.error ? "error" : "success");

    var gdno = $('#gdnumbers').val();

    getDeliveryOrderDisplayInfo(gdno);
                });
            }

        }).dxDataGrid("instance");
        //  })
    }


    function createGatePass() {

        checkFiledData();

    if (result == false) {


            var gatepass = {
        gdNumber: $('#gdnumbers').val(),
    enteringCargoId: $('#enteringCargoId').val(),
    exportDeliveryOrderId: $('#exportDeliveryOrderId').val(),
    remarks: $('#remarks').val(),
    truckNumber: $('#truckNumber').val(),
    numberOfPackges: $('#numberOfPackage').val(),
    packgesType: $('#packageType').val(),
    noOfPackages: $('#packages').val(),
    totalDelivered: $('#total-delivered').val(),
            };

    console.log("gatepass", gatepass)


    $.post("/Export/Delivery/CreateGatePass", {model: gatepass }, function (resp) {

        showToast(resp.message, resp.error ? "error" : "success");

    getDeliveryOrderDisplayInfo(gatepass.gdNumber);

            });
        }

        //$.post("/Export/Delivery/CreateGatePass", {doNumber: d_no, remarks: remarks, items: items }, function (resp) {
        //    getDeliveryOrderDisplayInfo(gd);

        //    showToast(resp.message, resp.error ? "error" : "success");

        //    if (resp.error == false) {
        //        var btnrout = document.getElementById("reportBtn");
        //        btnrout.style.display = 'inline-grid';
        //        gatePassId = resp.gatePassExportId
        //    }
        //    else {
        //        var btnrout = document.getElementById("reportBtn");
        //        btnrout.style.display = 'none';
        //    }
        //})
    }


    var result;
    function checkFiledData() {

        result = false;


    if (!$('#gdnumbers').val()) {

        result = true;
    return showToast("please select gd number", "error");
        }

    if (!$('#enteringCargoId').val()) {
        result = true;
    return showToast("there is no cargo code", "error");
        }


    if (!$('#exportDeliveryOrderId').val()) {
        result = true;
    return showToast("there is no do code", "error");
        }

    if (!$('#remarks').val()) {
        result = true;
    return showToast("please add remarks", "error");
        }

    if (!$('#truckNumber').val()) {
        result = true;
    return showToast("please add Truck Number", "error");
        }
    if (!$('#numberOfPackage').val()) {
        result = true;
    return showToast("please add number Of Package", "error");
        }

    if (!$('#packageType').val()) {
        result = true;
    return showToast("please select Package Type", "error");
        }


    if (!$('#packages').val()) {
        result = true;
    return showToast("there is no packages", "error");
        }

    }



    function routeToReport(gatePassId) {

        window.open('/Export/Reports/ExportGatePassDrayOff?gatePassExportId=' + gatePassId, '_blank');

    }
    function formfiled() {

    }
