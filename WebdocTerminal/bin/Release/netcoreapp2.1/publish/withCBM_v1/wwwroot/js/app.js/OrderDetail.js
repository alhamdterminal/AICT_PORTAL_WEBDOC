

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
    var containersnumberList = []
    var dOidAndoDid;
    var totalQuantity;
    var subtractTotalQuantity;
    var balacce;
    var iposData;

    ///new Var//
    var containerIndexId;
    var containerId;
    var voyageid;
    var containerIndexNumber;
    var type = "CY";

    var iposvalue = [];
    var returnId = [];
    var doItems = [];
    var totaldelivereditem = [];


    function formatDate(date) {

        return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
    }

    //document.onload = $(function () {
        function formfiled() {



            if (PermissionInputs.find(x => x.fieldName == "IsWeight" && x.isChecked == false)) {
                document.getElementById('Weight').disabled = true;


            } if (PermissionInputs.find(x => x.fieldName == "IsNoOfPackages" && x.isChecked == false)) {

                document.getElementById('Packages').disabled = true;

            } if (PermissionInputs.find(x => x.fieldName == "DODate" && x.isChecked == false)) {
                document.getElementById('single_cal1').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "DOYear" && x.isChecked == false)) {
                document.getElementById('single_cal2').disabled = true;

            } if (PermissionInputs.find(x => x.fieldName == "DONo" && x.isChecked == false)) {
                document.getElementById('donumber').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "TotalPackages" && x.isChecked == false)) {
                document.getElementById('totalpac').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "Balance" && x.isChecked == false)) {
                document.getElementById('balance').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "Delivered" && x.isChecked == false)) {
                document.getElementById('delivereditem').disabled = true;

            } if (PermissionInputs.find(x => x.fieldName == "TotalDelivered" && x.isChecked == false)) {
                document.getElementById('totaldelivereditem').disabled = true;

            }
            //if (PermissionInputs.find(x => x.fieldName == "Importbill" && x.isChecked == false)) {
            //    document.getElementById('billno').disabled = true;

            //}
            if (PermissionInputs.find(x => x.fieldName == "BillDate" && x.isChecked == false)) {
                document.getElementById('billdate').disabled = true;

            } if (PermissionInputs.find(x => x.fieldName == "Remarks" && x.isChecked == false)) {
                document.getElementById('remarks').disabled = true;

            }


            var currentdate = formatDate(new Date());






            //  document.getElementById("edi").addEventListener("click", sendEDI, false);



            var url_string = window.location.href

            var url = new URL(url_string);


            doNumber = url.searchParams.get("doNumber");

            // loadGatePassAndDeliveryorderNumber();

            $.get('/APICalls/GetDeliveryOrderBYdeliveryOrderNumber?doNumber=' + doNumber, function (data) {

                if (data != null) {

                    if (data.containerIndexId != null) {
                        type = "CFS"
                        $.get('/APICalls/GetDeliveryOrderByDONumber?doNumber=' + doNumber, function (value) {

                            console.log("value", value);
                            console.log("value.validTo", value.validTo);


                            var validto = formatDate(value.validTo);

                            console.log("currentdate", currentdate);

                            console.log("validto", validto);

                            if (value.cargoType == "LCL") {

                                if (value.gatePassType) {
                                    unitType = value.gatePassType;

                                    if (unitType == "PACKAGES") {
                                        document.getElementById('Weight').disabled = true;
                                    }
                                    else {
                                        document.getElementById('Packages').disabled = true;
                                    }


                                } else {
                                    unitType = "PACKAGES";
                                }

                            }
                            else if (value.cargoType == "FCL") {
                                //unitType = "WEIGHT";
                                if (value.gatePassType) {
                                    unitType = value.gatePassType;

                                    if (unitType == "PACKAGES") {
                                        document.getElementById('Weight').disabled = true;
                                    }
                                    else {
                                        document.getElementById('Packages').disabled = true;
                                    }
                                } else {
                                    unitType = "PACKAGES";
                                }
                            }
                            else {
                                return showToast("please set cargo type", "error");
                            }

                            $('#vesl').val(value.vesselName);
                            $('#igmyear').val(value.igmYear);
                            $('#cargoType').val(value.cargoType);
                            $('#deliveryType').val(value.deliveryType);
                            $('#igm').val(value.igmNumber);
                            $('#containerValue').val(value.containerNumber);
                            $('#containerIndexValue').val(value.indexNumner);
                            $('#blnumb').val(value.blNumber);
                            $('#voyageno').val(value.voyageNo);

                            blnumber = value.blNumber;

                            $.get('/APICalls/GetPackTypeByBLNumner?blnumber=' + blnumber + '&containerIndexId=' + data.containerIndexId + '&type=' + type, function (itemList) {

                                console.log("itemList", itemList);

                                iposData = itemList
                                reloadGrid(unitType);

                                if (doItems.length > 0)
                                    calculateBalance(0, doItems.map(item => item.Quantity).reduce((prev, next) => prev + next, 0));
                            });

                            if (value.cargoType == "FCL") {
                                getcfsFcLcontainerList(value.igmNumber, value.indexNumner);
                            }

                            if (value.cargoType == "LCL") {
                                getcycontainerList("", "");
                            }

                        });
                    }

                    else {

                        type = "CY"

                        $.get('/APICalls/GetDeliveryOrderBydeliveryOrderNumberForCyContainer?doNumber=' + doNumber, function (value) {
                            $('#vesl').val(value.vesselName);
                            $('#igmyear').val(value.igmYear);
                            $('#igm').val(value.igmNumber);
                            $('#cargoType').val(value.cargoType);
                            $('#deliveryType').val(value.deliveryType);
                            $('#containerValue').val(value.containerNumber);
                            $('#containerIndexValue').val(value.indexNumner);
                            $('#blnumb').val(value.blNumber);
                            $('#voyageno').val(value.voyageNo);

                            if (value.cargoType == "CY") {
                                unitType = "PACKAGES";
                            }
                            else {
                                return showToast("please set cargo type", "error");
                            }

                            blnumber = value.blNumber;
                            $.get('/APICalls/GetPackTypeByIGMBLNumner?blnumber=' + blnumber + '&igm=' + value.igmNumber, function (itemList) {
                                console.log("itemList", itemList)
                                iposData = itemList
                                reloadGrid(unitType);

                                if (doItems.length > 0)
                                    calculateBalance(0, doItems.map(item => item.Quantity).reduce((prev, next) => prev + next, 0));
                            });


                            getcycontainerList(value.igmNumber, value.indexNumner)


                        });
                    }

                }

            });
            getGatePassByDoNumber();
        };



    function getcycontainerList(igm, index) {


        $.get('/APICalls/GetCYContainerNumbersByIgmIndex?igm=' + igm + '&index=' + index, function (res) {

            console.log("res for container list", res)

            containerListDropDown(res)


        });
    }

    function getcfsFcLcontainerList(igm, index) {


        $.get('/APICalls/GetcfsFcLContainerNumbersByIgmIndex?igm=' + igm + '&index=' + index, function (res) {

            console.log("res for container list", res)

            containerListDropDown(res)


        });
    }

    function containerListDropDown(res) {

        $("#contaiernsdropdown").dxSelectBox({
            dataSource: res,
            displayExpr: "value",
            valueExpr: "value",
            acceptCustomValue: true,
            onValueChanged: function (data) {

            },

        }).dxValidator({
            validationRules: [{ type: 'required' }]
        });  ;
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


        var cargotpe = $('#cargoType').val()

    if (cargotpe == "CY") {

        unitType = "PACKAGES";

    $('.unit-type').html('Total Qty');

    reloadGrid(unitType);

    reportGrid(unitType);

        }
    else if (cargotpe == "FCL") {

        //unitType = "WEIGHT";

        //$('.unit-type').html('Total Weight');

        unitType = $("input[name='type']:checked").val();

    if (unitType == "WEIGHT")
    $('.unit-type').html('Total Weight');
    else
    $('.unit-type').html('Total Packages');

    reloadGrid(unitType);

    reportGrid(unitType);

        }
    else {

        unitType = $("input[name='type']:checked").val();

    if (unitType == "WEIGHT")
    $('.unit-type').html('Total Weight');
    else
    $('.unit-type').html('Total Packages');

    reloadGrid(unitType);

    reportGrid(unitType);

        }

  

    }

    function loadGatePassAndDeliveryorderNumber() {
        $.get('/APICalls/GetOrderDetail', function (value) {

            $('#donumber').val(doNumber);

            $.get('/APICalls/GetDeliveryOrderBill?DoNumber=' + doNumber, function (resp) {

                $('#billno').val(resp.billNo);

                $('#billdate').val(formatDate(resp.billDate));

            });


        });
    }



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


        console.log("unit with ty", type)
        console.log("unit with ty", unit)
    oderDetailItems = [];

    if (type == "CY") {
        iposData.forEach(x => {
            oderDetailItems.push(x.numberofPackages)
        });
    totalQuantity = '';
            totalQuantity = oderDetailItems.reduce((a, b) => a + b, 0);
    $('#totalpac').val(totalQuantity);

        } else {
            if (unitType == "PACKAGES") {

        iposData.forEach(x => {
            oderDetailItems.push(x.numberofPackages)
        });
    totalQuantity = '';
                totalQuantity = oderDetailItems.reduce((a, b) => a + b, 0);

    $('#totalpac').val(totalQuantity);


            }

    else {
        iposData.forEach(x => {
            oderDetailItems.push(x.blGrossWeight)
        });

    totalQuantity = '';
                totalQuantity = oderDetailItems.reduce((a, b) => a + b, 0);

    $('#totalpac').val(totalQuantity.toFixed(2));

            }
               

            
        }



    var columns = [];


    if (type == "CY") {

        console.log("test containersnumberList", containersnumberList)


            columns = [
    "truckNumber",
    {
        dataField: "numberofPackages",
    caption: "No of Contanier Qty",

    dataType: "number",
    format: "#,##0.##",
    editorOptions: {
        step: 0
                    },

    validationRules: [{type: "required" }, {
        type: "pattern",
    message: 'This is required',
    pattern: '^[1-9][0-9]*$'
                    }]
                },
                 //{
        //           dataField: "gatePasscontainerNumber",
        //           validationRules: [{ type: "required" }],
        //           caption: "Container No",
        //           lookup: {
        //               dataSource: containersnumberList,
        //               valueExpr: "value",
        //               displayExpr: "value"
        //           }
        //       },
        "packageType"
                ]

        }
    if (type == "CFS") {
        if (unit == "PACKAGES") {
        columns = [
            "truckNumber",
            {
                dataField: "numberofPackages",
                caption: "No of Packages",

                dataType: "number",
                format: "#,##0.##",
                editorOptions: {
                    step: 0
                },

                validationRules: [{ type: "required" }, {
                    type: "pattern",
                    message: 'This is required',
                    pattern: '^[1-9][0-9]*$'
                }]
            },
            "packageType"
        ]
    }
    else {

        columns = [
            {
                dataField: "truckNumber",
                caption: "Truck Number",
                validationRules: [{ type: "required" }],
            },
            {
                dataField: "blGrossWeight",
                caption: "Weight",

                dataType: "number",
                format: "#,##0.##",
                editorOptions: {
                    step: 0.0
                },
                validationRules: [{ type: "required" }],
                //validationRules: [{ type: "required" }, {
                //    type: "pattern",
                //    message: 'This is required',
                //    pattern: '^\d+((.)|(.\d{0,1})?)$'
                //}]
            },
            "packageType"
        ]

    }
            }

    var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


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

        console.log(e);

    var quantity = unitType == "PACKAGES" ? e.data.numberofPackages : e.data.blGrossWeight;
                 
                 calculateBalance(quantity, doItems.map(item => item.Quantity).reduce((prev, next) => prev + next, 0));
                

                
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

    function calculateBalance(delivered, totalDelivered) {

        console.log("delivered", delivered)
        console.log("totalDelivered", totalDelivered)

    quantity = delivered + totalDelivered;
    totaldelivereditem = [];
    subtractTotalQuantity = '';
    subtractTotalQuantity = quantity
    balacce = totalQuantity - subtractTotalQuantity;

    //$('#balance').val(balacce);
    //$('#delivereditem').val(delivered);


    var doitem = orderDetailForm.elements["Delivered"].value;
    totaldelivereditem.push(parseInt(doitem, 10));
        var tdoitem = totaldelivereditem.reduce((a, b) => a + b, 0);
    $('#totaldelivereditem').val(quantity.toFixed(2));


    if (type == "CY") {
        $('#balance').val(balacce);
    $('#delivereditem').val(delivered);
    $('#totaldelivereditem').val(quantity);

        }
    else {
            if (unitType == "PACKAGES") {
        $('#balance').val(balacce);
    $('#delivereditem').val(delivered);
    $('#totaldelivereditem').val(quantity);

            }
    else {
        $('#balance').val(balacce.toFixed(2));
    $('#delivereditem').val(delivered.toFixed(2));
    $('#totaldelivereditem').val(quantity.toFixed(2));

            }

        }


    }

    function myFunc(total, num) {
        return total + num;
    }

    function saveOrderDetail() {

        var form = document.getElementById('orderDetailForm');

    console.log("form, form", form)

    form.reportValidity();

    if (form.reportValidity()) {
        iposvalue = [];


    var values = $('#orderDetailForm').serialize();

    console.log("values", values);

            iposData.forEach(x => {

        let y = {
        "TruckNumber": x.truckNumber,
    "Quantity": unitType == "PACKAGES" ? x.numberofPackages : x.blGrossWeight,
    "PackageType": x.packageType,
                }
    iposvalue.push(y);
            });

            // $.get('/APICalls/CheckGatePassByDONumber?doNumber=' + doNumber, function (data) {

            //if (data.error == true) {
            //    showToast(data.message, "error");
            //}

            //else {

            var truckNumberEmpty = iposvalue.find(t => typeof t.TruckNumber === 'undefined');

    if (truckNumberEmpty) {

        showToast('Please enter truck number', "error");

    return;
            }

    console.log("type",type)

    var containerNum = $("#contaiernsdropdown").dxSelectBox('instance').option('value');
    var cargoType = $("#cargoType").val();

    if (cargoType) {
                if (type == "CY") {
        $("#btnSubmit").attr("disabled", true);
    console.log("containerNum", containerNum);

    if (containerNum) {

                        var igmnumber = $('#igm').val();
    var indexnumber = $('#containerIndexValue').val();

    $.get('/APICalls/CheckCyContainerHold?&igm=' + igmnumber + '&index=' + indexnumber, function (res) {
                             
                            if (res.length) {

        loadGridhold(res);
    $('#HoldStatusModal').modal('toggle');
                                 
                            }
    else {

        loadGridhold([]);

    $.post("/Import/Delivery/CreateOrderDetail?val" + values, {type: type, containernumber: containerNum, doNumber: doNumber, unitType: unitType, iposvalue: iposvalue }, function (data) {

        //  loadGatePassAndDeliveryorderNumber();

        getGatePassByDoNumber();
    if (data.error == true) {

        showToast(data.message, "error");

    window.setInterval('refresh()', 3000);
    return false;
                                    }
    else {

        showToast("GatePass Created with GP # " + data.gatePassNumber, "success");

    window.setInterval('refresh()', 3000);

                                    }


                                })
                            }




                        });

                      

                    } else {
        showToast("please select container number ", "error")
    }
                }
    if (type == "CFS") {
        $("#btnSubmit").attr("disabled", true);
    if (cargoType == "FCL" && unitType == "PACKAGES") {

                        if (cargoType == "FCL" && unitType == "PACKAGES" && Number($('#delivereditem').val()) == 1) {
                            if (containerNum) {

                                var igmnumber = $('#igm').val();
    var indexnumber = $('#containerIndexValue').val();

    $.get('/APICalls/CheckCFSContainerHoldIGMIndexWise?&igm=' + igmnumber + '&indexno=' + indexnumber, function (res) {

                                    if (res.length) {

        loadGridhold(res);
    $('#HoldStatusModal').modal('toggle');

                                    }
    else {

        loadGridhold([]);

    $.post("/Import/Delivery/CreateOrderDetail?val" + values, {type: type, containernumber: containerNum, doNumber: doNumber, unitType: unitType, iposvalue: iposvalue }, function (data) {

        //  loadGatePassAndDeliveryorderNumber();

        getGatePassByDoNumber();
    if (data.error == true) {

        showToast(data.message, "error");

    window.setInterval('refresh()', 3000);

    return false;
                                            }
    else {

        showToast("GatePass Created with GP # " + data.gatePassNumber, "success");
    window.setInterval('refresh()', 3000);

                                            }


                                        })
                                    }




                                })



                            }
    else {
        showToast("please select container number ", "error")
    }

                        }
    else {
        showToast("In case of CFS FCL (Packages Delivery must select no of packages = 1 ", "error")
    }
                           
                        

                    } else {

                        if (cargoType == "FCL" && unitType == "WEIGHT") {
                            if (containerNum) {
                                return alert("please un select container no");
                            }
                        }
    var igmnumber = $('#igm').val();
    var indexnumber = $('#containerIndexValue').val();

    $.get('/APICalls/CheckCFSContainerHoldIGMIndexWise?&igm=' + igmnumber + '&indexno=' + indexnumber, function (res) {

                            if (res.length) {

        loadGridhold(res);
    $('#HoldStatusModal').modal('toggle');

                            }
    else {

        loadGridhold([]);

    $.post("/Import/Delivery/CreateOrderDetail?val" + values, {type: type, containernumber: containerNum, doNumber: doNumber, unitType: unitType, iposvalue: iposvalue }, function (data) {


        //  loadGatePassAndDeliveryorderNumber();

        getGatePassByDoNumber();
    if (data.error == true) {

        showToast(data.message, "error");

    window.setInterval('refresh()', 3000);

    return false;
                                    }
    else {

        showToast("GatePass Created with GP # " + data.gatePassNumber, "success");
    window.setInterval('refresh()', 3000);

                                    }


                                })
                            }




                        })


                    }
                
                }

            }
    else {
        showToast("Cargo Type is not define ", "success");
            }

            
           
        }




    }


    function loadGridhold(data) {

        $("#holdGrid").dxDataGrid({
            dataSource: data,
            keyExpr: "holdId",
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,
            paging: {
                pageSize: 10
            },
            searchPanel: {
                visible: true,
                width: 240,
                placeholder: "Search..."
            },

            columns: [
                {
                    dataField: "auctionLotNo",
                    caption: "Auction Lot No",
                    allowEditing: false,
                },
                {
                    dataField: "holdDate",
                    caption: "Hold Date",
                    allowEditing: false,
                    dataType: 'date',
                    format: 'dd/MM/yyyy',
                    allowEditing: false,
                },
                {
                    dataField: "holdType",
                    caption: "hold Type",
                    allowEditing: false,
                },
                {
                    dataField: "specialInstructions",
                    caption: "special Instructions",
                    allowEditing: false,
                },
                {
                    dataField: "role",
                    caption: "Role",
                    allowEditing: false,
                },
                {
                    dataField: "isHold",
                    caption: "Hold Status",
                    allowEditing: false,
                },
                {
                    dataField: "ro",
                    caption: "RO Number",

                },
                {
                    dataField: "removeInstruction",
                    caption: "Remove Instruction",
                    validationRules: [{
                        type: "required",
                        message: "Remove Instruction Is Required"
                    }],

                },
            ],

        });

    }


    function getGatePassByDoNumber() {

        doItems = [];
    $.get("/Import/Delivery/GetGatePassbyDoNumber", {doNumber: doNumber }, function (data) {

        returnId = data;

    console.log("returnId", returnId);
    console.log("type", type);


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
                    "NoOfPackages": y.quantity,
                    "GateOutDisplay": y.isGateOut == true ? "GateOut Sent to Customs" : "Un Deliverd",
                    "IsGateOut": false,
                    "gatePassId": y.gatePassId,
                    "ContainerNo": x.gatePasscontainerNumber,
                }

                doItems.push(z);

            });
        });
            }

    reportGrid(unitType);

        })
    }

    function GatePassReport(id) {

        window.open('/import/reports/GatePass?type=' + type + "&id=" + id, "_blank");

    }


    function GatePassReportDotMatrix(id) {

        window.open('/import/Reports/GatePassDotMatrix?type=' + type + "&id=" + id, "_blank");

    }
    function DeleteGatePass(value) {
        console.log(value);
    var GatePassId = value;
    $.post('/Import/Delivery/DeleteGatePass?GatePassId=' + GatePassId, function (data) {

            if (data.error == true) {

        showToast(data.message, "error");

            }
    else {
        showToast(data.message, "success");

            }

    window.setInterval('refresh()', 3000);



        })
    }


    function refresh() {
        window.location.reload();
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
        dataField: "ContainerNo",
    caption: "Container No",
    allowEditing: false,
                },
    {
        dataField: "TruckNumber",
    caption: "Truck No",
    allowEditing: false,
                },
    {
        dataField: "Quantity",
    allowEditing: false,
    caption: unit == "WEIGHT" ? "Weight" : "No of Packages"
                },
    {
        dataField: "PackageType",
    caption: "Package Type",
    validationRules: [{type: "required" }],
    allowEditing: false,
                },
    {
        dataField: "NoOfPackages",
    caption: unit == "WEIGHT" ? "Weight" : "No of Packages",
    allowEditing: false,

    validationRules: [{type: "required" }, {
        type: "pattern",
    message: 'This is required',
    pattern: '^[1-9][0-9]*$'
                    }]
                },
    {
        dataField: "Status",
    caption: "Status",
    validationRules: [{type: "required" }],
    allowEditing: false,
    lookup: {
        dataSource: [{value: "P" }, {value: "F" }],
    displayExpr: "value",
    valueExpr: "value"
                    }
                },
    {
        dataField: "GateOutDisplay",
    allowEditing: false,
                },
    {
        caption: "Gate out",
    dataField: "IsGateOut",
    dataType: "boolean",
    allowEditing: false,
                },
    {
        dataField: "gatePassId",
    caption: '',
    cellTemplate: function (container, options) {
        $("<button type='button' class='btn btn-success' onClick='GatePassReport(" + options.value + ")'>Print Laser</button>")
            .appendTo(container);
                    }, allowEditing: false
                },
    {
        dataField: "gatePassId",
    caption: '',
    cellTemplate: function (container, options) {
        $("<button type='button' class='btn btn-success' onClick='GatePassReportDotMatrix(" + options.value + ")'>Print Dot-Matrix</button>")
            .appendTo(container);
                    }, allowEditing: false
                },
    {
        dataField: "gatePassId",
    caption: '',
    cellTemplate: function (container, options) {
        $("<button type='button' class='btn btn-success' onClick='DeleteGatePass(" + options.value + ")'>Delete</button>")
            .appendTo(container);
                    }, allowEditing: false
                },
    ], onRowUpdating: function (e) {
    }

        }).dxDataGrid("instance");

    }

    function sendEDI() {
        var result = doItems.filter(c => c.PackageType != null && c.IsGateOut == true && c.GateOutDisplay == "GateOut Sent to Customs");
    console.log("result", result)
    console.log("result.length", result.length)
    if (result.length) {
        showToast("Can't Submite Delivery Edi", "error");
    timer();
        }
    else {
            var gateoutTrucks = doItems.filter(c => c.PackageType != null && c.IsGateOut == true && c.GateOutDisplay == "Un Deliverd");

    console.log("gateoutTrucks", gateoutTrucks);

    $.post("/Import/Delivery/SendGateOut?DoNumber=" + doNumber + "&Type=" + type, {DONumber: doNumber, Type: type, doItems : gateoutTrucks }, function (resp) {

        doItems = doItems.filter(c => c.PackageType != null && c.IsGateOut == false);

    if (!resp.error) {
        showToast(resp.message, "success"); 
                }
    else {
        showToast(resp.message, "error"); 
                }

    window.setInterval('refresh()', 3000);
    reportGrid(unitType);

            });

        }


    }


    function timer() {
        setTimeout(pageRelod, 3000);

    }

    function pageRelod() {
        window.location.href = window.location.href;
    }

