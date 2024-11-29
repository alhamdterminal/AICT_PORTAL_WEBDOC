

    var financialYears = [];

    var Years = [];


    $(function () {

        var currentYear = new Date().getFullYear()
    max = currentYear + 5

    for (var year = currentYear; year <= max; year++) {
        Years.push(year)
    }


    getfinancialYears();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getfinancialYears() {
        $.get('/Setup/GetFinancialYear', function (data) {
            financialYears = data;
            console.log(financialYears);
            loadgrid();
        });
    }

    function loadgrid() {



        $("#gridContainer").dxDataGrid({
            dataSource: financialYears,
            keyExpr: "financialYearId",
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
                    dataField: "year",
                    validationRules: [{ type: "required" }],
                    lookup: {
                        dataSource: Years,
                    },
                    caption: "Year"
                },

            ],

            onRowInserted: function (e) {

                console.log(e.data)

                var model = e.data;
                $.post('/Setup/AddFinancialYear', { model, model }, function (data) {


                    if (data.error == true) {
                        showToast(data.message, "error")
                        getfinancialYears();
                    } else {
                        showToast(data.message, "success")
                        getfinancialYears();


                    }


                });


            },

            onRowUpdated: function (e) {
                console.log(e.data);
                var model = e.data;

                $.post('/Setup/UpdateFinancialYear', { model, model }, function (data) {

                    if (data.error == true) {
                        showToast(data.message, "error")
                        getfinancialYears();
                    } else {
                        showToast(data.message, "success")
                        getfinancialYears();
                    }
                });
            },

        });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

    }



    function formfiled() {

    }
