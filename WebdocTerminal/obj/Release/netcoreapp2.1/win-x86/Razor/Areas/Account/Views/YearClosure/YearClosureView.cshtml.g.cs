#pragma checksum "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\Views\YearClosure\YearClosureView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7b8b2bf8f41981240a063961d4ab29a4b29cd7a1c047d7c36f772f2ee92068c5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Account_Views_YearClosure_YearClosureView), @"mvc.1.0.view", @"/Areas/Account/Views/YearClosure/YearClosureView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Account/Views/YearClosure/YearClosureView.cshtml", typeof(AspNetCore.Areas_Account_Views_YearClosure_YearClosureView))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"7b8b2bf8f41981240a063961d4ab29a4b29cd7a1c047d7c36f772f2ee92068c5", @"/Areas/Account/Views/YearClosure/YearClosureView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Account/_ViewImports.cshtml")]
    public class Areas_Account_Views_YearClosure_YearClosureView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/YearClosureView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\Views\YearClosure\YearClosureView.cshtml"
  
    ViewData["Title"] = "Year Closure";

    var VoucherType = (IEnumerable<SelectListItem>)ViewData["VoucherType"];

    var FinancialYear = (IEnumerable<SelectListItem>)ViewData["FinancialYear"];

    var Department = (IEnumerable<SelectListItem>)ViewData["Department"];

#line default
#line hidden

            BeginContext(291, 487, true);
            WriteLiteral(@"
<style>
     

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
            BeginContext(778, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7b8b2bf8f41981240a063961d4ab29a4b29cd7a1c047d7c36f772f2ee92068c55347", async() => {
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
            BeginContext(832, 836, true);
            WriteLiteral(@"

<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""title_left"">
            <h3>
                Year Closure
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

                    <div class=""col-md-3 col-sm-4 col-xs-12"">
                        <div class=""form-group"">
                            <label class=""control-label col-md-2 col-sm-2 col-xs-12"" for=""first-name"">
                                Year:
                            </label>
                            <div class=""col-md-10 col-sm-10 col-xs-12"">
                                ");
            EndContext();
            BeginContext(1669, 194, false);
            Write(
#line 53 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\Views\YearClosure\YearClosureView.cshtml"
                                 Html.DropDownList("FinancialYearId", FinancialYear, "Select Financial Year", new { @class = "form-control select2", @id = "financialYearId", @required = "required", @onchange = "showdetail()" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(1863, 507, true);
            WriteLiteral(@"
                            </div>
                        </div>
                    </div>

                    <div class=""col-md-3 col-sm-4 col-xs-12"">
                        <div class=""form-group"">
                            <label class=""control-label col-md-2 col-sm-2 col-xs-12"" for=""first-name"">
                                Voucher Type:
                            </label>
                            <div class=""col-md-10 col-sm-10 col-xs-12"">
                                ");
            EndContext();
            BeginContext(2371, 182, false);
            Write(
#line 64 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\Views\YearClosure\YearClosureView.cshtml"
                                 Html.DropDownList("VoucherTypeId", VoucherType, "Select Voucher Type", new { @class = "form-control select2", @id = "voucherTypeId", @required = "required", @disabled = "disabled" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(2553, 503, true);
            WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-3 col-sm-4 col-xs-12"">
                        <div class=""form-group"">
                            <label class=""control-label col-md-2 col-sm-2 col-xs-12"" for=""first-name"">
                                Department:
                            </label>
                            <div class=""col-md-10 col-sm-10 col-xs-12"">
                                ");
            EndContext();
            BeginContext(3057, 177, false);
            Write(
#line 74 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Account\Views\YearClosure\YearClosureView.cshtml"
                                 Html.DropDownList("DepartmentId", Department, "Select Department", new { @class = "form-control select2", @id = "departmentId", @required = "required", @disabled = "disabled" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(3234, 2041, true);
            WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-3 col-sm-4 col-xs-12"">
                        <div class=""form-group"">
                            <div class=""col-md-12 col-sm-12 col-xs-12"">
                                <button class=""btn btn-success"" style=""float:right;"" id=""btnSubmit"" onclick=""PostClosureEntry()"">Post</button>
                            </div>
                        </div>
                    </div> 
                </div>
            </div>
        </div>

        <div class=""clearfix""></div>

        <div class=""row"">

            <div class=""col-md-12 col-sm-12 col-xs-12"">
                <div class=""x_panel"">
                    <div id=""large-indicator""></div>
                    <div class=""x_content"" id=""gridContainer"" style=""height:500px"">

                    </div>
                   
                </div>
            </div>

        </div>

    </div>


  ");
            WriteLiteral(@"  <div id=""ErroeStatusModal"" class=""modal fade"" role=""dialog"">
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
                                <div class=""x_content"">
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
</script>");
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