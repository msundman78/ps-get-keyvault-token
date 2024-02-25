### Overview

This PS module uses the [Smartersoft.Identity.Client.Assertion](https://github.com/Smartersoft/identity-client-assertion/) library to perform an OIDC authentication with Client Credentials flow using a certificate in Azure Key Vault to sign the JWT auth token and returns the AccessToken to be used with other standard PowerShell modules like Connect-MgGraph or Connect-AzAccount.

All creds for this project goes to Stephan van Rooij for creating the [Smartersoft.Identity.Client.Assertion](https://github.com/Smartersoft/identity-client-assertion/) library.



### Initial addition of packages:
```
dotnet add package Smartersoft.Identity.Client.Assertion
dotnet add package Microsoft.Identity.Client
dotnet build
```

### Load module and check available commands:
```
Import-Module ./bin/Debug/net6.0/GetKeyVaultToken
Get-Command -Module GetKeyVaultToken
```

### Test run:
```
Get-KeyVaultToken `
    -ClientId "70e9ae2c-d00b-4581-9a31-b71c2a0ec38e" `
    -TenantId "ffb9e08b-6f50-443c-8fe8-48aed2dc3204" `
    -KeyVaultUri "https://sundmankv1.vault.azure.net/" `
    -CertificateName "cert2"
```
