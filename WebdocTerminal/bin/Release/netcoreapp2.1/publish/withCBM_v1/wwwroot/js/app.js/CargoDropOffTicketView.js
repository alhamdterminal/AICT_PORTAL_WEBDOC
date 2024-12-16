

    function gdNumber_changed() {
        var res = $('#gdnumbers').val();

    console.log("resdata resdata", res);

    $.get('/Export/ConsolidateGDDetail/GetGateInGDsDetail?res=' + res, function (resdata) {


        console.log("resdata", resdata);


    $("#gridContainer").dxDataGrid({
        dataSource: resdata,
    keyExpr: "gateInExportId",
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
        dataField: "gdNumber",


                    },
    {
        dataField: "numberofPackages",


                    },
    {
        dataField: "packageType",

                    },
    {
        dataField: "weight",
                    },
    {
        dataField: "gateInDate",
    caption: "Gate-In Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                    },
    {
        dataField: "vehicleNumber",
                    },
    {
        dataField: "gateInStatus",
                    },
    {
        width: 100,
    alignment: 'center',
    cellTemplate: function (container, options) {
        $('<a/>').addClass('dx-link')
            .text('Print')
            .on('dxclick', function () {
                print_invoice(options.row.data)
            })
            .appendTo(container);
                        }
                    }

    ],

            });

        });

    }


    function print_invoice(data) {

        console.log("print_invoice data", data)

        window.open('/Export/Reports/CargoDropoffTicket?gateInExportId=' + data.gateInExportId, "_blank");


    }

    function formfiled() {

    }
