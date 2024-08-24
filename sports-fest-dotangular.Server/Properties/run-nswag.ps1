$ErrorActionPreference = "Stop"

function Find-NSwagExe
{
    $nswagExe = (Get-Command nswag -ErrorAction SilentlyContinue).Source
    if (-not $nswagExe)
    {
        $nswagExe = (Get-ChildItem -Path $env:USERPROFILE.dotnet\tools -Filter nswag.exe -Recurse | Select-Object -First 1).FullName
    }
    return $nswagExe
}

$nswagExe = Find-NSwagExe

if (-not $nswagExe)
{
    Write-Host "NSwag executable not found. Installing NSwag..."
    dotnet tool install -g NSwag.ConsoleCore
    $nswagExe = Find-NSwagExe
}

if (-not $nswagExe)
{
    throw "NSwag executable not found even after installation attempt."
}

Write-Host "Using NSwag executable: $nswagExe"

$configuration = $args[0]
$nswagConfigPath = "Properties/nswag.json"

if (-not (Test-Path $nswagConfigPath))
{
    throw "nswag.json not found at path: $nswagConfigPath"
}

Write-Host "Running NSwag..."
try
{
    & $nswagExe run $nswagConfigPath /variables:Configuration=$configuration
    if ($LASTEXITCODE -ne 0)
    {
        throw "NSwag execution failed with exit code: $LASTEXITCODE"
    }
}
catch
{
    Write-Error "An error occurred while running NSwag: $_"
    throw
}

Write-Host "NSwag execution completed successfully."