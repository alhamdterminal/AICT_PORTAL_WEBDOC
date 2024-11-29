

    function callChangefunc(val){
        console.log("val", val);

    if (val) {
        $.get('/Import/Setup/GetPercentForAictBillingToLine?ShippingAgentId=' + val, function (res) {
            if (res) {
                Griddata(res)

            }
            else {
                Griddata([])

            }

        });
        }
    else {
        Griddata([]);
        }
      
    }

    function Griddata(res) {
        var dataGrid = $("#gridlist").dxDataGrid({
        dataSource: res,
    keyExpr: "percentForAictBillingToLineId",
    wordWrapEnabled: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: true
            },
    editing: {
        mode: "popup",
    allowDeleting: false,
    allowUpdating: false,
            },
    searchPanel: {
        visible: true,
    width: 240,
    placeholder: "Search..."

            },
    hoverStateEnabled: true,
    columns: [

    {
        dataField: "performedDate",
    caption: "Performed",
    allowEditing: false,
    cssClass: 'myClass',
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "userNmae",
    caption: "User",

                },
    {
        dataField: "storagePercentToLine",
    caption: "Storage %",

                },
    {
        dataField: "serviceChargesPercentToLine",
    caption: "Service Charges %",

                },

    ],


           

        }).dxDataGrid("instance");

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }

    function formfiled () {

    }

    function savedetail() {

        var storagePercentToLine = $('#storagePercentToLine').val();
    var serviceChargesPercentToLine = $('#serviceChargesPercentToLine').val();
    var shippingAgentId  =$('#shippingAgentId').val();

    if (shippingAgentId) {

            var data = {
        storagePercentToLine: storagePercentToLine,
    serviceChargesPercentToLine: serviceChargesPercentToLine,
    shippingAgentId: shippingAgentId 
            }
    console.log("model", data);

    $.post('/Import/Setup/AddPercentForAictBillingToLine', {data: data }, function (data) {

                if (data.error == true) {
        showToast(data.message, "error");
    callChangefunc(shippingAgentId)
                }
    else {
        showToast(data.message, "success");
    callChangefunc(shippingAgentId)
                }
            });

        } else {
        showToast("please select FF", "error")
    }
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }
