#pragma checksum "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\PartyLedger\ExcessRefundView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "d226421c44aa0553c82dfe0e1aa0f0fad3000fb9d4add95d7be6c01d273be518"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Import_Views_PartyLedger_ExcessRefundView), @"mvc.1.0.view", @"/Areas/Import/Views/PartyLedger/ExcessRefundView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Import/Views/PartyLedger/ExcessRefundView.cshtml", typeof(AspNetCore.Areas_Import_Views_PartyLedger_ExcessRefundView))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d226421c44aa0553c82dfe0e1aa0f0fad3000fb9d4add95d7be6c01d273be518", @"/Areas/Import/Views/PartyLedger/ExcessRefundView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Import/_ViewImports.cshtml")]
    public class Areas_Import_Views_PartyLedger_ExcessRefundView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/ExcessRefundView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal form-label-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("RefoundAmountForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\PartyLedger\ExcessRefundView.cshtml"
  
    var banks = (IEnumerable<SelectListItem>)ViewData["banks"];     

#line default
#line hidden

            BeginContext(77, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d226421c44aa0553c82dfe0e1aa0f0fad3000fb9d4add95d7be6c01d273be5185281", async() => {
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
            BeginContext(132, 959, true);
            WriteLiteral(@"

<style>
    .error {
        color: red;
    }
     
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
</style>

<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""title_left"">
            <h3>Excess Amount Refund</h3>
        </div>
        <div class=""title_right"">
        </div>
    </div>
    <div class=""clearfix""></div>

    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">

                <div class=""x_content"">

                    ");
            EndContext();
            BeginContext(1091, 9760, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d226421c44aa0553c82dfe0e1aa0f0fad3000fb9d4add95d7be6c01d273be5187463", async() => {
                BeginContext(1160, 6653, true);
                WriteLiteral(@"
                        <div class=""form-horizontal form-label-left row"">


                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Knock Of Invoice
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""knockofinvoice"" name=""InvoiceNo"" required>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                ");
                WriteLiteral(@"        Excess Amount
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" class=""form-control mask"" id=""excessAmount"" readonly>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">

                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <button id=""btnSubmitInovice No"" type=""button"" onclick=""FindRecord()"" class=""btn btn-success"" style=""float: right;"">Find</button>
                                    </div>
                                </div>
                ");
                WriteLiteral(@"            </div>
                        </div>




                        <div class=""form-horizontal form-label-left row"">


                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Cheque No :
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""chequeNumber"" name=""ChequeNumber"" required>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-x");
                WriteLiteral(@"s-12"" for=""name"">
                                        Cheque Date
                                    </label>
                                    <div class=""control-group"">
                                        <div class=""controls"">
                                            <div class=""col-md-8 col-sm-8 col-xs-12"">
                                                <input type=""datetime"" class=""form-control has-feedback-left"" id=""single_cal2"" name=""ChequeDate"" aria-describedby=""inputSuccess2Status"">
                                                <span class=""fa fa-calendar-o form-control-feedback left"" aria-hidden=""true""></span>
                                                <span id=""inputSuccess2Status"" class=""sr-only"">(success)</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div cl");
                WriteLiteral(@"ass=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Amount :
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" class=""form-control"" id=""amount"" name=""Amount"" required>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class=""form-horizontal form-label-left row"">


                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        In Favorof");
                WriteLiteral(@" :
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""inFavorof"" name=""InFavorof"" required>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        AICT Service Charges :
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" class=""form-control"" id=""aictserviceCharges"" name=""AICTServiceCharges"" required>
                                    </div>
                                </");
                WriteLiteral(@"div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label  col-md-4 col-xs-12"" for=""first-name"">
                                        Bank <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-xs-12"">
                                        ");
                EndContext();
                BeginContext(7814, 128, false);
                Write(
#line 160 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\PartyLedger\ExcessRefundView.cshtml"
                                         Html.DropDownList("BankId", banks, "Select Bank", new { @class = "form-control select2", @id = "bank", @required = "required" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(7942, 2902, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>




                        </div>

                        <div class=""form-horizontal form-label-left row"">


                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Online Acc Title :
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""onllineAccountTitle"" name=""OnllineAccountTitle"" required>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                    ");
                WriteLiteral(@"            <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Online Acc Amount :
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""number"" class=""form-control mask"" id=""onllineAccountAmount"" name=""OnllineAccountAmount"" required>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Online Account Number :
                                    </label>
                                    <div class=""col-md-8 col-sm-8 ");
                WriteLiteral(@"col-xs-12"">
                                        <input type=""text"" class=""form-control"" id=""onllineAccountNumber"" name=""OnllineAccountNumber"" required>
                                    </div>
                                </div>
                            </div>




                        </div>

                        <div class=""form-group"">
                            <div class=""col-md-10"">
                            </div>
                            <div class=""col-md-2"">
                                <button type=""submit"" id=""btnCancel"" class=""btn btn-primary"">Cancel</button>
                                <button type=""button"" id=""btnSubmit"" onclick=""submitRefundAmount()"" class=""btn btn-success"">Submit</button>
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
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(10851, 473, true);
            WriteLiteral(@"


                    <div class=""row"">

                        <div class=""col-md-12 col-sm-12 col-xs-12"">

                            <div class=""x_content"">
                                <div id=""detailGrid""></div>

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
</script>
");
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
