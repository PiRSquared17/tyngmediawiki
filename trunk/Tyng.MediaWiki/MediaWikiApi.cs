using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Web;
using Tyng.MediaWiki.Configuration;

namespace Tyng.MediaWiki
{
    
    public sealed class MediaWikiApi
    {
        internal const string RedirectRegex = @"^\s*#REDIRECT \[\[(?<title>[^\]\|]?[^\]]*)\]\]";
        internal const string CategoryRegex = @"\[\[Category:(?<title>[^\]\|]?[^\]]*)\]\]";
        internal const string FormContentType = "application/x-www-form-urlencoded";
        internal const string HiddenInputRegex = "type=['\"]hidden['\"] value=['\"](?<value>[^'\"]*?)['\"] name=['\"]{0}['\"]";
        internal const string HiddenInputRegexAlt = "type=['\"]hidden['\"] name=['\"]{0}['\"] value=['\"](?<value>[^'\"]*?)['\"]";
        internal const string EditQuery = "title={0}&action=edit";

        string _token;
        string _username;
        int _userId;
        bool _postDataAlways;
        bool _isAnonymous;
        string _sessionId;

        private MediaWikiApi()
        {
            _isAnonymous = true;
        }

        private MediaWikiApi(string user, string password) 
        {
            _isAnonymous = false;
            Login(user, password, null);
        }

        public static string UserAgent
        {
            get
            {
                Assembly lib = Assembly.GetExecutingAssembly();
                Assembly entry = Assembly.GetEntryAssembly();

                string libName = lib.GetName().Name;
                string entryName = entry.GetName().Name;

                object[] title = lib.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if(title != null && title.Length > 0) libName = ((AssemblyTitleAttribute)title[0]).Title;

                title = entry.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if(title != null && title.Length > 0) entryName = ((AssemblyTitleAttribute)title[0]).Title;

                return string.Format(Config.UserAgentFormat, libName, lib.GetName().Version, entryName, entry.GetName().Version);
            }
        }

        private static MediaWikiApi _anonymous;
        private static Dictionary<string, MediaWikiApi> _apiCache = new Dictionary<string,MediaWikiApi>();

        //TODO: threading locks...
        public static MediaWikiApi GetMediaWikiApi(string loginName)
        {
            if (loginName == null) return Anonymous;

            if (_apiCache.ContainsKey(loginName))
            {
                return _apiCache[loginName];
            }
            else
            {
                ApiLoginSettings login = Config.Logins.GetLogin(loginName);
                if (login == null) throw new System.Configuration.ConfigurationErrorsException("Login '" + loginName + "' not found in logins element.");
                MediaWikiApi api = new MediaWikiApi(login.LoginName, login.Password);
                _apiCache.Add(login.LoginName, api);
                return api;                
            }
        }

        public static MediaWikiApi GetDefault()
        {
            return GetMediaWikiApi(Config.DefaultLoginName);
        }

        public static MediaWikiApi Anonymous
        {
            get
            {
                if (_anonymous == null)
                    _anonymous = new MediaWikiApi();

                return _anonymous;
            }
        }

        public bool AlwaysPostData
        {
            get
            {
                return _postDataAlways;
            }
            set
            {
                _postDataAlways = value;
            }
        }

        public bool IsAnonymous
        {
            get
            {
                return _isAnonymous;
            }
        }

        private HttpWebRequest PrepareRequest(string method, string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = false;
            request.UserAgent = UserAgent;
            request.Method = method;

            if (method == "POST") request.ContentType = FormContentType;

            return request;
        }

