namespace Efficax.DBClient; //{}

internal class DBSession : TcpClient
{
    private DBClient dbClient;

    private BitReader reader = new();
    private BitWriter writer = new();
    private SessionState sessionState = SessionState.Connecting;

    private RSAParameters rsaPrivateKey;
    private byte[]? aesKey;

    private bool stop = false;

    public DBSession(DBClient dbClient, string address, int port) : base(address, port)
    {
        this.dbClient = dbClient;
    }

    public void DisconnectAndStop()
    {
        Console.WriteLine($"[DB Client] Triggered disconnect and stop on {Id}");
        stop = true;
        DisconnectAsync();
        while (IsConnected)
            Thread.Yield();
    }

    protected override void OnConnected()
    {
        if (sessionState != SessionState.Connecting)
        {
            DisconnectAndStop();
            return;
        }

        (RSAParameters, RSAParameters) rsaKeyPair = CryptoUtils.GenRSAKeyPair(2048);
        rsaPrivateKey = rsaKeyPair.Item2;
        string rsaPublicKeyString = CryptoUtils.GetRSAPublicKeyString(rsaKeyPair.Item1);
        writer.Reset();
        writer.Put(rsaPublicKeyString, 16);
        SendAsync(writer.Assemble());
        sessionState = SessionState.SentRSAPublicKey;
        Console.WriteLine($"[DB Client] {Id} initiating, sending RSA key");
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"[DB Client] {Id} disconnected");

        Thread.Sleep(1000);

        if (!stop)
        {
            sessionState = SessionState.Connecting;
            Console.WriteLine($"[DB Client] {Id} attempting reconnect");
            ConnectAsync();
        }
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
                    case SessionState.SentRSAPublicKey:
                        aesKey = CryptoUtils.RSADecrypt(data, rsaPrivateKey);
                        SendAsync(CryptoUtils.AESEncrypt(aesKey, BitConverter.GetBytes(dbClient.authToken)));
                        sessionState = SessionState.SentAuthToken;
                        Console.WriteLine($"[DB Client] {Id} received AES key, sending auth token");
                        return;
                    case SessionState.SentAuthToken:
                        DBPacketHeaderCB response = (DBPacketHeaderCB)data[0];
                        if (response != DBPacketHeaderCB.SessionConfirmation) break;
                        sessionState = SessionState.Open;
                        Console.WriteLine($"[DB Client] {Id} session opened");
                        return;
                }
            }
            catch (Exception) {}
            DisconnectAndStop();
            return;
        }
        // Connection is open

    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"[DB Client] Caught an error with code {error}");
    }

}