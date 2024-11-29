
    var AccountNatures = [];


    $(function () {

        getAccountNatures();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getAccountNatures() {
        $.get('/Setup/GetAccountNature', function (data) {
            AccountNatures = data;
            console.log(AccountNatures);
            loadgrid();
        });
    }

    function loadgrid() {

        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: AccountNatures,
    keyExpr: "accountNatureId",
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
    allowUpdating: false,
    allowDeleting: false,
    allowAdding: false
            },
    columns: [
    {
        dataField: "code",
    validationRules: [{type: "required" }],
    caption: "Code"
                },
    {
        dataField: "name",
    validationRules: [{type: "required" }],
    caption: "Name"
                },
    {
        dataField: "isActive",
    dataType: "boolean",
    caption: "Status",
                },

    ],

    onRowInserted: function (e) {

                var model = e.data;
    $.post('/Setup/AddAccountNature', {model, model}, function (data) {


                        if (data.error == true) {
        showToast(data.message, "error")
                            getAccountNatures();
                        } else {
        showToast(data.message, "success")
                            getAccountNatures();


                        }


                    });


            },

    onRowUpdated: function (e) {

                var model = e.data;
    $.post('/Setup/UpdateAccountNature', {model, model}, function (data) {

                    if (data.error == true) {
        showToast(data.message, "error")
                        getAccountNatures();
                    } else {
        showToast(data.message, "success")
                        getAccountNatures();


                    }
                });
            },

        });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');
    }



    function formfiled() {

    }