        private void Login(string user, string password, string domain)
        {
            if (_isAnonymous) throw new InvalidOperationException("Cannot login from anonymous session.");

            string[] parameters;

            user = Uri.EscapeDataString(user);
            password = Uri.EscapeDataString(password);

            if (string.IsNullOrEmpty(domain))
                parameters = new string[] { "lgname=" + user, "lgpassword=" + password };
            else
            {
                domain = Uri.EscapeDataString(domain);
                parameters = new string[] { "lgname=" + user, "lgpassword=" + password, "lgdomain=" + domain };
            }

            XmlDocument xml = RequestApi("login", true, parameters);

            XmlElement result = (XmlElement) xml.SelectSingleNode("/api/login");
            
            string resultValue = result.Attributes["result"].Value;

            switch(resultValue)
            {
                case "Success":
                    _userId = int.Parse(result.Attributes["lguserid"].Value);
                    _token = result.Attributes["lgtoken"].Value;
                    _username = result.Attributes["lgusername"].Value;
                    break;
                case "NoName":
                    throw new LoginException("Empty name.", resultValue);
                case "Illegal":
                    throw new LoginException("Illegal login.", resultValue);
                case "WrongPluginPass":
                    throw new LoginException("Incorrect plugin password.", resultValue);
                case "NotExists":
                    throw new LoginException("User does not exist.", resultValue);
                case "WrongPass":
                    throw new LoginException("Incorrect password.", resultValue);
                case "EmptyPass":
                    throw new LoginException("Empty password.", resultValue);
                default:
                    throw new LoginException("An unexpected login error occurred.", resultValue);
            }
        }

        private string GetHiddenInputValue(string name, string html)
        {
            Match m = Regex.Match(html, string.Format(HiddenInputRegex, name));
            if (!m.Success) m = Regex.Match(html, string.Format(HiddenInputRegexAlt, name));
            if (!m.Success) return null;

            return m.Groups["value"].Value;
        }

        internal XmlDocument EditPage(string title, string newContent, string editSummary, bool minorEdit)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(newContent)) throw new ArgumentNullException("newContent");
            if (string.IsNullOrEmpty(editSummary)) throw new ArgumentNullException("editSummary");

            string url = CombineUrlPath(Config.Server, Config.ScriptPath, Config.ScriptName + "?" + string.Format(EditQuery, Uri.EscapeDataString(title)));

            //TODO: move to API once its implemented there
            HttpWebRequest request = PrepareRequest("GET", url);

