
    var grid;


    $(function () {

        getvalues();
    });
    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {

            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "containerNumber");
    checkColumn(e, "vehicleNumber");
    checkColumn(e, "gdNumber");
    checkColumn(e, "pccssSeal");
    checkColumn(e, "status");
    checkColumn(e, "bondedCarrierName");
    checkColumn(e, "bondedCarrierNTN");
        //if (PermissionInputs.find(x => x.fieldName == "Status" && x.isChecked == false)) {

        //    document.getElementById('status').disabled = true;
        //}
        if (PermissionInputs.find(x => x.fieldName == "IsCheck" && x.isChecked == false)) {

        document.getElementById('isChecked').disabled = true;
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

    function getvalues() {
        $.get('/APICalls/GetGateOutExport', function (data) {
            loadGrid(data);
        });
    }

    function loadGrid(dataSource) {

        $.get('/APICalls/GetAllVehicles', function (restransportter) {



            var dataGrid = $("#gateOutContainers").dxDataGrid({
                dataSource: dataSource,
                keyExpr: "key",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                columnAutoWidth: true,
                allowColumnResizing: true,
                paging: {
                    enabled: false
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },
                editing: {
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [
                    {
                        dataField: "gdNumber",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "containerNumber",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "vehicleNumber",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "pccssSeal",
                        validationRules: [{ type: "required" }],
                    },
                    {
                        dataField: "bondedCarrierName",
                        validationRules: [{ type: "required" }],

                    },
                    {
                        dataField: "bondedCarrierNTN",
                        validationRules: [{ type: "required" }],

                    },
                    {
                        dataField: "location",
                        validationRules: [{ type: "required" }],
                        lookup: {
                            dataSource: [{ location: "SAPT" }, { location: "QICT" }, { location: "PICT" }, { location: "KICT" }],
                            displayExpr: "location",
                            valueExpr: "location"
                        }
                    },
                    {
                        dataField: "lineSeal",
                        validationRules: [{ type: "required" }],

                    },
                    {
                        dataField: "containerGrossWeight",
                        validationRules: [{ type: "required" }],

                    },
                    {
                        dataField: "transporterId",
                        caption: "Transporter",
                        lookup: {
                            dataSource: restransportter,
                            displayExpr: "transporterName",
                            valueExpr: "transporterId"
                        },
                        validationRules: [{ type: "required" }]

                    },
                    "status",
                    {
                        caption: "",
                        dataField: "isChecked",
                        dataType: "boolean"
                    }
                ],
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {

                    $("#btnSubmit").attr("disabled", false);

                }

            }).dxDataGrid("instance");

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


        });

      


    }

    function save() {


        var Griddata = $("#gateOutContainers").dxDataGrid("option", "dataSource");

        var gateOutcontainers = Griddata.filter(c => c.isChecked == true);

    $("#btnSubmit").attr("disabled", true);
    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        //$.post('SaveGateOutContainers', {containers: gateOutcontainers, portAndterminalId: $('#loadingTeminal option:selected').val(), countryExit: $('#countryExit').val(), weight: $('#weight').val(), seal: $('#seal').val() },
    $.post('SaveGateOutContainers', {containers: gateOutcontainers }, function (data) {

            if (data.error == true) {
        showToast(data.message, "error");

            }
    else {
        showToast(data.message, "success");
    $("#btnSubmit").attr("disabled", true);
    //loadGrid(containers.filter(c => c.isChecked == false));
    getvalues();
            }




        });
    }

    function formfiled() {

        //if (PermissionInputs.find(x => x.fieldName == "ContainerNo" && x.isChecked == false)) {

        //    document.getElementById('exportContainer').style.display = "none";
        //}
        //if (PermissionInputs.find(x => x.fieldName == "PortofExit" && x.isChecked == false)) {

        //    document.getElementById('loadingTeminal').disabled = true;
        //}
        //if (PermissionInputs.find(x => x.fieldName == "ContainerGrossWeight" && x.isChecked == false)) {

        //    document.getElementById('weight').disabled = true;
        //}
        //if (PermissionInputs.find(x => x.fieldName == "LineSeal" && x.isChecked == false)) {

        //    document.getElementById('seal').disabled = true;
        //}

        //console.log("PermissionInputs", PermissionInputs);

        //if (PermissionInputs.find(x => x.fieldName == "btnSubmit" && x.isChecked == false)) {

        //    document.getElementById('btnSubmit').style.display = "none";
        //}

    }


    var containerno = "";
    var gdno = "";

    function container_changed(data) {
        containerno = data.value;
    }

    function gd_changed(data) {
        gdno = data.value;
    }

    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });
    }
    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }

    function myFunction() {

        loadingBar();

    var fromdate = document.getElementById("fromdate").value;
    var todate = document.getElementById("todate").value;

    $.get('/Export/Reports/ViewExportGatePasses?containerNumber=' + containerno + '&' + 'vessel=' + vesselExportId
    + '&' + 'voyage=' + voyageExportId + '&' + 'fromdate=' + fromdate + '&' + 'todate=' + todate + '&gdnumber=' + gdno
    , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }
