#pragma checksum "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Reports\AuctionReportForCustom.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "809a2ba32d5a97c77f288a836ec2304f367a5fc6489bc5822f9a20761de48f36"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Import_Views_Reports_AuctionReportForCustom), @"mvc.1.0.view", @"/Areas/Import/Views/Reports/AuctionReportForCustom.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Import/Views/Reports/AuctionReportForCustom.cshtml", typeof(AspNetCore.Areas_Import_Views_Reports_AuctionReportForCustom))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"809a2ba32d5a97c77f288a836ec2304f367a5fc6489bc5822f9a20761de48f36", @"/Areas/Import/Views/Reports/AuctionReportForCustom.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Import/_ViewImports.cshtml")]
    public class Areas_Import_Views_Reports_AuctionReportForCustom : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/viewer.part.bundle.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/viewer.part.bundle.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/AuctionReportForCustom.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Reports\AuctionReportForCustom.cshtml"
  
    var shippingAgents = (IEnumerable<SelectListItem>)ViewData["shippingAgents"];

#line default
#line hidden

            BeginContext(90, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "809a2ba32d5a97c77f288a836ec2304f367a5fc6489bc5822f9a20761de48f366393", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(151, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(153, 50, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "809a2ba32d5a97c77f288a836ec2304f367a5fc6489bc5822f9a20761de48f367669", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(203, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(205, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "809a2ba32d5a97c77f288a836ec2304f367a5fc6489bc5822f9a20761de48f368870", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(266, 761, true);
            WriteLiteral(@"


<div class=""right_col"" role=""main"">
    <div class=""page-title x_panel"">
        <div class=""title_left "">
            <h3>
                Auction Report For Custom
            </h3>
        </div>
        <div class=""title_right"">

        </div>
    </div>
    <div class=""clearfix""></div>
    <div class=""x_panel"">
        <div class=""row"">
            <div class=""col-md-4"">
                <div class=""form-group"">
                    <label class=""control-label  col-md-4"" for=""first-name"">
                        Type <span class=""required"">*</span>
                    </label>
                    <div class=""col-md-8 col-xs-12"">
                        <select class=""form-control"" id=""type2"">
                            ");
            EndContext();
            BeginContext(1027, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "809a2ba32d5a97c77f288a836ec2304f367a5fc6489bc5822f9a20761de48f3610864", async() => {
                BeginContext(1045, 11, true);
                WriteLiteral("Select Type");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1065, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(1095, 30, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "809a2ba32d5a97c77f288a836ec2304f367a5fc6489bc5822f9a20761de48f3612300", async() => {
                BeginContext(1113, 3, true);
                WriteLiteral("CFS");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1125, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(1155, 29, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "809a2ba32d5a97c77f288a836ec2304f367a5fc6489bc5822f9a20761de48f3613727", async() => {
                BeginContext(1173, 2, true);
                WriteLiteral("CY");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1184, 422, true);
            WriteLiteral(@"
                        </select>
                    </div>
                </div>
            </div>

            <div class=""col-lg-4 col-md-4 col-sm-4"">
                <div class=""form-group"">
                    <label class=""control-label  col-md-3"" for=""first-name"">
                        ShippingAgent
                    </label>
                    <div class=""col-md-9"">
                        ");
            EndContext();
            BeginContext(1607, 164, false);
            Write(
#line 44 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Reports\AuctionReportForCustom.cshtml"
                         Html.DropDownList("ShippingAgentId", shippingAgents, "Select Shipping Agent", new { @class = "form-control select2", @id = "shippingAgent", required = "required" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(1771, 564, true);
            WriteLiteral(@"

                    </div>
                </div>
            </div>


        </div>
        <div class=""clearfix""></div>
        <div class=""row"">

            <div class=""col-sm-4"">

                <div class=""form-group"">
                    <label class=""control-label  col-md-4"" for=""first-name"">
                        Location <span class=""required"">*</span>
                    </label>
                    <div class=""col-md-8 col-xs-12"">
                        <select class=""form-control"" id=""type"">
                            ");
            EndContext();
            BeginContext(2335, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "809a2ba32d5a97c77f288a836ec2304f367a5fc6489bc5822f9a20761de48f3616684", async() => {
                BeginContext(2353, 11, true);
                WriteLiteral("On Terminal");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2373, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(2403, 36, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "809a2ba32d5a97c77f288a836ec2304f367a5fc6489bc5822f9a20761de48f3618120", async() => {
                BeginContext(2421, 9, true);
                WriteLiteral("Delivered");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2439, 1776, true);
            WriteLiteral(@"
                        </select>
                    </div>

                </div>
            </div>
            <div class=""col-md-4  col-xs-12"">
                <div class=""control-group"">
                    <div class=""controls"">
                        <label class=""control-label col-md-3 col-xs-12"" for=""first-name"">
                            From:
                        </label>
                        <div class=""col-md-9  col-xs-12"">
                            <input type=""date"" class=""form-control"" id=""fromdate"">
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-md-4  col-xs-12"">
                <div class=""control-group"">
                    <div class=""controls"">
                        <label class=""control-label col-md-3  col-xs-12"" for=""first-name"">
                            To:
                        </label>
                        <div class=""col-md-9  col-xs-12"">
                ");
            WriteLiteral(@"            <input type=""date"" class=""form-control"" id=""todate"">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class=""row"">
            <div class=""col-md-12  col-xs-12"">
                <button type=""button"" class=""btn btn-dark mobBtn"" onclick=""myFunction()"" style=""float:right"">Submit</button>
            </div>
        </div>
    </div>





    <div class=""row"">

        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">
                <div id=""large-indicator""></div>
                <div class=""x_content"" id=""repotdata"">

                </div>
            </div>
        </div>

    </div>

</div>
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
