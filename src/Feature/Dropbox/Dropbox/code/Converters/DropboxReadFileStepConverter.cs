using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Feature.Dropbox.Models;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace Feature.Dropbox.Converters
{
    public class DropboxReadFileStepConverter : BasePipelineStepConverter<ItemModel>
    {
        private static readonly Guid TemplateId = Guid.Parse("{69537E60-8B38-44FB-8108-6058F62E1029}");
        public DropboxReadFileStepConverter(IItemModelRepository repository) : base(repository)
        {
            this.SupportedTemplateIds.Add(TemplateId);
        }
        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            AddEndpointSettings(source, pipelineStep);
        }
        private void AddEndpointSettings(ItemModel source, PipelineStep pipelineStep)
        {
            var settings = new EndpointSettings();
            var endpointFrom = base.ConvertReferenceToModel<Endpoint>(source, ReadDropboxFileItemModel.EndpointFrom);
            if (endpointFrom != null)
            {
                settings.EndpointFrom = endpointFrom;
            }
            pipelineStep.Plugins.Add(settings);
        }
    }
}