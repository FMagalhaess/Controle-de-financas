var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/categories", () => "Hello World!");
app.MapPost("/categories", () => "Hello World!");
app.MapPut("/categories", () => "Hello World!");
app.MapDelete("/categories", () => "Hello World!");

app.Run();
