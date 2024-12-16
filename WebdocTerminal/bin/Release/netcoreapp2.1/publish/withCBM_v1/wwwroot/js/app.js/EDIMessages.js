
    var grid;
    var url = "";


    function showEdimessages() {
        var FileName = $('#fileName').val()

    loadGrid(FileName)
    }


    $(function () {
        var url_string = window.location.href
    var urlstring = new URL(url_string);
    url = {
        a: urlstring.origin
        }
    });


    function loadGrid(FileName) {
        console.log("2", FileName)
        $.get('/DynamicReport/GetEDIMessages?FileName=' + FileName, function (data) {
        console.log("data", data);
    var datasource = data;
    var dataGrid = $("#ediGrids").dxDataGrid({
        dataSource: datasource,
    keyExpr: "ackId",
    wordWrapEnabled: true,
    showBorders: true,
    showBorders: true,
    columnAutoWidth: true,
    allowColumnResizing: true,
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
    "fileName",
    "sender",
    "receiver",
    "performed",


    {
        type: "buttons",
    buttons: ["edit", "delete", {
        text: "My Command",
    icon: "repeat",
    hint: "My Command",
    onClick: function (e) {
        CheckFileExist(e.row.data.fileName)

    }
                        }]
                    },
    {
        type: "buttons",
    buttons: ["edit", "delete", {
        text: "My Command",
    icon: "download",
    hint: "My Command",
    onClick: function (e) {
                                var s = "ACK_"+e.row.data.fileName
    CheckFileExistACK(s)
                            }
                        }]
                    }


    ],
    onEditorPreparing: function (e) {
        hideMenifestedColumns(e);
                },
    onRowUpdated: function (e) {

        $("#btnSubmit").attr("disabled", false);

    if (e.data.isChecked == true) {
                        var buttn = document.getElementById("buttn");
    buttn.style.display = 'block';

                    }
    else {
                        var buttn = document.getElementById("buttn");
    buttn.style.display = 'none';
                    }
                }

            }).dxDataGrid("instance");

        });

    }


    function CheckFileExist(data) {

        var Url = '/DynamicReport/Download?fileName=' + data
    $.ajax({
        url: Url,
    type: 'GET',
    async: false,
    success: function (data) {
        window.open(Url, '_blank');
            },
    error: function (e) {
        tempAlert(3000);
    console.log(e)
            }
        });
    }


    function CheckFileExistACK(data) {

        var Url = '/DynamicReport/DownloadACK?fileName=' + data
    $.ajax({
        url: Url,
    type: 'GET',
    async: false,
    success: function (data) {
        window.open(Url, '_blank');
            },
    error: function (e) {
        tempAlert(3000);
    console.log(e)
                // alert('Error : File not found .');
            }
        });
    }


