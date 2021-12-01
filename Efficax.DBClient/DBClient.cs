namespace Efficax.DBClient; //{}

public class DBClient
{
    internal DBSession dbSession;

    internal ulong authToken;

    public DBClient()
    {
        string[] secrets = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\db.secrets");
        string address = secrets[0];
        int port = int.Parse(secrets[1]);
        authToken = ulong.Parse(secrets[2]);

        dbSession = new DBSession(this, address, port);

        dbSession.ConnectAsync();
    }

    public void Disconnect()
    {
        dbSession.DisconnectAndStop();
    }
}