

    var CategoryType = [
    {Name: "Auction" },
    {Name: "ExaminationCharges" },
    {Name: "PortCharges" },
    {Name: "SealCharges" },
    {Name: "SpecialHandlingCharges" },
    {Name: "Storage" },
    {Name: "Tariff" },
    ];

    var ItemHeads = [];
    var FilteredItemHeads = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getdata() {
        $.get('/Import/Setup/GetInvoiceItemHeadsDetail', function (HeadsDetail) {

            ItemHeads = HeadsDetail;


            $.get('/Import/Setup/GetCRSummaryHeadDetail', function (data) {

                loadGrid(data, ItemHeads);

            });

        });
    }



    $(function () {

        getdata();

    });

    function formfiled() {


    }

    var catgry = "";
    var filterType = true;

    function loadGrid(dataSource, ItemHeads) {

        var grid = $("#CRDataSummaryDetail").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    var grid = $("#CRDataSummaryDetail").dxDataGrid(this.gridSettings).dxDataGrid('instance');
    var dataGrid = $("#CRDataSummaryDetail").dxDataGrid({
        dataSource: dataSource,
    keyExpr: "crSummaryHeadDetailId",

    wordWrapEnabled: true,
    showBorders: true,
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
    allowDeleting: true,
    allowAdding: true,
    allowUpdating: true
                },
    columns: [

    {
        dataField: "serialNo",
    dataType: "number",
    format: "#,##0.##",
    width: 120,
    caption: "Serial No",
    validationRules: [{type: "required" }]

                    },
    {
        dataField: "category",
    caption: "Category",
    setCellValue(rowData, value, e) {

        filterType = false;
    rowData.description = null;
    rowData.category = value;
                             
                         },
    width: 200,
    lookup: {
        dataSource: CategoryType,
    displayExpr: "Name",
    valueExpr: "Name"
                        },
    validationRules: [{type: "required" }]

                    },
    {
        dataField: "description",
    caption: "Description",

    width: 200,
    lookup: {

        //dataSource: ItemHeads,

        dataSource(options) {
                                return {
        store: ItemHeads,
    filter: options.data ? ['category', '=', options.data.category] : null,
                                };
                            },

    displayExpr: "description",
    valueExpr: "description"
                        },
    validationRules: [{type: "required" }]

                    },
                    //{
        //    dataField: "description",
        //    caption: "Description",
        //    validationRules: [{ type: "required" }],
        //},
        {
            dataField: "caption",
            caption: "Caption",
            validationRules: [{ type: "required" }],
        },
 

                ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
            },
    onRowInserted: function (e) {

        console.log(e.data);

    $.post('/Import/Setup/AddCRSummaryHeadDetail?name=' + e.data.description + '&category=' + e.data.category + '&caption=' + e.data.caption + '&serialno=' + e.data.serialNo , function (result) {

        console.log("result", result)
                                if (result.error) {
        showToast(result.message, "warning");
    getdata();
                                }

    else {

        showToast(result.message, "success");
    getdata();
                                 }

                             }); 

                },

    onRowUpdated: function (e) {

        console.log(e.data);
    $.post('/Import/Setup/UpdateCRSummaryHeadDetail?name=' + e.data.description + '&category=' + e.data.category + '&caption=' + e.data.caption + '&crsummaryHeadDetailId=' + e.data.crSummaryHeadDetailId, function (result) {

        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");
    getdata();
                    }

    else {

        showToast(result.message, "success");
    getdata();
                    }

                });
            },
    onRowRemoved: function (e) {

        $.post('/Import/Setup/DeleteCRSummaryHeadDetail?crsummaryHeadDetailId=' + e.data.crSummaryHeadDetailId, function (result) {

            console.log("result", result)
            if (result.error) {
                showToast(result.message, "warning");
                getdata();
            }

            else {

                showToast(result.message, "success");
                getdata();
            }

        });



                }

            }).dxDataGrid("instance");


    grid.on('editorPrepared', function (e) {
            if (e.parentType !== 'dataRow') {
                return;
            }

    e.editorElement[e.editorName]('instance').on('keyDown', function (arg) {

                var gridComponent = e.component;

    var event = arg.jQueryEvent;

    if (event.keyCode === 38) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                } else if (event.keyCode === 40) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                }
    else if (event.keyCode === 189) {
                    if (e.editorName == "dxNumberBox") {
        event.stopPropagation();
    event.preventDefault();
                    }

                }
            });


        });

    }


    function hideMenifestedColumns(e) {

        checkColumn(e, "description");

    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    if (filterType == true) {
        e.editorOptions.disabled = true;
        }

    if (filterType == false) {
        e.editorOptions.disabled = false;
        }

    }

    function refresh() {
        window.location.reload();
    }



