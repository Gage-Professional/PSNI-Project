using Project.Messaging;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMessaging();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment()) { }

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();