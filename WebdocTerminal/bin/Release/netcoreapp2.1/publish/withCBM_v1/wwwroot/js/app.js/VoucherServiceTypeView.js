

    var VoucherServiceTypes = [];


    $(function () {

        getVoucherServiceTypes();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getVoucherServiceTypes() {
        $.get('/Setup/GetVoucherServiceType', function (data) {
            VoucherServiceTypes = data;
            console.log(VoucherServiceTypes);
            loadgrid();
        });
    }

    function loadgrid() {



        $("#gridContainer").dxDataGrid({
            dataSource: VoucherServiceTypes,
            keyExpr: "voucherServiceTypeId",
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
                    validationRules: [{ type: "required" }],
                    caption: "Code"
                },
                {
                    dataField: "name",
                    validationRules: [{ type: "required" }],
                    caption: "Name"
                },
                {
                    dataField: "isActive",
                    dataType: "boolean",
                    caption: "Status",
                },

            ],

            onRowInserted: function (e) {

                console.log(e.data)

                var model = e.data;
                $.post('/Setup/AddVoucherServiceType', { model, model }, function (data) {


                    if (data.error == true) {
                        showToast(data.message, "error")
                        getVoucherServiceTypes();
                    } else {
                        showToast(data.message, "success")
                        getVoucherServiceTypes();


                    }


                });


            },

            onRowUpdated: function (e) {
                console.log(e.data);
                var model = e.data;

                $.post('/Setup/UpdateVoucherServiceType', { model, model }, function (data) {

                    if (data.error == true) {
                        showToast(data.message, "error")
                        getVoucherServiceTypes();
                    } else {
                        showToast(data.message, "success")
                        getVoucherServiceTypes();
                    }
                });
            },

        });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }



    function formfiled() {

    }
