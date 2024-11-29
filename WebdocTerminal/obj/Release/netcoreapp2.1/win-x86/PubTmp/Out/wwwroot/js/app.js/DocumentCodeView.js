

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var DocumentCodeData = [];


    function getDocumentCodes() {
        $.get('/Import/DocumentCode/GetDocumentCode', function (data) {
            DocumentCodeData = data;

            datagrid();
        });
    }
    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

    }
    function datagrid() {
        function logEvent(eventName) {
            var logList = $("#events ul"),
                newItem = $("<li>", { text: eventName });
            logList.prepend(newItem);
        }

        $("#gridData").dxDataGrid({
        dataSource: DocumentCodeData,
    keyExpr: "documentCodeId",
    wordWrapEnabled: true,
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
    allowDeleting: true,
    allowAdding: true
            },
    columns: [
    {
        dataField: "code",
    caption: "Document Code"
                },
    {
        dataField: "documentName",
    caption: "Document Name"
                }
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
        logEvent("RowInserting");
            },
    onRowInserted: function (e) {
        logEvent("RowInserted");

    var documentCode = e.data;
    console.log(documentCode)
    if (documentCode.code && documentCode.documentName) {
        $.post('/Import/DocumentCode/AddDocumentCode', { documentCode, documentCode }, function (data) {

            showToast("Created", "success");

            getDocumentCodes();

        });
                }


            },
    onRowUpdating: function (e) {
        logEvent("RowUpdating");
            },
    onRowUpdated: function (e) {
        logEvent("RowUpdated");
    console.log(e);
    var documentCode = e.data;
    $.post('/Import/DocumentCode/UpdateDocumentCode', {documentCode, documentCode}, function (data) {
        showToast("Updated", "success");
                });
            },
    onRowRemoving: function (e) {
        logEvent("RowRemoving");
            },
    onRowRemoved: function (e) {
        logEvent("RowRemoved");
    var key = e.data.documentCodeId;
    $.post('/Import/DocumentCode/DeleteDocumentCode?key=' + key, function (data) {
        showToast("Deleted", "success");
                });
            }
        });
    }


    function formfiled() {
        getDocumentCodes();

    }
