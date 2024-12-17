using Dima.Api.Data;
using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection")
     ?? string.Empty;
builder.Services.AddDbContext<AppDbContext>(
    x =>
    {
        x.UseSqlServer(connStr);
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});
builder.Services.AddTransient<Handler>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapPost(
    "/categories",
    (Request request, Handler handler) => handler.Handle(request)).WithName("Categories -> Create")
    .WithSummary("Creates a new category")
    .Produces<Response>();
app.Run();
// request 
public class Request
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class Response
{
    public long Id { get; set; } 
    public string Title { get; set; } = string.Empty;
}

public class Handler(AppDbContext context)
{
    public Response Handle(Request request)
    {
        var category = new Category
        {
            Title = request.Title,
            Description = request.Description
        };
        context.Categories.Add(category);
        context.SaveChanges();
        //Faz sempre todo o processo de criação, persistencia no banco e cia
        return new Response
        {
            Id = category.Id,
            Title = category.Title,
        }; 
    }
}

