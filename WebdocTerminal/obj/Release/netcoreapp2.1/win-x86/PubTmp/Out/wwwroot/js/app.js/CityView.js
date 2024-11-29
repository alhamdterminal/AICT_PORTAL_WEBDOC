
    var Cities = [];


    $(function () {

        getcities();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getcities() {
        $.get('/Setup/GetCity', function (data) {
            Cities = data;
            console.log(Cities);
            loadgrid();
        });
    }

    function loadgrid() {

        $.get('/Setup/GetCountries', function (data) {
            countries = data;

            $("#gridContainer").dxDataGrid({
                dataSource: Cities,
                keyExpr: "cityId",
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
                columns: [
                    {
                        dataField: "cityCode",
                        validationRules: [{ type: "required" }],
                        caption: "City Code"
                    },
                    {
                        dataField: "cityName",
                        validationRules: [{ type: "required" }],
                        caption: "City Name"
                    },
                    {
                        dataField: "cityPhoneCode",
                        validationRules: [{ type: "required" }],
                        caption: "Phone Code"

                    },
                    {
                        dataField: "countryId",
                        caption: "Country",
                        lookup: {
                            dataSource: countries,
                            displayExpr: "countryName",
                            valueExpr: "countryId"
                        },
                        validationRules: [{ type: "required" }]

                    },
                    {
                        dataField: "status",
                        dataType: "boolean",
                        caption: "Status",
                    },

                ],

                onRowInserted: function (e) {

                    console.log(e.data)

                    var model = e.data;
                    $.post('/Setup/AddCity', { model, model }, function (data) {


                        if (data.error == true) {
                            showToast(data.message, "error")

                        } else {
                            showToast(data.message, "success")
                            getcities();


                        }


                    });


                },

                onRowUpdated: function (e) {
                    console.log(e.data)
                    var model = e.data;
                    $.post('/Setup/UpdateCity', { model, model }, function (data) {

                        if (data.error == true) {
                            showToast(data.message, "error")

                        } else {
                            showToast(data.message, "success")
                            getcities();


                        }
                    });
                },

            });

            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });
    }



    function formfiled() {
        getcities();

    }