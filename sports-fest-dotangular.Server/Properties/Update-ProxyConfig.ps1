$openApiPath = "Properties\openapi.json"
$proxyConfigPath = "../sports-fest-dotangular.client/src/proxy.conf.js"
$openApi = Get-Content $openApiPath | ConvertFrom-Json
$paths = $openApi.paths.PSObject.Properties.Name
$proxyConfig = @"
const { env } = require('process');
const target = env.ASPNETCORE_HTTPS_PORT ? ``https://localhost:`${env.ASPNETCORE_HTTPS_PORT}`` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7149';
const PROXY_CONFIG = [
  {
    context: [
$(foreach ($path in $paths) { "      `"$path`",`n"} )
    ],
    target,
    secure: false
  }
]
module.exports = PROXY_CONFIG;
"@
$proxyConfig | Set-Content -Path $proxyConfigPath
Write-Host "proxy.config.js has been updated successfully."