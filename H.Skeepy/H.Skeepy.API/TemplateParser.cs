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
        private readonly string template;

        public TemplateParser(string template)
        {
            this.template = template ?? string.Empty;
        }

        public string Compile(params (string, string)[] payload)
        {
            var result = template;
            foreach(var tag in payload)
            {
                result = result.Replace($"@{{{tag.Item1}}}", tag.Item2);
            }
            result = new Regex(@"(@\{[a-zA-Z0-9]+\})").Replace(result, string.Empty);
            return result;
        }
    }
}
