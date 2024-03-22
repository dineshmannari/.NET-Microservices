using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataService;
using PlatformService.SyncDataService.HTTP;

var builder = WebApplication.CreateBuilder(args);

var env= builder.Environment;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



if(env.IsProduction())
{
    Console.WriteLine("--> Using SQL Db");
    builder.Services.AddDbContext<AppDbContext>(opt=> opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}
else
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext> (opt=> opt.UseInMemoryDatabase("InMem"));
}
builder.Services.AddScoped<IPlatformRepo,PlatformRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient,HttpCommandDataClient>();
Console.WriteLine($"--> Command Service Endpint{builder.Configuration["CommandService"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

PrepDb.PrePopulation(app, env.IsProduction());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
