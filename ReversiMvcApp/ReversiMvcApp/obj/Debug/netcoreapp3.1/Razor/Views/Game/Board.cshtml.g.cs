#pragma checksum "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ae38db847f2b4831c28f5c4283ed79853207181c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Game_Board), @"mvc.1.0.view", @"/Views/Game/Board.cshtml")]
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
#line 1 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\_ViewImports.cshtml"
using ReversiMvcApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\_ViewImports.cshtml"
using ReversiMvcApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
using ReversiRestApi.Enums;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae38db847f2b4831c28f5c4283ed79853207181c", @"/Views/Game/Board.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e1810c03914d26bfabd007bc85cf18329314c121", @"/Views/_ViewImports.cshtml")]
    public class Views_Game_Board : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ReversiRestApi.Models.ApiGame>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Start", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SurrenderGame", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Waiting", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/signalr/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<link");
            BeginWriteAttribute("href", " href=\"", 141, "\"", 183, 1);
#nullable restore
#line 7 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
WriteAttributeValue("", 148, Url.Content("~/css/style.min.css"), 148, 35, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" rel=\"stylesheet\" type=\"text/css\" />\r\n\r\n");
#nullable restore
#line 9 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
  
    ViewData["Title"] = "Board";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae38db847f2b4831c28f5c4283ed79853207181c6679", async() => {
                WriteLiteral("\r\n    <H1>Game hiero!</H1>\r\n");
#nullable restore
#line 15 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
     if (Model.Status.Equals("Waiting"))
    {

#line default
#line hidden
#nullable disable
                WriteLiteral("        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae38db847f2b4831c28f5c4283ed79853207181c7266", async() => {
                    WriteLiteral("Start Game");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-token", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 17 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
                                   WriteLiteral(Model.Token);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["token"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-token", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["token"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 18 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
    }
    else if (Model.Status.Equals("Running"))
    {

#line default
#line hidden
#nullable disable
                WriteLiteral("        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae38db847f2b4831c28f5c4283ed79853207181c9974", async() => {
                    WriteLiteral("Surrender");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-token", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 21 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
                                           WriteLiteral(Model.Token);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["token"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-token", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["token"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 22 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
    }
    else if (Model.Status.Equals("Finished"))
    {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <p>Game has ended</p>\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae38db847f2b4831c28f5c4283ed79853207181c12723", async() => {
                    WriteLiteral("Leave the game");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 27 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
    }

#line default
#line hidden
#nullable disable
                WriteLiteral("    <p>");
#nullable restore
#line 28 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
  Write(Model.Status);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n");
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
            WriteLiteral("\r\n\r\n<div id=\"board\"></div>\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 888, "\"", 924, 1);
#nullable restore
#line 34 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
WriteAttributeValue("", 894, Url.Content("~/js/vendor.js"), 894, 30, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 948, "\"", 987, 1);
#nullable restore
#line 35 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
WriteAttributeValue("", 954, Url.Content("~/js/templates.js"), 954, 33, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 1011, "\"", 1044, 1);
#nullable restore
#line 36 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
WriteAttributeValue("", 1017, Url.Content("~/js/app.js"), 1017, 27, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"text/javascript\"></script>\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae38db847f2b4831c28f5c4283ed79853207181c16823", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n    <script type=\"text/javascript\">\r\n        Game.init(\"production\", \"");
#nullable restore
#line 40 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
                            Write(Model.Token);

#line default
#line hidden
#nullable disable
                WriteLiteral("\", \"");
#nullable restore
#line 40 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
                                            Write(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

#line default
#line hidden
#nullable disable
                WriteLiteral(@""");
    </script>

    <script type=""text/javascript"">

        var connection = new signalR.HubConnectionBuilder().withUrl(""/gameHub"").build();

        async function start() {
            try {
                await connection.start();
                await connection.invoke(""JoinGroup"", """);
#nullable restore
#line 50 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
                                                 Write(Model.Token);

#line default
#line hidden
#nullable disable
                WriteLiteral(@""");
            } catch (err) {
                setTimeout(start, 5000);
            }
        }
        connection.onclose(start);
        start();

        connection.on(""ReceiveMessage"",
            function(gameToken, message) {
                Game.update();
            });

        $('#board').on('updateBoard', async function() {
            await connection.invoke(""SendMessageToGroup"", """);
#nullable restore
#line 64 "C:\Users\Sietse Bijlsma\Desktop\SE 2 projects\ReversiBackend\ReversiBackend\ReversiMvcApp\ReversiMvcApp\Views\Game\Board.cshtml"
                                                      Write(Model.Token);

#line default
#line hidden
#nullable disable
                WriteLiteral("\", \"test\");\r\n        });\r\n\r\n    </script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ReversiRestApi.Models.ApiGame> Html { get; private set; }
    }
}
#pragma warning restore 1591
