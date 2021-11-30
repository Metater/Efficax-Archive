namespace Efficax.DB; //{}

public class DBSecrets
{
    public readonly List<ulong> dbAuthTokens = new();
    public readonly int port;

    public DBSecrets()
    {
        string[] secrets = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\db.secrets");
        int dbAuthTokenCount = int.Parse(secrets[0]);
        for (int i = 1; i < dbAuthTokenCount + 1; i++)
            dbAuthTokens.Add(ulong.Parse(secrets[i]));
        port = int.Parse(secrets[dbAuthTokenCount + 1]);
    }
}