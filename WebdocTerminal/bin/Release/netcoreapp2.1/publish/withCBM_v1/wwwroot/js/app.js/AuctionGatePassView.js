

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var vissalname;
    var doNumber;
    var contanr;
    var containerindexid;
    var blnumber;

    var unitType;

    var oderDetailItems = []
    var dOidAndoDid;
    var totalQuantity;
    var subtractTotalQuantity;
    var balacce;
    var iposData;

    ///new Var//
    var containerIndexId;
    // var containerId;
    var voyageid;
    var containerIndexNumber;
    var type = "";

    var iposvalue = [];
    var returnId = [];
    var doItems = [];
    var totaldelivereditem = [];


    // For Auction GatePass
    var containerId;
    //document.onload = $(function () {
        function formfiled() {


            //   document.getElementById("edi").addEventListener("click", sendEDI, false);

            unitType = "PACKAGES";

            var url_string = window.location.href

            var url = new URL(url_string);


            containerId = url.searchParams.get("containerId");
            type = url.searchParams.get("type");

            console.log("my container Id ", containerId);
            console.log("type ", type);


            //  loadGatePassAndDeliveryorderNumber();

            if (type == "CFS") {
                type = "CFS"
                $.get('/APICalls/GetDeliveryOrderByContainerId?containerId=' + containerId, function (data) {
                    console.log("data", data)
                    $('#vesl').val(data.vesselName);
                    $('#igmyear').val(data.igmYear);
                    $('#igm').val(data.igmNumber);
                    $('#containerValue').val(data.containerNumber);
                    $('#containerIndexValue').val(data.indexNumner);
                    $('#blnumb').val(data.blNumber);
                    $('#voyageno').val(data.voyageNo);
                    blnumber = data.blNumber;

                    $.get('/APICalls/GetPackTypeByBLNumner?blnumber=' + blnumber + '&containerIndexId=' + data.containerIndexId + '&type=' + type, function (itemList) {

                        console.log("itemList", itemList)

                        iposData = itemList
                        reloadGrid(unitType);
                        console.log("doItems cfs", doItems)
                        if (doItems.length > 0)
                            calculateBalance(0, doItems.map(item => item.Quantity).reduce((prev, next) => prev + next, 0));


                    });

                });
            }
            else {
                $.get('/APICalls/GetCYContainerByContainerId?containerId=' + containerId, function (data) {
                    type = "CY"

                    $('#vesl').val(data.vesselName);
                    $('#igmyear').val(data.igmYear);
                    $('#igm').val(data.igmNumber);
                    $('#containerValue').val(data.containerNumber);
                    $('#containerIndexValue').val(data.indexNumner);
                    $('#blnumb').val(data.blNumber);
                    $('#voyageno').val(data.voyageNo);
                    blnumber = data.blNumber;

                    $.get('/APICalls/GetPackTypeByBLNumner?blnumber=' + blnumber, function (itemList) {
                        console.log("itemList", itemList)
                        iposData = itemList
                        reloadGrid(unitType);
                        console.log("doItems", doItems)
                        if (doItems.length > 0)
                            calculateBalance(0, doItems.map(item => item.Quantity).reduce((prev, next) => prev + next, 0));
                    });

                });
            }


            getGatePassByDoNumber();

        }

    //document.onload = $(function () {
    function formatDate(date) {
            var d = new Date(date),
                month = '' + (d.getMonth() + 1),
                day = '' + d.getDate(),
                year = d.getFullYear();

            if (month.length < 2)
                month = '0' + month;
            if (day.length < 2)
                day = '0' + day;

            return [year, month, day].join('-');
        }

    function setType() {

        unitType = $("input[name='type']:checked").val();

    if (unitType == "WEIGHT")
    $('.unit-type').html('Total Weight');
    else
    $('.unit-type').html('Total Packages');

    reloadGrid(unitType);

    reportGrid(unitType);

    }

    //function loadGatePassAndDeliveryorderNumber() {
        //    $.get('/APICalls/GetOrderDetail', function (value) {

        //        $('#donumber').val(doNumber);

        //        $.get('/APICalls/GetDeliveryOrderBill?DoNumber=' + doNumber, function (resp) {

        //            $('#billno').val(resp.billNo);

        //            $('#billdate').val(formatDate(resp.billDate));

        //        });


        //    });
        //}



        function checkColumn(e, field) {

            if (e.parentType === "dataRow" && e.dataField !== field) {
                return;
            }

            e.editorOptions.disabled = true;
        }

    function hideMenifestedColumns(e) {


        if (PermissionInputs.find(x => x.fieldName == "TruckNumber" && x.isChecked == false)) {

        checkColumn(e, "truckNumber");
        }
        if (PermissionInputs.find(x => x.fieldName == "NumberOfPackages" && x.isChecked == false)) {

        checkColumn(e, "numberofPackages");
        }
        if (PermissionInputs.find(x => x.fieldName == "PackageType" && x.isChecked == false)) {

        checkColumn(e, "packageType");
        }
        if (PermissionInputs.find(x => x.fieldName == "Weight" && x.isChecked == false)) {

        checkColumn(e, "Weight");
        }


    }


    function reloadGrid(unit) {

        oderDetailItems = [];

    if (unitType == "PACKAGES")
            iposData.forEach(x => {
        oderDetailItems.push(x.numberofPackages)
    });
    else
            iposData.forEach(x => {
        oderDetailItems.push(x.blGrossWeight)
    });

    totalQuantity = '';
        totalQuantity = oderDetailItems.reduce((a, b) => a + b, 0);
    $('#totalpac').val(totalQuantity);

    var columns = [];

    if (unit == "PACKAGES") {
        columns = [
            "truckNumber",
            {
                dataField: "numberofPackages",
                caption: "No of Packages"
            },
            "packageType"
        ]
    }
    else {

        columns = [
            "truckNumber",
            {
                dataField: "blGrossWeight",
                caption: "Weight"
            },
            "packageType"
        ]

    }

    var dataGrid = $("#gridContainer").dxDataGrid({
        dataSource: iposData,
    keyExpr: "igmoId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },
    editing: {
        mode: "batch",
    allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click"
            },


    columns: columns,
    onEditorPreparing: function (e) {

        hideMenifestedColumns(e);
            },

    onRowUpdated: function (e) {

                var quantity = unitType == "PACKAGES" ? e.data.numberofPackages : e.data.blGrossWeight;

                calculateBalance(quantity, doItems.map(item => item.Quantity).reduce((prev, next) => prev + next, 0));
            },


        }).dxDataGrid("instance");
    }

    function calculateBalance(delivered, totalDelivered) {

        console.log("delivered", delivered);
    console.log("totalDelivered", totalDelivered);

    quantity = delivered + totalDelivered;
    totaldelivereditem = [];
    subtractTotalQuantity = '';
    subtractTotalQuantity = quantity
    balacce = totalQuantity - subtractTotalQuantity;
    $('#balance').val(balacce);
    $('#delivereditem').val(delivered);
    var doitem = orderDetailForm.elements["Delivered"].value;
    totaldelivereditem.push(parseInt(doitem, 10));
        var tdoitem = totaldelivereditem.reduce((a, b) => a + b, 0);
    $('#totaldelivereditem').val(quantity);

    }

    function myFunc(total, num) {
        return total + num;
    }

    function saveOrderDetail() {

        iposvalue = [];


    var values = $('#orderDetailForm').serialize();

        iposData.forEach(x => {

        let y = {
        "TruckNumber": x.truckNumber,
    "Quantity": unitType == "PACKAGES" ? x.numberofPackages : x.blGrossWeight,
    "PackageType": x.packageType,
            }
    iposvalue.push(y);
        });

        var truckNumberEmpty = iposvalue.find(t => typeof t.TruckNumber === 'undefined');

    if (truckNumberEmpty) {

        showToast('Please enter truck number', "error");

    return;
        }

    $.post("/Import/Delivery/CreateOrderDetailAuction?val" + values, {type: type, containerId: containerId, unitType: unitType, iposvalue: iposvalue }, function (data) {

            //  loadGatePassAndDeliveryorderNumber();

            if (data.error == true) {

        showToast(data.message, "error");

            }
    else {

        getGatePassByDoNumber();

    showToast("GatePass Created with GP # " + data.gatePassNumber, "success");


            }




        })



    }

    function getGatePassByDoNumber() {

        doItems = [];
    $.get("/Import/Delivery/GetAuctionGatePassbyDoNumber", {containerId: containerId, type: type }, function (data) {

        returnId = data;

    if (returnId) {

        returnId.forEach(x => {

            unitType = x.unitType;

            x.doItems.forEach(y => {
                let z = {

                    "DOItemId": y.doItemId,
                    "TruckNumber": y.truckNumber,
                    "Quantity": y.quantity,
                    "PackageType": y.packageType,
                    "Status": y.status,
                    "NoOfPackages": y.noOfPackages,
                    "GateOutDisplay": y.isGateOut == true ? "GateOut Sent to Customs" : "",
                    "IsGateOut": false,
                    "gatePassAuctionId": y.gatePassAuctionId,
                }

                console.log("zzzzzzzzzzzzzzzzzzz", z)
                doItems.push(z);

            });
        });
            }

    reportGrid(unitType);

        })
    }

    function GatePassReport(id) {

        window.open('/import/reports/GatePassAuction?type=' + type + "&gatePassId=" + id, "_blank");
    }
    function DeleteGatePass(value) {
        console.log(value);
    var GatePassId = value;
    $.post('/Import/Delivery/DeleteAuctionGatePass?GatePassAuctionId=' + GatePassId, function (data) {
        showToast("Deleted", "success");
    location.reload();


        })
    }

    function reportGrid(unit) {

        var dataGrid = $("#gridContainer2").dxDataGrid({
        dataSource: doItems,
    keyExpr: "DOItemId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },
    editing: {
        mode: "batch",
    allowUpdating: true,
    selectTextOnEditStart: true,
    startEditAction: "click"
            },
    columns: [
    {
        dataField: "TruckNumber",
    caption: "Truck No"
                },
    {
        dataField: "Quantity",
    caption: unit == "WEIGHT" ? "Weight" : "No of Packages"
                },
    {
        dataField: "PackageType",
    caption: "Package Type",
    validationRules: [{type: "required" }]
                },
    {
        dataField: "NoOfPackages",
    caption: "Number of Packages",
    validationRules: [{type: "required" }, {
        type: "pattern",
    message: 'This is required',
    pattern: '^[1-9][0-9]*$'
                    }]
                },
                //{
        //    dataField: "Status",
        //    caption: "Status",
        //    validationRules: [{ type: "required" }],
        //    lookup: {
        //        dataSource: [{ value: "P" }, { value: "F" }],
        //        displayExpr: "value",
        //        valueExpr: "value"
        //    }
        //},
        {
            dataField: "GateOutDisplay"
        },
        //{
        //    caption: "Gate out",
        //    dataField: "IsGateOut",
        //    dataType: "boolean"
        //},
        {
            dataField: "gatePassAuctionId",
            caption: 'S No.',
            cellTemplate: function (container, options) {
                $("<button type='button' class='btn btn-success' onClick='GatePassReport(" + options.value + ")'>Print</button>")
                    .appendTo(container);
            }, allowEditing: false
        }
                //{
                //    dataField: "gatePassAuctionId",
                //    caption: '#',
                //    cellTemplate: function (container, options) {
                //        $("<button type='button' class='btn btn-success' onClick='DeleteGatePass(" + options.value + ")'>Delete</button>")
                //            .appendTo(container);
                //    }, allowEditing: false
                //},
            ], onRowUpdating: function (e) {
    }

        }).dxDataGrid("instance");

    }

    //function sendEDI() {

    //    var gateoutTrucks = doItems.filter(c => c.PackageType != null && c.IsGateOut == true);

    //    $.post("/Import/Delivery/SendGateOut?DoNumber=" + doNumber + "&Type=" + type, {DONumber: doNumber, Type: type, doItems: gateoutTrucks }, function (resp) {

    //        doItems = doItems.filter(c => c.PackageType != null && c.IsGateOut == false);

    //        if (!resp.error)
    //            showToast(resp.message, "success");
    //        else
    //            showToast(resp.message, "error");

    //        reportGrid(unitType);

    //    });


    //}

