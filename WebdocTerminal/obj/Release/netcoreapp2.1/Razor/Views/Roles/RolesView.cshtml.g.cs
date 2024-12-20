#pragma checksum "D:\AICT_GIT_PROJECTS\WebDoc\WebdocTerminal\Views\Roles\RolesView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "ef0899b02e751a35fa3b56110d70b2f90d678aff03b43e2a837401d66c320d42"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Roles_RolesView), @"mvc.1.0.view", @"/Views/Roles/RolesView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Roles/RolesView.cshtml", typeof(AspNetCore.Views_Roles_RolesView))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"ef0899b02e751a35fa3b56110d70b2f90d678aff03b43e2a837401d66c320d42", @"/Views/Roles/RolesView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d9c22d530c8a142b4db368d33b9ab0d7e8720da4515d4bce483967cecc7394c6", @"/Views/_ViewImports.cshtml")]
    public class Views_Roles_RolesView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 3425, true);
            WriteLiteral(@"<script>
    var roles = [];


    //$(function () {

      


   // });

    function showToast(message, icon) {

        $.toast({
            heading: message,
            showHideTransition: 'slide',
            position: 'bottom-right',
            icon: icon
        });
    }

       function checkColumn(e, field) {

        if (e.parentType === ""dataRow"" && e.dataField !== field) {
            return;
        }

        e.editorOptions.disabled = true;
    }

    function hideMenifestedColumns(e) {


        if (PermissionInputs.find(x => x.fieldName == ""Name"" && x.isChecked == false)) {

            checkColumn(e, ""name"");
        }
    
    }

    function loadGrid() {


        $.get('/Roles/GetRole', function (data) {

         
            roles = data;
               console.log(roles)



            var dataGrid = $(""#roles"").dxDataGrid({
                dataSource: roles,
                keyExpr: ""id"",
                wordWrapEnabled: true,");
            WriteLiteral(@"
                showBorders: true,
                showBorders: true,
                columnAutoWidth: true,
                
                paging: {
                    pageSize: 10
                },

                searchPanel: {
                    visible: true,
                    width: 240,
                    placeholder: ""Search...""

                },
                editing: {
                    mode: ""row"", 
                    allowAdding: true
                },
                columns: [
                    ""name""
                ],
                onEditorPreparing: function (e) {
                hideMenifestedColumns(e);
            },
                onEditingStart: function (e) {
                },
                onInitNewRow: function (e) {
                },
                onRowInserting: function (e) {
                    console.log(e)
                },
                onRowInserted: function (e) {
                 
                    var name ");
            WriteLiteral(@"= e.data.name;
                    console.log(name)
                    $.post('/Roles/CreateRoles?name='+name, function (data) {

                        if (data.error == true) {
                            showToast(data.message, ""error"");
                        }
                        else {
                            showToast(data.message, ""success"");
                        }
                        loadGrid();

                    });
                } 
            }).dxDataGrid(""instance"");


            $('.dx-datagrid-search-panel').removeClass('dx-state-disabled');
            $('div.dx-texteditor-input-container > input.dx-texteditor-input').removeAttr('disabled');

        });
    }
     
    function formfiled() {
        loadGrid();
    } 
 
      

</script>
<div class=""right_col"" role=""main"">
    <div class=""page-title"">
        <div class=""title_left"">
            <h3>
                Roles
            </h3>
        </div>
        <div class=""title_");
            WriteLiteral(@"right"">
        </div>
    </div>
    <div class=""clearfix""></div>
    <div class=""row"">
        <div class=""col-md-12 col-sm-12 col-xs-12"">
            <div class=""x_panel"">
                <div class=""x_content"">
                    <div id=""roles""></div>

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
