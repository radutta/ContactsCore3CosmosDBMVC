#pragma checksum "C:\RD\ContactsCore3CosmosDBMVC\Views\Home\Vault.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "201795bea5d0daacf45cf4b781cf19c96736092b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Vault), @"mvc.1.0.view", @"/Views/Home/Vault.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\RD\ContactsCore3CosmosDBMVC\Views\_ViewImports.cshtml"
using ContactsCore3CosmosDBMVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\RD\ContactsCore3CosmosDBMVC\Views\_ViewImports.cshtml"
using ContactsCore3CosmosDBMVC.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\RD\ContactsCore3CosmosDBMVC\Views\_ViewImports.cshtml"
using ContactsCore3CosmosDBMVC.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\RD\ContactsCore3CosmosDBMVC\Views\_ViewImports.cshtml"
using Microsoft.ApplicationInsights.AspNetCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"201795bea5d0daacf45cf4b781cf19c96736092b", @"/Views/Home/Vault.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b24e7df749fcd0c04bc2d1ca62afb6ee1ade3cd3", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Vault : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IDictionary<string, string>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\RD\ContactsCore3CosmosDBMVC\Views\Home\Vault.cshtml"
  
  ViewData["Title"] = "VaultResults";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>VaultResults</h1>\r\n<table class=\"table table-bordered table-responsive-sm table-hover table-striped col-6\">\r\n  <tr>\r\n    <th>Id</th>\r\n    <th>Value</th>\r\n  </tr>\r\n");
#nullable restore
#line 12 "C:\RD\ContactsCore3CosmosDBMVC\Views\Home\Vault.cshtml"
   foreach (var item in Model)
  {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n      <td>");
#nullable restore
#line 15 "C:\RD\ContactsCore3CosmosDBMVC\Views\Home\Vault.cshtml"
     Write(item.Key);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n      <td>");
#nullable restore
#line 16 "C:\RD\ContactsCore3CosmosDBMVC\Views\Home\Vault.cshtml"
     Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n");
#nullable restore
#line 18 "C:\RD\ContactsCore3CosmosDBMVC\Views\Home\Vault.cshtml"
  }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</table>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.ApplicationInsights.AspNetCore.JavaScriptSnippet JavaScriptSnippet { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IDictionary<string, string>> Html { get; private set; }
    }
}
#pragma warning restore 1591
