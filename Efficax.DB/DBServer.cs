namespace Efficax.DB;

internal class DBServer : TcpServer
{
    private EfficaxDB db;

    public DBServer(EfficaxDB db, IPAddress address, int port) : base(address, port)
    {
        this.db = db;
    }

    protected override TcpSession CreateSession() { return new DBSession(db, this); }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP server caught an error with code {error}");
    }
}