

    var gdnumber;

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var result;
    function AddCustomerNOC() {

        checkFiledData();

    if (result == false) {

            var model = $('#CustomerNOCForm').serialize();

    $.post('AddCustomerNOC?' + model, function (data) {
                if (data.error == true) {
        showToast(data.message, "error");
    loadGrid();
                }
    else {
        showToast(data.message, "success");
    loadGrid();
                }
            })
        }


    }


    function checkFiledData() {

        result = false;


    if (!$('#gdnumbers').val()) {

        result = true;
    return showToast("please select gd number", "error");
        }

    if (!$('#shippingAgentA').val()) {
        result = true;
    return showToast("there is no pervious agent define", "error");
        }

    if (!$('#emailReceived').val()) {
        result = true;
    return showToast("please add Email Received", "error");
        }

    if (!$('#nameOfPerson').val()) {
        result = true;
    return showToast("please add Name Of Person", "error");
        }
    if (!$('#contactNumber').val()) {
        result = true;
    return showToast("please add Contact Number", "error");
        }
    if (!$('#shippingAgentB').val()) {
        result = true;
    return showToast("please select Sec Shipping Agent", "error");
        }


    }

    function gdNumber_changed(data) {

        console.log("data", data)

        gdnumber = data;
    $.get('/APICalls/GetlpGdNumber?gdnumber=' + gdnumber, function (resp) {
            if (resp) {
        $('#shippingAgentA').val(resp.shippingAgentExportId);

            }
    else {

        $('#shippingAgentA').val('');

            }
        });

    loadGrid();
    }


    function loadGrid() {


        var gdnumber = $('#gdnumbers').val();


    console.log("gdnumber", gdnumber);

    if (gdnumber) {

        $.get("/APICalls/GetCustomerNOCBygdnumber?gdnumber=" + gdnumber, function (resdata) {

            printgrid(resdata);

        });
        }
    else {
        printgrid([]);
        }



    }


    function printgrid(resdata) {

        console.log("resdata", resdata)

        var dataGrid = $("#cargoInfoDetail").dxDataGrid({
        dataSource: resdata,
    keyExpr: "",
    wordWrapEnabled: true,
    showBorders: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
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
        dataField: "shippingAgentName",
    caption: "From Agent",
                },
    {
        dataField: "shippingAgentNameB",
    caption: "To Agent",
                },

    {
        dataField: "nameOfPerson",
    caption: "Person",
                },

    {
        dataField: "emailReceived",
    dataType: "date",
    format: "dd/MM/yyyy hh:mm:ss"
                },
    {
        dataField: "contactNumber",
    caption: "Contact Number",
                },

    ],



        }).dxDataGrid("instance");

    }



    function formfiled() {


    }


    function formfiled() {


    }
