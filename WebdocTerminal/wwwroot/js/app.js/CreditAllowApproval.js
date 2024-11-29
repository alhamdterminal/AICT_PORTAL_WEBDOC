


    $(function () {
        getCreditallowData();
    });

    var CreditallowData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getCreditallowData() {
        $.get('/Import/Setup/CreditAllowApprovalData', function (data) {
            CreditallowData = data;
            console.log("CreditallowData", CreditallowData);
            CreditAllowgrid();
        });
    }


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function refresh() {
        window.location.reload();
    }
    function CreditAllowgrid() {


        console.log("CreditallowData", CreditallowData)

        $("#gridContainer").dxDataGrid({
        dataSource: CreditallowData,
    keyExpr: "creditAllowedId",
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
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
    allowUpdating: false,
    allowAdding: false
            },
    columns: [
    {
        dataField: "virNumber",
    validationRules: [{type: "required" }],
    allowEditing: false,
    caption: "IGM"
                },
    {
        dataField: "indexNumber",
    validationRules: [{type: "required" }],
    allowEditing: false,
    caption: "Index"
                },
    {
        caption: "Created Date",
    dataField: "createdDate",
    dataType: "date",
    validationRules: [{type: "required" }],
    allowEditing: false,
    format: 'dd/MM/yyyy',
                },
    {
        caption: "CreditAllowed Rs",
    dataField: "creditAllowedRs",
    validationRules: [{type: "required" }],
    allowEditing: false,

                },
    {
        caption: "Credit Days",
    dataField: "creditDays",
    validationRules: [{type: "required" }],
    allowEditing: false,

                },
    {
        caption: "Remarks",
    dataField: "remarks",
    validationRules: [{type: "required" }],
    allowEditing: false,

                },
    {
        caption: "Status",
    dataField: "statusType",
    allowEditing: false,

                },

    {
        dataField: "creditAllowedId",
    caption: '',
    cellTemplate: function (container, options) {
        $("<button type='button' class='btn btn-success' onClick='ApproveCreditAllow(" + options.value + ")'>Approve</button>")
            .appendTo(container);
                    }, allowEditing: false
                },
    {
        dataField: "creditAllowedId",
    caption: '',
    cellTemplate: function (container, options) {
        $("<button type='button' class='btn btn-danger' onClick='RejectCreditAllow(" + options.value + ")'>Reject</button>")
            .appendTo(container);
                    }, allowEditing: false
                },

    ],




    onRowUpdated: function (e) {

    }
        });
    }


    function ApproveCreditAllow(id) {
        console.log("id", id);
    $.post('/Import/Setup/ApproveCreditAllow?id=' + id, function (data) {

        alert(data.message);
    getCreditallowData();
        });
    }

    function RejectCreditAllow(id) {
        console.log("id", id);

    $.post('/Import/Setup/RejectCreditAllow?id=' + id, function (data) {

        alert(data.message);
    getCreditallowData();
        });

    }

    function formfiled() {


    }
