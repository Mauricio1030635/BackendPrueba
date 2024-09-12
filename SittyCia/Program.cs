using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SittyCia.Data;
using SittyCia.Models;
using SittyCia.Service.IService;
using SittyCia.Service;
using Microsoft.OpenApi.Models;
using SittyCia.Extensions;

var builder = WebApplication.CreateBuilder(args);



builder.Services.RegisterBackgroundTask(builder.Configuration);


builder.Services.SwaggerExtension();



builder.Services.AddCors(option => {
    option.AddPolicy("newPolitics", app => { app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("newPolitics");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();


//Migraciones
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}