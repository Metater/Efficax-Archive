using EfficaxDBClient;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

DBClient? dbClient = null;

Random random = new Random();
byte[] a = new byte[8];
random.NextBytes(a);
ulong e = BitConverter.ToUInt64(a);
Console.WriteLine(e);

app.MapGet("/auth", (ctx) =>
{
    if (dbClient == null)
    {
        dbClient = new DBClient();
        return ctx.Response.WriteAsync("Making db client!");
    }
    return ctx.Response.WriteAsync("Already made db client!");
});

app.Run();