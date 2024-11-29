

    var itemsList = [];

    $(document).ready(function () {

        loadGrid();

     });

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function loadGrid() {

        $.get('/Import/Setup/GetNonWebocGroundingCY', function (data) {

            itemsList = data;

            console.log("itemsList", itemsList)

            var dataGrid = $("#NonWebocListContainer").dxDataGrid({
                dataSource: itemsList,
                keyExpr: "ihcId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                allowColumnResizing: true,
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

                    allowUpdating: true,
                    selectTextOnEditStart: true,
                    startEditAction: "click",
                    mode: "cell"
                },
                columns: [


                    {
                        dataField: "virNumber",
                        caption: "Vir No",
                        allowEditing: false

                    },
                    {
                        dataField: "containerNumber",
                        caption: "Container No",
                        allowEditing: false

                    },
                    {
                        dataField: "handlingCode",
                        caption: "Handling Code",
                        allowEditing: false
                    },


                    {
                        caption: "",
                        dataField: "isGround",
                        dataType: "boolean"

                    }


                ],


                onRowUpdated: function (e) {

                },


            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');



        });

    }




    function saveInfo() {

        $("#btnSubmit").attr("disabled", true);

    setTimeout(function () {$("#btnSubmit").attr("disabled", false); }, 120000);

    console.log("save info", itemsList);


        var datalist = itemsList.filter(c => c.isGround == true);


    console.log("ready to save", datalist)

    $.post('/Import/Setup/UpdateGroundingIHCs', {data: datalist }, function (data) {

            if (data.error) {
        showToast(data.message, "error");
    window.location.href = window.location.href;
    return;
            }
    else {
        showToast(data.message, "success");
    window.location.href = window.location.href;

            }
    window.location.href = window.location.href;
            loadGrid(itemsList.filter(c => c.isGround == false));

        });

    }

    function formfiled() {


    }

