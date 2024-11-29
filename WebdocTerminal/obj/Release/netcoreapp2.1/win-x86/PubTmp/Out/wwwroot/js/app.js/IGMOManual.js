



    var containersize = [
    {Name: "20" },
    {Name: "40" },
    {Name: "45" }
    ];

    var igmos = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getigmos() {
        $.get('/Import/Setup/GetIGMOOManual', function (data) {

            igmos = data;

            console.log(igmos);
            igmogrid();
        });
    }




    function igmogrid() {

        $("#gridContainer").dxDataGrid({
            dataSource: igmos,
            keyExpr: "igmoId",
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
                mode: "form",
                allowUpdating: false,
                allowDeleting: false,
                allowAdding: true
            },
            columns: [
                {
                    dataField: "virNumber",
                    validationRules: [{ type: "required" }],

                    editorOptions: {
                        mask: "CCCC-0000-00000000",
                    },

                },
                {
                    dataField: "blNumber",
                    validationRules: [{ type: "required" }],

                },
                {
                    dataField: "indexNumber",
                    validationRules: [{ type: "required" }],
                    dataType: 'number',
                },
                {
                    dataField: "containerNumber",
                    validationRules: [{ type: "required" }],

                },
                {
                    dataField: "containerGrossWeight",
                    validationRules: [{ type: "required" }],
                    dataType: 'number',
                },
                {
                    dataField: "blGrossWeight",
                    validationRules: [{ type: "required" }],
                    dataType: 'number',

                },
                {
                    dataField: "hsCode",
                    validationRules: [{ type: "required" }],
                    dataType: 'number',
                },
                {
                    dataField: "description",
                    validationRules: [{ type: "required" }],

                },
                {
                    dataField: "marksAndNumber",
                    validationRules: [{ type: "required" }],

                },
                {
                    dataField: "numberofPackages",
                    validationRules: [{ type: "required" }],
                    dataType: 'number',
                },
                {
                    dataField: "packageType",
                    validationRules: [{ type: "required" }],

                },
                {
                    dataField: "isoCode",
                    validationRules: [{ type: "required" }],
                    caption: "Size",
                    lookup: {
                        dataSource: containersize,
                        valueExpr: "Name",
                        displayExpr: "Name"
                    },
                },
                {
                    dataField: "manifestedSealNumber",
                    validationRules: [{ type: "required" }],

                },
                {
                    dataField: "shippingLine",
                    validationRules: [{ type: "required" }],

                },


            ],

            onRowInserted: function (e) {

                console.log(e)
                console.log(e.data)
                var model = e.data;

                $.post('/Import/Setup/AddIGMOManual', { model, model }, function (data) {

                    if (data.error == true) {
                        showToast(data.message, "error")
                        window.location.href = window.location.href;
                    } else {
                        showToast(data.message, "success")
                        window.location.href = window.location.href;
                    }

                });

            },


            onRowRemoved: function (e) {
                console.log(e);
            }
        });
    }



    function formfiled() {
        getigmos()

    }
