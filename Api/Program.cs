var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/weatherforecast", () => "hello");
app.Run();
