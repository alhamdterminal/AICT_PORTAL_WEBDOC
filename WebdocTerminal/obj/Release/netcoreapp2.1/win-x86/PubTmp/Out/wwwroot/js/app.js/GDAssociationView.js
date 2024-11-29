

    var gdnumberList = [];
    var containerList = [];


    $(function () {

        GetGdslist();
    Getcontainernolist();

    });




    function GetGdslist() {
        $.get('/APICalls/GetPendingGdsForAssociation', function (res) {
            if (res) {
                $("#searchBoxForGdnumber").dxSelectBox({
                    dataSource: {
                        store: res,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'GDNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                    onValueChanged: function (e) {
                        getgdDetail(e.value)
                    }
                })



            }
            else {
                $("#searchBoxForGdnumber").dxSelectBox({
                    dataSource: {
                        store: [],
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'GDNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                });


            }
        });
    }

    function Getcontainernolist() {
        $.get('/APICalls/GetPendingcontainers', function (res) {
            if (res) {
                $("#searchBoxForcontainernumber").dxSelectBox({
                    dataSource: {
                        store: res,
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ContainerNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,
                    onValueChanged: function (e) {
                        getgdDetailcontainerwise(e.value)
                    }

                })

            }
            else {
                $("#searchBoxForcontainernumber").dxSelectBox({
                    dataSource: {
                        store: [],
                        requireTotalCount: true,
                        pageSize: 4,
                        paginate: true,
                    },
                    name: 'ContainerNumber',
                    displayExpr: "text",
                    valueExpr: "value",
                    acceptCustomValue: true,
                    searchEnabled: true,

                })

            }
        });
    }

    function getgdDetail() {

        var gdnumber = $("#searchBoxForGdnumber").dxSelectBox('instance').option("value");

    console.log("gdnumber", gdnumber);

    $.get('/APICalls/GetExportContainerItemsByGdnumberFOrAssociation?Gdnumber=' + gdnumber, function (data) {

        GridData(data);

        });
    }

    function getgdDetailcontainerwise() {

        var exportcontainerid = $("#searchBoxForcontainernumber").dxSelectBox('instance').option("value");

    console.log("exportcontainerid", exportcontainerid);

    $.get('/APICalls/GetExportContainerItemsByExportcontainerIdFOrAssociation?exportcontainerid=' + exportcontainerid, function (data) {

        GridData(data);

        });
    }

    function GridData(res) {
        var dataGrid = $("#ExportContainerItem").dxDataGrid({
        dataSource: res,
    keyExpr: "exportContainerItemId",
    wordWrapEnabled: true,
    showBorders: true,
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

        mode: "batch",
    allowUpdating: false,
    startEditAction: "click", 
            },

    columns: [
    {
        dataField: "gdNumber",
    caption: "GD Number",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "containerNumber",
    caption: "Container No",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "numberOfPackages",
    caption: "No Of Packages",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "allowLoading",
    caption: "Allow Loading",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
    {
        dataField: "destination",
    caption: "Destination",
    allowEditing: false,
    validationRules: [{type: "required" }]
                },
                //{
                //    dataField: "orderNumber",
                //    caption: "Order No",
                //    dataType: "number",
                //     validationRules: [{type: "required" }]
                //},
            ], 

        }).dxDataGrid("instance");


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }



    function AddExportContainerItems() {

        var models = $("#ExportContainerItem").dxDataGrid("instance").option("dataSource");

    if (models.length) {

            if (confirm('Are you sure want to send OGDC message to Weboc')) {

        $.post('/Export/ExportContainer/AddGDAssociationList', { models: models }, function (resout) {

            if (resout.error) {
                alert(resout.message);
                window.location.href = window.location.href;
            }
            else {
                alert(resout.message);
                window.location.href = window.location.href;
            }

        });

            }
    else {
        window.location.href = window.location.href;
            }

        }
    else {
        showToast("no data found to save", "danger");
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

    function formfiled() {

    }

