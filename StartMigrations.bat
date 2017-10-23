cd ProjectManagement\ProjectManagement
dotnet ef --startup-project ..\WebApi database update -c ProjectManagementContext
cd ..
cd ProjectManagementView
dotnet ef --startup-project ..\WebApi database update -c ProjectManagementViewContext
cd ..
cd UserManagement
dotnet ef --startup-project ..\WebApi database update -c UserManagementContext