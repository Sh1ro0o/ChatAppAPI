using ChatAppAPI.Hubs;
using ChatAppAPI.Interface;
using ChatAppAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

//Scoped services
builder.Services.AddScoped<IChatService, ChatService>();

//Singleton services
builder.Services.AddSingleton<IRoomStoreService, RoomStoreService>();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200") //Local frontend
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();

//CORS
app.UseCors("AllowAngularApp");

//SignalR
app.MapHub<ChatHub>("/chat");
app.Run();
