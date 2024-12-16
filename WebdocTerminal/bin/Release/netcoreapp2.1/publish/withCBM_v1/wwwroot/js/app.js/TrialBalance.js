

    var chartOfAccountsList = [];

    var selectedaccounts = [];

    var accountids = "";

    $(function () {

        getchartofaccountData();
    })

    function loadingBar() {

        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });

    var bar = document.getElementById("large-indicator");
    bar.style.display = 'block';
    }

    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }

    function myFunction() {

        loadingBar();
    var fromdate = $('#single_cal2').val();
    var todate = $('#single_cal3').val();
    var departmentId = $('#departmentId').val();

    if (selectedaccounts.length) {

        accountids = selectedaccounts.map(function (item) { return item["chartOfAccountId"]; });
    accountids = accountids.toString();

    console.log("accountids", accountids)
        }
    else {
        accountids = "";
        }

    $.post('/Account/Reports/ViewTrialBalance?' + 'accountid=' + accountids + '&fromdate=' + fromdate + '&todate=' + todate + '&departmentid=' + departmentId , function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });

    }




    function getchartofaccountData() {
        $.get('/Account/ChartOfAccount/GetChartOfAccountNameAndCode', function (data) {
            chartOfAccountsList = data;


            $('#gridBox').dxDropDownBox({
                valueExpr: 'chartOfAccountId',
                placeholder: 'Select a value...',
                keyExpr: 'chartOfAccountId',
                displayExpr: 'accountName',
                showClearButton: true,
                inputAttr: { 'aria-label': 'Owner' },
                dataSource: chartOfAccountsList,
                contentTemplate(e) {
                    const $dataGrid = $('<div>').dxDataGrid({
                        dataSource: e.component.getDataSource(),
                        columns: ['accountName'],
                        hoverStateEnabled: true,
                        paging: { enabled: true, pageSize: 10 },
                        filterRow: { visible: true },
                        scrolling: { mode: 'virtual' },
                        height: 345,
                        selection: { mode: 'multiple' },
                        onSelectionChanged(selectedItems) {
                            selectedaccounts = selectedItems.selectedRowKeys;
                        },
                    });

                    return $dataGrid;
                },
            });

        });
    }


    function formfiled() {

    }
