@{
    RootModule             = "KeyVaultToken.dll"
    ModuleVersion          = '0.0.1'
    GUID                   = '961a1533-097f-4776-a3f3-bca95f53ab91'
    Author                 = 'Mathias Sundman'
    CompanyName            = 'IT-Total Sweden AB'
    Copyright              = '(c) 2024 IT-Total Sweden AB. All rights reserved.'
    Description            = 'Use Azure Key Vault for OIDC Certficate based auth.'
    PowerShellVersion      = '5.1'
    DotNetFrameworkVersion = '4.6.1'
    RequiredModules        = @()
    RequiredAssemblies     = @(
        "System.Threading.Tasks.Extensions.dll"
    )
    NestedModules          = @()
    FunctionsToExport      = @()
    CmdletsToExport        = @("Get-KeyVaultToken")
    VariablesToExport      = '*'
    AliasesToExport        = '*'
}
