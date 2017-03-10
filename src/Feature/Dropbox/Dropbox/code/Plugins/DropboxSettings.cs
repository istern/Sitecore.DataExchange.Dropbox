using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feature.Dropbox.Plugins
{
    public class DropboxSettings : Sitecore.DataExchange.IPlugin
    {
        public string DropboxKey { get; set; }       
    }
}
