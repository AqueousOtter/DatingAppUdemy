
using API;


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



//Configure the HTTP request pipeline. 
app.UseMiddleware<ExceptionMiddleware>();

// Order matters
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors( meCorsBuilder => meCorsBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
// must be after cors
app.UseAuthentication();
app.UseAuthorization();
// must be before Controllers
app.MapControllers();

app.Run();
