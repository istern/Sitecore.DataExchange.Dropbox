using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Dropbox.Api;
using Dropbox.Api.Files;
using Feature.Dropbox.Extensions;
using Feature.Dropbox.Models.DropboxModels;
using Feature.Dropbox.Plugins;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Processors.PipelineSteps;
using Sitecore.Jobs.AsyncUI;
using Sitecore.Security;
using Sitecore.Services.Core.Diagnostics;
using Sitecore.Tasks;


namespace Feature.Dropbox.Processors
{
    [RequiredEndpointPlugins(typeof(DropboxSettings))]
    public class DropboxFileProcessor : BaseReadDataStepProcessor
    {
        protected override void ReadData(Endpoint endpoint, PipelineStep pipelineStep, PipelineContext pipelineContext)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }
            if (pipelineStep == null)
            {
                throw new ArgumentNullException(nameof(pipelineStep));
            }
            if (pipelineContext == null)
            {
                throw new ArgumentNullException(nameof(pipelineContext));
            }
             var logger = pipelineContext.PipelineBatchContext.Logger;


            var settings = endpoint.GetDropboxSettings();
            ;
            if (settings == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(settings.DropboxKey))
            {
                logger.Error (
                    "No path is specified on the endpoint. " +
                    "(pipeline step: {0}, endpoint: {1})",
                    pipelineStep.Name, endpoint.Name);
                return;
            }


            DropboxClient client = GetClient(settings.DropboxKey);
            var dropboxEntries = new List<Metadata>();
            var files = client.Files.ListFolderAsync("/").Result;
            foreach (var file in files.Entries)
            {
                dropboxEntries.Add(file);
            }

            var dataSettings = new IterableDataSettings(dropboxEntries);
            logger.Info(
                "{0} rows were read from the file. (pipeline step: {1}, endpoint: {2})",
                lines.Count, pipelineStep.Name, endpoint.Name);
            //
            //add the plugin to the pipeline context
            pipelineContext.Plugins.Add(dataSettings);
        }

        private DropboxClient GetClient(string dropboxKey)
        {
            if (string.IsNullOrWhiteSpace(dropboxKey))
            {
                return null;
            }

            return new DropboxClient(dropboxKey, new DropboxClientConfig("SitecoreDataExchange"));
        }
    }
}