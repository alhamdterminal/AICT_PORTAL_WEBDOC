

    $(function () {
        $.get('/APICalls/GetFiltersBrtCFS', function (partial) {

            $('#filters').html(partial);

        })
        itemsGrid([]);
    resetFormVlaues();
    });


    function resetFormVlaues() {

        $('#weight').val(0);
    $('#palletweight').val(0);

    $('#cfsContainerGateInDate').val('');
    $('#blNo').val('');
    $('#containerNo').val('');
    $('#noofpackages').val('');
    $('#manifestedWeight').val('');
    $('#dischargeqty').val('');
    $('#remarks').val('');

    }

    function containerChangeCallback() {

        resetFormVlaues();

    var indexno = $("#indexdb option:selected").text();
    console.log("indexno", indexno)

    getIndexdata(igm, indexno);

    getdetail(igm, indexno);

        //window.setInterval('getweight()', 2000);

    }


    function getIndexdata(igm, indexno) {

        $.get('/Import/Weighment/GetIndexInfo?igm=' + igm + '&indexno=' + indexno, function (result) {

            console.log("result", result)
            if (result) {



                if (result.gateInDate != null) {

                    $('#cfsContainerGateInDate').val(new Date(result.gateInDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#cfsContainerGateInDate').val('');
                }


                $('#blNo').val(result.blNo);
                $('#containerNo').val(result.containerNo);
                $('#noofpackages').val(result.numberOfPackages);
                $('#manifestedWeight').val(result.manifestedWeight);
                $('#dischargeqty').val(result.dischargeQTY);
            }
            else {

                $('#cfsContainerGateInDate').val('');
                $('#blNo').val('');
                $('#containerNo').val('');
                $('#noofpackages').val('');
                $('#manifestedWeight').val('');
                $('#dischargeqty').val('');
                $('#remarks').val('');

            }
        });

    }

    function getdetail(igm, indexno) {

        $.get('/Import/Weighment/GetIndexWeighmentInfo?igm=' + igm + '&indexno=' + indexno, function (result) {

            console.log("result", result)
            if (result) {
                itemsGrid(result);
            }
            else {
                itemsGrid([]);
            }
        });

    }



    function itemsGrid(resadta) {

        var grid = $("#itemsGRidData").dxDataGrid(this.gridSettings).dxDataGrid('instance');

    var dataGrid = $("#itemsGRidData").dxDataGrid({
        dataSource: resadta,
    keyExpr: "indexWeighmentId",
    wordWrapEnabled: true,
    showBorders: true,
    columnAutoWidth: true,

    paging: {
        enabled: false
            },

    editing: {
        mode: "row",
    allowAdding: false,
    allowUpdating: true,
    allowDeleting: true,
            },
    columns: [
    {

        dataField: "remarks",
    caption: "Remarks",
                },
    {

        dataField: "palletWeight",
    dataType: "number",
    caption: "Pallet Weight",
    allowEditing: false,

                },
    {

        dataField: "indexWeight",
    dataType: "number",
    caption: "Index Weight",
    allowEditing: false,

                },
    {

        dataField: "weight",
    dataType: "number",
    caption: "Total Weight",
    allowEditing: false,

                },
    {
        dataField: "createdAt",
    caption: "Create Date",
    dataType: 'date',
    format: 'dd/MM/yyyy',
    width: 200,
    allowEditing: false,
                },
    {

        dataField: "sequence",
    dataType: "number",
    caption: "Marking Sequence",
    allowEditing: false,

                },
    {
        width: 200,
    alignment: 'center',
    cellTemplate: function (container, options) {
        $('<a/>').addClass('dx-link')
            .text('Print Individual')
            .on('dxclick', function () {
                print_weightSlip(options.row.data)
            })
            .appendTo(container);
                    }
                },
    {
        width: 200,
    alignment: 'center',
    cellTemplate: function (container, options) {
        $('<a/>').addClass('dx-link')
            .text('Print Complete')
            .on('dxclick', function () {
                print_weightSlipComplete(options.row.data)
            })
            .appendTo(container);
                    }
                },

    ],

    summary: {
        totalItems: [
    {
        column: "indexWeight",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },
    {
        column: "palletWeight",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },

    {
        column: "weight",
    summaryType: "sum",
    customizeText(data) {
                            return `  ${data.value.toLocaleString()}`;
                        },

                    },

    ]
            },

    onRowUpdated: function (e) {
        console.log(e.data);
    $.post('/Import/Weighment/UpdateIndexWeight?indexWeighmentId=' + e.data.indexWeighmentId + "&remarks=" + e.data.remarks, function (result) {
        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");
                    }

    else {

        showToast(result.message, "success");

                    }

    getdetail(igm, $("#indexdb option:selected").text())
                });
            },
    onRowRemoving: function (e) {
        console.log(e);
    $.post('/Import/Weighment/DeleteIndexWeight?id=' + e.data.indexWeighmentId, function (result) {
        console.log("result", result)
                    if (result.error) {
        showToast(result.message, "warning");
                    }

    else {

        showToast(result.message, "success");

                    }

    getdetail(igm, $("#indexdb option:selected").text())
                });
            }

        }).dxDataGrid("instance");



    }

    function print_weightSlip(data) {
        console.log(data)
        window.open('/import/reports/WeightSlipView?IndexWeighmentId=' + data.indexWeighmentId + '&type=' + 'Individual', "_blank");

    }

    function print_weightSlipComplete(data) {
        console.log(data)
        window.open('/import/reports/WeightSlipView?IndexWeighmentId=' + data.indexWeighmentId + '&type=' + 'Complete', "_blank");

    }


    function getweight() {



        $.get('/Import/Weighment/GetIndexWeigth', function (result) {
            console.log("result", result)
            if (result > 0) {
                $('#weight').val(result);
            }
            else {
                $('#weight').val(0);
            }
        });

    }

    function save() {

        $('#weight').val();
    var palletweight = $('#palletweight').val();
    var remarks = $('#remarks').val();

        if (Number($('#weight').val()) > 0) {

        $.post('/Import/Weighment/AddIndexWeighment?igm=' + igm + '&indexno=' + $("#indexdb option:selected").text() + '&weight=' + $('#weight').val() +
            '&palletweight=' + palletweight + '&remarks=' + remarks, function (data) {

                $('#palletweight').val(0);

                if (data.error == true) {
                    showToast(data.message, "error");
                } {
                    showToast(data.message, "success");
                }

                getdetail(igm, $("#indexdb option:selected").text())

            });

        }

    else {
        alert("weight must be greater then 1");
        }


    }


    function MarkComplete() {

        $.post('/Import/Weighment/IndexWeightMarkComplete?virno=' + igm + '&index=' + $("#indexdb option:selected").text(), function (data) {

            if (data.error == true) {
                showToast(data.message, "error");
            } {
                showToast(data.message, "success");
            }

            getdetail(igm, $("#indexdb option:selected").text())

        });

    }
    function formfiled() {



    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

