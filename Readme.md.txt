//create Mirgrations
dotnet ef migrations add SelfReferencingSchema --project PMS.Infrastructure --startup-project PMS.API

//apply Migrations
dotnet-ef database update --project PMS.Infrastructure --startup-project PMS.API

//remove Migrations
dotnet ef migrations remove --project PMS.Infrastructure --startup-project PMS.API