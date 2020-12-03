
# Getting started

dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:ZeldaContext" "Host=<host>;Database=<db>;Username=<user>;Password=<password>"
dotnet tool install --global dotnet-ef
dotnet-ef database upgrade