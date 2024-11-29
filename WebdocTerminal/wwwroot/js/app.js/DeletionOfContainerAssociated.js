

    var exportContainerItems = [];
    var shippingLine = [];
    var containerNumber;
    $(function () {
        var url_string = window.location.href
    var url = new URL(url_string);
    exportContainerId = url.searchParams.get("exportContainerId");


    $("#exportContainer").change(function () {

        GetExportContainerItems()
    });


    });

    function GetExportContainerItems() {
        var ContainerNumber = $('#exportContainer option:selected').text();
    console.log("ContainerNumber", ContainerNumber)
    loadGrid(ContainerNumber)

    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        checkColumn(e, "gdnumber");
    checkColumn(e, "noOfPackages");
    checkColumn(e, "destination");
    checkColumn(e, "shipperId");

    }



    function loadGrid(ContainerNumber) {
        console.log("ContainerNumber2", ContainerNumber)

        $.get('/Export/ExportContainer/GetExportContainerItemsBYContainerNumber?ContainerNumber=' + ContainerNumber, function (data) {
            if (data) {
        $.get('/ShippingLine/GetShippingLines', function (ShippingLinedata) {
            shippingLine = ShippingLinedata;

            exportContainerItems = data;



            var dataGrid = $("#ExportContainerItem").dxDataGrid({
                dataSource: exportContainerItems,
                keyExpr: "exportContainerItemId",
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
                    mode: "row",
                    allowDeleting: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [
                    "gdNumber",
                    "noOfPackages",
                    {
                        dataField: "shippingLineId",
                        width: 200,
                        caption: "Shipping Line Code",
                        lookup: {
                            dataSource: shippingLine,
                            displayExpr: "shippingLineCode",
                            valueExpr: "shippingLineId"
                        }
                    },



                ],

                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowRemoved: function (e) {

                    var exportContainerItemId = e.key;

                    $.post('/Export/ExportContainer/DeleteContainerByExportContainerItemId?exportContainerItemId=' + exportContainerItemId, function (data) {

                    });
                }

            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });
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


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "ContainerNumber" && x.isChecked == false)) {

        document.getElementById('exportContainer').disabled = true;
        }



    }
