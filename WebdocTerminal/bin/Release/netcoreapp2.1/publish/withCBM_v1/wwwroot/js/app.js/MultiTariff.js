

    var type;
    var formdata = [];
    formdata

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }


    function showFilters() {

        type = $("input[name='type']:checked").val();

    if (type == 'topup' || type == 'insert' || type == 'update') {
        $('#filters').css('display', 'block');
        } else $('#filters').hide();
        
    }

    function ontypechange() {

        var select = $('#field');
    select.empty();

    var typeval = document.getElementById("tarifftype").value;

    if (typeval != 'default') {
        $.post('/APICalls/GetTariffCol?Type=' + typeval, function (data) {
            console.log("Tariff Type Value: ", data);

            $.each(data.table, function (index, item) {
                $('<option>').val(item.column_name).text(item.column_name).appendTo(select);
            });
        });
        }
    }

    let selectedValues;
    function fieldchange() {

        var select = $('#criteria');
    select.empty();

    var typeval = document.getElementById("tarifftype").value;
    var fieldval = document.getElementById("field").value;

    $.post('/APICalls/GetTariffColValue?Type=' + typeval + '&Col=' + fieldval, function (data) {
        console.log("Tariff Type Value: ", data);

    mycol = [];
    var res = camelCase($('#field').val())
    mycol.push(res)

    let dataGrid;

    $('#gridBox').dxDropDownBox({
        //valueExpr: this,
        placeholder: 'Select a value...',
    //displayExpr: mycol,
    inputAttr: {'aria-label': 'Owner' },
    dataSource: data.table,
    contentTemplate(e) {
                    const v = e.component.option('value');
    const $dataGrid = $('<div>').dxDataGrid({
        dataSource: e.component.getDataSource(),
        columns: mycol,
        hoverStateEnabled: true,
        filterRow: {visible: true },
        scrolling: {mode: 'virtual' },
        //height: 345,
        selection: {mode: 'multiple' },
        selectedRowKeys: v,
        onSelectionChanged(selectedItems) {
                            const keys = selectedItems.selectedRowKeys;
        e.component.option('value', keys);
                        },
                    });

        dataGrid = $dataGrid.dxDataGrid('instance');

                    e.component.on('valueChanged', (args) => {
                        const {value} = args;
        dataGrid.selectRows(value, false);
        selectedValues = value;
        console.log('Selected Values:', selectedValues);
                    });

        return $dataGrid;
                },
            });
        });

    }


        function camelCase(str) {
        return str
        .replace(/\s(.)/g, function (a) {
                return a.toUpperCase();
            })
        .replace(/\s/g, '')
        .replace(/^(.)/, function (b) {
                return b.toLowerCase();
            });
    }

        function adddata() {


        var a = $('input[name="type"]:checked').val();
        var b = $("#desc").val()
        var c = $("#tarifftype").val()
        var d = $("#field").val()
        var e = $("#operator").val()
        var f = selectedValues.map(obj => Object.values(obj).join(','))
        //$("#bankVoucherNo75").val(data.bankVoucherNo75)

        //var formData = { };

        //for (var i = 0; i < form.elements.length; i++) {
            //    var element = form.elements[i];
            //    // Skip elements that are buttons or do not have a name attribute
            //    if (element.tagName.toLowerCase() !== "button" && element.name) {
            //        if (element.type === "checkbox" || element.type === "radio") {
            //            // Handle checkboxes and radio buttons separately
            //            if (element.checked) {
            //                formData[element.name] = element.value;
            //            }
            //        } else {
            //            // For other input types (text, email, number, select, textarea)
            //            formData[element.name] = element.value;
            //        }
            //    }
            //}

            console.log(a);
        console.log(b);
        console.log(c);
        console.log(d);
        console.log(e);
        console.log(f);

    }


    //function getdata(igm, indexno) {

            //    console.log("igm", igm);

            //    console.log("indexno", indexno);

            //    $.get('/APICalls/GetStorageHolidays?igm=' + igm + '&index=' + indexno, function (data) {

            //        console.log("data", data);

            //        $('#freedays').val(data);

            //    })

            //}


            function formfiled() {

                $('#freedays').val(0);
                $('#filters').hide();

            }


    //function UpdateDays() {


    //    var freedays = $('#freedays').val();

    //    console.log("type", type)

    //    if (type == "CFS") {

    //        var indexno = $("#indexdb option:selected").text();

    //        console.log("igm", igm);

    //        console.log("indexno", indexno);

    //        if (igm && indexno) {

    //            $.post('/Tariff/AddStorageHolidays?igm=' + igm + '&indexno=' + indexno + '&noofdays=' + freedays  , function (data) {

    //                if (data.error == true) {
    //                    showToast(data.message, "error");

    //                } else {
    //                    showToast(data.message, "success");
    //                    getdata(igm, indexno);
    //                }

    //            });

    //        } else {
    //            showToast("please select igm index", "error");
    //        }


    //    }

    //    if (type == "CY") {

    //        var indexno = $("#containerIndexCYdb option:selected").val();

    //        console.log("igm", igm);

    //        console.log("indexno", indexno);

    //        if (igm && indexno) {

    //            $.post('/Tariff/AddStorageHolidays?igm=' + igm + '&indexno=' + indexno + '&noofdays=' + freedays, function (data) {

    //                if (data.error == true) {
    //                    showToast(data.message, "error");

    //                } else {
    //                    showToast(data.message, "success");
    //                    getdata(igm, indexno);
    //                }

    //            });

    //        } else {
    //            showToast("please select igm index", "error");
    //        }

    //    }

    //}

    //$(() => {
    //    $('#gridContainer1').dxDataGrid({
    //        dataSource: employees,
    //        keyExpr: 'ID',
    //        showBorders: true,
    //        paging: {
    //            enabled: false,
    //        },
    //        editing: {
    //            mode: 'form',
    //            allowUpdating: true,
    //            allowAdding: true,
    //            allowDeleting: true,
    //        },
    //        columns: [
    //            {
    //                dataField: 'Prefix',
    //                caption: 'Title',
    //                width: 70,
    //            },
    //            'FirstName',
    //            'LastName', {
    //                dataField: 'Position',
    //                width: 170,
    //            }, {
    //                dataField: 'StateID',
    //                caption: 'State',
    //                width: 125,
    //                lookup: {
    //                    dataSource: states,
    //                    displayExpr: 'Name',
    //                    valueExpr: 'ID',
    //                },
    //            }, {
    //                dataField: 'BirthDate',
    //                dataType: 'date',
    //            }, {
    //                dataField: 'Notes',
    //                visible: false,
    //                formItem: {
    //                    colSpan: 2,
    //                    editorType: 'dxTextArea',
    //                    editorOptions: {
    //                        height: 100,
    //                    },
    //                },
    //            },
    //        ],
    //    });
    //});

    function refresh() {
                window.location.reload();
            }



