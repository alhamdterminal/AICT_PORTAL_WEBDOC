

    var grid;

    var containers = [];



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
     }





    function updateIndexes() {
        console.log("dddd");
    $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

        var resuktContainers = containers.filter(c => c.isChecked == true);
    console.log(resuktContainers, "RC");
    if (resuktContainers.length) {
        console.log("resuktContainers", resuktContainers)

            sendcontainers(resuktContainers);
             
        }


    }



    function sendcontainers(resuktContainers) {


        console.log(resuktContainers, "");
    $.post('UnMarkAuctionIndex', {model: resuktContainers }, function (data) {
        console.log(data);
    if (data.error == true) {
        alert(data.message)
                   
                  window.location.href = window.location.href;

                }
    else {

        showToast(data.message, "success");

    window.location.href = window.location.href;

                }

        });

    }

    function formfiled() {
        ShowDetail();
    }



    function ShowDetail() {


        $.get('/APICalls/GetAllAuctionUnMark', function (data) {

            console.log("data", data);

            containers = data;

            var dataGrid = $("#groundedContainer").dxDataGrid({
                dataSource: containers,
                keyExpr: "key",
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
                    "virNumber",
                    "indexNumber",
                    "type",
                    {
                        caption: "",
                        dataField: "isChecked",
                        dataType: "boolean"
                    }
                ],

            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });



     }



