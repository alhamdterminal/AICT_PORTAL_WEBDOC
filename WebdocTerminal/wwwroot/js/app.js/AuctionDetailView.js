

    var grid;

    var containers = [];

    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }



    function hideMenifestedColumns(e) {

        checkColumn(e, "virNo");
    checkColumn(e, "blNo");
    checkColumn(e, "containerNo");
    checkColumn(e, "indexNo");
    checkColumn(e, "size");
 
         
         
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
     }





    function updateIndexes() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

    console.log("containers", containers)

        var resuktContainers = containers.filter(c => c.isAuction == true);

    console.log("resuktContainers", resuktContainers)

    if (resuktContainers.length) {
        console.log("resuktContainers", resuktContainers)
             
            sendgroundingcontainers(resuktContainers);
                

        }


    }



    function sendgroundingcontainers(resuktContainers) {

         var cargoType = $("#CargoType option:selected").val();

    console.log("cargoType", cargoType);

    if (cargoType) {
        $.post('AlotAuctionNumberCFS?type=' + cargoType, { model: resuktContainers }, function (data) {

            if (data.error == true) {

                showToast(data.message, "error");

                window.location.href = window.location.href;

            }
            else {

                showToast(data.message, "success");

                window.location.href = window.location.href;


            }

        });
         }
    else {
        showToast("please select cargo type Status", "error")
    }

    }

    function formfiled() {

    }



    function ShowDetail() {

         var fromdate = document.getElementById("single_cal2").value;

    var todate = document.getElementById("single_cal3").value;

    var cargoType = $("#CargoType option:selected").val();

    console.log("fromdate", fromdate);

    console.log("todate", todate);

    console.log("cargoType", cargoType);


    var f = document.getElementById('AuctioneDetail');

    if (f.checkValidity()) {

        $.get('/APICalls/GetCargoDetailCFSUnDeliveredCargo?type=' + cargoType + '&frmodate=' + fromdate + '&todate=' + todate, function (data) {

            console.log("data", data);

            containers = data;

            var dataGrid = $("#groundedContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "blNo",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                paging: {
                    enabled: false
                },
                editing: {
                    mode: "batch",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },
                columns: [
                    "virNo",
                    //"blNo",
                    //"containerNo",
                    "indexNo",
                    {
                        caption: "Aging Days",
                        dataField: "size",
                    },
                    //{
                    //    dataField: "auctionLotNo",
                    //    validationRules: [{ type: "required" }],
                    //    caption: "Auction Lot No"
                    //},
                    {
                        caption: "",
                        dataField: "isAuction",
                        dataType: "boolean"
                    }
                ],
                onEditorPreparing: function (e) {
                    hideMenifestedColumns(e);
                },
                onRowUpdated: function (e) {

                }

            }).dxDataGrid("instance");

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


        });

            
              
         }
    checkValidity();
      


     }

    function checkValidity(){
         if (!$('#CargoType').val()) {

        $('#cargoTypeerror').html('This is a required field');
         }
    else {
        $('#cargoTypeerror').html('');
         }
     }


