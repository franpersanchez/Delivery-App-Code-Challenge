using Microsoft.EntityFrameworkCore;
using DB;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DeliveryAppContext>(
                options =>
                {
                    options.UseNpgsql(conn);
                    //}, ServiceLifetime.Scoped); // This doesn't work.
                });


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
