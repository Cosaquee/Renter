#
# AddMigration.ps1
#
$migrationName = Read-Host -Prompt 'Migration name'
dotnet ef migrations add $migrationName -s ../Renter/