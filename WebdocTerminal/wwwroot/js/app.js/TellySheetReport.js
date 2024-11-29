

    var isDestuff = false;

    //$(function () {



        //});

        function containerCallback() {

            var container = $("#containerdb option:selected").text();
        }

    function containerChangeCallback(ornt) {
        orientation = ornt;
    }
    var containerid;
    function submit() {

        if (orientation == "container") {

            var containerNo = $("#containerdb option:selected").text();
    loadingBar();
    $.get('/Import/Reports/ViewTallySheet?containerNo=' + containerNo + '&virno=' + igm + '&orientation=' + orientation, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });
        }
    if (orientation == "index") {
            var containerNo = $("#indexdb option:selected").text();
    loadingBar();
    $.get('/Import/Reports/ViewTallySheet?containerNo=' + containerNo + '&virno=' + igm + '&orientation=' + orientation, function (data) {
        loadingBarHide();
    $('#repotdata').html(data);
            });
        }
    }



    function CheckHoldStatus() {



        if (orientation == "container") {

            var containerNo = $("#containerdb option:selected").text();
    loadingBar();
    $.get('/APICalls/CheckCFSTellysheetHold?virno=' + igm + '&containerno=' + containerNo, function (res) {
                if (res.error == true) {

        loadGridhold(res.message);
    $('#HoldStatusModal').modal('toggle');

                }
    else {
        loadGridhold([]);
    submit();

                }
            });
        }
    if (orientation == "index") {
            var containerNo = $("#indexdb option:selected").text();
    loadingBar();
    $.get('/APICalls/CheckCFSTellysheetHoldbyindex?virno=' + igm + '&indexno=' + containerNo, function (res) {
                if (res.error == true) {

        loadGridhold(res.message);
    $('#HoldStatusModal').modal('toggle');

                }
    else {
        loadGridhold([]);
    submit();

                }
            });
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
                    dataField: "virNo",
                    caption: "IGM",
                    allowEditing: false,
                },
                {
                    dataField: "indexNo",
                    caption: "Index No",
                    allowEditing: false,
                },
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


    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });
    }
    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }

    function formfiled() {
        console.log("Test")
        setOrientation(isDestuff);

    var containers = [];



        if (PermissionInputs.find(x => x.fieldName == "ContainerSelection" && x.isChecked == false)) {



        document.getElementById('TellySheet').style.display = "none";
        }


    }

