using Microsoft.EntityFrameworkCore;
using DB;
using DB.Interfaces;
using DB.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Accelergreat.EntityFramework;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data.Entity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});


var conn = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine("Using Connection String: " + conn);

builder.Services.AddDbContextFactory<DeliveryAppContext>(
                options =>
                {
                    options.UseNpgsql(conn);
                });

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


var app = builder.Build();

//execution of migrations on app initialization
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DeliveryAppContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
