#pragma checksum "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\Views\Voucher\VoucherView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "bb2b7b467381b14c5386e13e35c1ed271359025400b061eb9e51486f1a852377"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Account_Views_Voucher_VoucherView), @"mvc.1.0.view", @"/Areas/Account/Views/Voucher/VoucherView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Account/Views/Voucher/VoucherView.cshtml", typeof(AspNetCore.Areas_Account_Views_Voucher_VoucherView))]
namespace AspNetCore
{
    #line default
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\_ViewImports.cshtml"
using WebdocTerminal

    ;
#line 2 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\_ViewImports.cshtml"
using WebdocTerminal.Models

    ;
#line 4 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc

    ;
#line 5 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\_ViewImports.cshtml"
using DevExpress.AspNetCore

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"bb2b7b467381b14c5386e13e35c1ed271359025400b061eb9e51486f1a852377", @"/Areas/Account/Views/Voucher/VoucherView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Account/_ViewImports.cshtml")]
    public class Areas_Account_Views_Voucher_VoucherView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/VoucherView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 3 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\Views\Voucher\VoucherView.cshtml"
  
    ViewData["Title"] = "Voucher";
    var VoucherType = (IEnumerable<SelectListItem>)ViewData["VoucherType"];

#line default
#line hidden

            BeginContext(124, 544, true);
            WriteLiteral(@"
<style>

    #groundedContainer {
        max-height: 700px;
    }


    #progressBarStatus {
        display: inline-block;
    }

    .complete .dx-progressbar-range {
        background-color: green;
    }
