

    var grid;

    var containers = [];



    var Flag1List = [
    {Name: "I" },
    {Name: "E" }
    ];


    var StatusList = [
    {Name: "F" },
    {Name: "P" }
    ];



    //$(function () {
        //  //  formfiled();

        //});

        function showToast(message, icon) {

            $.toast({
                heading: message,
                showHideTransition: 'slide',
                position: 'bottom-right',
                icon: icon
            });
        }

    function loadGrid(blno) {

        if (blno) {

        $.get('/APICalls/GetUnCrossStuffingDetail?blno=' + blno, function (data) {

            console.log("data", data);

            containers = data;

            console.log(containers);

            var dataGrid = $("#groundedContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "oldContainerNumber",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
                columnAutoWidth: true,
                paging: {
                    enabled: false
                },

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },
                editing: {
                    mode: "cell",
                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [
                    {
                        dataField: "virNumber",
                        caption: "IGM",
                        allowEditing: false,

                    },
                    {
                        dataField: "blNumber",
                        caption: "BL No",
                        allowEditing: false,

                    },
                    {
                        dataField: "oldContainerNumber",
                        caption: "Container No",
                        allowEditing: false,

                    },
                    {
                        dataField: "gdNumber",
                        caption: "GD No",
                        allowEditing: false,

                    },
                    {
                        dataField: "newContainerNumber",
                        caption: "New Container No",
                        validationRules: [{ type: "required" }],
                        allowEditing: false,
                    },
                    {
                        dataField: "orderNumber",
                        caption: "Order No",
                        validationRules: [{ type: "required" }],
                        dataType: "number",
                        editorOptions: {
                            step: 0
                        }
                    },

                    //{
                    //    dataField: "status",
                    //    validationRules: [{ type: "required" }],
                    //    caption: "Status",
                    //    lookup: {
                    //        dataSource: StatusList,
                    //        valueExpr: "Name",
                    //        displayExpr: "Name"
                    //    }
                    //},

                    {
                        dataField: "flag1",
                        validationRules: [{ type: "required" }],
                        caption: "Flag 1",
                        lookup: {
                            dataSource: Flag1List,
                            valueExpr: "Name",
                            displayExpr: "Name"
                        }
                    },
                    {
                        dataField: "flag2",
                        caption: "Flag 2",
                    },
                    {
                        dataField: "flag3",
                        caption: "Flag 3",
                    },
                    {
                        caption: "",
                        dataField: "isContainerStuffed",
                        dataType: "boolean"
                    }
                ],
                onEditorPreparing: function (e) {

                },
                onRowUpdated: function (e) {

                }

            }).dxDataGrid("instance");

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


        });
        }


    }

    function SendEdiMessage() {

        var datalist = containers.filter(c => c.isContainerStuffed == true);

    console.log("datalist", datalist)
    if (datalist.length) {
        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);


    $.post('CreateEdiGDCRLst', {containers: datalist }, function (data) {


                if (data.error == true) {
        showToast(data.message, "error");
    window.location.href = window.location.href;

                }
    else {

        showToast(data.message, "success");
    window.location.href = window.location.href;


                }
    window.location.href = window.location.href;


            });



        } else {
        showToast("please select data", "error")
    }


    }

    function savedata() {

        var datalist = containers.filter(c => c.isContainerStuffed == true);

    console.log("datalist", datalist)
    if (datalist.length) {
        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);


    $.post('SaveGDCRLst', {containers: datalist }, function (data) {


                if (data.error == true) {
        showToast(data.message, "error");
    window.location.href = window.location.href;

                }
    else {

        showToast(data.message, "success");
    window.location.href = window.location.href;


                }
    window.location.href = window.location.href;


            });

        } else {
        showToast("please select data", "error")
    }

    }


    function formfiled() {

        // loadGrid();


        $('#blnumber').on('change', function (data) {

            console.log("Value", this.value)


            loadGrid(this.value);


        });


    }


    function printtellyshet() {

        var res = $('#blnumber').val()

    console.log(res)
    if (res) {

        window.open("/import/reports/CrossStuffingReportView?BLNumber=" + res)


    } else {
        showToast("please select bl no", "error")
    }
    }


