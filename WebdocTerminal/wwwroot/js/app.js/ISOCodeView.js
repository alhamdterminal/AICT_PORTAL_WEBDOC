


    $(function () {

        getISOCodeHeadData();

    });

    var ISOCodeData = [];
    var ISOCodeHeadData = [];

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }



    function getISOCodeHeadData() {
        $.get('/Import/Setup/GetISOCodeHeads', function (data) {
            ISOCodeHeadData = data;
            console.log("ISOCodeHeadData", ISOCodeHeadData);

        });
    }

    function getISOCodeData() {
        $.get('/Import/ISOCode/GetISOCodes', function (data) {
            ISOCodeData = data;
            console.log(ISOCodeData);
            ISOCodegrid();
        });
    }


    function checkColumn(e, field) {

        if (e.parentType === "dataRow" && e.dataField !== field) {
            return;
        }

    e.editorOptions.disabled = true;
    }

    function refresh() {
        window.location.reload();
    }
    function ISOCodegrid() {


        $("#gridContainer").dxDataGrid({
            dataSource: ISOCodeData,
            keyExpr: "isoCodeId",
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
                allowAdding: true
            },
            columns: [
                {
                    dataField: "code",
                    validationRules: [{ type: "required" }],
                    caption: "Code"
                },
                {
                    dataField: "descrition",
                    validationRules: [{ type: "required" }],
                    caption: "Description"
                },
                {
                    dataField: "containerSize",
                    validationRules: [{ type: "required" }],

                    caption: "Size"
                },
                {
                    dataField: "isoCodeHeadId",
                    caption: "ISO Code Head",
                    lookup: {
                        dataSource: ISOCodeHeadData,
                        displayExpr: "isoCodeHeadDescription",
                        valueExpr: "isoCodeHeadId"
                    },
                    validationRules: [{ type: "required" }]

                }
            ],


            onRowInserted: function (e) {

                console.log(e)
                console.log(e.data)
                var data = e.data;

                $.post('/Import/ISOCode/AddISOCode', { data, data }, function (result) {

                    console.log("result", result)
                    if (result.error) {
                        showToast(result.message, "warning");
                    }

                    else {

                        showToast(result.message, "success");

                    }

                    window.setInterval('refresh()', 3000);
                });



            },

            onRowUpdated: function (e) {
                console.log(e);
                var data = e.data;
                $.post('/Import/ISOCode/updateISOCode', { data, data }, function (result) {
                    console.log("result", result)
                    if (result.error) {
                        showToast(result.message, "warning");
                    }

                    else {

                        showToast(result.message, "success");

                    }

                    window.setInterval('refresh()', 3000);
                });
            }
        });
    }



    function formfiled() {

        getISOCodeData();
    }
