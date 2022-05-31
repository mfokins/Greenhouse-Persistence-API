using Api.BridgeIot;
using Core;
using Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<GreenHouseDbContext>();

//Adding core services
builder.Services.AddCoreServices();

//Adding repositories
builder.Services.AddRepositoryServices();

builder.Services.AddScoped<IMessageHandler, MessageHandler>();
builder.Services.AddHostedService<BridgeMain>();
builder.Services.AddScoped<DownlinkHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
//Needed for intigration tests
public partial class Program { }