# PowerShell script to set env value in terraform.tfvars based on current git branch

$branch = git rev-parse --abbrev-ref HEAD
$envValue = if ($branch -eq "main") { "prd" } else { "dev" }

$tfvarsPath = "terraform/terraform.tfvars"
Set-Content -Path $tfvarsPath -Value "env = \"$envValue\""
Write-Host "Set env to '$envValue' in $tfvarsPath based on branch '$branch'"
