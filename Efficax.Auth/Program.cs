var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

DBClient dbClient = new DBClient();

Random random = new Random();
byte[] a = new byte[8];
random.NextBytes(a);
ulong e = BitConverter.ToUInt64(a);
Console.WriteLine(e);

HttpClient client = new HttpClient();

app.MapGet("/auth", async (ctx) =>
{
    dbClient.Disconnect();
    HttpResponseMessage response = await client.GetAsync("https://api.coinbase.com/v2/currencies");
    string data = "API unavailable.";
    if (response.IsSuccessStatusCode)
        data = await response.Content.ReadAsStringAsync();
    await ctx.Response.WriteAsync(data);
});

app.Run();