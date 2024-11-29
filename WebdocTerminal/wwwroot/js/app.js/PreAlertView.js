

    var voyageId = 0;
    var vesselId = 0;
    var Voyages = []

    function refresh() {
        window.location.reload();
    }


    function getPreAlterNumber() {
        $.get('/Import/PreAlert/GenPreAlertCode', function (data) {

            console.log(data);
            $('#prealertNumber').val(data);

        });
    }

    var PreAlterDetail = [];


    $(function () {
        getPreAlterNumber();
    loadGrid();
    })

    var ContainerType = [
    {Name: "CFS" },
    {Name: "CY" }
    ];

    var CargoType = [
    {Name: "G" },
    {Name: "DG" },
    {Name: "V" }
    ];

    var SOCCOC = [
    {Name: "SOC" },
    {Name: "COC" }
    ];



    function submitPreAlert() {



        var f = document.getElementById('PrealterForm');

    console.log("f", f)
    console.log("f.checkValidity()", f.checkValidity())


    if (f.checkValidity()) {

            var values = $('#PrealterForm').serialize();

    console.log("values", values)
    console.log("PreAlterDetail", PreAlterDetail)




            if (PreAlterDetail.length > 0) {




                var valueArr = PreAlterDetail.map(function (item) { return item.containerNumber.trim() });

    console.log("valueArr", valueArr)

    var isDuplicate = valueArr.some(function (item, idx) {
                    return valueArr.indexOf(item) != idx
                });

    console.log("isDuplicate", isDuplicate);

    if (isDuplicate == true) {
        //showToast("dublicate container number in list", "warning");
        alert("dublicate container number in list");
                } else {



        $.post('/Import/PreAlert/AddPreAlert?' + values, { Details: PreAlterDetail }, function (result) {

            console.log("result", result)
            if (result.error) {
                //showToast(result.message, "warning");
                alert(result.message);
            }

            else {
                alert(result.message);
                //showToast(result.message, "success");

                window.setInterval('refresh()', 3000);

            }



        })

    }

            }
    else {
        alert("Add at least 1 Detail");
            }



        }

    checkValidations();
    }

    function checkValidations() {

        if (!$('#shippingAgent').val()) {

        $('#ShippingAgenterror').html('This is a required field');
        }
    else {
        $('#ShippingAgenterror').html('');
        }

    if (!$('#line').val()) {

        $('#Lineerror').html('This is a required field');
        }
    else {
        $('#Lineerror').html('');
        }

        //if (!$('#vesselName').val()) {

        //    $('#Vesselerror').html('This is a required field');
        //}
        //else {
        //    $('#Vesselerror').html('');
        //}

        if (!$('#Vessel').val()) {

        $('#Vesselerror').html('This is a required field');
        }
    else {
        $('#Vesselerror').html('');
        }


    if (!$('#Voyage').val()) {

        $('#Voyageerror').html('This is a required field');
        }
    else {
        $('#Voyageerror').html('');
        }


        //console.log("$('#voyageNumber').val()", $('#voyageNumber').val())
        //console.log("instance", $("#voyageSelectBox").dxSelectBox('instance').option('value'))

        //var voyage = $("#voyageSelectBox").dxSelectBox('instance').option('value');
        //if (voyage == undefined || voyage == null) {

        //    $('#voyageNumbererror').html('This is a required field');
        //}
        //else {
        //    $('#voyageNumbererror').html('');
        //}

        if (!$('#portAndTerminal').val()) {

        $('#PortAndTerminalerror').html('This is a required field');
        }
    else {
        $('#PortAndTerminalerror').html('');
        }

    }


    function loadGrid() {


        //$.get("/APICalls/GetPortAndTerminals", function (PortAndTerminal) {

        $.get("/APICalls/GetShippingLines", function (ShippingLine) {

            console.log("ShippingLine", ShippingLine)

            //console.log("PortAndTerminal", PortAndTerminal)

            var dataGrid = $("#prealertdetails").dxDataGrid({
                dataSource: PreAlterDetail,
                keyExpr: "preAlertDetailId",
                wordWrapEnabled: true,
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
                    mode: "row",
                    allowUpdating: true,
                    allowDeleting: true,
                    allowAdding: true
                },
                columns: [
                    {
                        dataField: "containerNumber",
                        validationRules: [{ type: "required" }],
                        width: 150,
                        caption: "Container No",

                    },
                    {
                        dataField: "masterBLNumber",
                        validationRules: [{ type: "required" }],
                        width: 150,
                        caption: "M BL No",
                    },
                    {
                        dataField: "size",
                        caption: "Size",
                        width: 110,
                        validationRules: [{ type: "required" }],
                        lookup: {
                            dataSource: [{ size: 20 }, { size: 40 }, { size: 45 }],
                            displayExpr: "size",
                            valueExpr: "size"
                        }
                    },
                    {
                        dataField: "type",
                        caption: "Type",
                        width: 110,
                        validationRules: [{ type: "required" }],
                        lookup: {
                            dataSource: ContainerType,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        dataField: "cargoType",
                        caption: "Cargo Type",
                        width: 110,
                        validationRules: [{ type: "required" }],
                        lookup: {
                            dataSource: CargoType,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        dataField: "shippingLineId",
                        caption: "Shipping Agent / Line",
                        validationRules: [{ type: "required" }],
                        lookup: {
                            dataSource: ShippingLine,
                            displayExpr: "shippingLineName",
                            valueExpr: "shippingLineId"
                        }
                    },
                    {
                        dataField: "portAndTerminalName",
                        caption: "P O L",
                        validationRules: [{ type: "required" }],
                        width: 200,
                        //lookup: {
                        //    dataSource: PortAndTerminal,
                        //    displayExpr: "portName",
                        //    valueExpr: "portAndTerminalId"
                        //}
                    },
                    {
                        dataField: "soccoc",
                        caption: "SOC / COC",
                        width: 150,
                        validationRules: [{ type: "required" }],
                        lookup: {
                            dataSource: SOCCOC,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        dataField: "remarks",
                        caption: "Remarks",
                    },
                    //,
                    //{
                    //    dataField: "eirReceivedDate",
                    //    caption: "Eir Received Date",
                    //    dataType: 'date',
                    //    format: 'dd/MM/yyyy',
                    //    validationRules: [{ type: "required" }],
                    //    width: 100
                    //},
                    //{
                    //    dataField: "OffHiredDate",
                    //    caption: "off Hired Date",
                    //    dataType: 'date',
                    //    format: 'dd/MM/yyyy',
                    //    validationRules: [{ type: "required" }],
                    //    width: 100
                    //},
                    //{
                    //    dataField: "eirHandOverDate",
                    //    caption: "Eir Hand Over Date",
                    //    dataType: 'date',
                    //    format: 'dd/MM/yyyy',
                    //    validationRules: [{ type: "required" }],
                    //    width: 100
                    //},

                    //{
                    //    dataField: "nocDate",
                    //    caption: "NOC Date",
                    //    dataType: 'date',
                    //    format: 'dd/MM/yyyy',
                    //    validationRules: [{ type: "required" }],
                    //    width: 100
                    //}

                ],
                onEditorPrepared: function (e) {
                    if (e.dataField === "containerNumber" && e.parentType === "dataRow") {
                        e.editorElement.dxTextBox("instance").option("maxLength", 11);
                    }
                },

            }).dxDataGrid("instance");

            //});

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



    function formfiled() {

    }

