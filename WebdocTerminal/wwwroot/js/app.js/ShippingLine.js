
    var shippingLines = [];


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function formfiled() {
        loadGrid();

    }
    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {


        if (PermissionInputs.find(x => x.fieldName == "ShippingLineName" && x.isChecked == false)) {

        checkColumn(e, "shippingLineName");
        }
        if (PermissionInputs.find(x => x.fieldName == "ShippingLineCode" && x.isChecked == false)) {

        checkColumn(e, "shippingLineCode");
        }
    }

    function loadGrid() {


        $.get('/ShippingLine/GetShippingLines', function (data) {

            shippingLines = data;

            console.log(shippingLines)



            var dataGrid = $("#shippingLine").dxDataGrid({
                dataSource: shippingLines,
                keyExpr: "shippingLineId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                columnAutoWidth: true,
                allowColumnResizing: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
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
                    allowUpdating: true,
                    allowDeleting: false,
                    allowAdding: true
                },
                columns: [
                    {
                        dataField: "shippingLineName",
                        validationRules: [{ type: "required" }],
                        caption: "Line Name"

                    },
                    {
                        dataField: "shippingLineCode",
                        caption: "Line Code"

                    },
                    {
                        dataField: "ntnNumber",
                        validationRules: [{ type: "required" }],

                        caption: "NTN"

                    },


                ],
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onEditingStart: function (e) {
                },
                onInitNewRow: function (e) {
                },
                onRowInserting: function (e) {
                    console.log(e)


                },
                onRowInserted: function (e) {

                    var values = e.data;

                    if (values.shippingLineName || values.shippingLineCode) {
                        $.post('/ShippingLine/AddShippingLine', { values: values }, function (data) {

                            if (data.error == true) {
                                alert(data.message);
                                window.location.href = window.location.href;
                            }
                            else {
                                alert(data.message);

                                window.location.href = window.location.href;

                            }


                        });
                    }

                },
                onRowUpdating: function (e) {

                },
                onRowUpdated: function (e) {
                    console.log(e);
                    var values = e.data;

                    $.post('/ShippingLine/updateShippingLine', { shippingLine: values }, function (data) {

                        if (data.error == true) {
                            alert(data.message);

                            window.location.href = window.location.href;
                        }
                        else {
                            alert(data.message);

                            window.location.href = window.location.href;
                        }


                    });
                },
                onRowRemoving: function (e) {

                },
                onRowRemoved: function (e) {

                    console.log(e)
                    var key = e.data.shippingLineId;
                    console.log(key);

                    $.post('/ShippingLine/Delete', { key, key }, function (data) {

                        alert(data.message);

                        window.location.href = window.location.href;
                    });

                }
            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');






        });
    }




