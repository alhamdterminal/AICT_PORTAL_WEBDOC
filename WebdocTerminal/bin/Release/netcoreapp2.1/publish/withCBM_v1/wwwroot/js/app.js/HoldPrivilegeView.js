

    var HoldPrivilegeData = [];

    var HoldTypes = [
    {Name: "SpecialHold", Text: "SpecialHold" },
    {Name: "AgenciesHold", Text: "AgenciesHold" },
    {Name: "ShippinLineHold", Text: "ShippinLineHold" },
    {Name: "CFSCargoDiscrepancyHold", Text: "CFSCargoDiscrepancyHold"},
    {Name: "FinanceHold", Text: "FinanceHold"},
    {Name: "AuctionHold", Text: "AuctionHold"},
    {Name: "Auction", Text: "FormAuction" },
    ];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getHoldPrivilege() {
        $.get('/Import/Setup/HoldPrivilege', function (data) {
            HoldPrivilegeData = data;
            console.log(HoldPrivilegeData);
            HoldPrivilegegrid();
        });
    }



    function HoldPrivilegegrid() {
        $.get('/Import/Setup/GetUsers', function (usersdata) {

            console.log("usersdata", usersdata)

            $("#gridContainer").dxDataGrid({
                dataSource: HoldPrivilegeData,
                keyExpr: "holdPrivilegeId",
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
                        dataField: "holdType",
                        validationRules: [{ type: "required" }],
                        caption: "Hold Type",
                        lookup: {
                            dataSource: HoldTypes,
                            valueExpr: "Name",
                            displayExpr: "Text"
                        }
                    },
                    {
                        dataField: "userName",
                        validationRules: [{ type: "required" }],
                        caption: "User Name",
                        lookup: {
                            dataSource: usersdata,
                            valueExpr: "userName",
                            displayExpr: "userName"
                        }
                    },
                    {
                        caption: "Is Active",
                        dataField: "isActive",
                        dataType: "boolean"
                    },

                ],

                onRowInserted: function (e) {


                    var model = e.data;

                    $.post('/Import/Setup/AddHoldPrivilegeView', { model, model }, function (data) {
                        showToast("Created", "success");
                        getHoldPrivilege();
                    });

                },

                onRowUpdated: function (e) {

                    var model = e.data;

                    $.post('/Import/Setup/UpdateHoldPrivilegeView', { model, model }, function (data) {
                        showToast("Updated", "success");
                        getHoldPrivilege();
                    });
                },
                onRowRemoved: function (e) {

                    console.log(e)
                    var key = e.data.holdPrivilegeId;
                    console.log("key", key)
                    $.post('/Import/Setup//DeletePrivilege?key=' + key, function (data) {
                        showToast("Deleted", "success");
                    });
                }

            });

        });
    }



    function formfiled() {
        getHoldPrivilege();

    }
