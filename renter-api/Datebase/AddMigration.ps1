#
# AddMigration.ps1
#
$migrationName = ""
dotnet ef migrations add $migrationName -s ../Renter/