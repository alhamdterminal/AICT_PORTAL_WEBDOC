#pragma checksum "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\PreAlert\PaymentUpdateView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "03896f8a90545b4bd491bc65694f117e49f126749967c9933669f00e504a93c9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Import_Views_PreAlert_PaymentUpdateView), @"mvc.1.0.view", @"/Areas/Import/Views/PreAlert/PaymentUpdateView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Import/Views/PreAlert/PaymentUpdateView.cshtml", typeof(AspNetCore.Areas_Import_Views_PreAlert_PaymentUpdateView))]
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
#line 1 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using WebdocTerminal

    ;
#line 2 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using WebdocTerminal.Models

    ;
#line 4 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc

    ;
#line 5 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\_ViewImports.cshtml"
using DevExpress.AspNetCore

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"03896f8a90545b4bd491bc65694f117e49f126749967c9933669f00e504a93c9", @"/Areas/Import/Views/PreAlert/PaymentUpdateView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Import/_ViewImports.cshtml")]
    public class Areas_Import_Views_PreAlert_PaymentUpdateView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<string>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/PaymentUpdateView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal form-label-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("PaymentUpdateForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(28, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\PreAlert\PaymentUpdateView.cshtml"
  

    var ShippingAgent = (IEnumerable<SelectListItem>)ViewData["ShippingAgent"];

    var ShippingLine = (IEnumerable<SelectListItem>)ViewData["ShippingLine"];

    var PortAndTerminal = (IEnumerable<SelectListItem>)ViewData["PortAndTerminal"];


#line default
#line hidden

            BeginContext(290, 111, true);
            WriteLiteral("\r\n<style>\r\n\r\n    .myClass {\r\n        background-color: #73cbad4f;\r\n        color: black;\r\n    }\r\n\r\n</style>\r\n\r\n");
            EndContext();
            BeginContext(401, 56, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03896f8a90545b4bd491bc65694f117e49f126749967c9933669f00e504a93c96084", async() => {
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
            BeginContext(457, 1469, true);
            WriteLiteral(@"
<style>
    .dx-header-row > td > .dx-datagrid-text-content {
        white-space: normal;
        vertical-align: top;
        text-align: center;
    }

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

     
 

    .dx-datagrid-headers.dx-header-multi-row .dx-row.dx-header-row > td {
        padding-top: 12px;
        padding-bottom: 12px;
        vertical-align: bottom;
        border-right: 3px solid #333;
    }
</style>

<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""row"">
            <div class=""col-m");
            WriteLiteral(@"d-11"">
                <div class=""title_left"">
                    <h3>Payment Update</h3>
                </div>
            </div>
            <div class=""col-md-1"">
            </div>
        </div>
    </div>
    <div class=""clearfix""></div>

    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">

                <div class=""x_content"" id=""odform"">
                    ");
            EndContext();
            BeginContext(1926, 5375, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03896f8a90545b4bd491bc65694f117e49f126749967c9933669f00e504a93c98815", async() => {
                BeginContext(1995, 544, true);
                WriteLiteral(@"


                        <div class=""form-horizontal form-label-left row"">




                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""item form-group"">
                                    <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                        PreAlert-Number: <span class=""required"">*</span>
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
");
                EndContext();
                BeginContext(3386, 153, true);
                WriteLiteral("\r\n                                        <select class=\"form-control fav_clr\" name=\"PreAlertNumber\" id=\"perAlertNumber\" multiple required=\"required\" >\r\n");
                EndContext();
#line 112 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\PreAlert\PaymentUpdateView.cshtml"
                                             foreach (var item in Model)
                                            {

#line default
#line hidden

                BeginContext(3660, 48, true);
                WriteLiteral("                                                ");
                EndContext();
                BeginContext(3708, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03896f8a90545b4bd491bc65694f117e49f126749967c9933669f00e504a93c910508", async() => {
                    BeginContext(3731, 4, false);
                    Write(
#line 114 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\PreAlert\PaymentUpdateView.cshtml"
                                                                       item

#line default
#line hidden
                    );
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
                WriteLiteral(
#line 114 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\PreAlert\PaymentUpdateView.cshtml"
                                                                item

#line default
#line hidden
                );
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3744, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 115 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\PreAlert\PaymentUpdateView.cshtml"
                                            }

#line default
#line hidden

                BeginContext(3793, 2106, true);
                WriteLiteral(@"                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">

                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <button id=""btnSubmit"" type=""button"" onclick=""showdetail()"" class=""btn btn-success"" style=""float:right;"">Find</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class=""form-horizontal form-label-left row"">
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div c");
                WriteLiteral(@"lass=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Request No
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                                        <input type=""text"" class=""form-control col-md-7 col-xs-12"" id=""requestNumber"" name=""RequestNumber"" readonly>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-4 col-sm-4 col-xs-12"">
                                <div class=""form-group"">
                                    <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                        Request Date
                                    </label>
                                    <div class=""col-md-8 col-sm-8 col-xs-12"">
                         ");
                WriteLiteral("                \r\n                                        ");
                EndContext();
                BeginContext(5901, 322, false);
                Write(
#line 151 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\PreAlert\PaymentUpdateView.cshtml"
                                          Html.DevExtreme().DateBox()
                                        .Type(DateBoxType.DateTime)
                                        .Value(DateTime.Now)

                                       .Name("RequestDate")
                                       .ID("requestDate")
                                        

#line default
#line hidden
                );
                EndContext();
                BeginContext(6224, 215, true);
                WriteLiteral("\r\n                                        <span class=\"error\" id=\"RequestDateerror\"></span>\r\n\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n");
                EndContext();
                BeginContext(7236, 58, true);
                WriteLiteral("\r\n                        </div>\r\n\r\n\r\n                    ");
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
            BeginContext(7301, 1115, true);
            WriteLiteral(@"

                    <div class=""clearfix""></div>
                    <div class=""row"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <div class=""x_panel"">
                                <div class=""x_content"">
                                    <div id=""paymentupdatedetails""></div>
                                    <div class=""ln_solid""></div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class=""form-group"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <button type=""button"" id=""btnCancel"" class=""btn btn-primary"" onClick=""restform()"" style=""float:right;"">Cancel</button>
                            <button id=""btnSubmit"" type=""button"" onclick=""submitPaymentUpdate()"" class=""btn btn-success"" style=""float:right;"">Submit</button>
                        </div>
                    <");
            WriteLiteral("/div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<string>> Html { get; private set; }
    }
}
#pragma warning restore 1591