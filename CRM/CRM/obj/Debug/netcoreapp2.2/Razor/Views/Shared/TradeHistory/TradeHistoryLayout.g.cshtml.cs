#pragma checksum "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "07a67d0e48ed0dd2a6465a6188150b6a311f6df2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_TradeHistory_TradeHistoryLayout), @"mvc.1.0.view", @"/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml", typeof(AspNetCore.Views_Shared_TradeHistory_TradeHistoryLayout))]
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
#line 1 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/_ViewImports.cshtml"
using CRM;

#line default
#line hidden
#line 2 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/_ViewImports.cshtml"
using CRM.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"07a67d0e48ed0dd2a6465a6188150b6a311f6df2", @"/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a3b16a7ccb85590fca9010f5f3fac5f1b6b0aa4c", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_TradeHistory_TradeHistoryLayout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 2 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
  
    Layout = null;

#line default
#line hidden
            BeginContext(25, 25, true);
            WriteLiteral("\n<!DOCTYPE html>\n\n<html>\n");
            EndContext();
            BeginContext(50, 909, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "07a67d0e48ed0dd2a6465a6188150b6a311f6df24426", async() => {
                BeginContext(56, 896, true);
                WriteLiteral(@"
    <meta name=""viewport"" content=""width=device-width"" />
    <title>TradeHistoryLayout</title>

    <style type=""text/css"">
        .filter_element {
            vertical-align: top;
            margin-right: 5px;
            display: flex;
            flex-wrap: wrap;
        }
       
        .filter_input {
            border: 2px solid #a76be3 !important;
            background-color: transparent;
            height:50px !important; 
            width:150px !important;
        }

        .filter_button {
            height: 50px !important;
            width: 305px !important;
        }

        .filter_panel {
            display: flex;
            align-items: center;
            align-content: center;
            justify-content: center;
            width: 100%;
            padding-top:20px;
            padding-bottom:20px;
        }


    </style>

");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(959, 1, true);
            WriteLiteral("\n");
            EndContext();
            BeginContext(960, 1720, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "07a67d0e48ed0dd2a6465a6188150b6a311f6df26486", async() => {
                BeginContext(966, 21, true);
                WriteLiteral("\r\n    <div>\r\n        ");
                EndContext();
                BeginContext(988, 22, false);
#line 49 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
   Write(Html.Partial("Navbar"));

#line default
#line hidden
                EndContext();
                BeginContext(1010, 20, true);
                WriteLiteral("\r\n    </div>\r\n\r\n    ");
                EndContext();
                BeginContext(1030, 1469, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "07a67d0e48ed0dd2a6465a6188150b6a311f6df27276", async() => {
                    BeginContext(1050, 173, true);
                    WriteLiteral("\r\n        <div class=\"filter_panel\">\r\n\r\n            <div class=\"filter_element\">\r\n                <select class=\"form-control filter_input\" name=\"coin\" id=\"coin_selector\">\r\n");
                    EndContext();
#line 57 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
                     foreach (var item in CRM.Models.DropDownFields.Coins)
                    {

#line default
#line hidden
                    BeginContext(1321, 16, true);
                    WriteLiteral("                ");
                    EndContext();
                    BeginContext(1337, 47, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "07a67d0e48ed0dd2a6465a6188150b6a311f6df28244", async() => {
                        BeginContext(1366, 9, false);
#line 59 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
                                       Write(item.Name);

#line default
#line hidden
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    BeginWriteTagHelperAttribute();
#line 59 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
                   WriteLiteral(item.Value);

#line default
#line hidden
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
                    BeginContext(1384, 2, true);
                    WriteLiteral("\r\n");
                    EndContext();
#line 60 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
                    }

#line default
#line hidden
                    BeginContext(1409, 189, true);
                    WriteLiteral("                </select>\r\n            </div>\r\n\r\n            <div class=\"filter_element\">\r\n                <select class=\"form-control filter_input\" name=\"accounts\" id=\"excange_selector\">\r\n");
                    EndContext();
#line 66 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
                     foreach (var item in CRM.Models.DropDownFields.Accounts)
                    {

#line default
#line hidden
                    BeginContext(1699, 16, true);
                    WriteLiteral("                ");
                    EndContext();
                    BeginContext(1715, 47, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "07a67d0e48ed0dd2a6465a6188150b6a311f6df211248", async() => {
                        BeginContext(1744, 9, false);
#line 68 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
                                       Write(item.Name);

#line default
#line hidden
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    BeginWriteTagHelperAttribute();
#line 68 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
                   WriteLiteral(item.Value);

#line default
#line hidden
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
                    BeginContext(1762, 2, true);
                    WriteLiteral("\r\n");
                    EndContext();
#line 69 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
                    }

#line default
#line hidden
                    BeginContext(1787, 705, true);
                    WriteLiteral(@"                </select>
            </div>


            <div class=""filter_element"">
                <input type=""date"" class=""form-control filter_input"" id=""startDate"" name=""startDate"" min=""2019-04-05"" />
            </div>

            <div class=""filter_element"">
                <input type=""date"" class=""form-control filter_input"" id=""endDate"" name=""endDate"" min=""2019-04-01"" />
            </div>

            <div class=""filter_element"">
                <button type=""submit"" id=""TradeHistory"" class=""btn filter_button btn-primary"" formaction=""../TH/TradeHistory"" formmethod=""post"">
                    История
                </button>
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
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2499, 159, true);
                WriteLiteral("\r\n\r\n    <script type=\"text/javascript\">\r\n        $(\"#TradeHistory\").click(function () {\r\n            setDataToCoockie()\r\n        });\r\n    </script>\r\n\r\n\r\n\r\n    ");
                EndContext();
                BeginContext(2659, 12, false);
#line 99 "/Users/arseniyvoytehovskiy/Projects/CRMCore/CRM/CRM/Views/Shared/TradeHistory/TradeHistoryLayout.cshtml"
Write(RenderBody());

#line default
#line hidden
                EndContext();
                BeginContext(2671, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2680, 9, true);
            WriteLiteral("\n</html>\n");
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
