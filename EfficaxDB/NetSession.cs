using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;
using Buffer = System.Buffer;
using BitManipulation;
using EfficaxShared.Utils;
using System.Security.Cryptography;

namespace EfficaxDB; //{}

internal class NetSession : TcpSession
{
    private DB db;

    private BitReader reader = new();
    private BitWriter writer = new();
    private SessionState sessionState = SessionState.Connecting;

    private byte[]? aesKey;
    private ulong authToken;

    public NetSession(DB db, TcpServer server) : base(server)
    {
        this.db = db;
    }

    protected override void OnConnected()
    {
        Console.WriteLine($"Chat TCP session with Id {Id} connected!");
        sessionState = SessionState.WaitingForRSAPublicKey;
        // put in external timeout verifier
        // eg: if not open in 5 sec disconnect
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Chat TCP session with Id {Id} disconnected!");
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
                        RSAParameters rsaPublicKey = CryptoUtils.GetRSAPublicKey(reader.GetString(8));
                        aesKey = CryptoUtils.GenAESKey();
                        SendAsync(CryptoUtils.RSAEncrypt(aesKey, rsaPublicKey));
                        sessionState = SessionState.SentAESKey;
                        return;
                    case SessionState.SentAESKey:
                        ulong authToken = CryptoUtils.AESDecrypt(aesKey, data);
                        if (!db.dbAuthTokens.Contains(authToken)) break;
                        return;
                    case SessionState.WaitingForAuthToken:
                        
                        return;
                }
            }
            catch (Exception) {}
            Disconnect();
            return;
        }
        // Connection is open

    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP session caught an error with code {error}");
    }
}