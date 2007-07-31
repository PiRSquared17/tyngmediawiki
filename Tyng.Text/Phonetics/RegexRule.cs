using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Tyng.Text.Phonetics
{
    public sealed class RegexRule
    {
        Regex _regex;
        string _replace;

        public RegexRule(string regex, string replace)
        {
            _regex = new Regex(regex, RegexOptions.Compiled);
            _replace = replace;
        }

        internal string Replace(string input)
        {
            return _regex.Replace(input, _replace);
        }
    }

    public sealed class RegexRuleCollection : List<RegexRule>
    {
        public string Evaluate(string input)
        {
            foreach (RegexRule r in this)
                input = r.Replace(input);

            return input;
        }
    }
}
