#pragma checksum "C:\Projects\PMS\PMS\PMS\Views\Commerce\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "03886d466d81963113deb09b6296644d70d53305"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Commerce_Index), @"mvc.1.0.view", @"/Views/Commerce/Index.cshtml")]
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
#line 1 "C:\Projects\PMS\PMS\PMS\Views\_ViewImports.cshtml"
using PMS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Projects\PMS\PMS\PMS\Views\_ViewImports.cshtml"
using PMS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03886d466d81963113deb09b6296644d70d53305", @"/Views/Commerce/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7646a9d3f6dbc3a97607785fcc1715c04f59c17b", @"/Views/_ViewImports.cshtml")]
    public class Views_Commerce_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<style>
    .commerce-tab ul {
        border-bottom: 3px solid #999999;
    }

    .commerce-tab li > a {
        color: #00ffff;
        font-size: 18px;
        font-weight: bold;
        letter-spacing: 0.5px;
        border: 0px;
    }
        .commerce-tab li > a:hover, .commerce-tab li.active > a, .commerce-tab li.active:hover > a {
            color: #1f1f1f;
            background-color: #00ffff;
            border: 0px;
            letter-spacing: 0.5px;
            box-shadow: 0px -5px 5px 5px rgba(31,31,31,0.5);
            -webkit-box-shadow: 0px -5px 5px 5px rgba(31,31,31,0.5);
            -moz-box-shadow: 0px -5px 5px 5px rgba(31,31,31,0.5);
        }
</style>

<div ng-app=""commerceModule"" ng-controller=""commerceController"">
    <br />
    <div>
        <uib-tabset class=""commerce-tab"" active=""activetab"">
            <uib-tab index=""0"" id=""workOrderCommerceTab"" heading=""Комерција"" ui-sref=""workOrderCommerce""></uib-tab>
            <uib-tab index=""1"" id=""workOrderEngi");
            WriteLiteral("neerTab\" heading=\"Инженер\" ui-sref=\"workOrderEngineer\"></uib-tab>\r\n        </uib-tabset>\r\n    </div>\r\n    <br />\r\n    <div ui-view></div>\r\n\r\n</div>\r\n");
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
