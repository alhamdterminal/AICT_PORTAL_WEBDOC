

    $(function () {

        getmaterlineData();

    });



    var AutoExaminationcharges = [];

    var Examinationcharges = [];



    function getmaterlineData() {
        $.get('/Import/Setup/GetExaminationCharges', function (data) {
            Examinationcharges = data;



            $.get('/Import/Setup/GetAutoExaminationCharges', function (data) {
                AutoExaminationcharges = data;
                console.log(AutoExaminationcharges);



                var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


                $("#gridContainer").dxDataGrid({
                    dataSource: AutoExaminationcharges,
                    keyExpr: "autoExaminationChargeId",
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

                    editing: {
                        mode: "row",
                        allowUpdating: true,
                        allowDeleting: false,
                        allowAdding: true
                    },
                    columns: [
                        {
                            dataField: "autoExaminationChargeName",
                            validationRules: [{ type: "required" }],
                            caption: "Name"
                        },

                        {
                            dataField: "examinationChargeId",
                            caption: "Examination Type",
                            lookup: {
                                dataSource: Examinationcharges,
                                displayExpr: "examinationChargeName",
                                valueExpr: "examinationChargeId"
                            },
                            validationRules: [{ type: "required" }]

                        },
                        {
                            dataField: "examinationChargeAmount20",
                            validationRules: [{ type: "required" }],
                            caption: "Amount 20",
                            format: "#,##0.##",
                            editorOptions: {
                                step: 0
                            }

                        },
                        {
                            dataField: "examinationChargeAmount40",
                            validationRules: [{ type: "required" }],
                            caption: "Amount 40",
                            format: "#,##0.##",
                            editorOptions: {
                                step: 0
                            }

                        },
                        {
                            dataField: "examinationChargeAmount45",
                            validationRules: [{ type: "required" }],
                            caption: "Amount 45",
                            format: "#,##0.##",
                            editorOptions: {
                                step: 0
                            }

                        },

                    ],

                    onRowInserted: function (e) {

                        console.log(e.data)

                        var model = e.data;


                        $.post('/Import/Setup/AddAutoExaminationCharges', { model, model }, function (data) {


                            if (data.error == true) {
                                showToast(data.message, "error")

                            } else {
                                showToast(data.message, "success")


                                getmaterlineData();
                            }


                        });



                    },

                    onRowUpdated: function (e) {

                        var model = e.data;
                        $.post('/Import/Setup/UpdateAutoExaminationCharges', { model, model }, function (data) {
                            showToast("Updated", "success");
                        });
                    },

                    onRowRemoved: function (e) {

                        var key = e.data.autoExaminationChargeId;
                        $.post('/Import/Setup/DeleteAutoExaminationCharges?key=' + key, function (data) {
                            showToast("Deleted", "success");
                        });
                    }

                });


                grid.on('editorPrepared', function (e) {
                    if (e.parentType !== 'dataRow') {
                        return;
                    }
                    e.editorElement[e.editorName]('instance').on('keyDown', function (arg) {

                        var gridComponent = e.component;

                        var event = arg.jQueryEvent;

                        if (event.keyCode === 38) {
                            if (e.editorName == "dxNumberBox") {
                                event.stopPropagation();
                                event.preventDefault();
                            }

                        } else if (event.keyCode === 40) {
                            if (e.editorName == "dxNumberBox") {
                                event.stopPropagation();
                                event.preventDefault();
                            }

                        }

                        else if (event.keyCode === 189) {
                            if (e.editorName == "dxNumberBox") {
                                event.stopPropagation();
                                event.preventDefault();
                            }

                        }
                    });


                });

                $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');
            });

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


    function getautoexaminationcharges() {


    }




    function Agentgrid() {


    }



    function formfiled() {

    }
