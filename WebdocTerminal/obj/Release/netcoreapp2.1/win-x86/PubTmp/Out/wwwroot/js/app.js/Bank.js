


    var bankData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getBanks() {
        $.get('/Import/Bank/GetBank', function (data) {
            bankData = data;
            console.log(bankData);
            bankgrid();
        });
    }


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

      
        if (PermissionInputs.find(x => x.fieldName == "BankCode" && x.isChecked == false)) {

        checkColumn(e, "bankCode");
        }
        if (PermissionInputs.find(x => x.fieldName == "BankName" && x.isChecked == false)) {

        checkColumn(e, "bankName");
        }
    }
    function bankgrid() {
        function logEvent(eventName) {
            var logList = $("#events ul"),
                newItem = $("<li>", { text: eventName });
            logList.prepend(newItem);
        }

        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: bankData,
    keyExpr: "bankId",
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
    allowUpdating: true,
    allowDeleting: false,
    allowAdding: true
            },
    columns: [
    {
        dataField: "bankCode",
    validationRules: [{type: "required" }],

    caption: "Code"
                },
    {
        dataField: "accountNo",
    validationRules: [{type: "required" }],
    dataType: 'number',
    caption: "Account No"
                },
    {
        dataField: "bankName",
    validationRules: [{type: "required" }],

    caption: "Name"
                },
    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
            },
    onEditingStart: function (e) {
        logEvent("EditingStart");
            },
    onInitNewRow: function (e) {
        logEvent("InitNewRow");
            },
    onRowInserting: function (e) {
        console.log(e)
                logEvent("RowInserting");

            },
    onRowInserted: function (e) {
        logEvent("RowInserted");
    console.log(e)
    console.log(e.data)
    var bank = e.data;

    if (bank.bankCode || bank.bankName) {
        $.post('/Import/Bank/AddBank', { bank, bank }, function (data) {

            showToast("Bank Created", "success");

            getBanks();
        });
                }
               
              
            },
    onRowUpdating: function (e) {
        logEvent("RowUpdating");
            },
    onRowUpdated: function (e) {
        logEvent("RowUpdated");
    console.log(e);
    var bank = e.data;
    $.post('/Import/Bank/updateBank', {bank, bank}, function (data) {
        showToast("Bank Updated", "success");
                });
            },
    onRowRemoving: function (e) {
        logEvent("RowRemoving");
            },
    onRowRemoved: function (e) {
        logEvent("RowRemoved");
    console.log(e)
    var key = e.data.bankId;
    $.post('/Import/Bank/Delete?key='+key, function (data) {
        showToast("Bank Deleted", "success");
                });
            }
        });


    grid.on('editorPrepared', function (e) {
            if (e.parentType !== 'dataRow') {
                return;
            }
    e.editorElement[e.editorName]('instance').on('keyDown', function (arg) {

                var gridComponent = e.component;

    var event = arg.jQueryEvent;

    if (event.keyCode === 38) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                } else if (event.keyCode === 40) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                }

    else if (event.keyCode === 189) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                }
            });


        });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');
    }



    function formfiled() {
        getBanks();

    }
