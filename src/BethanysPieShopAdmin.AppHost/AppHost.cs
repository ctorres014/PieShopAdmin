using Microsoft.Extensions.DependencyInjection;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Add orchestation sql
var db = builder.AddSqlServer("sql")
                .AddDatabase("BethanysPieShopDbContextConnection");

// Add HealthChecks
builder.Services.AddHealthChecks();
//    .AddDbContextCheck<BethanysPieShopDbContext>();

// Add proyect UI
builder.AddProject<BethanysPieShopAdmin>("web")
        .WithReference(db);


builder.Build().Run();
