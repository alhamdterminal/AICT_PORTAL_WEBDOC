#pragma checksum "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Tariff\AICTAndLineIndexTariffView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "422cb87588560a41e0c38cf4082285ca6ad1b403803a0f329866536e36f5d579"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Tariff_AICTAndLineIndexTariffView), @"mvc.1.0.view", @"/Views/Tariff/AICTAndLineIndexTariffView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Tariff/AICTAndLineIndexTariffView.cshtml", typeof(AspNetCore.Views_Tariff_AICTAndLineIndexTariffView))]
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
#line 1 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\_ViewImports.cshtml"
using WebdocTerminal

    ;
#line 2 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\_ViewImports.cshtml"
using WebdocTerminal.Models

    ;
#line 4 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc

    ;
#line 5 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\_ViewImports.cshtml"
using DevExpress.AspNetCore

#line default
#line hidden
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"422cb87588560a41e0c38cf4082285ca6ad1b403803a0f329866536e36f5d579", @"/Views/Tariff/AICTAndLineIndexTariffView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Views/_ViewImports.cshtml")]
    public class Views_Tariff_AICTAndLineIndexTariffView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/app.js/AICTAndLineIndexTariffView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "ALL", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "DLV", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "UNDLV", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Tariff\AICTAndLineIndexTariffView.cshtml"
  
    var agents = (IEnumerable<SelectListItem>)ViewData["ShippingAgents"];
    var goodsHead = (IEnumerable<SelectListItem>)ViewData["GoodsHead"];

#line default
#line hidden

            BeginContext(157, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "422cb87588560a41e0c38cf4082285ca6ad1b403803a0f329866536e36f5d5795378", async() => {
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
            BeginContext(222, 3351, true);
            WriteLiteral(@"

<style>

    #CargoInformation {
        max-height: 750px;
    }

    .my-class {
        background-color: #4b5f71;
        color: white;
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

    .dx-header-row > td > .dx-datagrid-text-content {
        white-space: normal;
        vertical-align: top;
        text-align");
            WriteLiteral(@": center;
    }

    .myClass {
        background-color: #73cbad4f;
        color: black;
    }

    .dx-scrollbar-vertical .dx-scrollbar-hoverable {
        width: 30px !important;
         
    }

    .dx-scrollbar-vertical .dx-scrollable-scroll {
        width: 30px !important; 
    }

    .dx-scrollbar-horizontal .dx-scrollbar-hoverable .dx-state-hover {
        height: 480px !important;
    }

    .dx-scrollbar-horizontal .dx-scrollable-scroll .dx-state-hover {
        height: 480px !important;
    }
    .dx-scrollbar-horizontal .dx-scrollable-scroll .dx-state-hover {
        height: 480px !important;
    } 
</style>


<script type=""text/javascript"" src=""https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.15.3/xlsx.full.min.js""></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/exceljs/4.1.1/exceljs.min.js""></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2.0.2/FileSaver.min.js""></script>



<style>
    .newbox {
        text-align: ce");
            WriteLiteral(@"nter;
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
    <div class=""page-title"">
        <div class=""row"">
            <div class=""col-md-11"">
                <div class=""title_left"">
                    <h3> AICT & Line Index Tariff </h3>
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

                <div class=""x_content"">


                    <div class=""row"">

                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control");
            WriteLiteral(@"-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                    Vir No: <span class=""required"">*</span>
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(3575, 510, false);
            Write(
#line 130 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Tariff\AICTAndLineIndexTariffView.cshtml"
                                      Html.DevExtreme()
                                                .Autocomplete()
                                                .ID("virnolist")
                                                .DataSource(d => d.Mvc()
                                                .Controller("APICalls")
                                                .Area("")
                                                .LoadAction("Getvirno"))
                                                .Placeholder("Type GDNumber...")

#line default
#line hidden
            );
            EndContext();
            BeginContext(4086, 574, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                    Container No: <span class=""required"">*</span>
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(4662, 527, false);
            Write(
#line 147 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Tariff\AICTAndLineIndexTariffView.cshtml"
                                      Html.DevExtreme()
                                                .Autocomplete()
                                                .ID("containerlist")
                                                .DataSource(d => d.Mvc()
                                                .Controller("APICalls")
                                                .Area("")
                                                .LoadAction("Getigmocontainers"))
                                                .Placeholder("Type Container No...")

#line default
#line hidden
            );
            EndContext();
            BeginContext(5190, 570, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-4 col-sm-4 col-xs-12"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4 col-sm-3 col-xs-12"" for=""name"">
                                    Index No: <span class=""required"">*</span>
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(5762, 522, false);
            Write(
#line 164 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Tariff\AICTAndLineIndexTariffView.cshtml"
                                      Html.DevExtreme()
                                                .Autocomplete()
                                                .ID("containerIndexlist")
                                                .DataSource(d => d.Mvc()
                                                .Controller("APICalls")
                                                .Area("")
                                                .LoadAction("Getigmoindexes"))
                                                .Placeholder("Type Index...")

#line default
#line hidden
            );
            EndContext();
            BeginContext(6285, 576, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>



                    </div>
                    <div class=""row"">

                        <div class=""col-md-4"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4"" for=""name"">
                                    Agent Name
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(6862, 113, false);
            Write(
#line 187 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Tariff\AICTAndLineIndexTariffView.cshtml"
                                     Html.DropDownList("Name", agents, "Select Agent", new { @class = "form-control select2", @id = "agentdropdown" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(6975, 503, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>

                        <div class=""col-md-4"">
                            <div class=""item form-group"">
                                <label class=""control-label col-md-4"" for=""name"">
                                    Goods Head
                                </label>
                                <div class=""col-md-8 col-sm-8 col-xs-12"">
                                    ");
            EndContext();
            BeginContext(7479, 125, false);
            Write(
#line 198 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Tariff\AICTAndLineIndexTariffView.cshtml"
                                     Html.DropDownList("Name", goodsHead, "Select Goods Head", new { @class = "form-control select2", @id = "goodsheaddropdown" })

#line default
#line hidden
            );
            EndContext();
            BeginContext(7604, 605, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                        <div class=""col-sm-4"">

                            <div class=""form-group"">
                                <label class=""control-label  col-md-4"" for=""first-name"">
                                    Status <span class=""required"">*</span>
                                </label>
                                <div class=""col-md-8 col-xs-12"">
                                    <select class=""form-control"" id=""type"">
                                        ");
            EndContext();
            BeginContext(8209, 32, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "422cb87588560a41e0c38cf4082285ca6ad1b403803a0f329866536e36f5d57916683", async() => {
                BeginContext(8229, 3, true);
                WriteLiteral("ALL");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(8241, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(8283, 32, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "422cb87588560a41e0c38cf4082285ca6ad1b403803a0f329866536e36f5d57918122", async() => {
                BeginContext(8303, 3, true);
                WriteLiteral("DLV");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(8315, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(8357, 36, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "422cb87588560a41e0c38cf4082285ca6ad1b403803a0f329866536e36f5d57919561", async() => {
                BeginContext(8379, 5, true);
                WriteLiteral("UNDLV");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(8393, 16802, true);
            WriteLiteral(@"
                                    </select>
                                </div>

                            </div>
                        </div>

                    </div>

                    <div class=""row"">


                        <div class=""col-md-4  col-xs-12"">
                            <div class=""control-group"">
                                <div class=""controls"">
                                    <label class=""control-label col-md-4 col-xs-12"" for=""first-name"">
                                        From:
                                    </label>
                                    <div class=""col-md-8  col-xs-12"">
                                        <input type=""date"" class=""form-control"" id=""fromdate"">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-4  col-xs-12"">
                            <div class=""cont");
            WriteLiteral(@"rol-group"">
                                <div class=""controls"">
                                    <label class=""control-label col-md-4  col-xs-12"" for=""first-name"">
                                        To:
                                    </label>
                                    <div class=""col-md-8  col-xs-12"">
                                        <input type=""date"" class=""form-control"" id=""todate"">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-4"">
                            <div class=""form-group"">
                                <button type=""button"" class=""btn btn-dark"" onclick=""showcargoDetaildesc()"" style="" float: right;""> View Detail</button>
                            </div>
                        </div>
                    </div>

                    
                    <div class=""row"">
                        <div ");
            WriteLiteral(@"class=""col-md-4 col-sm-4 col-xs-12"">
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
                            </div>
                        </div>
                    </div>
    ");
            WriteLiteral(@"                <div class=""ln_solid""></div>

                    <div class=""row"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <div id=""large-indicator""></div>
                            <div id=""CargoInformation"">

                            </div>
                        </div>
                    </div>

                    <div class=""row"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <button type=""button"" class=""btn btn-success"" onclick=""SaveInfo()""  style="" float: right; height: 50px; width: 200px;"">Save Information</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>


 



<script>
    $('.select2').select2();
     
    var selectedFile;
    document
        .getElementById(""fileUpload"")
        .addEventListener(""change"", function (event) {
            selectedFile = event.");
            WriteLiteral(@"target.files[0];
        });
    document
        .getElementById(""uploadExcel"")
        .addEventListener(""click"", function () {

            var resultvalue = true;

            if (selectedFile) {
                var fileReader = new FileReader();
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

                            newresult.pop();

                            //var newresult = rowObject.filter(c => c.Delivered == 'false');
                  ");
            WriteLiteral(@"         // var deliverdresultdata = rowObject.filter(c => c.Delivered == 'true');
  
                            if (newresult.length) {

                                console.log(""ok newresult"", newresult)


                                newresult.forEach(c => {


                                    if ($.isNumeric(c.AICTCBM) == false) {
                                        resultvalue = false;
                                        return alert(""please add Additional Charge AICT Per CBM Rate in numbers "" )
                                    }
                                    if ($.isNumeric(c.AICTIndex) == false) {
                                        resultvalue = false;
                                        return alert(""please add Additional Charge AICT Per Index Rate in numbers "")
                                    }
                                    //if ($.isNumeric(c.FFCBM) == false) {
                                    if ($.isNumeric(c.AdditionalFFCBM) == fa");
            WriteLiteral(@"lse) {
                                        resultvalue = false;
                                        return alert(""please add Additional Charge FF Per CBM Rate in numbers "")
                                    }
                                    //if ($.isNumeric(c.FFIndex) == false) {
                                    if ($.isNumeric(c.AdditionalFFIndex) == false) {
                                        resultvalue = false;
                                        return alert(""please add Additional Charge FF Per Index Rate in numbers "")
                                    }
                                    if ($.isNumeric(c.ShareAICTCBM) == false) {
                                        resultvalue = false;
                                        return alert(""please add aict Per CBM Rate in numbers "")
                                    }
                                    if ($.isNumeric(c.ShareAICTIndex) == false) {
                                        resultvalue = fal");
            WriteLiteral(@"se;
                                        return alert(""please add aict Per Index Rate in numbers "")
                                    }
                                    if ($.isNumeric(c.ShareFFCBM) == false) {
                                        resultvalue = false;
                                        return alert(""please add FF Per CBM Rate in numbers "")
                                    }
                                    if ($.isNumeric(c.ShareFFIndex) == false) {
                                        resultvalue = false;
                                        return alert(""please add FF Per Index Rate in numbers "")
                                    }


                                    if (c.AICTCBM < 0) {
                                        resultvalue = false;
                                        return alert(""please add Additional Charge AICT Per CBM Rate > 0  "")
                                    }
                                    if (c.AICTIndex");
            WriteLiteral(@" < 0) {
                                        resultvalue = false;
                                        return alert(""please add Additional Charge AICT Per Index Rate > 0 "")
                                    }
                                    //if (c.FFCBM < 0) {
                                    if (c.AdditionalFFCBM < 0) {
                                        resultvalue = false;
                                        return alert(""please add Additional Charge FF Per CBM Rate > 0 "")
                                    }
                                    //if (c.FFIndex < 0) {
                                    if (c.AdditionalFFIndex < 0) {
                                        resultvalue = false;
                                        return alert(""please add Additional Charge FF Per Index Rate > 0 "")
                                    }
                                    if (c.ShareAICTCBM < 0) {
                                        resultvalue = false;
        ");
            WriteLiteral(@"                                return alert(""please add aict Per CBM Rate > 0 "")
                                    }
                                    if (c.ShareAICTIndex < 0) {
                                        resultvalue = false;
                                        return alert(""please add aict Per Index Rate > 0 "")
                                    }
                                    if (c.ShareFFCBM < 0) {
                                        resultvalue = false;
                                        return alert(""please add FF Per CBM Rate > 0 "")
                                    }
                                    if (c.ShareFFIndex < 0) {
                                        resultvalue = false;
                                        return alert(""please add FF Per Index Rate > 0 "")
                                    }


                                    //var totalcbm = Number(c.AICTCBM) + Number(c.FFCBM) + Number(c.ShareAICTCBM) + Number(c.ShareFFC");
            WriteLiteral(@"BM);
                                    var totalcbm = Number(c.AICTCBM) + Number(c.AdditionalFFCBM) + Number(c.ShareAICTCBM) + Number(c.ShareFFCBM);
                                    //var totalindx = Number(c.AICTIndex) + Number(c.FFIndex) + Number(c.ShareAICTIndex) + Number(c.ShareFFIndex);
                                    var totalindx = Number(c.AICTIndex) + Number(c.AdditionalFFIndex) + Number(c.ShareAICTIndex) + Number(c.ShareFFIndex);

                                    console.log(""totalcbm"", Number(totalcbm));
                                    console.log(""totalindx"", Number(totalindx));

                                    if (Number(totalcbm) <= 0 && Number(totalindx) <= 0) {

                                        resultvalue = false;
                                        return alert(""please add amount in cbm or index in igm "" + c.IGM + "" index "" + c.Index)
                                    }


                                });

                                if");
            WriteLiteral(@" (resultvalue == true) {
                                    var result = [];
                                    newresult.forEach(c => {




                                        let x = {

                                            perBoxRate: c.PerBox,
                                            additionalChargeAICTPerCBMRate: c.AICTCBM,
                                            additionalChargeAICTPerIndexRate: c.AICTIndex,
                                            //additionalChargeFFPerCBMRate: c.FFCBM,
                                            additionalChargeFFPerCBMRate: c.AdditionalFFCBM,
                                            //additionalChargeFFPerIndexRate: c.FFIndex,
                                            additionalChargeFFPerIndexRate: c.AdditionalFFIndex,

                                            aictPerCBMRate: c.ShareAICTCBM,
                                            aictPerIndexRate: c.ShareAICTIndex,
                                            ff");
            WriteLiteral(@"PerCBMRate: c.ShareFFCBM,
                                            ffPerIndexRate: c.ShareFFIndex,

                                            totalCBMRate: c.TotalCBM,
                                            totalPerIndexRate: c.TotalIndex,

                                            containerNumber: c.Container,
                                            igmNo: c.IGM,
                                            indexNo: c.Index,
                                            higherCBM: c.CBM

                                        };

                                        result.push(x);

                                    });

                                    console.log(""result"", result)

                                    $.post('/Tariff/UpdateTariffAmountFromExcelSheet', { model: result }, function (data) {

                                        if (data.error == true) {
                                            // showToast(data.message, ""error"")

            ");
            WriteLiteral(@"                                alert(data.message)
                                            //  alert(data.message + ""and total "" + deliverdresultdata.length + ""are Delivered"")



                                            //var virno = $(""#virnolist"").dxAutocomplete(""instance"").option(""value"")
                                            //var containerno = $(""#containerlist"").dxAutocomplete(""instance"").option(""value"");
                                            //var indexno = $(""#containerIndexlist"").dxAutocomplete(""instance"").option(""value"")
                                            //var agent = $(""#agentdropdown"").val();

                                            
                                            //listGrid(virno, containerno, indexno, agent);


                                        } else {

                                            alert(data.message)
                                            // alert(data.message + ""and total "" + deliverdresultdata.length + ");
            WriteLiteral(@"""are Delivered"")
                                            //showToast(, ""success"");

                                            //var virno = $(""#virnolist"").dxAutocomplete(""instance"").option(""value"")
                                            //var containerno = $(""#containerlist"").dxAutocomplete(""instance"").option(""value"");
                                            //var indexno = $(""#containerIndexlist"").dxAutocomplete(""instance"").option(""value"")
                                            //var agent = $(""#agentdropdown"").val();

                                            //console.log('virno', virno);
                                            //console.log('containerno', containerno);
                                            //console.log('indexno', indexno);
                                            //console.log('agent', agent);

                                            

                                            var virno = result[0].igmNo;
                         ");
            WriteLiteral(@"                   var containerno = """";
                                            var indexno = result[0].indexNo;
                                            var agent = """";
                                            var goodhead = """";

                                            var fromdate = document.getElementById(""fromdate"").value;
                                            var todate = document.getElementById(""todate"").value;
                                            var type = document.getElementById(""type"").value;

                                            console.log('virno', virno);
                                            console.log('containerno', containerno);
                                            console.log('indexno', indexno);
                                            console.log('agent', agent);
                                            console.log('goodhead', goodhead);
                                            listGrid(virno, containerno, indexno, agen");
            WriteLiteral(@"t, goodhead, type, fromdate, todate);

                                        }

                                    });
                                }
                               
                            }

                        }

 
                    });
                };
                fileReader.readAsBinaryString(selectedFile);
            }
        });

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
