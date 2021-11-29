namespace EfficaxDBClient;

public class DBClient
{
    internal NetManager netManager;

    private ulong authToken;

    public DBClient()
    {
        string[] secrets = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\db.secrets");
        string address = secrets[0];
        int port = int.Parse(secrets[1]);
        authToken = ulong.Parse(secrets[2]);

        netManager = new NetManager(this, address, port);

        netManager.ConnectAsync();
    }

    public void Disconnect()
    {
        netManager.DisconnectAndStop();
    }
}