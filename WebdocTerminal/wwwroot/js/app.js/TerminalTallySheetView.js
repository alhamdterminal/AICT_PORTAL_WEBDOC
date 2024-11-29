
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
         if (PermissionInputs.find(x => x.fieldName == "Weight" && x.isChecked == false)) {

        checkColumn(e, "weight");
        }
        if (PermissionInputs.find(x => x.fieldName == "Status" && x.isChecked == false)) {

        checkColumn(e, "status");
        }
              

        

    }

    function loadGrid() {

        $.get('/APICalls/GetCCMOForTTSO', function (data) {
            ccmo = data;
            console.log(ccmo)


            var dataGrid = $("#gateinContainer").dxDataGrid({
                dataSource: ccmo,
                keyExpr: "ccmoId",
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
                    "weight",
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
    function saveTTSO() {
        var data = ccmo.filter(c => c.status == true);
    console.log(data);
    $.post('CreateTerminalTallySheet', {data: data }, function (data) {

        showToast("TTSO Saved", "success");
    loadGrid();

        });
    }


    function formfiled() {

        loadGrid()

    }