            CookieCollection cookies = new CookieCollection();
            cookies.Add(new Cookie("enwikiUserID", _userId.ToString(), "/", "en.wikipedia.org"));
            cookies.Add(new Cookie("enwikiToken", _token, "/", "en.wikipedia.org"));
            cookies.Add(new Cookie("enwikiUserName", _username, "/", "en.wikipedia.org"));
            if (!string.IsNullOrEmpty(_sessionId)) cookies.Add(new Cookie("enwiki_session", _sessionId, "/", "en.wikipedia.org"));

            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookies);

            Sleep("query");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
                throw new EditException(response);

            if(response.Cookies["enwiki_session"] != null)
                _sessionId = response.Cookies["enwiki_session"].Value;

            string editToken = null;
            string editTime = null;
            string startTime = null;

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                string html = sr.ReadToEnd();
                editToken = GetHiddenInputValue("wpEditToken", html);
                editTime = GetHiddenInputValue("wpEdittime", html);
                startTime = GetHiddenInputValue("wpStarttime", html);
            }

            request = PrepareRequest("POST", url);

            cookies = new CookieCollection();
            cookies.Add(new Cookie("enwikiUserID", _userId.ToString(), "/", "en.wikipedia.org"));
            cookies.Add(new Cookie("enwikiToken", _token, "/", "en.wikipedia.org"));
            cookies.Add(new Cookie("enwikiUserName", _username, "/", "en.wikipedia.org"));
            cookies.Add(new Cookie("enwiki_session", _sessionId, "/", "en.wikipedia.org"));

            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookies);

            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write("{0}={1}", "wpEdittime", Uri.EscapeDataString(editTime));
                sw.Write("&{0}={1}", "wpStarttime", Uri.EscapeDataString(startTime));
                sw.Write("&{0}={1}", "wpEditToken", Uri.EscapeDataString(editToken));
                sw.Write("&{0}={1}", "wpSave", "Save Page");

                if (minorEdit) sw.Write("&{0}={1}", "wpMinoredit", "1");

                sw.Write("&{0}={1}", "wpTextbox1", Uri.EscapeDataString(newContent));
                sw.Write("&{0}={1}", "wpSummary", Uri.EscapeDataString(editSummary));
            }

            Sleep("edit");

            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                //probably successful, check url
                string redirect = response.Headers[HttpResponseHeader.Location];
            }
            else
            {
                throw new EditException(response);
            }

            XmlDocument xml;
            XmlElement page;
            do
            {
                //TODO: once the edit api is implemented and it returns xml, can get rid of this...
                xml = Page.FetchPageXml(this, "titles=" + Uri.EscapeDataString(title));

                //poll it until page is ready...
                page = (XmlElement)xml.SelectSingleNode("/api/query/pages/page");

            } while (page == null || page.Attributes["missing"] != null);

            return xml;
        }

        internal XmlDocument RequestApi(string action, params string[] parameters)
        {
            return RequestApi(action, _postDataAlways, parameters);
        }

        internal XmlDocument RequestApi(string action, bool postData, params string[] parameters)
        {
            UriBuilder uri = new UriBuilder(CombineUrlPath(Config.Server, Config.ScriptPath, Config.ApiName));

            StringBuilder sb = new StringBuilder("format=xml&action=");
            
            sb.Append(action);

            for(int i = 0; i < parameters.Length; i++)
            {
                sb.Append("&");
                sb.Append(parameters[i]);
            }

            if (!_isAnonymous && !string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_token))
            {
                sb.AppendFormat("&lgtoken={0}&lgusername={1}&lguserid={2}", _token, _username, _userId);
            }

            if (!postData)  uri.Query = sb.ToString();

            HttpWebRequest request = PrepareRequest(postData ? "POST" : "GET", uri.ToString());
            
            if (postData)
            {
                using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
                {
                    sw.Write(sb.ToString());
                }
            }

            Sleep(action);

            WebResponse response = request.GetResponse();
            
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                string fullText = sr.ReadToEnd();
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(fullText);
                return xml;
            }
        }

        private static string CombineUrlPath(params string[] parts)
        {
            if (parts == null) throw new ArgumentNullException("parts");
            if (parts.Length == 0) return string.Empty;
            if (parts.Length == 1) return parts[0];

            StringBuilder sb = new StringBuilder(parts[0]);

            for (int i = 1; i < parts.Length; i++)
            {
                if ((parts[i].StartsWith("/") && !parts[i - 1].EndsWith("/")) || (!parts[i].StartsWith("/") && parts[i - 1].EndsWith("/")))
                    sb.Append(parts[i]);
                else if (parts[i].StartsWith("/") && parts[i - 1].EndsWith("/"))
                    sb.Append(parts[i].Substring(1));
                else if (!parts[i].StartsWith("/") && !parts[i - 1].EndsWith("/"))
                    sb.Append("/" + parts[i]);
                else
                    throw new ArgumentException("Incorrect part data", "parts");
            }

            return sb.ToString();
        }

        #region Request Timers
        static Dictionary<string, AutoResetEvent> _syncs = new Dictionary<string, AutoResetEvent>();

        private static void Sleep(string sleepType)
        {
            if (!Config.Sleep.ContainsKey(sleepType)) throw new ArgumentException("Unexpected sleep type", "sleepType");

            int sleepTime = Config.Sleep.GetSleep(sleepType);

            if (sleepTime == 0) return;

            AutoResetEvent reset = null;
            lock (_syncs)
            {
                if (!_syncs.TryGetValue(sleepType, out reset))
                {
                    reset = new AutoResetEvent(true);
                    _syncs.Add(sleepType, reset);
                }
            }

            reset.WaitOne();
            ThreadPool.QueueUserWorkItem(delegate(object state) { Thread.Sleep(sleepTime); reset.Set(); });
        }
        #endregion
    }
}
