#pragma checksum "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\CustomerNOC\CustomerNOCView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "d6be19cd4809fc8f8c9b0f3abdf70a162b380331714e74519af9f140ef6581be"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Export_Views_CustomerNOC_CustomerNOCView), @"mvc.1.0.view", @"/Areas/Export/Views/CustomerNOC/CustomerNOCView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Export/Views/CustomerNOC/CustomerNOCView.cshtml", typeof(AspNetCore.Areas_Export_Views_CustomerNOC_CustomerNOCView))]
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
#line 1 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using WebdocTerminal

    ;
#line 2 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using WebdocTerminal.Models

    ;
#line 4 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc

    ;
#line 5 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\_ViewImports.cshtml"
using DevExpress.AspNetCore

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d6be19cd4809fc8f8c9b0f3abdf70a162b380331714e74519af9f140ef6581be", @"/Areas/Export/Views/CustomerNOC/CustomerNOCView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Export/Views/_ViewImports.cshtml")]
    public class Areas_Export_Views_CustomerNOC_CustomerNOCView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/CustomerNOCView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal form-label-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("CustomerNOCForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\CustomerNOC\CustomerNOCView.cshtml"
  
    var ShippingAgent = (IEnumerable<SelectListItem>)ViewData["ShippingAgent"];

    var GDNumbers = (IEnumerable<SelectListItem>)ViewData["GDNumbers"];

#line default
#line hidden

            BeginContext(163, 1299, true);
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

    input[type=""date""] {
        color: #fff;
        position: relative;
    }

        input[type=""date""]::-webkit-datetime-edit-year-field {
            position: absolute !important;
            border-left: 1px solid #8c8c8c;
            padding: 2px;
            color: #000;
            left: 56px;
        }

        input[type=""date""]::-webkit-datetime-edit-month-field {
            position: absolute !important;
            border-left: 1px solid #8c8c8c;
            padding: 2px;");
            WriteLiteral(@"
            color: #000;
            left: 26px;
        }


        input[type=""date""]::-webkit-datetime-edit-day-field {
            position: absolute !important;
            color: #000;
            padding: 2px;
            left: 4px;
        }
</style>

");
            EndContext();
            BeginContext(1462, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d6be19cd4809fc8f8c9b0f3abdf70a162b380331714e74519af9f140ef6581be6986", async() => {
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
            BeginContext(1516, 585, true);
            WriteLiteral(@"



<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""row"">
            <div class=""col-md-11"">
                <div class=""title_left"">
                    <h3>Customer NOC</h3>
                </div>
            </div>
            <div class=""col-md-1"">
            </div>
        </div>
    </div>
    <div class=""clearfix""></div>






    <div class=""clearfix""></div>

    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">

                <div class=""x_content""");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2101, "\"", 2106, 0);
            EndWriteAttribute();
            BeginContext(2107, 25, true);
            WriteLiteral(">\r\n\r\n                    ");
            EndContext();
            BeginContext(2132, 4868, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d6be19cd4809fc8f8c9b0f3abdf70a162b380331714e74519af9f140ef6581be9036", async() => {
                BeginContext(2199, 502, true);
                WriteLiteral(@"

                        <div class=""form-horizontal form-label-left row"">


                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-6"" for=""first-name"">
                                        GD Number
                                    </label>
                                    <div class=""col-md-6"">
                                        ");
                EndContext();
                BeginContext(2702, 164, false);
                Write(
#line 106 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\CustomerNOC\CustomerNOCView.cshtml"
                                         Html.DropDownList("GDNumber", GDNumbers, "Select GD Number", new { @class = "form-control select2", @id = "gdnumbers", @onchange = "gdNumber_changed(this.value)" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(2866, 556, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>


                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-6"" for=""first-name"">
                                        From  Shipping Agent
                                    </label>
                                    <div class=""col-md-6"">
                                        ");
                EndContext();
                BeginContext(3423, 159, false);
                Write(
#line 118 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\CustomerNOC\CustomerNOCView.cshtml"
                                         Html.DropDownList("ShippingAgentExportId", ShippingAgent, "Select Shipping Agent", new { @class = "form-control", @id = "shippingAgentA", @readonly = "true" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(3582, 2962, true);
                WriteLiteral(@"


                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Email Received
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""date"" class=""form-control"" name=""EmailReceived"" id=""emailReceived"" required>
                                        <span class=""error"" id=""EmailReceivederror""></span>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class=""form-horizontal form-label-left row"">
                    ");
                WriteLiteral(@"        <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        Name Of Person
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" name=""NameOfPerson"" id=""nameOfPerson"" required>
                                        <span class=""error"" id=""NameOfPersonerror""></span>

                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                        ");
                WriteLiteral(@"Contact Number
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control"" name=""ContactNumber"" id=""contactNumber"" required>
                                        <span class=""error"" id=""ContactNumbererror""></span>
                                    </div>
                                </div>
                            </div>

                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-5"" for=""first-name"">
                                        To Shipping Agent
                                    </label>
                                    <div class=""col-md-7"">
                                        ");
                EndContext();
                BeginContext(6545, 173, false);
                Write(
#line 170 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Export\Views\CustomerNOC\CustomerNOCView.cshtml"
                                         Html.DropDownList("ShippingAgentExportBId", ShippingAgent, "Select Shipping Agent ", new { @class = "form-control select2", @id = "shippingAgentB", @required = "required" })

#line default
#line hidden
                );
                EndContext();
                BeginContext(6718, 275, true);
                WriteLiteral(@"
                                        <span class=""error"" id=""ShippingAgenterror""></span>
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
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(7000, 982, true);
            WriteLiteral(@"
                    <div class=""form-group"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <button type=""button"" id=""btnCancel"" class=""btn btn-primary"" onclick=""restform()"" style=""float:right;"">Cancel</button>
                            <button id=""btnSubmit"" type=""button"" style=""float:right;"" onclick=""AddCustomerNOC()"" class=""btn btn-success"">Submit</button>
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
                <div class=""x_content"">
                    <div id=""cargoInfoDetail""></div>
                    <div class=""ln_solid""></div>
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