using ChatAppAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();

//SignalR
app.MapHub<ChatHub>("/chat");

app.Run();
