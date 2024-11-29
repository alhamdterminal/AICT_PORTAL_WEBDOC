

    var igm;
    var igmnumber;


    function igm_changed(data) {
        resetformvalues();
    griddata([]);

    igm = data.value;
    igmnumber = data.value;
    console.log(igm)
    console.log("igmnumber", igmnumber)

    igmnumber = igmnumber.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '_').length;

    console.log("igmnumber", igmnumber)



    if (igmnumber == 16) {
            if (igm) {

        $.get('/APICalls/GetIndexDropdownForCFSAndCY?IGM=' + igm, function (indexdb) {

            $('#containerIndex').html(indexdb);


        });
            }
        }
    else {
        $('#containerIndex').html([]);


        }


    }

    var container;
    var containerNumber;

    function Container_changed(data) {
        resetformvalues();
    griddata([]);
    container = data.value;
    containerNumber = data.value;
    console.log(container)
    console.log("containerNumber", containerNumber)

    containerNumber = containerNumber.replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, '_').length;

    console.log("containerNumber", containerNumber)



    if (containerNumber == 11) {
            if (container) {

        $.get('/APICalls/GetVirNumberDropdownForCFSAndCY?containernumber=' + container, function (indexdb) {

            $('#igmlists').html(indexdb);

            populateContainerIndexsDropbox();

        });
            }
        }
    else {
        $('#igmlists').html([]);

    populateContainerIndexsDropbox();

        }
    }


    function populateContainerIndexsDropbox() {
        resetformvalues();
    griddata([]);
    var virnumber = $("#virnumberdb option:selected").val();
    console.log("virnumber", virnumber)
    if (virnumber) {
        console.log("virnumber", virnumber)
            $.get('/APICalls/GetIndexDropdownForCFSAndCY?IGM=' + virnumber, function (data) {

        $('#Indexlists').html(data); 
            });
        }
    else {
        $('#Indexlists').html([]);
        }

       

    }


    var blnumber;

    function bl_changed(data) {
        resetformvalues();
    griddata([]);
    blnumber = data.value;
    console.log("blnumber", blnumber)
          
    }



    $(function () {

        $('#igmlists').on('change', function (val) {
            resetformvalues();
            populateContainerIndexsDropbox();

        });

 
         
    });



    function showcargoDetaildesc() {

        resetformvalues();
    griddata([]);

    var virno = $("#virnolist").dxAutocomplete("instance").option("value");

    var indexno = $("#containerIndexCYdb option:selected").val();

    console.log('virno', virno);

    console.log('indexno', indexno);

    if (virno && indexno) {
        $.get('/APICalls/GetInquiryFromData?igm=' + virno + '&index=' + indexno, function (data) {

            console.log("data", data);

            if (data.length) {

                griddata(data);

                var resdetail = data[0];

                $('#blnumber').val(resdetail.blNo);
                $('#ContainerNumber').val(resdetail.containerNo);
                $('#crnNumber').val(resdetail.crNumber);
                $('#dono').val(resdetail.doNo);
                $('#vesselname').val(resdetail.vesselName);

                if (resdetail.vesselArrivalDate != null) {

                    $('#vesselArrival').val(new Date(resdetail.vesselArrivalDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#vesselArrival').val('');
                }


                $('#voyage').val(resdetail.voyageNumber);
                $('#linename').val(resdetail.agentName);

                if (resdetail.igmDate != null) {

                    $('#igmdate').val(new Date(resdetail.igmDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#igmdate').val('');
                }
                if (resdetail.gateInDate != null) {

                    $('#aictArrival').val(new Date(resdetail.gateInDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#aictArrival').val('');
                }

                $('#truckin').val(resdetail.truckIn);
                $('#cfscy').val(resdetail.status);
                $('#lclFCL').val(resdetail.fcllcl);
                $('#size').val(resdetail.size);
                $('#type').val(resdetail.goodType);


                if (resdetail.destuffDate != null) {

                    $('#DestuffDate').val(new Date(resdetail.destuffDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#DestuffDate').val('');
                }

                $('#TruckOut').val(resdetail.truckOut);

                if (resdetail.deliveryDate != null) {

                    $('#DeliveryDate').val(new Date(resdetail.deliveryDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#DeliveryDate').val('');
                }

                $('#brt').val(resdetail.brtlocation);
                $('#Consignee').val(resdetail.consigneeName);
                $('#BLNo').val(resdetail.blNo);
                $('#ClearingAgent').val(resdetail.clearingAgentName);
                $('#LineDoNo').val(resdetail.lineDoNumber);
                $('#DisPackages').val(resdetail.disPackages);
                $('#DisQty').val(resdetail.disQty);


                if (resdetail.examDate != null) {

                    $('#ExamDate').val(new Date(resdetail.examDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#ExamDate').val('');
                }

                $('#isexamin').val(resdetail.examinationStatus);
                $('#Description').val(resdetail.description);
                $('#ReleaseStatus').val(resdetail.releaseStatus);
                $('#manifestQty').val(resdetail.manifestedQTY);
                $('#packgeType').val(resdetail.manifestedUOP);
                $('#GatePassNo').val(resdetail.gatePassNo);

                if (resdetail.gatePassDate != null) {

                    $('#GatePassDate').val(new Date(resdetail.gatePassDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#GatePassDate').val('');
                }
            }
            else {
                resetformvalues();
                griddata([])
            }

        });
        }
    else {
        alert("You Must Have to Select Igm And Index")
    }
     

    }


    function resetformvalues() {
        $('#blnumber').val('');
    $('#ContainerNumber').val('');
    $('#crnNumber').val('');
    $('#dono').val('');
    $('#vesselname').val('');
    $('#vesselArrival').val('');
    $('#voyage').val('');
    $('#linename').val('');
    $('#aictArrival').val('');
    $('#igmdate').val('');
    $('#truckin').val('');
    $('#cfscy').val('');
    $('#lclFCL').val('');
    $('#size').val('');
    $('#type').val('');
    $('#DestuffDate').val('');
    $('#TruckOut').val('');
    $('#DeliveryDate').val('');
    $('#brt').val('');
    $('#Consignee').val('');
    $('#BLNo').val('');
    $('#ClearingAgent').val('');
    $('#LineDoNo').val('');
    $('#DisPackages').val('');
    $('#DisQty').val('');
    $('#ExamDate').val('');
    $('#isexamin').val('');
    $('#GatePassNo').val('');
    $('#GatePassDate').val('');
    $('#Description').val('');
    $('#ReleaseStatus').val('');
    $('#manifestQty').val('');
    $('#packgeType').val('');

    griddata([])
    }


    function griddata(data) {


        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: data,
    keyExpr: "containerNo",
    allowColumnReordering: true,
    showBorders: true,
    grouping: {
        autoExpandAll: true,
            },
    searchPanel: {
        visible: true,
            },
    paging: {
        pageSize: 40,
            },
    groupPanel: {
        visible: true,
            },
    export: {
        enabled: true
            },
    onExporting(e) {

        console.log("e", e)
                const workbook = new ExcelJS.Workbook();

    const worksheet = workbook.addWorksheet('LineBoxIndexWiseDetail');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
    worksheet,
    autoFilterEnabled: true,
                }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {


            var resffandContainerno = "LineBoxIndexWiseDetail.xlsx";


            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), resffandContainerno);

        });
                });
    e.cancel = true;


            },
    columns: [

    {
        dataField: "containerNo",
    caption: "Containe rNo"  
                },
    {
        dataField: "size",
    caption: "Size" 
                },
    {
        dataField: "blNo",
    caption: "BL No", 
                },
    {
        dataField: "gateInDate",
    caption: "Arrival Date",
    dataType: "date",
    format: "dd/MMM/yyyy" 
                },

    {
        dataField: 'containerNo',
    groupIndex: 0,
                },
    {
        dataField: 'size',
    groupIndex: 1,
                },
    ],


            //summary: {

        //    groupItems: [
        //        {
        //            column: "indexNo",
        //            summaryType: "count",
        //            showInGroupFooter: true,
        //            alignByColumn: true,

        //        },
        //        {
        //            column: "higherCBM",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,

        //        },
        //        {
        //            column: "aictPerCBMRate",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,

        //        },
        //        {
        //            column: "aictPerIndexRate",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,

        //        },
        //        {
        //            column: "additionalChargeAICTPerCBMRate",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,

        //        },
        //        {
        //            column: "additionalChargeAICTPerIndexRate",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,

        //        },
        //        {
        //            column: "totalAICT",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,

        //        },
        //        {
        //            column: "ffPerCBMRate",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },

        //        },
        //        {
        //            column: "ffPerCBMRate",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,
        //        },
        //        {
        //            column: "additionalChargeFFPerCBMRate",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,
        //        },
        //        {
        //            column: "additionalChargeFFPerIndexRate",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,

        //        },
        //        {
        //            column: "totalFF",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,
        //        },
        //        {
        //            column: "totalCBMRate",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,
        //        },
        //        {
        //            column: "totalPerIndexRate",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,
        //        },
        //        {
        //            column: "totalCharges",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,
        //        },
        //        {
        //            column: "specialCharges",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,
        //        },
        //        {
        //            column: "portCharges",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,
        //        },
        //        {
        //            column: "billToLine",
        //            summaryType: "sum",
        //            customizeText(data) {
        //                return `  ${data.value.toLocaleString()}`;
        //            },
        //            showInGroupFooter: true,
        //            alignByColumn: true,
        //        },
        //    ]

        //},




    }).dxDataGrid('instance');


    $('#autoExpand').dxCheckBox({
        value: true,
    text: 'Expand All Groups',
    onValueChanged(data) {
        dataGrid.option('grouping.autoExpandAll', data.value);
            },
        });


    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');
    }

    function showcargoDetailsec() {

        resetformvalues();
    griddata([]);

    var virno = $("#virnumberdb option:selected").val();

    var indexno = $("#containerIndexCYdb option:selected").val();

    console.log('virno', virno);

    console.log('indexno', indexno);

    if (virno && indexno) {
        $.get('/APICalls/GetInquiryFromData?igm=' + virno + '&index=' + indexno, function (data) {

            console.log("data", data);

            if (data.length) {

                griddata(data);

                var resdetail = data[0];

                $('#blnumber').val(resdetail.blNo);
                $('#ContainerNumber').val(resdetail.containerNo);
                $('#crnNumber').val(resdetail.crNumber);
                $('#dono').val(resdetail.doNo);
                $('#vesselname').val(resdetail.vesselName);

                if (resdetail.vesselArrivalDate != null) {

                    $('#vesselArrival').val(new Date(resdetail.vesselArrivalDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#vesselArrival').val('');
                }


                $('#voyage').val(resdetail.voyageNumber);
                $('#linename').val(resdetail.agentName);

                if (resdetail.igmDate != null) {

                    $('#igmdate').val(new Date(resdetail.igmDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#igmdate').val('');
                }
                if (resdetail.gateInDate != null) {

                    $('#aictArrival').val(new Date(resdetail.gateInDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#aictArrival').val('');
                }

                $('#truckin').val(resdetail.truckIn);
                $('#cfscy').val(resdetail.status);
                $('#lclFCL').val(resdetail.fcllcl);
                $('#size').val(resdetail.size);
                $('#type').val(resdetail.goodType);


                if (resdetail.destuffDate != null) {

                    $('#DestuffDate').val(new Date(resdetail.destuffDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#DestuffDate').val('');
                }

                $('#TruckOut').val(resdetail.truckOut);

                if (resdetail.deliveryDate != null) {

                    $('#DeliveryDate').val(new Date(resdetail.deliveryDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#DeliveryDate').val('');
                }

                $('#brt').val(resdetail.brtlocation);
                $('#Consignee').val(resdetail.consigneeName);
                $('#BLNo').val(resdetail.blNo);
                $('#ClearingAgent').val(resdetail.clearingAgentName);
                $('#LineDoNo').val(resdetail.lineDoNumber);
                $('#DisPackages').val(resdetail.disPackages);
                $('#DisQty').val(resdetail.disQty);

                if (resdetail.examDate != null) {

                    $('#ExamDate').val(new Date(resdetail.examDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#ExamDate').val('');
                }

                $('#isexamin').val(resdetail.examinationStatus);
                $('#GatePassNo').val(resdetail.gatePassNo);
                if (resdetail.gatePassDate != null) {

                    $('#GatePassDate').val(new Date(resdetail.gatePassDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#GatePassDate').val('');
                }
                $('#Description').val(resdetail.description);
                $('#ReleaseStatus').val(resdetail.releaseStatus);
                $('#manifestQty').val(resdetail.manifestedQTY);
                $('#packgeType').val(resdetail.manifestedUOP);
            }
            else {
                resetformvalues();
                griddata([])
            }

        });
        }
    else {
        alert("You Must Have to Select Igm And Index")
    }


    }

    function formfiled() { }


    function showbl_changed() {

        resetformvalues();
    griddata([]);


    if (blnumber ) {
        $.get('/APICalls/GetInquiryFromDatabyblnumber?blnumber=' + blnumber, function (data) {

            console.log("data", data);

            if (data.length) {

                griddata(data);

                var resdetail = data[0];

                $('#blnumber').val(resdetail.blNo);
                $('#ContainerNumber').val(resdetail.containerNo);
                $('#crnNumber').val(resdetail.crNumber);
                $('#dono').val(resdetail.doNo);
                $('#vesselname').val(resdetail.vesselName);

                if (resdetail.vesselArrivalDate != null) {

                    $('#vesselArrival').val(new Date(resdetail.vesselArrivalDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#vesselArrival').val('');
                }


                $('#voyage').val(resdetail.voyageNumber);
                $('#linename').val(resdetail.agentName);

                if (resdetail.igmDate != null) {

                    $('#igmdate').val(new Date(resdetail.igmDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#igmdate').val('');
                }
                if (resdetail.gateInDate != null) {

                    $('#aictArrival').val(new Date(resdetail.gateInDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#aictArrival').val('');
                }

                $('#truckin').val(resdetail.truckIn);
                $('#cfscy').val(resdetail.status);
                $('#lclFCL').val(resdetail.fcllcl);
                $('#size').val(resdetail.size);
                $('#type').val(resdetail.goodType);


                if (resdetail.destuffDate != null) {

                    $('#DestuffDate').val(new Date(resdetail.destuffDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#DestuffDate').val('');
                }

                $('#TruckOut').val(resdetail.truckOut);

                if (resdetail.deliveryDate != null) {

                    $('#DeliveryDate').val(new Date(resdetail.deliveryDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#DeliveryDate').val('');
                }

                $('#brt').val(resdetail.brtlocation);
                $('#Consignee').val(resdetail.consigneeName);
                $('#BLNo').val(resdetail.blNo);
                $('#ClearingAgent').val(resdetail.clearingAgentName);
                $('#LineDoNo').val(resdetail.lineDoNumber);
                $('#DisPackages').val(resdetail.disPackages);
                $('#DisQty').val(resdetail.disQty);

                if (resdetail.examDate != null) {

                    $('#ExamDate').val(new Date(resdetail.examDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#ExamDate').val('');
                }

                $('#isexamin').val(resdetail.examinationStatus);
                $('#GatePassNo').val(resdetail.gatePassNo);
                if (resdetail.gatePassDate != null) {

                    $('#GatePassDate').val(new Date(resdetail.gatePassDate.split("T")[0]).toISOString().substr(0, 10));

                } else {
                    $('#GatePassDate').val('');
                }
                $('#Description').val(resdetail.description);
                $('#ReleaseStatus').val(resdetail.releaseStatus);
                $('#manifestQty').val(resdetail.manifestedQTY);
                $('#packgeType').val(resdetail.manifestedUOP);
            }
            else {
                resetformvalues();
                griddata([])
            }

        });
        }
    else {
        alert("You Must Have to Select BL No")
    }


    }
