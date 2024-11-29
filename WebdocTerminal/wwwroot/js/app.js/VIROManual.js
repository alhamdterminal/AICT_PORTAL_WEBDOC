




    var viros = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getvrios() {
        $.get('/Import/Setup/GetVIROManual', function (data) {

            viros = data;

            console.log(viros);
            virogrid();
        });
    }




    function virogrid() {

        console.log("viros", viros)
        $("#gridContainer").dxDataGrid({
        dataSource: viros,
    keyExpr: "viroId",
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
    validationRules: [{type: "required" }],

    editorOptions: {
        mask: "CCCC-0000-00000000",                         
                    },
                     
                },
    {
        dataField: "vesselName",
    validationRules: [{type: "required" }],
                     
                },
    {
        dataField: "voyage",
    validationRules: [{type: "required" }],
                     
                },
    {
        dataField: "berthOn",
    validationRules: [{type: "required" }],
    dataType: 'date',
    format: 'dd/MM/yyyy',
                     
                },
    {
        dataField: "berthAt",
    validationRules: [{type: "required" }],
                     
                },

    ],

    onRowInserted: function (e) {

        console.log(e)
                console.log(e.data)
    var model = e.data;

    $.post('/Import/Setup/AddVIROManual', {model, model}, function (data) {
                     
                        if (data.error == true) {
        showToast(data.message, "error")

    } else {
        showToast(data.message, "success")
                            window.location.href = window.location.href;
                        }
                     
                    });

            },


    onRowRemoved: function (e) {
        console.log(e)
                var key = e.data.viroId;
    $.post('/Import/Setup/DeleteViro?key=' + key, function (data) {
                    if (data.error == true) {
        showToast(data.message, "error")

    } else {
        showToast(data.message, "success")
                        window.location.href = window.location.href;
                    }

                });
            }
        });
    }



    function formfiled() {
        getvrios()

    }
