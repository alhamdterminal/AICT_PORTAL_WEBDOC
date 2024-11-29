

    var TruckInOutList = [];

    var type;

    function showFilters() {


        type = $("input[name='type']:checked").val();

    $.get('/APICalls/GetFilterIndexWise?Type=' + type, function (partial) {

        $('#filters').html(partial);
             
        })

    if (type == "CFS") {
        document.getElementById('ContainerType').style.display = "block"
    } else {
        document.getElementById('ContainerType').style.display = "none"
    }

    loadGrid([] , []);

    }


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    function containerChangeCallback(ornt) {

        loadGrid([], []);
    index = $("#indexdb option:selected").text();

    gettruckinoutdetail(igm, index)

    }

    function containerCallback() {

        console.log("cy containerCallback")
        loadGrid([] , []);
    index = $("#containerIndexCYdb option:selected").text();

    gettruckinoutdetail(igm, index);

    }

    function gettruckinoutdetail(igm, index) {




        $.get('/APICAlls/GetTruckInOutList?igm=' + igm + '&index=' + index + '&type=' + type, function (data) {

            console.log("data", data);
            if (data) {

                TruckInOutList = data;
            }
            else {
                TruckInOutList = []
            }


            var url = '';
            if (type == "CFS") {
                url = '/APICAlls/GetALLcfsFcLContainerNumbersByIgmIndex?igm=' + igm + '&index=' + $("#indexdb option:selected").text()
            }
            else {
                url = '/APICAlls/GetALLCYContainerNumbersByIgmIndex?igm=' + igm + '&index=' + $("#containerIndexCYdb option:selected").text()
            }

            $.get(url, function (containerno) {
                loadGrid(TruckInOutList, containerno);
            });



        }); 

    }

    function loadGrid(TruckInOutList, containerno) {
         
       
            var dataGrid = $("#truckinoutprocesslist").dxDataGrid({
        dataSource: TruckInOutList,
    keyExpr: "truckInOutId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        pageSize: 10
                },
    editing: {
        mode: "cell",
    allowAdding: true,
    allowUpdating: true,
                 },
    columns: [

    {
        dataField: "truckNo",
    caption: "Truck No",
    validationRules: [{type: "required" }]

                    },
    {
        dataField: "containerNumber",
    caption: "Container No",
    lookup: {
        dataSource: containerno,
    displayExpr: "value",
    valueExpr: "value"
                        },
    validationRules: [{type: "required" }]

                    },
    {
        dataField: "truckInDate",
    caption: "TruckIn  Date",
    dataType: "datetime",
    allowEditing: false,

                    },
    {
        dataField: "truckEntryDate",
    caption: "Truck Entry  Date",
    dataType: "datetime",
    allowEditing: false,
                    },
    {
        dataField: "truckOutDate",
    caption: "Truck Out Date",
    dataType: "datetime",
    allowEditing: false,

                    },
    {
        dataField: "validTo",
    caption: "Valid To",
    dataType: "datetime",
    allowEditing: false,
                    },
    {
        dataField: "status",
    caption: "Status",
    allowClearing: true,
    allowEditing: true,
    lookup: {
        dataSource: [{containerStatus: "Empty" }, {containerStatus: "Load" } ],
    displayExpr: "containerStatus",
    valueExpr: "containerStatus"
                        }
                    },
    {
        dataField: "truckInOutId",
    caption: 'Truck In Pass',
    cellTemplate: function (container, options) {
        $("<button type='button' class='btn btn-success' onClick='TruckInPassReport(" + options.value + ")'>Print</button>")
            .appendTo(container);
                        }, allowEditing: false
                    },
    {
        width: 100,
    alignment: 'center',
    cellTemplate: function (container, options) {
        $('<a/>').addClass('dx-link')
            .text('Mark In')
            .on('dxclick', function () {
                markInTruck(options.row.data)
            })
            .appendTo(container);
                        }
                    },
    {
        width: 100,
    alignment: 'center',
    cellTemplate: function (container, options) {
        $('<a/>').addClass('dx-link')
            .text('Mark Out')
            .on('dxclick', function () {
                markOutTruck(options.row.data)
            })
            .appendTo(container);
                        }
                    },


    ],
    onEditorPreparing: function (e) {
        console.log("e", e)
                    if (e.dataField === "truckNo" && e.parentType === "dataRow") {
        e.editorOptions.disabled = e.row.data && e.row.data.truckInOutId > 0;
                    }

    if (e.dataField === "containerNumber" && e.parentType === "dataRow") {
        e.editorOptions.disabled = e.row.data && e.row.data.truckInOutId > 0;
                    }

    if (e.parentType === "dataRow" && e.dataField == "status") {
                        return;
                    }

                },

    onRowInserted: function (e) {
                    var indexno = "";
    if (type == "CFS") {
        indexno = $("#indexdb option:selected").text();
                }
    else {
        indexno = $("#containerIndexCYdb option:selected").text();
                }


    var truckno = e.data.truckNo;
    var containerNumber = e.data.containerNumber;

    let model = {

        virNumber: igm,
    indexNumber: indexno,
    truckNo: truckno,
    containerNumber: containerNumber,
    type: type
                    };

    $.post('/Import/Setup/AddTruckInOutProcess', {model : model} , function (result) {
        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");

                         
                    }
    else {
        showToast(result.message, "success");
                        
                        }
    if (type == "CFS") {
        gettruckinoutdetail(igm, $("#indexdb option:selected").text());
                        }
    else {
        gettruckinoutdetail(igm, $("#containerIndexCYdb option:selected").text());
                        }
                       

                });

            },

    onRowUpdated: function (e) {

    },


        }).dxDataGrid("instance");

        
    }


    function markInTruck(data) {
        console.log("data", data);

    if (data) {
            if (data.truckOutDate == null) {

        $.post('/Import/Setup/MarkInTruckInOutProcess?truckInOutId=' + data.truckInOutId + '&validate=' + data.validTo, function (result) {
            console.log("result", result)
            if (result.error) {
                showToast(result.message, "warning");

            }
            else {
                showToast(result.message, "success");

            }
            if (type == "CFS") {
                gettruckinoutdetail(igm, $("#indexdb option:selected").text());
            }
            else {
                gettruckinoutdetail(igm, $("#containerIndexCYdb option:selected").text());
            }


        }); 
            }
    else {
        alert("aleary out mark");
            }
        }
    }

    function markOutTruck(data) {
        console.log("data", data);

    if (data) {
            if (data.truckInDate != null) {

                if (data.status != "null" && data.status != "") {

        $.post('/Import/Setup/MarkOutTruckInOutProcess?truckInOutId=' + data.truckInOutId + '&status=' + data.status, function (result) {
            console.log("result", result)
            if (result.error) {
                showToast(result.message, "warning");
            }
            else {
                showToast(result.message, "success");
            }
            if (type == "CFS") {
                gettruckinoutdetail(igm, $("#indexdb option:selected").text());
            }
            else {
                gettruckinoutdetail(igm, $("#containerIndexCYdb option:selected").text());
            }
        });
                }
    else {
        alert("please mark In first and select status");
                }

                
            }
    else {
        alert("please mark In first");
            }
        }
    }

    function formfiled() {

    }



    function refresh() {

        window.location.href = window.location.href;

       
    }


    function truckindetail() {
        window.open('/import/reports/TruckInDetail', "_blank");
    }

    function TruckInPassReport(id) {
        window.open('/import/reports/PickUpTicket?type=' + type + "&id=" + id, "_blank");

    }
