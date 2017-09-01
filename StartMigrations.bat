cd ProjectManagement\ProjectManagement
dotnet ef --startup-project ..\WebApi database update -c ProjectManagementContext
cd ..
cd UserManagement
dotnet ef --startup-project ..\WebApi database update -c UserManagementContext