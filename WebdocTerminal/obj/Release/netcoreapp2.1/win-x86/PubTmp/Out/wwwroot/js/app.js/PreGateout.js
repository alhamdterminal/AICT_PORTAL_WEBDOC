
    var ccmo = [];

    //$(function () {

        //    loadGrid();

        //});
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

 
          if (PermissionInputs.find(x => x.fieldName == "VirNo" && x.isChecked == false)) {

        checkColumn(e, "virNo");
        }

        if (PermissionInputs.find(x => x.fieldName == "BlNo" && x.isChecked == false)) {

        checkColumn(e, "blNo");
        }

        if (PermissionInputs.find(x => x.fieldName == "VehicleNo" && x.isChecked == false)) {

        checkColumn(e, "vehicleNo");
        }
         if (PermissionInputs.find(x => x.fieldName == "Status" && x.isChecked == false)) {

        checkColumn(e, "status");
        }  
    }

    function loadGrid() {

        $.get('/APICalls/GetUnAssignPGOO', function (data) {
            ccmo = data;
            console.log(ccmo)


            var dataGrid = $("#gateinContainer").dxDataGrid({
                dataSource: ccmo,
                keyExpr: "containerNo",
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
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
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [
                    "bondedCarrierId",
                    "bondedCarrierName",
                    "containerNo",
                    "vehicleNo",
                    // "virNo", 
                    {
                        caption: "",
                        dataField: "status",
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



        });

    }
    function savePGOO() {
        var data = ccmo.filter(c => c.status == true);
    console.log(data);

    $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);


    $.post('CreatePreGateOut', {data: data }, function (data) {

            
            if (data.error) {
        showToast(data.message, "error");
    window.location.href = window.location.href;
    return;
            }
    else {
        showToast(data.message, "success");
    window.location.href = window.location.href;

            }
    window.location.href = window.location.href;


        });
    }

    function formfiled() {

        loadGrid();

    }

