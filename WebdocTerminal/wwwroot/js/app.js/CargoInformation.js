
    var type;
    var containers = [];
    var shippingAgents = [];

    var packageTypes = [
    {Name: "BULK" },
    {Name: "LOOSE SCRAP" },
    {Name: "BAGS" },
    {Name: "BALES" },
    {Name: "BOX" },
    {Name: "BUNDLE" },
    {Name: "CARTONS" },
    {Name: "CASE" },
    {Name: "CONTAINER" },
    {Name: "CRATES" },
    {Name: "DRUM" },
    {Name: "PACKAGES" },
    {Name: "PALLET" },
    {Name: "ROLL" },
    {Name: "UNIT" },
    {Name: "UNITS" }];

    //function showFilters() {

        //    type = $("input[name='type']:checked").val();

        //    $.get('/APICalls/GetFilters?Type=' + type, function (partial) {

        //        $('#filters').html(partial);
        //        containers = [];
        //        dataGrid(containers)
        //    })

        //}


        function showcargoDetaildesc() {


            var virno = $("#virnolist").dxAutocomplete("instance").option("value")
            var containerno = $("#containerlist").dxAutocomplete("instance").option("value");
            var indexno = $("#containerIndexlist").dxAutocomplete("instance").option("value")
            var blnumberlist = $("#blnumberlist").dxAutocomplete("instance").option("value")

            console.log('virno', virno);
            console.log('containerno', containerno);
            console.log('indexno', indexno);
            console.log('blnumberlist', blnumberlist);


            $.get('/APICalls/GetCargoInformationImport?igm=' + virno + '&container=' + containerno + '&index=' + indexno + '&blnumberlist=' + blnumberlist, function (data) {
                containers = data;
                dataGrid(containers)
            });

        }

    function containerCallback() {

        if (type == "CFS") {

        containerChangeCallback()

    }
    else {

        submitCargoInformation()
    }



    }

    function containerChangeCallback() {
        containerIndexId = $("#indexdb option:selected").val();
    $.get('/APICalls/GetCargoInformationCFSConatiner?containerIndexId=' + containerIndexId, function (data) {
        containers = data;
    dataGrid(containers);
        });

    }

    function submitCargoInformation() {

        containernumber = $("#containerCYdb option:selected").val();
    $.get('/APICalls/GetCargoInformationCYConatiner?CYContainerId=' + containernumber, function (data) {
        containers = data;
    dataGrid(containers);
        });

    }

    function dataGrid(containers) {
        console.log("containers", containers)
         
        var dataGrid = $("#CargoInformation").dxDataGrid({
        dataSource: containers,
    keyExpr: "",
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
        mode: "cell",
    allowUpdating: true
                //allowDeleting: true,
               // allowAdding: true
            },
    columns: [

    {
        dataField: "virNumber",
    caption: "Vir Number"
                },
    {
        dataField: "containerNumber",
    caption: "Container No"
                },
    {
        dataField: "blNumber",
    caption: "BL No"
                },
    {
        dataField: "indexNumber",
    caption: "Index No"
                },
    {
        dataField: "manifestedQTY",
    caption: "Manifested QTY"
                },
    {
        dataField: "goodsDesc",
    caption: "Goods Desc"
                },
    {
        dataField: "crNumber",
    caption: "CR No"
                },
    {
        dataField: "DO No",
    caption: "DONumber"
                },
    {
        dataField : "vesselArrival",
    caption : "Vessel Arrival",
    dataType : 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "line",
    caption: "Line"
                },
    {
        dataField: "line",
    caption: "Line"
                },
    {
        dataField: "vesselName",
    caption: "Vessel Name"
                },
    {
        dataField: "voyageNumber",
    caption: "Voyage No"
                },
    {
        dataField: "igmDate",
    caption: "IGM Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },

    {
        dataField: "arrivalInAICT",
    caption: "Arrival In AICT",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "truckNumber",
    caption: "Truck No"
                },
    {
        dataField: "cfscy",
    caption: "CFS / CY"
                },
    {
        dataField: "lclfcl",
    caption: "FCL / LCL"
                },
    {
        dataField: "size",
    caption: "Size"
                },
    {
        dataField: "type",
    caption: "Type"
                },
    {
        dataField: "destuffingDate",
    caption: "DestuffingDate",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "deliveryDate",
    caption: "Delivery Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "emptyOffHireDate",
    caption: "Empty Off Hire",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "brtShed",
    caption: "BRT Shed"
                },
    {
        dataField: "consigneeName",
    caption: "Consignee Name"
                },
    {
        dataField: "clearingAgent",
    caption: "ClearingAgent"
                },
    {
        dataField: "lineDONumber",
    caption: "Line DO No"
                },
    {
        dataField: "disPackages",
    caption: "Dis Packages"
                },
    {
        dataField: "foundQTY",
    caption: "Found QTY"
                },
    {
        dataField: "examinationDate",
    caption: "Examination Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },

    {
        dataField: "gatePassDate",
    caption: "GatePass Date",
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },
    {
        dataField: "gatePassNumber",
    caption: "GatePass No"
                },
    {
        dataField: "releaseStatus",
    caption: "Release Status"
                },
    {
        dataField: "actualQty",
    caption: "Actual Qty"
                },
    {
        dataField: "balanceQty",
    caption: "Balance Qty"
                },
    {
        dataField: "goodsType",
    caption: "Goods Type"
                },
    {
        dataField: "isdgCargo",
    caption: "DG Cargo"
                }




    ],
    onEditorPrepared(e) {

    },
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
            },
    onRowUpdated: function (e) {
        //type = $("input[name='type']:checked").val();

        //console.log(e);
        //console.log(e.data.shippingAgentId);
        //if (type == "CFS") {
        //    console.log("type", type);
        //    var ContainerIndexId = $('#indexdb option:selected').val();
        //    console.log("ContainerIndexId", ContainerIndexId)

        //    $.post('/Import/CargoInformation/UpdateContainerCFSAgent?ContainerIndexId=' + ContainerIndexId + '&ShippingAgentId=' + e.data.shippingAgentId, function (data) {
        //        showToast("Agent Updated", "success");
        //    });
        //}
        //if (type == "CY") {
        //    console.log("type", type);
        //    var CyConatinerId = $('#containerCYdb option:selected').val();
        //    console.log("CyConatinerId", CyConatinerId)
        //    $.post('/Import/CargoInformation/UpdateContainerCY?CyConatinerId=' + CyConatinerId + '&ShippingAgentId=' + e.data.shippingAgentId, function (data) {
        //        showToast("Agent Updated", "success");
        //    });
        //}



    }

        }).dxDataGrid("instance");
         
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
         if (PermissionInputs.find(x => x.fieldName == "ContainerType" && x.isChecked == false)) {

        document.getElementById('filters').style.display = "none";
        }
    }

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }


    function hideMenifestedColumns(e) {

        checkColumn(e, "containerNo");
    checkColumn(e, "virNo");
    checkColumn(e, "blNo");
    checkColumn(e, "indexNo");
    checkColumn(e, "manifestedWeight");
    checkColumn(e, "manifestedQTY");
    checkColumn(e, "manifestedUOP");
    checkColumn(e, "description");
    checkColumn(e, "marksAndNumber");
    checkColumn(e, "location");
    checkColumn(e, "gateInDate");
    checkColumn(e, "destuffDate");
    }