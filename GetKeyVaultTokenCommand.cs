using System;
using System.Threading;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using Azure.Identity;
using Microsoft.Identity.Client;
using Smartersoft.Identity.Client.Assertion;

namespace ITTotal.PowerShell.Azure.Commands
{
    [Cmdlet(VerbsCommon.Get,"KeyVaultToken")]
    [OutputType(typeof(AuthenticationResult))]
    public class GetKeyVaultTokenCommand : PSCmdlet
    {
        [Parameter(Mandatory = true)] public string ClientId { get; set; }
        [Parameter(Mandatory = true)] public string TenantId { get; set; }
        [Parameter(Mandatory = true)] public string KeyVaultUri { get; set; }
        [Parameter(Mandatory = true)] public string CertificateName { get; set; }
        [Parameter(Mandatory = false)] public string Scope { get; set; } = "https://graph.microsoft.com/.default";

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            // Synchronously calling the asynchronous method and waiting for its completion
            AuthenticationResult authResult = GetTokenAsync().GetAwaiter().GetResult();
            WriteObject(authResult);
        }

        private async Task<AuthenticationResult> GetTokenAsync()
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

            var authResult = await app.AcquireTokenForClient(new[] { Scope })
                .ExecuteAsync();

            return authResult;
        }
    }
}
