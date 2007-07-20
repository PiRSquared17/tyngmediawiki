using System;
using System.Collections.Generic;
using System.Text;

namespace Tyng.MediaWiki
{
    public static class ContentHelper
    {
        public const string UnorderedListPrefix = "*";
        public const string OrderedListPrefix = "#";

        public static string ToUnorderedList(params string[] items)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (items.Length == 0) return string.Empty;

            return UnorderedListPrefix + string.Join("\n" + UnorderedListPrefix, items);
        }

        public static string ToOrderedList(params string[] items)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (items.Length == 0) return string.Empty;

            return OrderedListPrefix + string.Join("\n" + OrderedListPrefix, items);
        }
    }
}
