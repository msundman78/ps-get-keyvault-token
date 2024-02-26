### Overview

This PS module uses the [Smartersoft.Identity.Client.Assertion](https://github.com/Smartersoft/identity-client-assertion/) library to perform an OIDC authentication with Client Credentials flow using a certificate in Azure Key Vault to sign the JWT auth token and returns the AccessToken to be used with other standard PowerShell modules like Connect-MgGraph or Connect-AzAccount.

All creds for this project goes to Stephan van Rooij for creating the [Smartersoft.Identity.Client.Assertion](https://github.com/Smartersoft/identity-client-assertion/) library.

This module is tested to work with PowerShell 7.2 runtimes in Azure Automation Accounts.

### Create certificate in Azure Key Vault
For an example how to create a certificate with a non-exportable key
in Azure Key Vault to be used with this module see: [create-keyvault-cert.ps1](create-keyvault-cert.ps1)

### Usage in an Azure Automation Account:
  - Enable Managed Identiy on your Automation Account
  - Assign the "Key Vault Crypto User" role to your AA MI on your Key Vault

```
Import-Module KeyVaultToken
$authResponse = Get-KeyVaultToken `
    -ClientId "70e9ae2c-d00b-4581-9a31-b71c2a0ec38e" `
    -TenantId "ffb9e08b-6f50-443c-8fe8-48aed2dc3204" `
    -Scope "https://graph.microsoft.com/.default" `
    -KeyVaultUri "https://sundmankv1.vault.azure.net/" `
    -CertificateName "cert2"
$secureAccessToken = $authResponse.AccessToken | ConvertTo-SecureString -AsPlainText -Force
Connect-MgGraph -AccessToken $secureAccessToken
```
