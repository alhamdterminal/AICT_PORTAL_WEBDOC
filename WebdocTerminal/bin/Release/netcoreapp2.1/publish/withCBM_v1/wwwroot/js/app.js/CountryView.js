

    var Countries = [];


    $(function () {

        getcountries();

    });


    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getcountries() {
        $.get('/Setup/GetCountries', function (data) {
            Countries = data;
            console.log(Countries);
            loadgrid();
        });
    }

    function loadgrid() {
      
        var grid = $("#gridContainer").dxDataGrid(this.gridSettings).dxDataGrid('instance');


    $("#gridContainer").dxDataGrid({
        dataSource: Countries,
    keyExpr: "countryId",
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
        dataField: "countryCode",
    validationRules: [{type: "required" }],
    caption: "Country Code"
                },
    {
        dataField: "countryName",
    validationRules: [{type: "required" }],
    caption: "Country Name"
                },
    {
        dataField: "countryPhoneCode",
    validationRules: [{type: "required" }],
    caption: "Phone Code"

                },
    {
        dataField: "status",
    dataType: "boolean",
    caption: "Status", 
                },

    ],

    onRowInserted: function (e) {

                var model = e.data;
    $.post('/Setup/AddCountry', {model, model}, function (data) {


                        if (data.error == true) {
        showToast(data.message, "error")

    } else {
        showToast(data.message, "success")
                            getcountries();


                        }


                    });
               

            },

    onRowUpdated: function (e) {
                
                var model = e.data;
    $.post('/Setup/UpdateCountry', {model, model}, function (data) {

                    if (data.error == true) {
        showToast(data.message, "error")

    } else {
        showToast(data.message, "success")
                        getcountries();


                    }
                });
            },
         
        });

    $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
        $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');
    }



    function formfiled() {
        getcountries();

    }
