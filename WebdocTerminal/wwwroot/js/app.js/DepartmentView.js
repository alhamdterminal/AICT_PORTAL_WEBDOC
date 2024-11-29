

    var departments = [];


    $(function () {

        getdepartments();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getdepartments() {
        $.get('/Setup/GetDepartment', function (data) {
            departments = data;
            console.log(departments);
            loadgrid();
        });
    }

    function loadgrid() {



        $("#gridContainer").dxDataGrid({
            dataSource: departments,
            keyExpr: "departmentId",
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
                allowUpdating: false,
                allowDeleting: false,
                allowAdding: true
            },
            export: {
                enabled: true
            },
            onExporting(e) {

                console.log("e", e)
                const workbook = new ExcelJS.Workbook();

                const worksheet = workbook.addWorksheet('Department');

                DevExpress.excelExporter.exportDataGrid({
                    component: e.component,
                    worksheet,
                    autoFilterEnabled: true,
                }).then(() => {
                    workbook.xlsx.writeBuffer().then((buffer) => {

                        saveAs(new Blob([buffer], { type: 'application/octet-stream' }), "Department.xlsx");

                    });
                });
                e.cancel = true;
            },
            columns: [
                {
                    dataField: "departmentCode",
                    validationRules: [{ type: "required" }],
                    caption: "Code"
                },
                {
                    dataField: "departmentName",
                    validationRules: [{ type: "required" }],
                    caption: "Name"
                },
                {
                    dataField: "isActive",
                    dataType: "boolean",
                    caption: "Status",
                },

            ],

            onRowInserted: function (e) {

                console.log(e.data)

                var model = e.data;
                $.post('/Setup/AddDepartment', { model, model }, function (data) {


                    if (data.error == true) {
                        showToast(data.message, "error")
                        getdepartments();
                    } else {
                        showToast(data.message, "success")
                        getdepartments();


                    }


                });


            },

            onRowUpdated: function (e) {
                console.log(e.data);
                var model = e.data;

                $.post('/Setup/UpdateDepartment', { model, model }, function (data) {

                    if (data.error == true) {
                        showToast(data.message, "error")
                        getdepartments();
                    } else {
                        showToast(data.message, "success")
                        getdepartments();
                    }
                });
            },

        });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }



    function formfiled() {
        //getdepartments();

    }
