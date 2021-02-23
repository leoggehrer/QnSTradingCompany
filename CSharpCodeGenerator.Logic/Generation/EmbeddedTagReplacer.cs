//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static CommonBase.Extensions.StringExtensions;

namespace CSharpCodeGenerator.Logic.Generation
{
    partial class EmbeddedTagReplacer
    {
        public static string FileKey => "file";
        public static string LabelKey => "label";
        public static string ContentKey => "content";

        public static string CommentBegin { get; private set; } = "@*";
        public static string CommentEnd { get; private set; } = "*@";
        public static string EmbeddedTagBegin => $"{CommentBegin}EmbeddedBegin:";
        public static string EmbeddedTagEnd => $"{CommentBegin}EmbeddedEnd:";
        public static string[] EmbeddedTags => new[]
        {
            EmbeddedTagBegin,
            CommentEnd,
            EmbeddedTagEnd,
            CommentEnd
        };
        public static bool DefaultReplaceHandler(TagInfo startTag, TagInfo endTag, StringBuilder replacedText, string embeddedPath)
        {
            var hasReplaced = false;
            var divTag = startTag - endTag;
            var data = new Dictionary<string, string>();

            startTag.InnerText.Split(':').ForeachAction(e =>
            {
                var d = e.Split("=");

                if (d.Length == 2)
                {
                    data.Add(d[0].ToLower(), d[1]);
                }
            });

            if (data.ContainsKey(FileKey))
            {
                var fileName = data[FileKey];
                var extension = Path.GetExtension(fileName);
                var embeddedFile = Directory.GetFiles(embeddedPath, $"*{extension}", SearchOption.AllDirectories)
                                            .SingleOrDefault(f => f.EndsWith($"{fileName}"));

                if (embeddedFile != null && data.ContainsKey(LabelKey))
                {
                    var embeddedText = File.ReadAllText(embeddedFile);
                    var replaceTag = $"{CommentBegin}{data[LabelKey]}{CommentEnd}";
                    var embeddedTag = embeddedText.GetAllTags(replaceTag, replaceTag).FirstOrDefault();

                    if (embeddedTag != null)
                    {
                        hasReplaced = true;
                        replacedText.Append(embeddedTag.InnerText);
                    }
                }
                if (hasReplaced == false
                    && data.ContainsKey(ContentKey)
                    && data[ContentKey].Equals("default", StringComparison.CurrentCultureIgnoreCase))
                {
                    hasReplaced = true;
                    replacedText.Append(divTag.InnerText);
                }
            }
            return hasReplaced;
        }

        public static IEnumerable<string> ReplaceEmbeddedTags(IEnumerable<string> lines,
                                                              string embeddedPath,
                                                              string commentBegin,
                                                              string commentEnd,
                                                              Func<TagInfo, TagInfo, StringBuilder, string, bool> replaceHandler = null,
                                                              bool removeTag = true,
                                                              bool removeDoubleEmptyLines = true)
        {
            lines.CheckArgument(nameof(lines));

            CommentBegin = commentBegin;
            CommentEnd = commentEnd;
            var text = lines.ToText();

            int textStartPos = 0;
            StringBuilder replacedText = new StringBuilder();
            var tags = text.GetAllTags(EmbeddedTags)
                           .OrderBy(e => e.StartTagIndex)
                           .ToArray();

            for (int i = 0; i < tags.Length - 1; i++, i++)
            {
                var startTag = tags[i];
                var endTag = tags[i + 1];
                var divTag = startTag - endTag;

                replacedText.Append(text.Partialstring(textStartPos, startTag.StartTagIndex - 1));
                if (removeTag == false)
                {
                    replacedText.Append(startTag);
                }

                replaceHandler ??= DefaultReplaceHandler;

                var hasReplaced = replaceHandler(startTag, endTag, replacedText, embeddedPath);

                if (hasReplaced == false
                    && removeTag == false)
                {
                    replacedText.Append(divTag.InnerText);
                }
                if (removeTag == false)
                {
                    replacedText.Append(endTag);
                }
                textStartPos = endTag.EndTagIndex + endTag.EndTag.Length;
            }
            if (text.Length > 0)
            {
                if (textStartPos < text.Length - 1)
                {
                    replacedText.Append(text.Partialstring(textStartPos, text.Length - 1));
                }
            }
            var result = new List<string>();

            replacedText.ToString().ToLines().ForeachAction(l =>
            {
                if (removeDoubleEmptyLines)
                {
                    var last = result.LastOrDefault();

                    if (last == null)
                        result.Add(l);
                    else if (last.Equals(string.Empty) == false || l.Equals(string.Empty) == false)
                        result.Add(l);
                }
                else
                {
                    result.Add(l);
                }
            });
            return result;
        }
    }
}
//MdEnd
