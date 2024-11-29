
    var CustomersData = [];

    $(function () {

        getcustomersData();
    })

    function formfiled() {

    }

    function getcustomersData() {
        $.get('/Setup/GetCustomersNameAndCode', function (data) {
            CustomersData = data;

            $("#searchpartyList").dxSelectBox({
                dataSource: {
                    store: CustomersData,
                    requireTotalCount: true,
                    pageSize: 4,
                    paginate: true,
                },
                name: 'CustomerId',
                displayExpr: "name",
                valueExpr: "customerId",
                acceptCustomValue: true,
                searchEnabled: true,
            })


        });
    }

    function loadingBar() {
        $("#large-indicator").dxLoadIndicator({
            height: 60,
            width: 60
        });
    }
    function loadingBarHide() {
        var bar = document.getElementById("large-indicator");
    bar.style.display = 'none';
    }
    function myFunction() {


        var tilldate = $('#single_cal2').val();

    var customerid = $("#searchpartyList").dxSelectBox('instance').option("value");

    console.log(tilldate);
    console.log(customerid);

    if (tilldate && customerid) {
        loadingBar();
    $.post('/Account/Reports/ViewAgingWiseBalanceReport?tdate=' + tilldate + '&' + 'csid=' + customerid, function (data) {

        loadingBarHide();

    $('#repotdata').html(data);
            });
        }
    else {
        alert("pelase select party and date")
    }


    }
