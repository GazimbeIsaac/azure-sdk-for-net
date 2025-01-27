// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.ResourceManager;

namespace Azure.ResourceManager.Grafana
{
    /// <summary> A class to add extension methods to SubscriptionResource. </summary>
    internal partial class SubscriptionResourceExtensionClient : ArmResource
    {
        private ClientDiagnostics _managedGrafanaGrafanaClientDiagnostics;
        private GrafanaRestOperations _managedGrafanaGrafanaRestClient;

        /// <summary> Initializes a new instance of the <see cref="SubscriptionResourceExtensionClient"/> class for mocking. </summary>
        protected SubscriptionResourceExtensionClient()
        {
        }

        /// <summary> Initializes a new instance of the <see cref="SubscriptionResourceExtensionClient"/> class. </summary>
        /// <param name="client"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        internal SubscriptionResourceExtensionClient(ArmClient client, ResourceIdentifier id) : base(client, id)
        {
        }

        private ClientDiagnostics ManagedGrafanaGrafanaClientDiagnostics => _managedGrafanaGrafanaClientDiagnostics ??= new ClientDiagnostics("Azure.ResourceManager.Grafana", ManagedGrafanaResource.ResourceType.Namespace, Diagnostics);
        private GrafanaRestOperations ManagedGrafanaGrafanaRestClient => _managedGrafanaGrafanaRestClient ??= new GrafanaRestOperations(Pipeline, Diagnostics.ApplicationId, Endpoint, GetApiVersionOrNull(ManagedGrafanaResource.ResourceType));

        private string GetApiVersionOrNull(ResourceType resourceType)
        {
            TryGetApiVersion(resourceType, out string apiVersion);
            return apiVersion;
        }

        /// <summary>
        /// List all resources of workspaces for Grafana under the specified subscription.
        /// Request Path: /subscriptions/{subscriptionId}/providers/Microsoft.Dashboard/grafana
        /// Operation Id: Grafana_List
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> An async collection of <see cref="ManagedGrafanaResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual AsyncPageable<ManagedGrafanaResource> GetManagedGrafanasAsync(CancellationToken cancellationToken = default)
        {
            async Task<Page<ManagedGrafanaResource>> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = ManagedGrafanaGrafanaClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetManagedGrafanas");
                scope.Start();
                try
                {
                    var response = await ManagedGrafanaGrafanaRestClient.ListAsync(Id.SubscriptionId, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new ManagedGrafanaResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            async Task<Page<ManagedGrafanaResource>> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = ManagedGrafanaGrafanaClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetManagedGrafanas");
                scope.Start();
                try
                {
                    var response = await ManagedGrafanaGrafanaRestClient.ListNextPageAsync(nextLink, Id.SubscriptionId, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new ManagedGrafanaResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            return PageableHelpers.CreateAsyncEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// List all resources of workspaces for Grafana under the specified subscription.
        /// Request Path: /subscriptions/{subscriptionId}/providers/Microsoft.Dashboard/grafana
        /// Operation Id: Grafana_List
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> A collection of <see cref="ManagedGrafanaResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual Pageable<ManagedGrafanaResource> GetManagedGrafanas(CancellationToken cancellationToken = default)
        {
            Page<ManagedGrafanaResource> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = ManagedGrafanaGrafanaClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetManagedGrafanas");
                scope.Start();
                try
                {
                    var response = ManagedGrafanaGrafanaRestClient.List(Id.SubscriptionId, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new ManagedGrafanaResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            Page<ManagedGrafanaResource> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = ManagedGrafanaGrafanaClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetManagedGrafanas");
                scope.Start();
                try
                {
                    var response = ManagedGrafanaGrafanaRestClient.ListNextPage(nextLink, Id.SubscriptionId, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new ManagedGrafanaResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            return PageableHelpers.CreateEnumerable(FirstPageFunc, NextPageFunc);
        }
    }
}
