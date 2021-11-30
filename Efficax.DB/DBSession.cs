using Buffer = System.Buffer;

namespace Efficax.DB; //{}

internal class DBSession : TcpSession
{
    private EfficaxDB db;

    private BitReader reader = new();
    private BitWriter writer = new();
    private SessionState sessionState = SessionState.Connecting;

    private byte[]? aesKey;
    private ulong authToken;

    private string sessionInfo;

    public DBSession(EfficaxDB db, TcpServer server) : base(server)
    {
        this.db = db;
    }

    protected override void OnConnected()
    {
        sessionInfo = $"{Id} {Socket.RemoteEndPoint}";
        Console.WriteLine($"[Client DB Session] {sessionInfo} initiating DB session");
        sessionState = SessionState.WaitingForRSAPublicKey;
        // put in external timeout verifier
        // eg: if not open in 5 sec disconnect
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"[Client DB Session] {sessionInfo} disconnected");
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        byte[] data = new byte[size];
        Buffer.BlockCopy(buffer, (int)offset, data, 0, (int)size);
        reader.SetSource(data);
        if (sessionState != SessionState.Open)
        {
            try
            {
                switch (sessionState)
                {
                    case SessionState.WaitingForRSAPublicKey:
                        RSAParameters rsaPublicKey = CryptoUtils.GetRSAPublicKey(reader.GetString(16));
                        aesKey = CryptoUtils.GenAESKey();
                        SendAsync(CryptoUtils.RSAEncrypt(aesKey, rsaPublicKey));
                        sessionState = SessionState.SentAESKey;
                        Console.WriteLine($"[Client DB Session] {sessionInfo} sent RSA public key, sending AES key");
                        return;
                    case SessionState.SentAESKey:
                        ulong authToken = BitConverter.ToUInt64(CryptoUtils.AESDecrypt(aesKey, data));
                        if (!db.dbAuthTokens.Contains(authToken)) break;
                        SendAsync(new byte[] { (byte)DBPacketHeaderCB.SessionConfirmation });
                        sessionState = SessionState.Open;
                        Console.WriteLine($"[Client DB Session] {sessionInfo} verified auth token, sending confirmation");
                        return;
                }
            }
            catch (Exception) {}
            Console.WriteLine($"[Client DB Session] {sessionInfo}, error in connection protocol, disconnecting");
            Disconnect();
            return;
        }
        // Connection is open

    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"[Client DB Session] Caught an error with code {error}");
    }
}