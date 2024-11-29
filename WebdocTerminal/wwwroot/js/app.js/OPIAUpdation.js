

    $(function () {

        getGDNumberdetail();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getGDNumberdetail() {
        var gdnumber  = $('#GDNumberdropdown').val();
    console.log("gdnumber", gdnumber)

    if (gdnumber) {
        $.get('/Export/OPIA/GetopiaByGd?gdnumber=' + gdnumber, function (data) {

            console.log("data", data);

            if (data) {
                loadgrid(data)
            } else {
                loadgrid([])
            }
        });
        }
    else {loadgrid([])}



    }


    function loadgrid(data) {
        var dataGrid = $("#opiasGrid").dxDataGrid({
        dataSource: data,
    keyExpr: "opiaId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: true
            },
    editing: {
        mode: "row",
    allowUpdating: true,
            },
    columns: [
    {
        dataField: "gdNumber",
    validationRules: [{type: "required" }],
    allowEditing: false,
    caption: "GD No"
                },
    {
        dataField: "noOfPackages",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "No Of Packages"
                },
    {
        dataField: "packageType",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "Package Type"
                },
    {
        dataField: "consignorName",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "Consignor Name"
                },
    {
        dataField: "consignorNTN",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "Consignor NTN"
                },
    {
        dataField: "consignorAddress",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "Consignor Address"
                },
    {
        dataField: "congisneeName",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "Congisnee Name"
                },
    {
        dataField: "congisneeAddress",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "Congisnee Address"
                },
    {
        dataField: "clearingAgentChallanNumber",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "ClearingAgent Challan No"
                },
    {
        dataField: "clearingAgentName",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "ClearingAgent Name"
                },
    {
        dataField: "grossWeight",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "Gross Weight"
                },
    {
        dataField: "messageTo",
    validationRules: [{type: "required" }],
    allowEditing: true,
    caption: "Message To"
                },



    ],



    onRowUpdated: function (e) {
        console.log(e);
    var values = e.data;

    console.log("values", values)

    $.post('/Export/OPIA/updateOPIASdetail', {values, values}, function (data) {

                    if (data.error == true) {
        showToast(data.message, "error");

                    }
    else {

        showToast(data.message, "success");

                    }
                });

            },


        }).dxDataGrid("instance");

    }




    function formfiled() {
        $('#GDNumberdropdown').on('change', function (data) {

            getGDNumberdetail();

        });
    }



