using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Feature.Dropbox.Plugins;
using Sitecore.DataExchange.Models;

namespace Feature.Dropbox.Extensions
{
    public static class DropboxEndpointExtensions
    {
        public static DropboxSettings GetDropboxSettings(this Endpoint endpoint)
        {
            return endpoint.GetPlugin<DropboxSettings>();
        }
        public static bool HasTextFileSettings(this Endpoint endpoint)
        {
            return (GetDropboxSettings(endpoint) != null);
        }
    }
}