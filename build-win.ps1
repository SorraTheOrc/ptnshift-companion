param(
    [string]$Configuration = "Release Windows"
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$framework = "net9.0"
$outputRoot = Join-Path $repoRoot "publish"
$targets = @(
    @{ Rid = "win-x64"; Platform = "x64"; Output = Join-Path $outputRoot "x64" },
    @{ Rid = "win-arm64"; Platform = "arm64"; Output = Join-Path $outputRoot "arm64" }
)

Write-Host "Building WinScreenStream native library..."
& (Join-Path $repoRoot "WinScreenStream/build.ps1")
if ($LASTEXITCODE -ne 0) {
    throw "WinScreenStream build failed with exit code $LASTEXITCODE."
}

if (-not (Test-Path $outputRoot)) {
    New-Item -ItemType Directory -Path $outputRoot | Out-Null
}

foreach ($target in $targets) {
    Write-Host "Publishing $($target.Rid) build to $($target.Output)..."
    dotnet publish (Join-Path $repoRoot "GUI/GUI.csproj") `
        --configuration $Configuration `
        --framework $framework `
        --runtime $($target.Rid) `
        --self-contained `
        --output $($target.Output) `
        -p:Platform=$($target.Platform)
    if ($LASTEXITCODE -ne 0) {
        throw "dotnet publish failed for $($target.Rid) with exit code $LASTEXITCODE."
    }
}

Write-Host "Windows builds created under $outputRoot for x64 and ARM64."
