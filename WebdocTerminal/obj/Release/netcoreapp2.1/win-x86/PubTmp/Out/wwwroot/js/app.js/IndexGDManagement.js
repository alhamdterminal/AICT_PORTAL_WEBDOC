
    function formfiled() {
        renderContent();
    }

    function renderContent(type) {
        console.log("type", type);

    searchGDs(type);
    }



    function searchGDs(type) {
        if (type == undefined) {
        type = "ANFStatus";
        }

    $.get('/Export/GDManagement/GetGDs?Type=' + type, function (data) {

            if (type === 'ANFStatus') {
        anfGDS = data;
    loadGrid(data, '#anf-grid');
            }
    else if (type === 'PaperReady') {
        paperreadyGDs = data;
    loadGrid(data, '#paperready-grid');
            }
        })
    }


    function loadGrid(dataSource, selector) {

        console.log("testing ", dataSource);

    var dataGrid = $(selector).dxDataGrid({
        dataSource: dataSource,
    keyExpr: "id",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    columnAutoWidth: true,
    allowColumnResizing: true,

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
        mode: "batch",
    allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click"
            },
    columns: [
    {
        dataField: "dateReceived",
    dataType: "date",
    format: "dd/MM/yyyy hh:mm:ss"
                },
    "gdNumber",
    "clearingAgent",
    "shipper",
    "noOfPackages",
    {
        caption: "Date",
    dataField: "paperReadyDate",
    dataType: "date",
    validationRules: [{type: "required" }],
    format: "dd/MM/yyyy"
                },

    {
        caption: "",
    dataField: "isChecked",
    dataType: "boolean"
                }
    ],

        }).dxDataGrid("instance");



    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }




    function submit() {

        var res = $("#anf-grid").dxDataGrid("option", "dataSource");

    console.log("res", res)

    if (res.length) {


            var resfilter = res.filter(x => x.isChecked == true);

    if (resfilter.length) {

        $.post('/Export/GDManagement/UpdateGDs', { gds: resfilter }, function (data) {
            if (data.error == true) {

                showToast(data.message, "error");

            }

            else {
                showToast(data.message, "success");
                window.location.href = window.location.href;
            }

        });
            }
        }

    }




    function submitpaperready() {

        var res = $("#paperready-grid").dxDataGrid("option", "dataSource");

    console.log("res", res);

    if (res.length) {

            var resfilter = res.filter(x => x.isChecked == true);

    if (resfilter.length) {
        $.post('/Export/GDManagement/UpdateGDsPaperReady', { gds: resfilter }, function (data) {
            if (data.error == true) {

                showToast(data.message, "error");

            }

            else {
                showToast(data.message, "success");

                window.location.href = window.location.href;
            }

        });
            }
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

