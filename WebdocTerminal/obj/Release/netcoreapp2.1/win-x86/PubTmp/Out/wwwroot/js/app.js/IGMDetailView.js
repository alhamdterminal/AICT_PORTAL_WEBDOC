


    var type;
    var containers = [];
    var consigneeList = [];
    var containerDetail = { };


    $(function () {

        $("#igmno").inputmask("aaaa-9999-99999999");

    test().then(function () {

        $("#type").attr("disabled", false);
        });


        
    });


    function test() {
        return new Promise(function (resolve, reject) {
        $.get('/APICalls/GetALlConsignees', function (res) {
            if (res) {
                consigneeList = res;
                resolve(); // Resolve the promise when AJAX request completes
            } else {
                consigneeList = [];
                resolve(); // Resolve the promise even if no data is returned
            }
        }).fail(function (error) {
            reject(error); // Reject the promise if AJAX request fails
        });
        });
    }


    function showFilters() {

        $('#IGMDetailSheet')[0].reset();


    type = $("#type option:selected").val();


    console.log("type", type)

    if (type == "CYContainers") {
        document.getElementById("consigneecolm").style.display = "block";
        } else {
        document.getElementById("consigneecolm").style.display = "none";
        }

    if (type == "CYContainers") {
        document.getElementById("vesselinfo").style.display = "none";
        } else {
        document.getElementById("vesselinfo").style.display = "block";
        }

    $.get('/APICalls/GetFilterForIGMDetail?Type=' + type, function (partial) {

        $('#filters').html(partial);

        });
    containers = [];

    dataGrid(containers)




    $("#searchBoxForConsigne").dxSelectBox({
        dataSource: {
        store: consigneeList,
    requireTotalCount: true,
    pageSize: 4,
    paginate: true,
            },
    name: 'ConsigneId',
    displayExpr: "text",
    valueExpr: "value",
    acceptCustomValue: true,
    searchEnabled: true,
        })

    restvalues();


    }



    var containrId;


    function refresh() {
        window.location.reload();
    }

    var InvoiceFoud = [
    {Name: "YES" },
    {Name: "NO" }
    ];

    var PackageFoud = [
    {Name: "YES" },
    {Name: "NO" }
    ];

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

    var CargoType = [
    {Name: "FCL" },
    {Name: "LCL" }
    ];



    function containerChangeCallback(ornt) {

        console.log("containerChangeCallback", ornt)

        populateTable();


    }


    function containerCallback() {
        //console.log("cycontainer", $("#containerIndexCYdb option:selected").val())

        populateTable();

    console.log("containerCallback")

    }


    function populateTable() {


        console.log("igm", igm)
        console.log("type", type)


    var number = "";

    if (type == "CFSIndexWise") {
        number = $("#indexdb option:selected").text()
    }
    if (type == "CFSContainerWise") {
        number = $("#containerdb option:selected").text();
        }

    if (type == "CYContainers") {
        number = $("#containerIndexCYdb option:selected").text();
        }



    console.log("indexnumber", number)

    if (igm && number) {

        $.get('/APICalls/GetIGMDetailContainers?igm=' + igm + "&number=" + number + "&type=" + type, function (data) {

            containers = data;
            containerDetail = data[0]

            console.log("containers", containers)

            dataGrid(containers);

            console.log("containerDetail", containerDetail)


            if (containerDetail) {

                console.log("containerDetail", containerDetail)



                //$('#shippingLiness').val(containerDetail.shippingLineId);
                //$('#consignee').val(containerDetail.consigneId);
                //$('#shippingAgents').val(containerDetail.shippingAgentId);
                //$('#goods').val(containerDetail.goodsHeadId);
                //$('#consignee').val(containerDetail.consigneId);
                //$('#shipper').val(containerDetail.shipperId);
                //$('#portAndTerminal').val(containerDetail.portAndTerminalId);

                console.log("type", type);
                if (type == "CYContainers") {
                    // $('#consignee').select2().val(containerDetail.consigneId).trigger('change.select2');
                    // $('#consignee').val(containerDetail.consigneId).trigger('change.select2');
                    if (containerDetail.consigneId) {

                        var ab = $("#searchBoxForConsigne").dxSelectBox('instance').option().dataSource.store.find(x => x.value == containerDetail.consigneId)

                        console.log("AB", ab);
                        $("#searchBoxForConsigne").dxSelectBox('instance').option("value", ab.value);
                        $("#searchBoxForConsigne").dxSelectBox('instance').option("text", ab.text);


                    }
                }
                $('#shippingLiness').select2().val(containerDetail.shippingLineId).trigger('change.select2');

                $('#shippingAgents').val(containerDetail.shippingAgentId).trigger('change.select2');
                $('#goods').val(containerDetail.goodsHeadId).trigger('change.select2');
                //$('#consignee').val(containerDetail.consigneId).trigger('change.select2');
                $('#shipper').val(containerDetail.shipperId).trigger('change.select2');
                $('#portAndTerminal').val(containerDetail.portAndTerminalId).trigger('change.select2');
                $('#portOfLoading').val(containerDetail.portOfLoadingId).trigger('change.select2');

                $('#vesselname').val(containerDetail.vesselName);
                $('#voyageNo').val(containerDetail.voyageNo);

                if (containerDetail.eta != null) {

                    console.log("etaa", new Date(containerDetail.eta.split("T")[0]).toISOString().substr(0, 10))
                    $('#etaDate').val(new Date(containerDetail.eta.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#etaDate').val('');
                }

                console.log("containerDetail", containerDetail)


                //if (containerDetail.isPartialContainer == true) {
                //    document.getElementById("isPartialContainer").checked = true;
                //    console.log("etst")

                //}
                //if (containerDetail.isPartialContainer == null) {
                //    document.getElementById("isPartialContainer").checked = false;
                //    console.log("test")
                //}

                if (containerDetail.isDGCargo == true) {
                    document.getElementById("isDGCargo").checked = true;


                }
                if (containerDetail.isDGCargo == null) {
                    document.getElementById("isDGCargo").checked = false;

                }

            }



        });

        }
    else {
        restvalues();
        }
    }




    function restvalues() {

        console.log("reset")

        $('#shippingLiness').select2().val('').trigger('change.select2');
    //$('#consignee').select2().val('').trigger('change.select2');


    $("#searchBoxForConsigne").dxSelectBox('instance').option("value", "");
    $("#searchBoxForConsigne").dxSelectBox('instance').option("text", "");

    $('#shippingAgents').select2().val('').trigger('change.select2');
    $('#goods').select2().val('').trigger('change.select2');
    $('#shipper').select2().val('').trigger('change.select2');
    $('#portAndTerminal').select2().val('').trigger('change.select2');
    $('#portOfLoading').select2().val('').trigger('change.select2');

    //document.getElementById("isPartialContainer").checked = false;

    document.getElementById("isDGCargo").checked = false;

    $('#etaDate').val('');
    $('#vesselname').val('');
    $('#voyageNo').val('');


    dataGrid([])

    }

    function dataGrid(containers) {


        console.log("containers", containers)
        console.log("type", type)
    console.log("consigneeList", consigneeList)

    var columns = [];

    if (type == "CYContainers") {

        console.log("CYContainers")

            columns = [
    {
        dataField: "serialNo",
    caption: "S.No #",
    allowEditing: false

                },
    {
        dataField: "containerNo",
    caption: "Container No",
    allowEditing: false

                },
    {
        dataField: "size",
    caption: "Size",
    allowEditing: false

                },
    {
        dataField: "isoCodeType",
    caption: "Type",
    allowEditing: false

                },
    {
        dataField: "manifestedSealNumber",
    caption: "Seal No",
    allowEditing: false
                },
    {
        dataField: "blNo",
    caption: "BL No",
    allowEditing: false

                },
    {
        dataField: "description",
    caption: "Description",
    allowEditing: false

                },
    {
        dataField: "marksAndNumber",
    caption: "Marks And Number",
    allowEditing: false

                },
    {
        dataField: "packageType",
    caption: "Package Type",
    allowEditing: false

                },
    {
        dataField: "noOfPackages",
    caption: "No Of Pkgs",
    allowEditing: false

                },
    {
        dataField: "blGrossWeight",
    caption: "BL Gross Weight",
    allowEditing: false

                },
    {
        dataField: "weightIntan",
    caption: "Weight In Tan",
    allowEditing: false

                },
    {
        dataField: "status",
    caption: "CY / CFS",
    allowEditing: false

                },

    {
        dataField: "cargoType",
    caption: "FCL / LCL",
    validationRules: [{type: "required" }],
    width: 100,
    lookup: {
        dataSource: CargoType,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },
    {
        dataField: "indexNo",
    caption: "Index No",
    allowEditing: false

                },
    {
        caption: "Is Part",
    dataField: "isPartialContainer",
    dataType: "boolean"
                },
    {
        caption: "Is DG Cargo",
    dataField: "isDGCargo",
    dataType: "boolean"
                },
    {
        dataField: "deliveryDate",
    caption: "Deliver Date",
    allowEditing: false,
    dataType: 'date',
    format: 'dd/MM/yyyy'
                },

    ]

        }

    else {


        console.log("CFSContainers")
            console.log("type", type)

    columns = [
    {
        dataField: "serialNo",
    caption: "S.No #",
    allowEditing: false

                },
    {
        dataField: "containerNo",
    caption: "Container No",
    allowEditing: false

                },
    {
        dataField: "size",
    caption: "Size",
    allowEditing: false

                },
    {
        dataField: "isoCodeType",
    caption: "Type",
    allowEditing: false

                },
    {
        dataField: "manifestedSealNumber",
    caption: "Seal No",
    allowEditing: false
                },
    {
        dataField: "blNo",
    caption: "BL No",
    allowEditing: false

                },
    {
        dataField: "description",
    caption: "Description",
    allowEditing: false

                },
    {
        dataField: "marksAndNumber",
    caption: "Marks And Number",
    allowEditing: false

                },
    {
        dataField: "packageType",
    caption: "Package Type",
    allowEditing: false

                },
    {
        dataField: "noOfPackages",
    caption: "No Of Pkgs",
    allowEditing: false

                },
    {
        dataField: "blGrossWeight",
    caption: "BL Gross Weight",
    allowEditing: false

                },
    {
        dataField: "manifestedWeight",
    caption: "Manifested Weight",
    validationRules: [{type: "required" }],
                },
    {
        dataField: "status",
    caption: "CY / CFS",
    allowEditing: false

                },
    {
        dataField: "cargoType",
    caption: "FCL / LCLL",
    validationRules: [{type: "required" }],
    width: 100,
    lookup: {
        dataSource: CargoType,
    valueExpr: "Name",
    displayExpr: "Name"
                    }
                },

    {
        dataField: "measurementCBM",
    caption: "Actual CBM",
    validationRules: [{type: "required" }],
                },
    {
        dataField: "consigneId",
    caption: "Consignee",
                    //customizeText: function (cellInfo) {
        //    return cellInfo.value.substring(0, cellInfo.value.indexOf('(') - 1);
        //},
        //visible: false,
        width:250,
    lookup: {
        dataSource: {
        store: consigneeList,
    requireTotalCount: true,
    pageSize: 4,
    paginate: true,
                        },
    displayExpr: "text",
    valueExpr: "value"
                    }
                },
    {
        dataField: "indexNo",
    caption: "Index No",
    allowEditing: false

                },
                //{
        //    dataField: "consigneId",
        //    caption: "Consignee",
        //    validationRules: [{ type: "required" }],
        //    width: 200,
        //    lookup: {
        //        dataSource: consigneeList,
        //        valueExpr: "value",
        //        displayExpr: "text"
        //    }
        //},
        {
            caption: "Is Part",
            dataField: "isPartialContainer",
            dataType: "boolean"
        },
        {
            caption: "Is DG Cargo",
            dataField: "isDGCargo",
            dataType: "boolean"
        },

        {
            dataField: "deliveryDate",
            caption: "Deliver Date",
            allowEditing: false,
            dataType: 'date',
            format: 'dd/MM/yyyy'
        },
        {
            width: 100,
            alignment: 'center',
            cellTemplate: function (container, options) {
                $('<a/>').addClass('dx-link')
                    .text('Apply to All')
                    .on('dxclick', function () {
                        aapplytoall(options, containers)
                    })
                    .appendTo(container);
            }
        },
            ]
        }

    var grid = $("#containerDetailList").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#containerDetailList").dxDataGrid({
        dataSource: containers,
    keyExpr: "containerIndexId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
    paging: {
        enabled: false
            },
    scrollBar: {
        visible: true,
            },
    zoomAndPan: {
        argumentAxis: 'both',
            },
    editing: {
        mode: "row",
    allowUpdating: true
            },
    columns: columns,

    onRowUpdated: function (e) {
        console.log(e);

    console.log("e.data", e.data)


    if (type == "CYContainers") {

        console.log("cy conatiner type")

                    console.log("containerIndexId", e.data.containerIndexId)
    console.log("cargoType", e.data.cargoType)

    var containerIndexId = e.data.containerIndexId;
    var cargoType = e.data.cargoType;
    var ispartcon = e.data.isPartialContainer;


    $.post('/Import/IGMInfo/UpdateContaierIndexDetailCY?cargotype=' + cargoType + '&containerId=' + containerIndexId + '&ispart=' + ispartcon   , function (data) {
        console.log(data);


    console.log(" data ", data);
    if (data.error) {
        showToast(data.message, "warning");

    populateTable();
                        }
    else {
        showToast(data.message, "success");
    populateTable();
                        }


                    });

                }
    else {

        console.log("containerIndexId", e.data);

    console.log("containerIndexId", e.data.containerIndexId)

    console.log("cargoType", e.data.cargoType)
    console.log("measurementCBM", e.data.measurementCBM)
    console.log("consigneId", e.data.consigneId)
    console.log("consigneId", e.data.manifestedWeight)

    var containerIndexId = e.data.containerIndexId;
    var cargoType = e.data.cargoType;
    var measurementCBM = e.data.measurementCBM;
    var consigneId = e.data.consigneId;
    var manifestedWeight = e.data.manifestedWeight;
    var isDGCargo = e.data.isDGCargo;
    var ispartcon = e.data.isPartialContainer;


    $.post('/Import/IGMInfo/UpdateContaierIndexDetail?measurementCBM=' + measurementCBM + '&consigneId=' + consigneId + '&cargotype=' + cargoType + '&manifestedWeight=' + manifestedWeight + '&containerId=' + containerIndexId + '&isDGCargo=' + isDGCargo + '&ispart=' + ispartcon , function (data) {

        console.log(data);

    console.log(" data ", data);
    if (data.error) {
        showToast(data.message, "warning");
    populateTable();
                        }
    else {
        showToast(data.message, "success");
    populateTable();
                        }


                    });
                }




            },

        }).dxDataGrid("instance");


    grid.on('editorPrepared', function (e) {
            if (e.parentType !== 'dataRow') {
                return;
            }

            //$('input[type=text]').on('wheel', function (ad) {

        //}); 


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
        });


        });


    }

    function aapplytoall(data, container) {

        console.log("aapplytoall data", data.data);


    console.log("aapplytoall container", container);

    if (data.data.consigneId && data.data.cargoType) {

        container.filter(x => x.consigneId = data.data.consigneId);
            container.filter(x => x.cargoType = data.data.cargoType);

    console.log("container", container)

    containers = container;
    dataGrid(containers);
        }
    else {
        showToast("plesae select consignee and Cargo Type ");
    dataGrid(containers);
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


    function updateInfo() {
        var values = $('#IGMDetailSheet').serialize();
    console.log("values", values)

    console.log("igm", igm)
    console.log("type", type)


    var number = "";

    if (type == "CFSIndexWise") {
        //number = $("#indexdb option:selected").text()
        number = $("#indexdb").val();
    console.log(number, "index CFSContainerWise");
        }
    if (type == "CFSContainerWise") {

        number = $("#containerdb :selected").map((_, e) => e.text).get();
    //number = $("#containerdb").val();
    console.log(number, "container CFSContainerWise");

        }

    if (type == "CYContainers") {
        //number = $("#containerIndexCYdb option:selected").text();
        number = $("#containerIndexCYdb").val();
    console.log(number, "container CFSContainerWise");


    var consid = $("#searchBoxForConsigne").dxSelectBox('instance').option("value");

             
        }

    $.post('/Import/IGMInfo/UpdateContaierDetail?' + values + '&containertype=' + type + '&VirNo=' + igm + '&ContainerNo=' + number + '&consid=' + consid, function (data) {

        console.log(" data ", data);
    if (data.error) {
        showToast(data.message, "warning");
            }
    else {
        showToast(data.message, "success");
            }


        });

    }


    function AddConsignee() {
        $('#consigneeModal').modal('show');
    }


    function saveInfo() {


        var form = document.getElementById('ConsigneeForm');

    var model = $('#ConsigneeForm').serialize();


    console.log("model", model)
    console.log("form.reportValidity", form.reportValidity)

    form.reportValidity();


    if (form.checkValidity()) {


        console.log("ok")
            console.log("model", model);

    $.post("/Import/Setup/AddConsigne?" + model, function (res) {

                if (res.error == true) {

        showToast(res.message, "error");

                }
    else {

        showToast(res.message, "success");

    window.location.href = window.location.href;

                }


            });



        }
    }



    function UploadInfo() {



        console.log("igmno", $('#igmno').val())

        var igmnumber = $('#igmno').val().replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '_').length;

    console.log("igmnumber", igmnumber)


    if (igmnumber == 16) {

        //var DatFiles = [{ FILE_NAME: "1" }, { FILE_NAME: "1" }, { FILE_NAME: "1" }, { FILE_NAME: "1" }];

        //string pathMCT , string pathMCS , string pathMIX , string pathMSR 


        //var pathMCT = "C:\\datfile\\MCIGMCT.dat";
        //var pathMCS = "C:\\datfile\\MCIGMCS.dat";
        //var pathMIX = "C:\\datfile\\MCIGMIX.dat";
        //var pathMSR = "C:\\datfile\\MCIGMSR.dat";

        //$.post('/Import/IGMInfo/ProcessDatFiles', { DatFiles: DatFiles }, function (res) {

        //?pathMCT = ' + pathMCT + ' & pathMCS=' + pathMCS + ' & pathMIX=' + pathMIX + ' & pathMSR=' + pathMSR
        $.post('/Import/IGMInfo/ProcessDatFiles', function (res) {

            console.log("res", res);

            if (res.results.length) {

                $.post('/Import/IGMInfo/UpdateIGMO?igm=' + $('#igmno').val(), { model: res.results }, function (result) {

                    console.log("res", res);

                    if (result.error == true) {


                        showToast(result.message, "error");

                    }
                    else {

                        showToast(result.message, "success");


                    }
                });


            }
        });


        } else {
        showToast("please type correct igm", "error")
    }


    }

    var documnetsor;

    function uploadFiles(event) {


        documnetsor = event.target.files[0];

    }


    function UploadDatFiles() {

        var files = $("#datfiles").get(0).files;
    var f = new FormData();

    for (var i = 0; i < files.length; i++) {
        console.log(files[i])
            f.append("f", files[i]);
        }

    console.log(f)
        //console.log(   document.getElementById('datfiles'))
        //console.log(   document.getElementById('datfiles').value)

        //if (document.getElementById('datfiles').value) {
        //    let f = new FormData();


        //    console.log(documnetsor)

        //    f.append('files', documnetsor);

        //    console.log("files",f)

        $.ajax({
            url: '/import/igminfo/UploadFiles',
            type: "POST",
            dataType: "json",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: f,
            success: function (result) {

                console.log("result", result)

                if (result.error == true) {
                    showToast(result.message, "error")
                } else {
                    showToast(result.message, "success")
                }

                // window.location.href = window.location.href

            }
        });

        //}
        //else {
        //    showToast("Please Select Image", "error");
        //}

    }

    function updateallInfo(  ) {

        //containers = container;
        //dataGrid(containers);

        console.log("containers", containers);


    var Typevale = $('#type').val();

    if (Typevale == "CYContainers") {
        showToast("for cy not allow", "error");
        }
    else {

        $.post('/Import/IGMInfo/UpdateContaierIndexDetailMultiple', { containers: containers }, function (data) {

            if (data.error) {
                showToast(data.message, "warning");
                dataGrid(containers);
            }
            else {
                showToast(data.message, "success");
                dataGrid(containers);
            }


        })
    }
    }

    /**/
