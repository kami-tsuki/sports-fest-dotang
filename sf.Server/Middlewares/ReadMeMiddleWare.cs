using static System.IO.File;
using static Markdig.Markdown;
using Markdig.SyntaxHighlighting;
using Markdig.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Markdig;
using Markdig.Syntax;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sf.Server.Middlewares;

public class ReadmeMiddleware(RequestDelegate next, string markdownPath, string requestPath, string pageTitle = "Documentation", string sidebarTitle = "Documentation")
{
    private static readonly Regex HeadingPattern = new(@"^(#{1,6})\s+(.*)", RegexOptions.Compiled);

    private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
                                                       .UseAdvancedExtensions()
                                                       .UseBootstrap()
                                                       .UseAutoLinks()
                                                       .UseAbbreviations()
                                                       .UseAutoIdentifiers()
                                                       .UseDefinitionLists()
                                                       .UseEmphasisExtras()
                                                       .UseDiagrams()
                                                       .UseGridTables()
                                                       .UseSoftlineBreakAsHardlineBreak()
                                                       .UseReferralLinks()
                                                       .UseSyntaxHighlighting()
                                                       .UseListExtras()
                                                       .UsePreciseSourceLocation()
                                                       .Build();

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments(requestPath))
        {
            var markdown = await ReadAllTextAsync(markdownPath);

            if (string.IsNullOrWhiteSpace(markdown))
            {
                Log.Warning($"{Path.GetFileName(markdownPath)} not found");
                await next(context);
                return;
            }

            Log.Information($"{Path.GetFileName(markdownPath)} found, {markdown.Length} bytes");

            var htmlContent = ToHtml(markdown, Pipeline);
            var sidebarContent = GenerateSidebar(markdown, sidebarTitle);

            var htmlTemplate = GenerateHtmlTemplate(htmlContent, sidebarContent, pageTitle);

            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(htmlTemplate);
        }
        else
        {
            await next(context);
        }
    }

    private string GenerateHtmlTemplate(string content, string sidebar, string title) =>
        $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>{title}</title>
                <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css' rel='stylesheet'>
                <link href='https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.7.0/styles/default.min.css' rel='stylesheet'>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        display: flex;
                    }}
                    .sidebar {{
                        width: 250px;
                        background-color: #343a40;
                        padding: 10px;
                        position: fixed;
                        height: 100%;
                        overflow-y: auto;
                    }}
                    .sidebar a {{
                        color: #fff;
                        text-decoration: none;
                        display: block;
                        padding: 10px;
                    }}
                    .sidebar a:hover {{
                        background-color: #495057;
                    }}
                    .content {{
                        margin-left: 260px;
                        padding: 20px;
                        flex-grow: 1;
                    }}
                    pre {{
                        background-color: #333;
                        color: #fff;
                        padding: 10px;
                        border-radius: 5px;
                        overflow-x: auto;
                    }}
                    code {{
                        background-color: #f4f4f4;
                        padding: 2px 4px;
                        border-radius: 4px;
                    }}
                    .collapsible {{
                        background-color: #f1f1f1;
                        color: #444;
                        cursor: pointer;
                        padding: 10px;
                        border: none;
                        text-align: left;
                        outline: none;
                        font-size: 15px;
                        width: 100%;
                        margin-top: 10px;
                    }}
                    .active, .collapsible:hover {{
                        background-color: #ccc;
                    }}
                    .collapsible-content {{
                        padding: 0 18px;
                        display: none;
                        overflow: hidden;
                        background-color: #f9f9f9;
                        margin-bottom: 10px;
                        border-left: 4px solid #007bff;
                    }}
                </style>
            </head>
            <body>
                <div class='sidebar'>
                    {sidebar}
                </div>
                <div class='content'>
                    {content}
                </div>
                <script src='https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.7.0/highlight.min.js'></script>
                <script src='https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js'></script>
                <script>
                    document.querySelectorAll('pre code').forEach((block) => {{
                        hljs.highlightElement(block);
                    }});

                    document.querySelectorAll('.collapsible').forEach(button => {{
                        button.addEventListener('click', () => {{
                            button.classList.toggle('active');
                            var content = button.nextElementSibling;
                            content.style.display = content.style.display === 'block' ? 'none' : 'block';
                        }});
                    }});

                    document.querySelectorAll('.sidebar a').forEach(anchor => {{
                        anchor.addEventListener('click', function(e) {{
                            e.preventDefault();
                            const href = this.getAttribute('href');
                            const target = document.querySelector(href);
                            if (target) {{
                                window.scrollTo({{
                                    top: target.offsetTop,
                                    behavior: 'smooth'
                                }});
                            }}
                        }});
                    }});
                </script>
            </body>
            </html>";

    private string GenerateSidebar(string markdown, string title)
    {
        var lines = markdown.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);
        var sidebarContent = new StringBuilder();
        sidebarContent.AppendLine($"<h2>{title}</h2>");

        // Collapsible Menu for External Links
        sidebarContent.AppendLine(
            GenerateCollapsibleMenu(
                "External Links",
                new()
                {
                    { "Swagger UI", "/swagger/index.html" },
                    { "Project GitHub", "https://github.com/kami-tsuki/sports-fest-dotang" },
                    { "README GitHub", "https://github.com/kami-tsuki/sports-fest-dotang/sf.Server/README.md" },
                    { "Database", "https://phpmyadmin.tsuki.wtf/index.php" }
                }));

        // Markdown Headings
        foreach (var line in lines)
        {
            var match = HeadingPattern.Match(line);
            if (!match.Success) continue;
            var headingLevel = match.Groups[1].Value.Length;
            var headingText = match.Groups[2].Value;
            var anchor = headingText.Replace(" ", "-").ToLower();
            sidebarContent.AppendLine($"<a href='#{anchor}' class='h{headingLevel}'>{headingText}</a>");
        }

        return sidebarContent.ToString();
    }

    private string GenerateCollapsibleMenu(string title, Dictionary<string, string> links)
    {
        var content = new StringBuilder();
        content.AppendLine(
            $@"
            <button class='collapsible'>{title}</button>
            <div class='collapsible-content'>");

        foreach (var (name, url) in links)
        {
            content.AppendLine($"<a href='{url}'>{name}</a>");
        }

        content.AppendLine("</div>");
        return content.ToString();
    }
}