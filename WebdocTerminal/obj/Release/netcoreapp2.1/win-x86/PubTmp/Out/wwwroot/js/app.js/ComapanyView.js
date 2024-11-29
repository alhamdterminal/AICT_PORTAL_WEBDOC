
    var companies = [];

    var url = "";
    var CompanyId = 0;
    //$(function () {

        //})




        function getCompanies() {
            $.get('/Company/GetCompanies', function (data) {

                companies = data

                var dataGrid = $("#company").dxDataGrid({
                    dataSource: companies,
                    keyExpr: "companyId",
                    wordWrapEnabled: true,
                    showBorders: true,
                    showBorders: true,
                    columnAutoWidth: true,
                    dateSerializationFormat: "yyyy/MM/dd HH:mm:ss",
                    paging: {
                        pageSize: 10
                    },
                    selection: {
                        mode: "single"
                    },
                    editing: {
                        mode: "row",
                        allowDeleting: true
                    },

                    searchPanel: {
                        visible: true,
                        width: 240,
                        placeholder: "Search..."

                    },

                    columns: [
                        {
                            dataField: "filePath",
                            width: 250,
                            allowFiltering: false,
                            allowSorting: false,
                            cellTemplate: function (container, options) {
                                console.log("url.a", url.a)
                                console.log("options.value", options.value)

                                $("<div >")
                                    .appendTo(container)
                                    .append($("<img>", { "src": url.a + "/" + options.value }))


                            }
                        },
                        "companyName",
                        "companyEmail",
                        "address",

                    ],

                    onSelectionChanged: function (selectedItems) {
                        var data = selectedItems.selectedRowsData[0];
                        if (data) {
                            $('#companyName').val(data.companyName);
                            $('#companyEmail').val(data.companyEmail);
                            $('#address').val(data.address);
                            CompanyId = data.companyId;
                            updateBtnShowHide(true);
                            submitBtnShowHide(false);
                        }
                    },

                    onEditingStart: function (e) {
                    },
                    onInitNewRow: function (e) {
                    },
                    onRowInserting: function (e) {
                        console.log(e)
                    },
                    onRowInserted: function (e) {


                    },
                    onRowUpdating: function (e) {

                    },
                    onRowUpdated: function (e) {
                        console.log(e);

                    },
                    onRowRemoving: function (e) {

                    },
                    onRowRemoved: function (e) {
                        var key = e.data.companyId;

                        $.post('/Company/Delete', { key, key }, function (data) {

                            if (data.error == true) {
                                showToast(data.message, "error");
                            }
                            else {
                                showToast(data.message, "success");

                            }
                            getCompanies();

                        });


                    }
                }).dxDataGrid("instance");


                $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
                $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


            })
        }



    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

    var documnetsor;

    function AddCompany() {

        if (document.getElementById('filePath').value) {
        $("#btnSubmit").attr("disabled", true);
    var CompanyName = CompanyForm.elements["CompanyName"].value
    var CompanyEmail = CompanyForm.elements["CompanyEmail"].value
    var Address = CompanyForm.elements["Address"].value
    let f = new FormData();
    f.append('f', documnetsor);
    f.append('CompanyName', CompanyName);
    f.append('CompanyEmail', CompanyEmail);
    f.append('Address', Address);

    console.log(documnetsor);
    console.log(f);

    $.ajax({
        url: '/Company/AddDocument',
    type: 'POST',
    data: f,
    dataType: 'json',
    processData: false,
    contentType: false,
    success: function (result) {
        window.location.href = window.location.href

    }
        });

        }
    else {
        showToast("Please Select Image", "error");
        }

        
       


    }


    function UpdateCompany() {
         if (CompanyId ) {

              var CompanyName = CompanyForm.elements["CompanyName"].value
    var CompanyEmail = CompanyForm.elements["CompanyEmail"].value
    var Address = CompanyForm.elements["Address"].value
    let f = new FormData();
    f.append('f', documnetsor);
    f.append('CompanyName', CompanyName);
    f.append('CompanyEmail', CompanyEmail);
    f.append('Address', Address);
    f.append('CompanyId', CompanyId);


    $.ajax({
        url: '/Company/UpdateDocument',
    type: 'POST',
    data: f,
    dataType: 'json',
    processData: false,
    contentType: false,
    success: function (result) {
        getCompanies();
            }
        });
        }
      


    }

    function uploadFiles(event) {
        documnetsor = event.target.files[0];
    }

    function updateBtnShowHide(show) {
        var btn = document.getElementById("btnSubmitUpdate");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }
    function submitBtnShowHide(show) {
        var btn = document.getElementById("btnSubmit");
    if (show)
    btn.style.display = 'inline-grid';
    else
    btn.style.display = 'none';
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function formfiled() {

        if (PermissionInputs.find(x => x.fieldName == "CompanyName" && x.isChecked == false)) {
        document.getElementById('companyName').disabled = true;

            }
            if (PermissionInputs.find(x => x.fieldName == "CompanyEmail" && x.isChecked == false)) {
        document.getElementById('companyEmail').disabled = true;

        } if (PermissionInputs.find(x => x.fieldName == "Address" && x.isChecked == false)) {
        document.getElementById('address').disabled = true;

        }
        if (PermissionInputs.find(x => x.fieldName == "FilePath" && x.isChecked == false)) {
        document.getElementById('filePath').disabled = true;

        }



    var url_string = window.location.href
    var urlstring = new URL(url_string);
    url = {
        a: urlstring.origin
        }

    getCompanies();
    }
