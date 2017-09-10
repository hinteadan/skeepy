using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace H.Skeepy.API
{
    public class TemplateParser
    {
        private class Part
        {
            private Part(string text, bool isTag)
            {
                Text = text;
                IsTag = isTag;
            }

            public readonly string Text;
            public readonly bool IsTag;

            public static Part Content(string text)
            {
                return new Part(text, false);
            }

            public static Part Tag(string name)
            {
                return new Part(name, true);
            }
        }

        private readonly string template;
        private readonly Queue<Part> parts;

        public TemplateParser(string template)
        {
            this.template = template ?? string.Empty;
            parts = SplitTemplate(this.template);
        }

        private static Queue<Part> SplitTemplate(string template)
        {
            var queue = new Queue<Part>();
            var remains = template;
            while (!string.IsNullOrEmpty(remains))
            {
                var a = remains.IndexOf("@{");
                if(a < 0)
                {
                    queue.Enqueue(Part.Content(remains));
                    break;
                }
                var b = remains.IndexOf('}', a + 2);
                if(b < 0)
                {
                    throw new InvalidOperationException($"Invalid tag in template, has no closing bracket. Tag starts at offset ${a}");
                }
                queue.Enqueue(Part.Content(remains.Substring(0, a)));
                queue.Enqueue(Part.Tag(remains.Substring(a+2, b-a-2)));
                remains = remains.Substring(b+1);
            }
            return queue;
        }

        public string Compile(params (string, string)[] payload)
        {
            var result = new StringBuilder();

            foreach(var part in parts)
            {
                result.Append(part.IsTag ? payload.SingleOrDefault(x => x.Item1 == part.Text).Item2 ?? string.Empty : part.Text);
            }

            return result.ToString();
        }
    }
}
