using System;
using System.Threading;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using Azure.Identity;
using Microsoft.Identity.Client;
using Smartersoft.Identity.Client.Assertion;

namespace GetKeyVaultToken
{
    [Cmdlet(VerbsCommon.Get,"KeyVaultToken")]
    [OutputType(typeof(String))]
    public class TestSampleCmdletCommand : PSCmdlet
    {
        [Parameter(Mandatory = true)] public string ClientId { get; set; }
        [Parameter(Mandatory = true)] public string TenantId { get; set; }
        [Parameter(Mandatory = true)] public string KeyVaultUri { get; set; }
        [Parameter(Mandatory = true)] public string CertificateName { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            // Synchronously calling the asynchronous method and waiting for its completion
            string accessToken = GetTokenAsync().GetAwaiter().GetResult();
            WriteObject(accessToken);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
        }

        private async Task<string> GetTokenAsync()
        {
            // Create a token credential that suits your needs, used to access the KeyVault
            var tokenCredential = new DefaultAzureCredential();

            // Use the ConfidentialClientApplicationBuilder as usual
            // but call `.WithKeyVaultCertificate(...)` instead of `.WithCertificate(...)`
            var app = ConfidentialClientApplicationBuilder
                .Create(ClientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, TenantId)
                .WithKeyVaultCertificate(new Uri(KeyVaultUri), CertificateName, tokenCredential)
                .Build();

            // Use the app, just like before
            var tokenResult = await app.AcquireTokenForClient(new[] { "https://graph.microsoft.com/.default" })
                .ExecuteAsync();

            return tokenResult.AccessToken;
        }
    }
}
