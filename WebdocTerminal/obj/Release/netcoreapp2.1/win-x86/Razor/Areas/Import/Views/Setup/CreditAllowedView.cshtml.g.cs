#pragma checksum "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Setup\CreditAllowedView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "16f93499749658cbe44a53170ecaf38d95406d94dea828b69ec12e74def33aaf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Import_Views_Setup_CreditAllowedView), @"mvc.1.0.view", @"/Areas/Import/Views/Setup/CreditAllowedView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Import/Views/Setup/CreditAllowedView.cshtml", typeof(AspNetCore.Areas_Import_Views_Setup_CreditAllowedView))]
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
#line 1 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using WebdocTerminal

    ;
#line 2 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using WebdocTerminal.Models

    ;
#line 4 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc

    ;
#line 5 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using DevExpress.AspNetCore

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"16f93499749658cbe44a53170ecaf38d95406d94dea828b69ec12e74def33aaf", @"/Areas/Import/Views/Setup/CreditAllowedView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Import/_ViewImports.cshtml")]
    public class Areas_Import_Views_Setup_CreditAllowedView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/CreditAllowedView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal form-label-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("novalidate", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("CreditAllowedForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 870, true);
            WriteLiteral(@"
<style>

    #error {
        visibility: hidden;
        width: 200px;
        background-color: #d25656;
        color: #fff;
        text-align: center;
        border-radius: 2px;
        padding: 16px;
        z-index: 1;
        font-size: 17px;
    }

        #error.show {
            visibility: visible;
            -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
            animation: fadein 0.5s, fadeout 0.5s 2.5s;
        }



    input[type=text]:disabled {
        background: #dddddd;
    }

    .error {
        color: red;
    }

    .newbox {
        text-align: center;
        padding: 3px 0px 3px 0px;
        font-size: 14px;
        font-weight: 600;
        margin: -3px -10px 10px -10px;
        color: white;
        background: #2a3f54;
        border-bottom: 1px solid #d1d1d1;
    }
</style>
");
            EndContext();
            BeginContext(870, 56, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "16f93499749658cbe44a53170ecaf38d95406d94dea828b69ec12e74def33aaf6347", async() => {
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
            BeginContext(926, 466, true);
            WriteLiteral(@"
<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""row"">
            <div class=""col-md-11"">
                <div class=""title_left"">
                    <h3>Credit Allowed</h3>
                </div>
            </div>
        </div>
    </div>
    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">
                <div class=""x_content"">

                    ");
            EndContext();
            BeginContext(1392, 12845, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "16f93499749658cbe44a53170ecaf38d95406d94dea828b69ec12e74def33aaf8037", async() => {
                BeginContext(1475, 532, true);
                WriteLiteral(@"


                        <div class=""row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        IGM: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(2009, 450, false);
                Write(
#line 69 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Setup\CreditAllowedView.cshtml"
                                          Html.DevExtreme()
                                .Autocomplete()
                                .ID("IGM")
                                .DataSource(d => d.Mvc()
                                .Controller("APICalls")
                                .Area("")
                                .LoadAction("GetIGMS"))
                                .Placeholder("Type IGM...")
                                .OnValueChanged("igm_changed")

#line default
#line hidden
                );
                EndContext();
                BeginContext(2460, 11770, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Index # <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"" id=""indexdiv"">

                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <div id=""hidebutton"">
                                        <button id=""btnSubmitindex"" type=""button"" onclick=""showdetail()"" class=""btn btn-success"" st");
                WriteLiteral(@"yle=""float: right;"">Show</button>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class=""row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        FF: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""ff"" readonly>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-grou");
                WriteLiteral(@"p"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        Consignee: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""consignee"" readonly>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        Clearing Agent: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                        ");
                WriteLiteral(@"                <input type=""text"" class=""form-control"" id=""clearingagent"" readonly>
                                    </div>
                                </div>
                            </div>

                        </div>


                        <div class=""row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        CBM: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""cbm"" readonly>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
   ");
                WriteLiteral(@"                             <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        Weight: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""weight"" readonly>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        FF /AICT Ch: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 ");
                WriteLiteral(@"col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""rate"" readonly>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class=""row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        Storage: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" class=""form-control mask"" id=""storage"" readonly>
                                    </div>
                                </div>
                            </div>
                        ");
                WriteLiteral(@"    <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        Other Charges: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" class=""form-control mask"" id=""otherCharges"" readonly>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        Total Charges: <span class=""required"">*</span>
                             ");
                WriteLiteral(@"       </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" class=""form-control mask"" id=""totalcharges"" readonly>
                                    </div>
                                </div>
                            </div>


                        </div>

                        <div class=""row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        Credit Allowed (Rs.): <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" class=""form-control mask"" id=""creditAllowedRs"" name=""CreditAllowedRs"" required>
         ");
                WriteLiteral(@"                           </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        Days: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" class=""form-control"" id=""creditDays"" name=""CreditDays"" required>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-");
                WriteLiteral(@"xs-12"" for=""name"">
                                        Remarks: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""remarks"" name=""Remarks"">
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class=""row"">

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""control-group"">
                                    <div class=""controls"">
                                        <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                            Created Date:
                                        </label>
                                        <div class=""col-md-8"">
       ");
                WriteLiteral(@"                                     <input type=""date"" class=""form-control has-feedback-left"" id=""createdDate"" readonly>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        Invoice No: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""invoiceNo"" readonly>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4");
                WriteLiteral(@" col-xs-12"">
                                <div class=""form-group"">
                                    <div id=""hidebutton"">
                                        <button id=""btnSubmitindex"" type=""button"" onclick=""SaveDetail()"" class=""btn btn-success"" style=""float: right;"">Save</button>
                                    </div>
                                </div>
                            </div>

                             

                        </div>

                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(14237, 84, true);
            WriteLiteral("\r\n\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
