using pusherSignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

string clientUrl = Environment.GetEnvironmentVariable("CLIENT_URL");

if (clientUrl == null) throw new Exception("you must provide a CLIENT_URL");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => { policy.WithOrigins(clientUrl).AllowCredentials().AllowAnyHeader().AllowAnyMethod(); });
});


var app = builder.Build();

app.UseCors();


app.MapHub<EventsHub>("/hub");


app.Run();