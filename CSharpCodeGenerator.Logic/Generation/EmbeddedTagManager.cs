//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CommonBase.Extensions.StringExtensions;

namespace CSharpCodeGenerator.Logic.Generation
{
    internal partial class EmbeddedTagManager
    {
        public static string LabelGridColumns => "GridColumns";
        public static string LabelAddFieldSet => "AddFieldSet";
        public static string LabelDeleteFieldSet => "DeleteFieldSet";

        public static bool Handle(Type type, TagInfo startTag, TagInfo endTag, StringBuilder replaceText, string path)
        {
            type.CheckArgument(nameof(type));
            startTag.CheckArgument(nameof(startTag));
            endTag.CheckArgument(nameof(endTag));
            replaceText.CheckArgument(nameof(replaceText));

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

            if (data.ContainsKey(EmbeddedTagReplacer.LabelKey)
                && data[EmbeddedTagReplacer.LabelKey].Equals(LabelGridColumns, StringComparison.CurrentCultureIgnoreCase))
            {
                hasReplaced = true;
                replaceText.Append(BlazorUIGenerator.CreateGridColumns(type).Select(rb => rb.ToString()));
            }
            else if (data.ContainsKey(EmbeddedTagReplacer.LabelKey)
                && data[EmbeddedTagReplacer.LabelKey].Equals(LabelAddFieldSet, StringComparison.CurrentCultureIgnoreCase))
            {
                hasReplaced = true;
                replaceText.Append(BlazorUIGenerator.CreateAddFieldSet(type).Select(rb => rb.ToString()));
            }
            else if (data.ContainsKey(EmbeddedTagReplacer.LabelKey)
                && data[EmbeddedTagReplacer.LabelKey].Equals(LabelDeleteFieldSet, StringComparison.CurrentCultureIgnoreCase))
            {
                hasReplaced = true;
                replaceText.Append(BlazorUIGenerator.CreateDeleteFieldSet(type).Select(rb => rb.ToString()));
            }
            else
            {
                hasReplaced = EmbeddedTagReplacer.DefaultReplaceHandler(startTag, endTag, replaceText, path);
            }
            return hasReplaced;
        }
    }
}
//MdEnd
