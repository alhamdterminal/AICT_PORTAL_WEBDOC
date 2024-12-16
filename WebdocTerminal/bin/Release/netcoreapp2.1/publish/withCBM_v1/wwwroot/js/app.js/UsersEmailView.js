
    var UserEmails = [];

    var ListOfDeparts = [
    {Name: "IT" },
    {Name: "OPERATION" },
    {Name: "SHED" },
    {Name: "FINANCE" },
    {Name: "MANAGEMENT" },
    {Name: "CSD" },
    {Name: "OTHERS" },
    {Name: "CREDITALLOW" },
    {Name: "MOVEMENTALTERS" },
    {Name: "CONTAINERMOVEMENT" },

    ];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getUserEmails() {
        $.get('/Users/GetUserEmails', function (data) {
            UserEmails = data;
            console.log("UserEmails", UserEmails);
            UserEmailsgrid();
        });
    }


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {

        if (PermissionInputs.find(x => x.fieldName == "Email" && x.isChecked == false)) {

        checkColumn(e, "email");
        } 

    }

    function UserEmailsgrid() {
        function logEvent(eventName) {
            var logList = $("#events ul"),
                newItem = $("<li>", { text: eventName });
            logList.prepend(newItem);
        }

        $("#gridContainer").dxDataGrid({
        dataSource: UserEmails,
    keyExpr: "usersEmailId",
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
        dataField: "email",
    validationRules: [{type: "required" }],
    caption: "Email"
                },
    {
        dataField: "department",
    validationRules: [{type: "required" }],
    caption: "Department",
    lookup: {
        dataSource: ListOfDeparts,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
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
    var UserEmail = e.data;

    console.log("UserEmail", UserEmail)

    if (UserEmail.email) {
        $.post('/Users/Post', { UserEmail, UserEmail }, function (data) {

            showToast("Email Created", "success");

            getUserEmails();

        });
                }


            },
    onRowUpdating: function (e) {
        logEvent("RowUpdating");
            },
    onRowUpdated: function (e) {
        logEvent("RowUpdated");
    console.log(e);
    var UserEmail = e.data;
    $.post('/Users/updateUserEmail', {UserEmail, UserEmail}, function (data) {
        showToast("Email Updated", "success");
                });
            },
    onRowRemoving: function (e) {
        logEvent("RowRemoving");
            },
    onRowRemoved: function (e) {
        logEvent("RowRemoved");
    console.log(e)
    var key = e.data.usersEmailId;
    $.post('/Users/Delete?key=' + key, function (data) {
        showToast("Email. Deleted", "success");
                });
            }
        });
    }



    function formfiled() {
        getUserEmails();

    }