</style>
<script type=""text/javascript"" src=""https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.15.3/xlsx.full.min.js""></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/exceljs/4.1.1/exceljs.min.js""></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2.0.2/FileSaver.min.js""></script>
");
            EndContext();
            BeginContext(668, 50, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb2b7b467381b14c5386e13e35c1ed271359025400b061eb9e51486f1a8523775169", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(718, 835, true);
            WriteLiteral(@"

<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""title_left"">
            <h3>
                Voucher
            </h3>
        </div>
        <div class=""title_right"">
        </div>
    </div>
    <div class=""clearfix""></div>

    <div class=""row"">

        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">
                <div class=""row"">
                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""form-group"">
                            <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                Voucher Type:
                            </label>
                            <div class=""col-md-8 col-sm-8 col-xs-12"">
                                ");
            EndContext();
            BeginContext(1554, 191, false);
            Write(
#line 51 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\Views\Voucher\VoucherView.cshtml"
                                 Html.DropDownList("VoucherTypeId", VoucherType, "Select Voucher Type", new { @class = "form-control select2", @id = "voucherTypeId", @required = "required", @onchange = "showvoucherCode()" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(1745, 3023, true);
            WriteLiteral(@"
                            </div>
                        </div>
                    </div>

                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""control-group"">
                            <div class=""controls"">
                                <label class=""control-label col-md-3 col-sm-3 col-xs-12"" for=""first-name"">
                                    Date:
                                </label>
                                <div class=""col-md-9 col-xs-12"">
                                    <input type=""text"" class=""form-control has-feedback-left"" id=""single_cal2"" name=""fromdate"" onchange=""showvoucherCode()"" aria-describedby=""inputSuccess2Status2"">
                                    <span class=""fa fa-calendar-o form-control-feedback left"" aria-hidden=""true""></span>
                                    <span id=""inputSuccess2Status2"" class=""sr-only"">(success)</span>
                                </div>
                            </div>
            WriteLiteral(@"
                        </div>
                    </div>
                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""item form-group"">
                            <label class=""control-label col-md-2 col-sm-3 col-xs-12"" for=""name"">
                                Voucher Code:
                            </label>
                            <div class=""col-md-5 col-sm-8 col-xs-12"">
                                <input type=""text"" class=""form-control"" id=""vouchercode"" readonly=""readonly"">
                            </div>
                            <label class=""control-label col-md-2 col-sm-3 col-xs-12"" for=""name"">
                                Unique No :
                            </label>
                            <div class=""col-md-3 col-sm-8 col-xs-12"">
                                <input type=""text"" class=""form-control"" id=""voucherid"" readonly=""readonly"">
                            </div>
                        </div>
               ");
            WriteLiteral(@"     </div>
                </div>

                <div class=""row"">
                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""item form-group"">
                            <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                File Select: <span class=""required"">*</span>
                            </label>
                            <div class=""col-md-8 col-sm-8 col-xs-12"">
                                <input type=""file"" class=""form form-control"" id=""fileUpload"" accept="".xls,.xlsx"" style="" background-color: beige; color: black;"" /><br />
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""item form-group"">
                            <button type=""button"" class=""btn btn-success"" id=""uploadExcel"">Upload File</button>
");
            EndContext();
            BeginContext(4900, 125, true);
            WriteLiteral("\r\n\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n                <div class=\"row\">\r\n");
            EndContext();
            BeginContext(5213, 338, true);
            WriteLiteral(@"                    <div id=""large-indicator""></div>
                </div>

                <div class=""x_content"">
                    <div id=""gridContainer""></div>
                    <div class=""ln_solid""></div>
                    <div class=""form-group"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
");
            EndContext();
            BeginContext(5830, 20631, true);
            WriteLiteral(@"                            <button class=""btn btn-primary"" style=""float:right;"" id=""btnSubmitsave"" onclick=""AddVoucherList()"">Save</button>
                            <button class=""btn btn-danger"" style=""float:right;"" id=""btnSubmit"" onclick=""PrintDetail()"">Print</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>


    <div id=""ErroeStatusModal"" class=""modal fade"" role=""dialog"">
        <div class=""modal-dialog"">
            <!-- Modal content-->
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
                    <h4 class=""modal-title"">Error List</h4>
                </div>
                <div class=""modal-body"">
                    <div class=""row"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <div class=""x_panel"">
      ");
            WriteLiteral(@"                          <div class=""x_content"">
                                    <div class=""x_content"" id=""errorGrid""></div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>
    </div>

</div>

<script>
    $('.select2').select2();


    var VoucherTypesData = [];
    var VoucherServicesData = [];
    var ChartOfAccountsListData = [];
    var DepartmentsData = [];
    var CustomersData = [];

    var filterServiceType = false;

    var selectedFile;
    document
        .getElementById(""fileUpload"")
        .addEventListener(""change"", function (event) {
            selectedFile = event.target.files[0];
        });
    document
        .getElementById(""uploadExcel"")
        .addEventListener(""click"", function () {


            GetVoucherTypesdata();
            GetServiceTypeData();
            getchartofaccountdata();
  ");
            WriteLiteral(@"          getdepartmentsData();
            getcustomersData();



            var filename = """";
            var VoucherList = [];
            var resultvalue = false;

            if (!VoucherTypesData.length) {
                return showToast(""please wait Voucher type on loading"", ""error"");
            }
            else if (!VoucherServicesData.length) {
                return showToast(""please wait Voucher service type on loading"", ""error"");
            }
            else if (!ChartOfAccountsListData.length) {
                return showToast(""please wait Chart Of Account on loading"", ""error"");
            }
            else if (!DepartmentsData.length) {
                return showToast(""please wait Department on loading"", ""error"");
            }
            else if (!CustomersData.length) {
                return showToast(""please wait Customers on loading"", ""error"");
            }
            else {

                if (selectedFile) {
                    loadingBar();
   ");
            WriteLiteral(@"                 var fileReader = new FileReader();

                    fileReader.onload = function (event) {

                        var data = event.target.result;

                        var workbook = XLSX.read(data, {
                            type: ""binary""
                        });
                        workbook.SheetNames.forEach(sheet => {

                            let rowObject = XLSX.utils.sheet_to_row_object_array(
                                workbook.Sheets[sheet]
                            );

                            if (rowObject) {

                                var newresult = rowObject;

                                console.log(""newresult"", newresult);


                                if (newresult.length) {

                                    newresult.forEach((x, index) => {
                                        //#region   start validation
                                        index += 1;
                                        i");
            WriteLiteral(@"f (x.VoucherNo == 0 || x.VoucherNo == undefined || x.VoucherNo == """" || x.VoucherNo == ""null"") {
                                            resultvalue = true;
                                            return alert(""please  VoucherNo in row number "" + index);
                                        }
                                        if (x.VoucherDate == 0 || x.VoucherDate == undefined || x.VoucherDate == """" || x.VoucherDate == ""null"") {
                                            resultvalue = true;
                                            return alert(""please add VoucherDate in row number "" + index);
                                        }
                                        if (x.VoucherType == 0 || x.VoucherType == undefined || x.VoucherType == """" || x.VoucherType == ""null"") {
                                            resultvalue = true;
                                            return alert(""please add VoucherType in row number "" + index);
                                ");
            WriteLiteral(@"        }
                                        if (x.Debit == undefined || x.Debit == ""null"") {
                                            resultvalue = true;
                                            return alert(""please add Debit in amount in row number "" + index);
                                        }
                                        if (x.Credit == undefined || x.Credit == ""null"") {
                                            resultvalue = true;
                                            return alert(""please add Credit amount in row number "" + index);
                                        }
                                        if (x.Credit <= 0 && x.Debit <= 0) {
                                            resultvalue = true;
                                            return alert(""in row number "" + index + "" debit and credit both are < 0"");
                                        }
                                        if (x.Credit > 0 && x.Debit > 0) {
           ");
            WriteLiteral(@"                                 resultvalue = true;
                                            return alert(""in row number "" + index + "" debit and credit both are > 0"");
                                        }
                                        if (x.ServiceType == 0 || x.ServiceType == undefined || x.ServiceType == """" || x.ServiceType == ""null"") {
                                            resultvalue = true;
                                            return alert(""please add ServiceType in row no "" + index);
                                        }
                                        if (x.Deaprtment == 0 || x.Deaprtment == undefined || x.Deaprtment == """" || x.Deaprtment == ""null"") {
                                            resultvalue = true;
                                            return alert(""please add Deaprtment in row no "" + index);
                                        }
                                        if (x.Narration == 0 || x.Narration == undefined || x.");
            WriteLiteral(@"Narration == """" || x.Narration == ""null"") {
                                            resultvalue = true;
                                            return alert(""please add Narration in row no "" + index);
                                        }
                                        if (x.Account == 0 || x.Account == undefined || x.Account == """" || x.Account == ""null"") {
                                            resultvalue = true;
                                            return alert(""please add Account code in row no "" + index);
                                        }

                                        var vt = VoucherTypesData.find(v => v.code == x.VoucherType);
                                        if (!vt) {
                                            resultvalue = true;
                                            return alert(""VoucherType Code not avaible in source"" + x.VoucherType + "" in row no "" + index);
                                        }

                 ");
            WriteLiteral(@"                       var vst = VoucherServicesData.find(v => v.code == x.ServiceType);
                                        if (!vst) {
                                            resultvalue = true;
                                            return alert(""ServiceType Code not avaible in source"" + x.ServiceType + "" in row no "" + index);
                                        }

                                        var dep = DepartmentsData.find(v => v.departmentCode == x.Deaprtment);
                                        if (!dep) {
                                            resultvalue = true;
                                            return alert(""Deaprtment Code not avaible in source"" + x.Deaprtment + "" in row no "" + index);
                                        }

                                        var coa = ChartOfAccountsListData.find(v => v.accCode == x.Account);
                                        if (!coa) {
                                            resultval");
            WriteLiteral(@"ue = true;
                                            return alert(""Account Code not avaible in source"" + x.Account + "" in row no "" + index);
                                        }

                                        if (x.ServiceType == ""CD"" || x.ServiceType == ""P"") {


                                            if (x.Party == 0 || x.Party == undefined || x.Party == """" || x.Party == ""null"") {
                                                resultvalue = true;
                                                return alert(""please add Account code in  voucher no "" + x.VoucherNo + "" in row no "" + index);
                                            }

                                            var party = CustomersData.find(v => v.code == x.Party);
                                            if (!party) {
                                                resultvalue = true;
                                                return alert(""Party Code not avaible in source"" + x.Party + "" in row n");
            WriteLiteral(@"o "" + index);
                                            }
                                        }

                                        //#region   end validation


                                    });

                                    if (resultvalue == false) {

                                        newresult.forEach(x => {

                                            var vt = VoucherTypesData.find(v => v.code == x.VoucherType);

                                            if (vt) {

                                                var finvoucher = VoucherList.find(f => f.VoucherNo == x.VoucherNo);

                                                if (!finvoucher) {

                                                    let abc = {

                                                        VoucherDate: new Date((x.VoucherDate - (25567 + 2)) * 86400 * 1000).toLocaleDateString(),
                                                        VoucherTypeId: vt.voucherTypeId,
        ");
            WriteLiteral(@"                                                VoucherNo: x.VoucherNo,
                                                        Status: ""InProgress"",
                                                        VoucherDetails: []
                                                    };

                                                    VoucherList.push(abc);
                                                }
                                            }

                                        });


                                        //creating voucher detail

                                        if (VoucherList.length) {

                                            VoucherList.forEach(x => {


                                                let voucherno = x.VoucherNo;
                                                var resd = newresult.filter(x => x.VoucherNo == voucherno);

                                                if (resd) {

                                               ");
            WriteLiteral(@"     resd.forEach(xx => {



                                                        if (xx.ServiceType == ""CD"" || xx.ServiceType == ""P"") {

                                                            var party = CustomersData.find(v => v.code == xx.Party && xx.Party != null && xx.Party != undefined && xx.Party != """" && xx.Party != ""null"" ? xx.Party : 0);
                                                            var serviceType = VoucherServicesData.find(v => v.code == xx.ServiceType) || null;
                                                            var departmentr = DepartmentsData.find(v => v.departmentCode == xx.Deaprtment) || null;

                                                            if (party && serviceType && departmentr) {

                                                                let abc = {
                                                                    Credit: xx.Credit,
                                                                    Debit: xx.Debit,
       ");
            WriteLiteral(@"                                                             Narration: xx.Narration,
                                                                    VoucherTypeId: x.VoucherTypeId,
                                                                    DepartmentId: departmentr.departmentId,
                                                                    VoucherServiceTypeId: serviceType.voucherServiceTypeId,
                                                                    CustomerId: party.customerId,
                                                                    ChartOfAccountId: party.chartOfAccountId,
                                                                    ChequeOrReference: xx.ChequeOrReference
                                                                };

                                                                x.VoucherDetails.push(abc);

                                                            }
                                                       ");
            WriteLiteral(@" }
                                                        else {

                                                            var party = CustomersData.find(v => v.code == xx.Party && xx.Party != null && xx.Party != undefined && xx.Party != """" && xx.Party != ""null"" ? xx.Party : 0);
                                                            var serviceType = VoucherServicesData.find(v => v.code == xx.ServiceType);
                                                            var departmentr = DepartmentsData.find(v => v.departmentCode == xx.Deaprtment);
                                                            var accountr = ChartOfAccountsListData.find(v => v.accCode == xx.Account);

                                                            if (serviceType && departmentr && accountr) {

                                                                let abc = {
                                                                    Credit: xx.Credit,
                                               ");
            WriteLiteral(@"                     Debit: xx.Debit,
                                                                    Narration: xx.Narration,
                                                                    VoucherTypeId: x.VoucherTypeId,
                                                                    DepartmentId: departmentr.departmentId,
                                                                    VoucherServiceTypeId: serviceType.voucherServiceTypeId,
                                                                    CustomerId: party != null ? party.customerId : """",
                                                                    ChartOfAccountId: accountr.chartOfAccountId,
                                                                    ChequeOrReference: xx.ChequeOrReference
                                                                };

                                                                x.VoucherDetails.push(abc);

                                                ");
            WriteLiteral(@"            }

                                                        }

                                                    });


                                                }


                                            });
                                        }

                                        console.log(""VoucherList 1"", VoucherList);


                                        if (VoucherList.length) {
                                            filename = $('#fileUpload').val().replace(/.*[\/\\]/, '');
                                            model = VoucherList;
                                            console.log(""model"", model)
                                            $.post('/Account/Voucher/PostVouchersFromExcel?filename=' + filename, { model, model }, function (data) {
                                                if (data.error == true) {
                                                    loadGriderror(data.message);
                              ");
            WriteLiteral(@"                      $('#ErroeStatusModal').modal('toggle');
                                                    loadingBarHide();
                                                }
                                                else {
                                                    loadGriderror([]);
                                                    showToast(data.message, ""success"");
                                                    loadingBarHide();
                                                }

                                            });

                                        }
                                        else {
                                            showToast(""no data found for upload"", ""error"");
                                        }


                                    }


                                }
                                else {
                                    showToast(""no data found"", ""error"");
                        ");
            WriteLiteral(@"        }


                            }


                        });
                    };
                    fileReader.readAsBinaryString(selectedFile);
                }
            }

        });


    function GetVoucherTypesdata() {
        $.get('/Setup/GetVoucherType', function (data) {
            VoucherTypesData = data;
        });
    }

    function GetServiceTypeData() {
        $.get('/Setup/GetVoucherServiceType', function (data) {
            VoucherServicesData = data;
        });
    }

    function getchartofaccountdata() {
        $.get('/Account/ChartOfAccount/GetChartOfAccountCode', function (data) {
            ChartOfAccountsListData = data;
        });
    }

    function getdepartmentsData() {
        $.get('/Setup/GetDepartment', function (data) {
            DepartmentsData = data;
        });
    }

    function getcustomersData() {
        $.get('/Setup/GetCustomersCode', function (data) {
            CustomersData = data;
        }");
            WriteLiteral(@");
    }

    function loadGriderror(data) {
        console.log(""data"", data)
        $(""#errorGrid"").dxDataGrid({
            dataSource: data,
            keyExpr: ""voucherId"",
            showBorders: true,
            allowColumnResizing: true,
            columnAutoWidth: true,
            paging: {
                pageSize: 10
            },
            columns: [
                {
                    dataField: ""voucherNo"",
                    caption: ""Voucher No"",
                    allowEditing: false,
                },
                {
                    dataField: ""narration"",
                    caption: ""Narration"",
                    allowEditing: false,
                },
            ],

        });

    }


    function loadingBar() {
        $(""#large-indicator"").dxLoadIndicator({
            height: 60,
            width: 60,
        });

        var bar = document.getElementById(""large-indicator"");
        bar.style.display = 'block';
    }
  ");
            WriteLiteral("  function loadingBarHide() {\r\n        var bar = document.getElementById(\"large-indicator\");\r\n        bar.style.display = \'none\';\r\n    }\r\n\r\n</script>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591