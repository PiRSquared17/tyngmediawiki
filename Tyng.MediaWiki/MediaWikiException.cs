using System;
using System.Collections.Generic;
using System.Text;

namespace Tyng.MediaWiki
{
    [Serializable()]
    public class MediaWikiException : Exception
    {
        public MediaWikiException(string message)
            : base(message)
        {
        }
    }

}
