

    $(function () {
        $.get('/APICalls/GetFiltersBrtCFS', function (partial) {

            $('#filters').html(partial);

        })
        brtitemsGrid();
    });


    var itemsDetail = [];


    function containerChangeCallback() {

        

        var ContainerIndexId = $("#indexdb option:selected").val();
    console.log("ContainerIndexId", ContainerIndexId)

    getdestuffdata(ContainerIndexId);
    getbrtdetail(ContainerIndexId);

    }


    function getbrtdetail(ContainerIndexId) {
        $.get('/APICalls/GetContainerIndexBRTLocation?ContainerIndexId=' + ContainerIndexId, function (location) {

            console.log("location", location);
            if (location) {
                $('#location').val(location.location);
            }
            else {
                $('#location').val('');
            }


            if (location.brtItems) {
                console.log("if");
                itemsDetail = location.brtItems;
                brtitemsGrid();
            }
            else {
                console.log("else");
                itemsDetail = [];
                brtitemsGrid();

            }

        });


    }

    function getdestuffdata(ContainerIndexId) {
        $.get('/APICalls/GetDestuffContainerIndexInfo?ContainerIndexId=' + ContainerIndexId, function (result) {

            console.log("result", result)
            if (result) {
                $('#manifestPackage').val(result.manifestQuantity);
                $('#package').val(result.packageQuantity);
            }
            else {
                $('#manifestPackage').val(result.manifestQuantity);
                $('#package').val(result.packageQuantity);
            }
        });

    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function saveContainerLocation() {


        var items = itemsDetail;
    var location = ExtraFreeDays.elements["location"].value;
    var ContainerIndexId = $("#indexdb option:selected").val();


    console.log("location", location)
    console.log("items", items)
    console.log("ContainerIndexId", ContainerIndexId)


    $.post('/Import/BRT/UpdateIndexLocation?ContainerIndexId=' + ContainerIndexId + '&location=' + location, {items: items}, function (data) {

        showToast(data.message, "success");
             
            });
 

    }


    function brtitemsGrid() {

        var grid = $("#vrtitemsGRid").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#vrtitemsGRid").dxDataGrid({
        dataSource: itemsDetail,
    keyExpr: "brtItemId",
    wordWrapEnabled: true,
    showBorders: true,
    columnAutoWidth: true,

    paging: {
        enabled: false
            },

    editing: {
        mode: "row",
    allowAdding: true,
    allowUpdating: true
            },
    columns: [
    {

        dataField: "numberOfItems",
    dataType: "number",
    caption: "Number Of Items",
    editorOptions: {
        step: 0
                    }

                },

    "packageLocation",
    {
        dataField: "createDate",
    caption: "Create Date",
    dataType: 'date',
    format: 'dd/MM/yyyy',
    width: 200
                },
    {
        dataField: "brtItemId",
    caption: "",
    allowEditing: false,
    cellTemplate: function (container, options) {
        $("<button class='btn btn-primary' onclick='DeleteBRTItem(" + options.value + ")'>Remove</button>")
            .appendTo(container);
                    },
    width: 100
                }

    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
            },
    onRowUpdated: function (e) {
        console.log(e);
    var brtItemId = e.data.brtItemId;
    var item = e.data;
    $.post('/Import/BRT/UpdateBRTItem?brtitemId=' + brtItemId, {item, item}, function (data) {
        showToast("Updated", "success");

    window.location.href = window.location.href;
                });
            },
          

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

    function DeleteBRTItem(key) {
        console.log("key", key)
        $.post('/Import/BRT/DeleteBRTItem?key=' + key, function (data) {
        showToast("Deleted", "success");
        });
    }

    function formfiled() {
             if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {

        document.getElementById('filters').style.display = "none";
     
         }
        if (PermissionInputs.find(x => x.fieldName == "Location" && x.isChecked == false)) {

        document.getElementById('location').disabled = true;
     
         }

    }



    function hideMenifestedColumns(e) {

        checkColumn(e, "createDate");
    }


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }
