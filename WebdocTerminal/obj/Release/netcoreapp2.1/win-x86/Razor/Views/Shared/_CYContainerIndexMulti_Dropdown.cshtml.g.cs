#pragma checksum "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Shared\_CYContainerIndexMulti_Dropdown.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "f3f539bb13c39003e87f64637cfbf1482d338dcb9d018c7a331316998443b37d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__CYContainerIndexMulti_Dropdown), @"mvc.1.0.view", @"/Views/Shared/_CYContainerIndexMulti_Dropdown.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_CYContainerIndexMulti_Dropdown.cshtml", typeof(AspNetCore.Views_Shared__CYContainerIndexMulti_Dropdown))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"f3f539bb13c39003e87f64637cfbf1482d338dcb9d018c7a331316998443b37d", @"/Views/Shared/_CYContainerIndexMulti_Dropdown.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__CYContainerIndexMulti_Dropdown : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CYContainer>>
    {
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(37, 828, true);
            WriteLiteral(@"

<script>
    $(document).ready(function () {
        $('.fav_clr').select2({
            placeholder: ' please select indexs ',
            width: '100%',
            border: '1px solid #e4e5e7',
        });
    });



    $(""#select_all"").change(function () {


        var status = this.checked;

        console.log(""status"", status)

        //$('#containerIndexCYdb option').prop('selected', status);
        if (status == true) {
            $("".fav_clr > option"").prop(""selected"", ""selected"");
            $("".fav_clr"").trigger(""change"");
        } else {
            $("".fav_clr > option"").prop(""selected"", false);
            $("".fav_clr"").trigger(""change"");
        }


    });


</script>
<select class=""form-control fav_clr"" multiple required=""required"" id=""containerIndexCYdb"">
");
            EndContext();
#line 39 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Shared\_CYContainerIndexMulti_Dropdown.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden

            BeginContext(906, 8, true);
            WriteLiteral("        ");
            EndContext();
            BeginContext(914, 52, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f3f539bb13c39003e87f64637cfbf1482d338dcb9d018c7a331316998443b37d4975", async() => {
                BeginContext(945, 12, false);
                Write(
#line 41 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Shared\_CYContainerIndexMulti_Dropdown.cshtml"
                                       item.IndexNo

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
#line 41 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Shared\_CYContainerIndexMulti_Dropdown.cshtml"
                        item.IndexNo

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
            BeginContext(966, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 42 "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Shared\_CYContainerIndexMulti_Dropdown.cshtml"
    }

#line default
#line hidden

            BeginContext(975, 69, true);
            WriteLiteral("</select>\r\n\r\n<input type=\"checkbox\" id=\"select_all\"> select all\r\n \r\n ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CYContainer>> Html { get; private set; }
    }
}
#pragma warning restore 1591
