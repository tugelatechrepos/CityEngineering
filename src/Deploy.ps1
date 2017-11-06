$publishFolder = "E:\VS2017Projects\CityEngineering\src"

$publishWorkspace= [System.IO.Path]::Combine($publishFolder,"workspace")

$appBundle = [System.IO.Path]::Combine($publishFolder,"app-bundle.zip")

<#

if(Test-Path $publishWorkspace){
Remove-Item $publishWorkspace -Confirm:$false -Force
}

if(Test-Path $appBundle)
{
Remove-Item $appBundle -Confirm:$false -Force
}
#>



Write-Host 'Create msdeploy archive for Security Host'
<#msbuild /p:VisualStudioVersion=14.0 .\Security\SecurityHost\SecurityHost.csproj /t:package /p:Configuration=Release#>
Copy-Item .\Security\SecurityHost\obj\Debug\Package\SecurityHost.zip $publishWorkspace

Write-Host 'Create msdeploy archive for Municipality Host'
Copy-Item .\Municipality\MunicipalityHost\obj\Debug\Package\MunicipalityHost.zip $publishWorkspace

Write-Host 'Copy deployment manifest'
Copy-Item .\aws-windows-deployment-manifest.json $publishWorkspace

Write-Host 'Zipping up publish workspace to create app bundle'
Add-Type -assembly "system.io.compression.filesystem" 
[io.compression.zipfile]::CreateFromDirectory( $publishWorkspace, $appBundle)