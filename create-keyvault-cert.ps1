# Login to Azure
Connect-AzAccount
Set-AzContext -Subscription "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"

# Variables
$keyVaultName="mykeyvault"
$certificateName="cert"
$subjectName="CN=myapp.it-total.internal"
$appName="myapp"

# Define the certificate police
$certificatePolicy = New-AzKeyVaultCertificatePolicy `
    -SubjectName $subjectName `
    -IssuerName "Self" `
    -ValidityInMonths 108 `
    -KeyUsage "DigitalSignature" `
    -EmailAtNumberOfDaysBeforeExpiry 30 `
    -SecretContentType "application/x-pkcs12" `
    -KeyNotExportable

# Create the certificate and get the base64 encoded certificate
Add-AzKeyVaultCertificate -VaultName $keyVaultName -Name $certificateName -CertificatePolicy $certificatePolicy
$certBase64 = Get-AzKeyVaultSecret -VaultName $keyVaultName -Name $certificateName -AsPlainText

# Create the App Registration and Service Principal in remote tenant
New-AzADApplication -DisplayName $appName -CertValue $certBase64 -EndDate (Get-Date).AddYears(9)
