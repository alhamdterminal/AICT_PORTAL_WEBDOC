#pragma checksum "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Reports\DeliveryOrderReportCFSContainerWeightWise.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2178a2272d3b920d79fa032d340d3cc5944e2d4411608ec9f0a55544c5b41178"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Import_Views_Reports_DeliveryOrderReportCFSContainerWeightWise), @"mvc.1.0.view", @"/Areas/Import/Views/Reports/DeliveryOrderReportCFSContainerWeightWise.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Import/Views/Reports/DeliveryOrderReportCFSContainerWeightWise.cshtml", typeof(AspNetCore.Areas_Import_Views_Reports_DeliveryOrderReportCFSContainerWeightWise))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"2178a2272d3b920d79fa032d340d3cc5944e2d4411608ec9f0a55544c5b41178", @"/Areas/Import/Views/Reports/DeliveryOrderReportCFSContainerWeightWise.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Import/_ViewImports.cshtml")]
    public class Areas_Import_Views_Reports_DeliveryOrderReportCFSContainerWeightWise : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/viewer.part.bundle.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/viewer.part.bundle.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/DeliveryOrderReportCFSContainerWeightWise.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Areas/Import/Views/Shared/_Filter_Index.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Reports\DeliveryOrderReportCFSContainerWeightWise.cshtml"
  
    var Agents = (IEnumerable<SelectListItem>)ViewData["Agents"];


#line default
#line hidden

            BeginContext(76, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2178a2272d3b920d79fa032d340d3cc5944e2d4411608ec9f0a55544c5b411786016", async() => {
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
            BeginContext(137, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(139, 50, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2178a2272d3b920d79fa032d340d3cc5944e2d4411608ec9f0a55544c5b411787292", async() => {
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
            BeginContext(189, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(191, 80, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2178a2272d3b920d79fa032d340d3cc5944e2d4411608ec9f0a55544c5b411788493", async() => {
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
            BeginContext(271, 707, true);
            WriteLiteral(@"

<style>
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



<div class=""right_col"" role=""main"">
    <div class=""page-title x_panel"">
        <div class=""title_left"">
            <h3>
                Delivery Order Report
            </h3>
        </div>

    </div>
    <div class=""clearfix""></div>
    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">
                <div class=""x_content"">

");
            EndContext();
            BeginContext(1056, 72, true);
            WriteLiteral("\r\n                    <div id=\"containerType\">\r\n                        ");
            EndContext();
            BeginContext(1128, 67, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2178a2272d3b920d79fa032d340d3cc5944e2d4411608ec9f0a55544c5b4117810598", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1195, 481, true);
            WriteLiteral(@"
                    </div>

                    <div class=""row"">
                        <div class=""col-md-5  col-xs-12"">
                            <div class=""form-group"">
                                <label class=""control-label  col-md-3  col-xs-12"" for=""first-name"">
                                    ShippingAgent
                                </label>
                                <div class=""col-md-9  col-xs-12"">
                                    ");
            EndContext();
            BeginContext(1677, 156, false);
            Write(
#line 54 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Reports\DeliveryOrderReportCFSContainerWeightWise.cshtml"
                                     Html.DropDownList("ShippingAgentId", Agents, "Select Shipping Agent", new { @class = "form-control select2", @id = "shippingAgent", required = "required" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(1833, 699, true);
            WriteLiteral(@"

                                </div>
                            </div>
                        </div>
                        <div class=""col-md-7"">

                            <div class=""col-lg-5 col-md-5 col-sm-5  col-xs-12"">
                                <div class=""control-group"">
                                    <div class=""controls"">
                                        <label class=""control-label col-md-3 col-sm-3 col-xs-12"" for=""first-name"">
                                            From:
                                        </label>
                                        <div class=""col-md-9 col-xs-10"">
                                            ");
            EndContext();
            BeginContext(2534, 318, false);
            Write(
#line 68 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Reports\DeliveryOrderReportCFSContainerWeightWise.cshtml"
                                              Html.DevExtreme().DateBox()
                                  .Type(DateBoxType.DateTime)
                                    .DisplayFormat("MM/dd/yyy hh:mm")
                                .Name("DateStartTime")
                               .ID("dateStartTime")

                                            

#line default
#line hidden
            );
            EndContext();
            BeginContext(2853, 708, true);
            WriteLiteral(@"

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-lg-5 col-md-5 col-sm-5  col-xs-12"">
                                <div class=""control-group"">
                                    <div class=""controls"">
                                        <label class=""control-label col-md-2 col-sm-2 col-xs-12"" for=""first-name"">
                                            To:
                                        </label>
                                        <div class=""col-md-9  col-xs-10"">
                                            ");
            EndContext();
            BeginContext(3563, 308, false);
            Write(
#line 87 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Areas\Import\Views\Reports\DeliveryOrderReportCFSContainerWeightWise.cshtml"
                                              Html.DevExtreme().DateBox()
                                .Type(DateBoxType.DateTime)
                                  .DisplayFormat("MM/dd/yyy hh:mm")
                              .Name("EndStartTime")
                             .ID("endStartTime")

                                            

#line default
#line hidden
            );
            EndContext();
            BeginContext(3872, 993, true);
            WriteLiteral(@"
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class=""row"">
                        <div class=""col-lg-2 col-md-2 col-sm-2  col-xs-12"">
                            <button type=""button"" class=""btn btn-dark"" onclick=""myFunction()"">Submit</button>
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
                <div class=""x_content"" id=""repotdata"">

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
