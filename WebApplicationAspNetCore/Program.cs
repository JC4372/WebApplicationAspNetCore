var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.MapGet("/add/{arg1}/{arg2}", (int arg1, int arg2) => $"Result: {arg1 + arg2}");

app.Run();