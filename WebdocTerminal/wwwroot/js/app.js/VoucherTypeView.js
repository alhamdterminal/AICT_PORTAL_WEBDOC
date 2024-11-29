

    var voucherTypes = [];


    $(function () {

        getvouchertypes();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getvouchertypes() {
        $.get('/Setup/GetVoucherType', function (data) {
            voucherTypes = data;
            console.log(voucherTypes);
            loadgrid();
        });
    }

    function loadgrid() {

        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: voucherTypes,
    keyExpr: "voucherTypeId",
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
    $.post('/Setup/AddVoucherType', {model, model}, function (data) {


                        if (data.error == true) {
        showToast(data.message, "error");
    getvouchertypes();

                        } else {
        showToast(data.message, "success")
                            getvouchertypes();


                        }


                    });


            },

    onRowUpdated: function (e) {

                var model = e.data;
    $.post('/Setup/UpdateCountry', {model, model}, function (data) {

                    if (data.error == true) {
        showToast(data.message, "error")

    } else {
        showToast(data.message, "success")
                        getcountries();


                    }
                });
            },

        });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');
    }



    function formfiled() {


    }
