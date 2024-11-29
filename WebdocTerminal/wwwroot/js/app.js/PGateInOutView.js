


    $(function () {

        $("#cnic").inputmask("99999-9999999-9");

     });
    var data_uri;

    var userlist = [];

    var url = "";

    function camera() {

        $('#cameraModal').modal('show');

    Webcam.set({
        width: 480,
    height: 240,
    image_format: 'jpeg',
    jpeg_quality: 90
        });
    Webcam.attach('#my_camera');
    }


    function take_snapshot() {
        // take snapshot and get image data
        Webcam.snap(function (image_uri) {

            data_uri = image_uri;

            document.getElementById('captured').innerHTML = '<img src="' + image_uri + '"/>';

            $('#cameraModal').modal('hide');
        });
    }


    function formfiled() {

        var url_string = window.location.href
    var urlstring = new URL(url_string);
    url = {
        a: urlstring.origin
        }

    $('#cameraModal').on('hidden.bs.modal', function () {

        Webcam.reset();
        });

    getuserslist();

    }

    function SavePGateIn() {

        if ($('#cnic').val().replace(/\D+/g, '').length == 13) {

            var form = document.getElementById('PGateInOutForm');


    form.reportValidity();

    if (form.reportValidity()) {

 
                if (data_uri) {

                    var repname = $('#repname').val();
    var phonenumber = $('#phonenumber').val();
    var cnic = $('#cnic').val();
    var visitReason = $('#visitReason').val();
    var documentRetain = $('#documentRetain').val();
    var visitorCompany = $('#visitorCompany').val();

    //var passNumber = $('#passNumber').val();
    var remarks = $('#remarks').val();

    Webcam.upload(data_uri, '/Import/GateInOut/SaveCapturedImage?name=image_' + repname + "&repname=" + repname + "&phonenumber=" + phonenumber + "&cnic=" + cnic + "&visitReason=" + visitReason
    + "&documentRetain=" + documentRetain + "&visitorCompany=" + visitorCompany + "&remarks=" + remarks
    //+ "&passNumber=" + passNumber
    , function (res) {

        console.log(res);

    if (res.error) {
        showToast(res.message, "error");

    window.location.href = window.location.href;
                            }
    else {

        showToast(res.message, "success");
    window.location.href = window.location.href;
                            }

                        });


                } else {

        showToast("please add image", "error");
                }

            }
        }
    else {
        alert("please enter 13 digit cnic")
    }
    }

    function OldInfoSavePGateIn() {

        if (imageurlsrc) {
            if ($('#cnic').val().replace(/\D+/g, '').length == 13) {

                var form = document.getElementById('PGateInOutForm');


    form.reportValidity();

    if (form.reportValidity()) {

                    var repname = $('#repname').val();
    var phonenumber = $('#phonenumber').val();
    var cnic = $('#cnic').val();
    var visitReason = $('#visitReason').val();
    var documentRetain = $('#documentRetain').val();
    var visitorCompany = $('#visitorCompany').val();

    //var passNumber = $('#passNumber').val();
    var remarks = $('#remarks').val();

    $.post('/Import/GateInOut/OldInforeplace?repname=' + repname + "&phonenumber=" + phonenumber + "&cnic=" + cnic + "&visitReason=" + visitReason
    + "&documentRetain=" + documentRetain + "&visitorCompany=" + visitorCompany + "&remarks=" + remarks + "&ImageUrl=" + imageurlsrc, function (res) {

        console.log(res);

    if (res.error) {
        showToast(res.message, "error");

    window.location.href = window.location.href;
                            }
    else {

        showToast(res.message, "success");
    window.location.href = window.location.href;
                            }

                        });

                }
            }
    else {
        alert("please enter 13 digit cnic")
    }
        }
    else {
        alert("image is not avaible")
    }
       
    }

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function getuserslist() {
        $.get('/Import/GateInOut/GetAllGateOutUsers', function (data) {

            userlist = data


            var dataGrid = $("#Users").dxDataGrid({
                dataSource: userlist,
                keyExpr: "pGateInOutId",
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
                    allowUpdating: true,
                    mode: "cell",
                },

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: "Search..."

                },

                columns: [
                    {
                        dataField: "imageUrl",
                        width: 250,
                        allowFiltering: false,
                        allowSorting: false,
                        cellTemplate: function (container, options) {

                            //console.log("url.a", url.a)
                            //console.log("options.value", options.value)

                            $("<div >")
                                .appendTo(container)
                                .append($("<img  >", { "src": url.a + "/" + options.value })
                                    .css("width", "100%"))
                        }
                    },
                    {
                        dataField: "name",
                        caption: "Visitor Name",
                        allowEditing: false,

                    },

                    {
                        dataField: "phoneNumber",
                        caption: "Visitor Phone No",
                        allowEditing: false,

                    },
                    {
                        dataField: "cnic",
                        caption: "Visitor CNIC",
                        allowEditing: false,

                    },
                    {
                        dataField: "visitReason",
                        caption: "Visitor Reason",
                        allowEditing: false,

                    },
                    {
                        dataField: "documentRetain",
                        caption: "Document Retain",
                        allowEditing: false,

                    },
                    {
                        dataField: "visitorCompany",
                        caption: "Visitor Company",
                        allowEditing: false,

                    },
                    //{
                    //    dataField: "passNumber",
                    //    caption: "Pass Number",
                    //    allowEditing: false,

                    //},
                    {
                        dataField: "remarks",
                        caption: "Remarks",
                        allowEditing: false,

                    },
                    {
                        dataField: "inDateTime",
                        caption: "Arrival",
                        allowEditing: false,
                        dataType: 'date',
                        format: 'dd/MM/yyyy hh:mm',
                    },
                    "isGateOut",


                ],


                onRowUpdated: function (e) {
                    console.log(e);

                },

            }).dxDataGrid("instance");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');


        })
    }


    function updatedata() {
         
        var users = userlist.filter(c => c.isGateOut == true);

    $.post('MarkOutUsers', {users: users }, function (data) {

            if (data.error) {
        showToast(data.message, "error");
               
            }
    else {
        showToast("Updated ", "success");
            }
    window.location.href = window.location.href;
             
        });

    }

    var imageurlsrc = "";

    function enterCNICNumebr() {
         
        var val = $('#cnic').val();



    if (val) {
            if (val.replace(/\D+/g, '').length == 13) {
        $.get('/APICalls/GetPGateInOutDetailBydCNIC?val=' + val, function (data) {
            console.log("data", data);
            if (data) {

                $('#repname').val(data.name);
                $('#phonenumber').val(data.phoneNumber);
                $('#visitReason').val(data.visitReason);
                $('#documentRetain').val(data.documentRetain);
                $('#visitorCompany').val(data.visitorCompany);
                $('#remarks').val(data.remarks);
                document.getElementById('captured').innerHTML = '<img src="' + url.a + "/" + data.imageUrl + '"/>';
                imageurlsrc = data.imageUrl
            }
            else {
                restvalues();
            }
        });
            }
    else {
        restvalues();
            }           
        }
    else {
        restvalues();
        }

    }

    function restvalues() {
        $('#repname').val('');
    $('#phonenumber').val('');
    $('#visitReason').val('');
    $('#documentRetain').val('');
    $('#visitorCompany').val('');
    $('#remarks').val('');
    document.getElementById('captured').innerHTML = '<img src="" />';
    imageurlsrc = "";
         
    }
