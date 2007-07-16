using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;

namespace Tyng.Google
{
    public static class AjaxSearch
    {
        public static AjaxSearchResult[] Search(AjaxSearchType type, string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) throw new ArgumentNullException("searchString");

            List<AjaxSearchResult> results = new List<AjaxSearchResult>();

            UriBuilder uri = new UriBuilder("http://www.google.com/uds/GwebSearch");

            uri.Query = "callback=CustomCompletion"
                + "&context=0"
                + "&lstkp=0"
                + "&rsz=small"
                + "&hl=en"
                + "&gss=.com"
                + "&sig=81f401ae6c9e33acdac25f3668dba3eb"
                + "&q=" + searchString
                + "&key=" + ConfigurationManager.AppSettings["GoogleAJAXSearchAPIKey"] 
                + "&v=1.0";

            WebRequest req = WebRequest.Create(uri.Uri);
            WebResponse resp = req.GetResponse();

            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                string responseText = sr.ReadToEnd();

                //remove callback
                responseText = responseText.Substring(responseText.IndexOf('{'));
                responseText = responseText.Substring(0, responseText.LastIndexOf("}") + 1);

                JavaScriptObject jso = (JavaScriptObject) JavaScriptConvert.DeserializeObject(responseText);
                JavaScriptArray resultsProperty = (JavaScriptArray)jso["results"];

                foreach (JavaScriptObject resultJso in resultsProperty)
                {
                    results.Add(new AjaxSearchResult((string)resultJso["GsearchResultClass"], (string)resultJso["unescapedUrl"], (string)resultJso["url"], (string)resultJso["visibleUrl"], (string)resultJso["cacheUrl"], (string)resultJso["title"], (string)resultJso["titleNoFormatting"], (string)resultJso["content"]));
                }

                return results.ToArray();
            }
        }
    }

    public sealed class AjaxSearchResult
    {
        string _unescapedUrl;
        string _url;
        string _visibleUrl;
        string _cacheUrl;
        string _title;
        string _titleNoFormatting;
        string _content;

        public AjaxSearchResult(string resultClass, string unescapedUrl, string url, string visibleUrl, string cacheUrl, string title, string titleNoFormatting, string content)
        {
            _unescapedUrl = unescapedUrl;
            _url = url;
            _visibleUrl = visibleUrl;
            _cacheUrl = cacheUrl;
            _title = title;
            _titleNoFormatting = titleNoFormatting;
            _content = content;
        }

        public string Url { get { return _url; } }
        public string Title { get { return _titleNoFormatting; } }
        public string Content { get { return _content; } }
    }

    public enum AjaxSearchType
    {
        Web,
        News,
        Blogs,
        Video,
        Local,
        Books
    };
}
