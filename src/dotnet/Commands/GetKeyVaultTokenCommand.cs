using System;
using System.Management.Automation;
using Azure.Identity;
using Microsoft.Identity.Client;
using Smartersoft.Identity.Client.Assertion;

namespace ITTotal.Azure.KeyVaultToken.Commands
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

    protected override void ProcessRecord()
    {
        var tokenCredential = new DefaultAzureCredential();

        // Use the ConfidentialClientApplicationBuilder as usual
        // but call `.WithKeyVaultCertificate(...)` instead of `.WithCertificate(...)`
        var app = ConfidentialClientApplicationBuilder
            .Create(ClientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, TenantId)
            .WithKeyVaultCertificate(new Uri(KeyVaultUri), CertificateName, tokenCredential)
            .Build();

        var authResult = app.AcquireTokenForClient(new[] { Scope })
            .ExecuteAsync().GetAwaiter().GetResult();

        WriteObject(authResult);
    }
    }
}
