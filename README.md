# Initial addition of packages:
dotnet add package Smartersoft.Identity.Client.Assertion
dotnet add package Microsoft.Identity.Client
dotnet build

# Load module and check available commands:
Import-Module ./bin/Debug/net6.0/GetKeyVaultToken
Get-Command -Module GetKeyVaultToken

# Test run:
Get-KeyVaultToken `
    -ClientId "70e9ae2c-d00b-4581-9a31-b71c2a0ec38e" `
    -TenantId "ffb9e08b-6f50-443c-8fe8-48aed2dc3204" `
    -KeyVaultUri "https://sundmankv1.vault.azure.net/" `
    -CertificateName "cert2"
