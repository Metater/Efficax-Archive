var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

DBClient dbClient = new DBClient();

Random random = new Random();
byte[] a = new byte[8];
random.NextBytes(a);
ulong e = BitConverter.ToUInt64(a);
Console.WriteLine(e);

app.MapGet("/auth", (ctx) =>
{
    dbClient.Disconnect();
    return ctx.Response.WriteAsync("e");
});

app.Run();