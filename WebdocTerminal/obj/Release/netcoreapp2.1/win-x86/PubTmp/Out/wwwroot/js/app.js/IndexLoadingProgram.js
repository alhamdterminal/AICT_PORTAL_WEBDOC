
    var grid;

    var loadingProgram = [];




    $(function () {


        loadGrid();

    });



    function deleteLoadingProram(model) {
        console.log("model", model);
    $.post('/Export/LoadingProgram/DeleteLoadingProgram' ,{model: model }, function (data) {

        console.log(data);
    if (data.error == false) {
        showToast(data.message, "success");
    loadGrid();
            }
    else {
        showToast(data.message, "warning");

            }



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



    function loadGrid( ) {

        $.get('/Export/LoadingProgram/GetLoadingPrograms', function (data) {

            loadingProgram = data;


            var dataGrid = $("#gateinContainer").dxDataGrid({
                dataSource: loadingProgram,
                keyExpr: "loadingProgramId",
                wordWrapEnabled: true,
                showBorders: true,
                showBorders: true,
                columnAutoWidth: true,
                dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                paging: {
                    pageSize: 10
                },

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },

                editing: {
                    mode: "row",
                    selectTextOnEditStart: true,
                    startEditAction: "click"
                },
                columns: [
                    "loadingProgramNumber",
                    "referenceNumber",
                    "destination",
                    {
                        width: 100,
                        alignment: 'center',
                        cellTemplate: function (container, options) {
                            $('<a/>').addClass('dx-link')
                                .text('View')
                                .on('dxclick', function () {
                                    window.location = ('/Export/LoadingProgram/LoadingProgramView?lpNumber=' + options.data.loadingProgramNumber)
                                })
                                .appendTo(container);
                        }
                    },

                    {
                        width: 100,
                        alignment: 'center',
                        cellTemplate: function (container, options) {
                            $('<a/>').addClass('dx-link')
                                .text('Delete')
                                .on('dxclick', function () {
                                    //  window.location = ('/Export/LoadingProgram/LoadingProgramView?lpNumber=' + options.data.loadingProgramNumber)
                                    deleteLoadingProram(options.data);
                                })
                                .appendTo(container);
                        }
                    },

                ],

                onEditorPreparing: function (e) {
                },
                onRowUpdating: function (e) {

                    console.log(e);
                },
                onRowUpdated: function (e) {

                    console.log(e);
                },
                onRowRemoved: function (e) {
                    console.log(e);
                }

            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');



        });

    }


