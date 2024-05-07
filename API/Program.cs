
using API;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Extensions, application services added to cut down on code
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddScoped<ITokenService, TokenService>();


var app = builder.Build();


app.UseCors( meCorsBuilder => meCorsBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
//Configure the HTTP request pipeline. 
app.UseMiddleware<ExceptionMiddleware>();

// Order matters
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// must be after cors, Authenticate then authorize
app.UseAuthentication();
app.UseAuthorization();
// must be before Controllers
app.MapControllers();


using var scope = app.Services.CreateScope(); // Gives access to all the services in this Program.cs 
var services = scope.ServiceProvider;

// need to handle exp bc this isnt going through HTTP pipeline
try{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();  // applies any pending migrations
    await Seed.SeedUsers(context);


}
catch (Exception ex){
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during Migrations");
}

app.Run();
