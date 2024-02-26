# Define paths
$moduleName = "KeyVaultToken"
$sourceDir = "../src/dotnet/bin/Release/netstandard2.0/"
$psd1File = "../src/*.psd1"
$publishDir = "../publish"
$zipFile = "$publishDir/$moduleName.zip"

if (Test-Path $publishDir) {
    Remove-Item -Path $publishDir -Force -Recurse
}
New-Item -Path "$publishDir/$moduleName" -ItemType Directory -Force > $null

# Copy all DLL and psd1 files from the source directory to the publish directory
Get-ChildItem -Path "$sourceDir/*.dll" -File | Copy-Item -Destination "$publishDir/$moduleName"
Get-ChildItem -Path $psd1File -File | Copy-Item -Destination "$publishDir/$moduleName"

# Create a ZIP archive of the publish directory
if (Test-Path $zipFile) {
    Remove-Item $zipFile # Remove existing ZIP file if it exists
}
Compress-Archive -Path "$publishDir/*" -DestinationPath $zipFile

Write-Host "Publish directory has been zipped to $zipFile"
