using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Tyng.Text.Phonetics
{

    public static class Metaphone
    {
        static readonly RegexRuleCollection _rules;

        static Metaphone()
        {
            _rules = new RegexRuleCollection();

            _rules.Add(new RegexRule(@"[^A-Z]", ""));
            _rules.Add(new RegexRule(@"([ABD-Z])\1", "$1"));
            _rules.Add(new RegexRule(@"^[KGP](?<l>N)|^A(?<l>E)|^W(?<l>R)|^(?<l>W)H", "${l}"));
            _rules.Add(new RegexRule(@"^X", "S"));
            _rules.Add(new RegexRule(@"X", "KS"));
            _rules.Add(new RegexRule(@"MB$", "M"));
            _rules.Add(new RegexRule(@"SH|SIO|SIA|TIA|TIO|CIA|T?CH", "X"));
            _rules.Add(new RegexRule(@"S?C[EIY]", "S"));
            _rules.Add(new RegexRule(@"SCH|C", "K"));
            _rules.Add(new RegexRule(@"DG[EIY]", "J"));
            _rules.Add(new RegexRule(@"D", "T"));
            _rules.Add(new RegexRule(@"GH(?=[BCDFGHJ-NP-TV-Z0]+)", "")); //includes 0 for TH
            _rules.Add(new RegexRule(@"G(?=N(ED)?)", ""));
            _rules.Add(new RegexRule(@"G[EIY]", "J"));
            _rules.Add(new RegexRule(@"G", "K"));
            _rules.Add(new RegexRule(@"(?<=[AEIOU])H(?=[^AEIOU])", ""));
            _rules.Add(new RegexRule(@"PH", "F"));
            _rules.Add(new RegexRule(@"TH", "0"));
            _rules.Add(new RegexRule(@"(?<=C)K", ""));
            _rules.Add(new RegexRule(@"Q", "K"));
            _rules.Add(new RegexRule(@"V", "F"));
            _rules.Add(new RegexRule(@"W(?!=[AEIOU])", ""));
            _rules.Add(new RegexRule(@"Y(?!=[AEIOU])", ""));
            _rules.Add(new RegexRule(@"Z", "S"));
            _rules.Add(new RegexRule(@"(?<=\w)[AEIOU]", ""));
        }

        public static string Get(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            return _rules.Evaluate(name.ToUpper());
        }
    }
}
