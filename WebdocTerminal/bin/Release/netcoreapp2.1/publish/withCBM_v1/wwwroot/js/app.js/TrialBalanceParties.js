
    var CustomersData = [];

    var selectedCustomerss = [];

    var Customersids = "";

    $(function () {

        getcustomersData();
    })


    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });

    var bar = document.getElementById("large-indicator");
    bar.style.display = 'block';
    }
    function getcustomersData() {
        $.get('/Setup/GetCustomersNameAndCode', function (data) {
            CustomersData = data;

            console.log("CustomersData", CustomersData)
            $('#searchpartyList').dxDropDownBox({
                valueExpr: 'customerId',
                placeholder: 'Select a value...',
                keyExpr: 'CustomerId',
                displayExpr: 'name',
                showClearButton: true,
                inputAttr: { 'aria-label': 'Owner' },
                dataSource: CustomersData,
                contentTemplate(e) {
                    const $dataGrid = $('<div>').dxDataGrid({
                        dataSource: e.component.getDataSource(),
                        columns: ['name'],
                        hoverStateEnabled: true,
                        paging: { enabled: true, pageSize: 10 },
                        filterRow: { visible: true },
                        scrolling: { mode: 'virtual' },
                        height: 345,
                        selection: { mode: 'multiple' },
                        onSelectionChanged(selectedItems) {
                            selectedCustomerss = selectedItems.selectedRowKeys;
                        },
                    });

                    return $dataGrid;
                },
            });


        });
    }

    function formfiled() {

    }

    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }
    function myFunction() {
        loadingBar();

    var fromdate = $('#single_cal2').val();
    var todate = $('#single_cal3').val();
    //var customer = $("#searchpartyList").dxSelectBox('instance').option("value");

    var departmentId = $('#departmentId').val();

    if (selectedCustomerss.length) {

        Customersids = selectedCustomerss.map(function (item) { return item["customerId"]; });
    Customersids = Customersids.toString();


        }
    else {
        Customersids = "";
        }

    console.log("Customersids", Customersids)
    console.log("departmentId", departmentId)

    $.post('/account/reports/ViewTrialBalanceParties?customerid=' + Customersids + '&fromdate=' + fromdate + '&todate=' + todate + '&departmentId=' + departmentId, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);

        });

    }
