#pragma checksum "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\Shared\_Filter_ContainerLocation.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7bed08939ed5277a5cd070dd002b7c20212beb88b2e3c875539a606d940a4007"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Import_Views_Shared__Filter_ContainerLocation), @"mvc.1.0.view", @"/Areas/Import/Views/Shared/_Filter_ContainerLocation.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Import/Views/Shared/_Filter_ContainerLocation.cshtml", typeof(AspNetCore.Areas_Import_Views_Shared__Filter_ContainerLocation))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"7bed08939ed5277a5cd070dd002b7c20212beb88b2e3c875539a606d940a4007", @"/Areas/Import/Views/Shared/_Filter_ContainerLocation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Areas/Import/_ViewImports.cshtml")]
    public class Areas_Import_Views_Shared__Filter_ContainerLocation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 1082, true);
            WriteLiteral(@"
<script>


    var containernumber;
     
    function conatiner_changed(data) {

        containernumber = data.value;

        if (containernumber) {
            if (containernumber.length == 11) {
                if (containernumber) {

                    $.get('/APICalls/GetVIRNumberList?containernumber=' + containernumber, function (indexdb) {

                        $('#indexdiv').html(indexdb);

                        containerChangeCallback(""index"");

                    });
                }
            }
            else {
                $('#indexdiv').html([]);

                containerChangeCallback(""index"");
            }
        }
        else {
            $('#indexdiv').html([]);

            containerChangeCallback(""index"");
        }
      


    }

    $(function () {

        $('#indexdiv').on('change', function (val) {

            containerChangeCallback(""index"");

        });




    });




</script>

<div class=""row"">
   ");
            WriteLiteral(" <div class=\"col-md-12 col-sm-12 col-xs-12\">\r\n        <div");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 1082, "\"", 1090, 0);
            EndWriteAttribute();
            BeginContext(1091, 21, true);
            WriteLiteral(">\r\n\r\n            <div");
            EndContext();
            BeginWriteAttribute("class", " class=\"", 1112, "\"", 1120, 0);
            EndWriteAttribute();
            BeginContext(1121, 107, true);
            WriteLiteral(">\r\n\r\n\r\n\r\n                <div class=\"row\">\r\n                    <div class=\"col-md-4 col-sm-4 col-xs-12\">\r\n");
            EndContext();
            BeginContext(2119, 356, true);
            WriteLiteral(@"
                        <div class=""item form-group"">
                            <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""name"">
                                Container No: <span class=""required"">*</span>
                            </label>
                            <div class=""col-md-8"">
                                ");
            EndContext();
            BeginContext(2477, 613, false);
            Write(
#line 90 "D:\AICTMAIN_WEBDOCTERMINAL\AICT_DECRYPTED\AICT-Source\WebdocTerminal\Areas\Import\Views\Shared\_Filter_ContainerLocation.cshtml"
                                  Html.DevExtreme()
                                                .Autocomplete()
                                                .ID("containerlist")
                                                .DataSource(d => d.Mvc()
                                                .Controller("APICalls")
                                                .Area("")
                                                .LoadAction("Getigmocontainers"))
                                                .Placeholder("Type Container No...")
                                                .OnValueChanged("conatiner_changed")

#line default
#line hidden
            );
            EndContext();
            BeginContext(3091, 693, true);
            WriteLiteral(@"
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-4 col-sm-4 col-xs-12"">
                        <div class=""form-group"">
                            <label class=""control-label col-md-4 col-sm-4 col-xs-12"" for=""first-name"">
                                IGM # <span class=""required"">*</span>
                            </label>
                            <div class=""col-md-8 col-sm-8 col-xs-12"" id=""indexdiv"">

                            </div>
                        </div>
                    </div>



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