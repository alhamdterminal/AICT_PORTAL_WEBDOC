
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

        if (PermissionInputs.find(x => x.fieldName == "IndexNo" && x.isChecked == false)) {

        checkColumn(e, "indexNo");
        }
         if (PermissionInputs.find(x => x.fieldName == "ContainerNo" && x.isChecked == false)) {

        checkColumn(e, "containerNo");
        }
        if (PermissionInputs.find(x => x.fieldName == "Weight" && x.isChecked == false)) {

        checkColumn(e, "weight");
        }
                  if (PermissionInputs.find(x => x.fieldName == "NoOfPackages" && x.isChecked == false)) {

        checkColumn(e, "noOfPackages");
        }

        if (PermissionInputs.find(x => x.fieldName == "Status" && x.isChecked == false)) {

        checkColumn(e, "status");
        }
         

    }

    function loadGrid(containerNumer) {

        $.get('/APICalls/GetUnAssignSCMO?containerNumer=' + containerNumer, function (data) {
            ccmo = data;
            console.log(ccmo)


            var dataGrid = $("#gateinContainer").dxDataGrid({
                dataSource: ccmo,
                keyExpr: "ccmoId",
                wordWrapEnabled: true,
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
                    "virNo",
                    "blNo",
                    "indexNo",
                    "containerNo",
                    "containerNumberIGMO",
                    "weight",
                    "noOfPackages",
                    "tpNo",
                    "vehicleNo",
                    "bondedCarrierName",
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

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });

    }


    function saveSCMO() {
        var data = ccmo.filter(c => c.status == true);
    console.log(data);

    $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);


    $.post('CreateStuffingConfirmation', {data: data }, function (data) {

            
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



    //    function formfiled() {
        //        loadGrid();
        //}



        function containerNumber_changed(data) {

            var containerNumer = data.selectedItem;
            console.log("data", data)
            getccmodetailbyContainerNo(containerNumer)

        }

    function getccmodetailbyContainerNo(containerNumer) {
        //  $.get('/APICalls/GetUnAssignSCMO?containerNumer=' + containerNumer, function (data) {

        loadGrid(containerNumer);

     //   })
    }
