using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpClient = NetCoreServer.TcpClient;
using EfficaxShared.Utils;

namespace EfficaxDBClient; //{}

internal sealed class NetManager : TcpClient
{
    private DBClient dbClient;

    private BitReader reader = new();
    private BitWriter writer = new();
    private SessionState sessionState = SessionState.Connecting;

    private RSAParameters rsaPrivateKey;
    private byte[]? aesKey;

    public NetManager(DBClient dbClient, string address, int port) : base(address, port)
    {
        this.dbClient = dbClient;
    }

    public void DisconnectAndStop()
    {
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

        Console.WriteLine($"Chat TCP client connected a new session with Id {Id}");
        (RSAParameters, RSAParameters) rsaKeyPair = CryptoUtils.GenRSAKeyPair(2048);
        rsaPrivateKey = rsaKeyPair.Item2;
        string rsaPublicKeyString = CryptoUtils.GetRSAPublicKeyString(rsaKeyPair.Item1);
        writer.Reset();
        writer.Put(rsaPublicKeyString, 8);
        SendAsync(writer.Assemble());
        sessionState = SessionState.SentRSAPublicKey;
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Chat TCP client disconnected a session with Id {Id}");
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
                        return;
                    case SessionState.SentAuthToken:
                        return;
                    default:
                        Disconnect();
                        return;
                }
            }
            catch (Exception)
            {
                Disconnect();
                return;
            }
        }
        // Connection is open
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP client caught an error with code {error}");
    }

}