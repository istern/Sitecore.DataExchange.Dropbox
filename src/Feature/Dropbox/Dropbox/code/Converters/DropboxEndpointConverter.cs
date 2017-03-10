using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Feature.Dropbox.Models;
using Feature.Dropbox.Plugins;
using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace Feature.Dropbox.Converters
{
    public class DropboxEndpointConverter
    {
        public class TextFileEndpointConverter : BaseEndpointConverter<ItemModel>
        {
            private static readonly Guid TemplateId = Guid.Parse("{4001B69A-0043-458B-9652-90BF6F4BE1EA}");
            public TextFileEndpointConverter(IItemModelRepository repository) : base(repository)
            {
                this.SupportedTemplateIds.Add(TemplateId);
            }
            protected override void AddPlugins(ItemModel source, Endpoint endpoint)
            {
                var settings = new DropboxSettings();
                settings.DropboxKey =base.GetStringValue(source, DropboxEndpointItemModel.DropboxKey);
                endpoint.Plugins.Add(settings);
            }
        }
    }
}