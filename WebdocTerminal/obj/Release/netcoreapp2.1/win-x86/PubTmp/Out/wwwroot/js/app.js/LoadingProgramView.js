
    var LPNumber = 0;
    var loadingProgramId = 0;
    var loadingProgramDetail = [];
    var voyageExportId = 0;
    var vesselExportId = 0;
    var VoyageExports = []

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

    function formatDateDev(date) {
        var d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();

    return [month, day, year].join('/');
    }

    $(function () {

        var url_string = window.location.href
    var url = new URL(url_string);
    var lpNumber = url.searchParams.get("lpNumber");

    if (lpNumber) {

        updateBtnShowHide(true);
    submitBtnShowHide(false);


    $.get('/Export/LoadingProgram/GetLoadingProgramByLPNumber?lpNumber=' + lpNumber, function (data) {
                if (data.loadingProgramDate) {
        $("#invDate").dxDateBox("instance").option({ value: new Date(formatDateDev(data.invoiceDate)) })
                    $("#lpdate").dxDateBox("instance").option({value: new Date(formatDateDev(data.loadingProgramDate)) })
                }
    else {
        $("#invDate").dxDateBox("instance").option({ value: new Date(formatDateDev(new Date())) })
                    $("#lpdate").dxDateBox("instance").option({value: new Date(formatDateDev(new Date())) })
                }

    voyageExportId = data.voyageExportId;

    $('#lpnumber').val(data.loadingProgramNumber);
    $('#lpdate').val(formatDate(data.loadingProgramDate));
    $('#rfNumber').val(data.referenceNumber);
    $('#destination').val(data.destination);
    $('#invnumber').val(data.invoiceNumber);
    $('#invDate').val(formatDate(data.invoiceDate));

    $('#vesselExports').val(data.vesselExportId);
    $('#voyageExport').val(data.voyageExportId);
    $('#cutOff').val(data.cargoCutOff);
    $('#vesseletd').val(data.vesselETD);
    $('#loadingTeminal').val(data.portAndTerminalId);
    $('#shippingLine').val(data.shippingLineId);
    $('#shipper').val(data.shipperId);
    $('#clearingAgentExport').val(data.clearingAgentExportId);
    $('#shippingAgent').val(data.shippingAgentExportId);
    callChangefunc(data.voyageExportId)
    loadingProgramId = data.loadingProgramId;
    loadingProgramDetail = data.loadingProgramDetails;
    changed_VesselExport(data.vesselExportId, voyageExportId)

    loadGrid();


            })

        }
    else {
        getLPNumber();
    loadGrid();
        }
    });

    function getLPNumber() {
        $.get('/Export/LoadingProgram/GenPCode', function (data) {
            LPNumber = data;
            $('#lpnumber').val(LPNumber);
        });
    }

    function formatDate(date) {
        var d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();
    hour = d.getHours();
    Minutes = d.getMinutes(),
    Seconds = d.getSeconds()

    if (month.length < 2)
    month = '0' + month;
    if (day.length < 2)
    day = '0' + day;

    return [year, month, day].join('-');
    }

    function submitLoadingProgram() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

    var f = document.getElementById('LoadingPrgramForm');

    if (f.checkValidity()) {

            var values = $('#LoadingPrgramForm').serialize();

    var voyageID = voyageExportId;

            if (!(voyageExportId > 0)) {

        $('#VoyageExporterror').html('This is a required field');
            }
    else {
        $('#VoyageExporterror').html('');
    if (voyageID) {

                    if (loadingProgramDetail.length > 0) {

        $.post('AddLoadingProgram?' + values + '&voyageID=' + voyageID, { loadingProgramDetail: loadingProgramDetail }, function (data) {
            if (data.error) {
                showToast(data.message, "warning");
            }

            else {
                loadingProgramId = data.id;
                showToast(data.message, "success");
                getLPNumber();
                $('#rfNumber').val('');
                updateBtnShowHide(false);
                submitBtnShowHide(true);
            }

            window.location.href = window.location.href


        })
    }
    else {
        alert("Add at least 1 PO");
                    }
                }

            }



        }

    checkValidations();
    }

    function updateLoadingProgram() {

        var f = document.getElementById('LoadingPrgramForm');

    if (f.checkValidity()) {

            var voyageID = $("#voyageSelectBox").dxSelectBox('instance').option('value');

    if (!voyageID) {

        $('#VoyageExporterror').html('This is a required field');
            }
    else {
        $('#VoyageExporterror').html('');
    if (voyageID) {

                    var values = $('#LoadingPrgramForm').serialize();

    $.post('/Export/LoadingProgram/UpdateLoadingProgram?' + values + '&loadingProgramId=' + loadingProgramId + '&voyageID=' + voyageID, {loadingProgramDetails: loadingProgramDetail }, function (data) {

        showToast("Updated", "success");
                    })
                }

            }

        }

    checkValidations();
    }

    function checkValidations() {

        if (!$('#rfNumber').val()) {

        $('#ReferenceNumbererror').html('This is a required field');
        }
    else {
        $('#ReferenceNumbererror').html('');
        }

    if (!$('#destination').val()) {

        $('#Destinationerror').html('This is a required field');
        }
    else {
        $('#Destinationerror').html('');
        }
    if (!$('#vesselExports').val()) {

        $('#VesselExporterror').html('This is a required field');
        }
    else {
        $('#VesselExporterror').html('');
        }

    if (!$('#shippingAgent').val()) {

        $('#ShippingAgenterror').html('This is a required field');
        }
    else {
        $('#ShippingAgenterror').html('');
        }

    if (!$('#loadingTeminal').val()) {

        $('#PortAndTerminalerror').html('This is a required field');
        }
    else {
        $('#PortAndTerminalerror').html('');
        }


    if (!$('#clearingAgentExport').val()) {

        $('#ClearingAgenterror').html('This is a required field');
        }
    else {
        $('#ClearingAgenterror').html('');
        }

    if (!$('#shippingLine').val()) {

        $('#ShippingLineerror').html('This is a required field');
        }
    else {
        $('#ShippingLineerror').html('');
        }

    if (!$('#shipper').val()) {

        $('#Shippererror').html('This is a required field');
        }
    else {
        $('#Shippererror').html('');
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
    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {
  
               

        if (PermissionInputs.find(x => x.fieldName == "PONumber" && x.isChecked == false)) {

        checkColumn(e, "poNumber");
        } if (PermissionInputs.find(x => x.fieldName == "Style" && x.isChecked == false)) {

        checkColumn(e, "style");
        } if (PermissionInputs.find(x => x.fieldName == "TotalPackges" && x.isChecked == false)) {

        checkColumn(e, "totalPackages");
        } if (PermissionInputs.find(x => x.fieldName == "PackgeType" && x.isChecked == false)) {

        checkColumn(e, "packageType");
        } if (PermissionInputs.find(x => x.fieldName == "TotalPices" && x.isChecked == false)) {

        checkColumn(e, "totalPieces");
        } if (PermissionInputs.find(x => x.fieldName == "InsDate" && x.isChecked == false)) {

        checkColumn(e, "insDate");
        } if (PermissionInputs.find(x => x.fieldName == "DocsDate" && x.isChecked == false)) {

        checkColumn(e, "docsDate");
        } if (PermissionInputs.find(x => x.fieldName == "Remarks" && x.isChecked == false)) {

        checkColumn(e, "remarks");
        }
    
    }
    function loadGrid() {

        $.get("/APICalls/GetAllExportPackages", function (packageTypes) {

            var dataGrid = $("#loadingPrograms").dxDataGrid({
                dataSource: loadingProgramDetail,
                keyExpr: "loadingProgramDetailId",
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
                    allowUpdating: true,
                    allowDeleting: true,
                    allowAdding: true
                },
                columns: [
                    "poNumber",
                    "style",
                    "totalPackages",
                    {
                        dataField: "packageType",
                        caption: "Package Type",
                        lookup: {
                            dataSource: packageTypes,
                            displayExpr: "packageType",
                            valueExpr: "packageType"
                        }
                    },
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
                    hideMenifestedColumns(e);
                },
                onRowRemoved: function (e) {
                    var loadingProgramDetailId = e.data.loadingProgramDetailId;

                    $.post('/Export/LoadingProgram/DeleteLoadingProgramDetail?loadingProgramDetailId=' + loadingProgramDetailId, function (data) {

                    });
                }

            }).dxDataGrid("instance");

        });

    }


    function callChangefunc(val) {

        var VoyageExportId = val;
    $.get("/Export/LoadingProgram/GetVoyageExportById?VoyageExportId=" + VoyageExportId, function (data) {
            if (data) {
        $('#eta').val(formatDate(data.eta));
    $('#etd').val(formatDate(data.etd));
    $('#cutoff').val(formatDate(data.cutOff));
            }

        })

    }

    function restform() {
        var r = confirm("Are you sure");
    if (r == true) {
        window.location.href = window.location.href
    }

    }


    function changed_VesselExport(arg, voyageid) {

        vesselExportId = arg;

    $.get('/APICalls/GetVoyagesFormvesselExportId?vesselExportId=' + vesselExportId, function (data) {

        VoyageExports = data
            if (VoyageExports[0]) {

                if (voyageid > 0)
    voyageExportId = voyageid;
    else
    voyageExportId = VoyageExports[0].voyageExportId

    callChangefunc(voyageExportId)

            }
    else {
        $('#eta').val('');
    $('#etd').val('');
    $('#cutoff').val('');
            }


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
                if (!(typeof data.value === 'string'))
                    callChangefunc(voyageExportId)
            },
        })
    }

        });


    }


    function formfiled() {
          if (PermissionInputs.find(x => x.fieldName == "LoadingProgramDate" && x.isChecked == false)) {

        document.getElementById('lpdate').style.pointerEvents = "none";
        } if (PermissionInputs.find(x => x.fieldName == "ReferenceNumber" && x.isChecked == false)) {

        document.getElementById('rfNumber').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "InvoiceNumber" && x.isChecked == false)) {

        document.getElementById('invnumber').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "InvoiceDate" && x.isChecked == false)) {

        document.getElementById('invDate').style.pointerEvents = "none";
        } if (PermissionInputs.find(x => x.fieldName == "Shipper" && x.isChecked == false)) {

        document.getElementById('shipper').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "Vessel" && x.isChecked == false)) {

        document.getElementById('vesselExports').disabled = true;
        }   if (PermissionInputs.find(x => x.fieldName == "Voyage" && x.isChecked == false)) {

        document.getElementById('voyageSelectBox').style.pointerEvents = "none";
        }
        if (PermissionInputs.find(x => x.fieldName == "ClearingAgent" && x.isChecked == false)) {

        document.getElementById('clearingAgentExport').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ShippingAgent" && x.isChecked == false)) {

        document.getElementById('shippingAgent').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "LoadingTerminal" && x.isChecked == false)) {

        document.getElementById('loadingTeminal').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "Destination" && x.isChecked == false)) {

        document.getElementById('destination').disabled = true;
        } if (PermissionInputs.find(x => x.fieldName == "ShippingLine" && x.isChecked == false)) {

        document.getElementById('shippingLine').disabled = true;
        } 
    }

