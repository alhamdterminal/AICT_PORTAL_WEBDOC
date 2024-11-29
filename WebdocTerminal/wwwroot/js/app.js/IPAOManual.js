

    var ipaos = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getipaos() {
        $.get('/Import/Setup/GetIPAOsManual', function (data) {

            ipaos = data;

            console.log(ipaos);
            ipaosgrid();
        });
    }




    function ipaosgrid() {

        $("#gridContainer").dxDataGrid({
            dataSource: ipaos,
            keyExpr: "ipaoId",
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
                    dataField: "containerNumber",
                    validationRules: [{ type: "required" }],

                },
                {
                    dataField: "bondedCarrierId",
                    validationRules: [{ type: "required" }],
                    dataType: 'number',
                },
                {
                    dataField: "bondedCarrierName",
                    validationRules: [{ type: "required" }],

                },
                {
                    dataField: "vehicleNumber",
                    validationRules: [{ type: "required" }],
                },
                {
                    dataField: "outTime",
                    validationRules: [{ type: "required" }],
                    dataType: 'date',
                    format: 'dd/MM/yyyy',
                },
                {
                    dataField: "pccssSealNumber",
                    validationRules: [{ type: "required" }],
                    dataType: 'number',
                },
                {
                    dataField: "portOfEntry",
                    validationRules: [{ type: "required" }],

                },

            ],

            onRowInserted: function (e) {

                console.log(e)
                console.log(e.data)
                var model = e.data;

                $.post('/Import/Setup/AddIPOAManual', { model, model }, function (data) {

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
        getipaos()

    }
