


    function container_changed(data) {

        var ExportContainerId = data;

    console.log("ExportContainerId", ExportContainerId);

    $.get('/Export/CargoRollOver/GetGDDeatilbyexportcontainerid?ExportContainerId=' + ExportContainerId, function (res) {

        console.log("res", res);

    $("#gridContainer").dxDataGrid({
        dataSource: res,
    keyExpr: "exportContainerItemId",
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    paging: {
        pageSize: 10
                },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."
                },

    editing: {
        mode: "row",
    allowUpdating: false,
    allowAdding: false
                },
    columns: [
    {
        dataField: "gdNumber",


                    },
    {
        dataField: "allowLoading",


                    },
    {
        dataField: "containerNumber",

                    },
    {
        dataField: "shipperName",
                    },
    {
        dataField: "isAmountReceived",
                    },
    ],

            });

        });

    }



    function changed_VesselExport(arg) {



        $('#voyageExportidres')
            .empty()
            .append('<option selected="selected" value="">Select Voyage</option>');



    $.get('/APICalls/GetVoyagesFormvesselExportId?vesselExportId=' + arg, function (data) {

        VoyageExports = data;

    if (VoyageExports.length) {
        console.log("VoyageExports", VoyageExports);

    var options = VoyageExports.map(function (val, ind) {
                    return $("<option></option>").val(val.voyageExportId).html(val.voyageNumber);
                });
    console.log("options", options);

    $('#voyageExportidres').append(options);
            }
    else {
        $('#voyageExportidres')
            .empty()
            .append('<option selected="selected" value="">Select Voyage</option>');

            }

        });


    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function addCargoRollOverContainerwise() {

        checkFiledDataContainer();

    if (result == false) {

            var x = confirm("Are you sure update container wise ");
    if (x == true) {


                var exportcontainerid = $('#containerNumber').val();
    var voyageExportId = $('#voyageExportidres').val();
    var vesselExportId = $('#vesselExport').val();

    $.post('AddCargoRollOverContainerWise', {exportcontainerid: exportcontainerid, voyageExportId: voyageExportId, vesselExportId: vesselExportId }, function (data) {

                    if (data.error) {
        showToast(data.message, "warning");
                    }
    else {

        showToast(data.message, "success");


                    }

                })
            }
    else {
        window.location.reload();
            }
        }
    }


    var result;


    function checkFiledDataContainer() {

        result = false;


    if (!$('#containerNumber').val()) {

        result = true;
    return showToast("please select container number", "error");
        }

    if (!$('#vesselExport').val()) {
        result = true;
    return showToast("please select Vessel", "error");
        }

    if (!$('#voyageExportidres').val()) {
        result = true;
    return showToast("please select Voyage Export", "error");
        }

    }


    function formfiled() {

    }
