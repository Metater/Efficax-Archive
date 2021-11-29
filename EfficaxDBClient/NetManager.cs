using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpClient = NetCoreServer.TcpClient;

namespace EfficaxDBClient;

internal sealed class NetManager : TcpClient
{
    private DBClient dbClient;

    //private bool stop;

    public NetManager(DBClient dbClient, string address, int port) : base(address, port)
    {
        this.dbClient = dbClient;
    }

    public void DisconnectAndStop()
    {
        //stop = true;
        DisconnectAsync();
        while (IsConnected)
            Thread.Yield();
    }

    protected override void OnConnected()
    {
        Console.WriteLine($"Chat TCP client connected a new session with Id {Id}");
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Chat TCP client disconnected a session with Id {Id}");

        //Thread.Sleep(1000);

        //if (!stop)
            //ConnectAsync();
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        Console.WriteLine(Encoding.UTF8.GetString(buffer, (int)offset, (int)size));
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP client caught an error with code {error}");
    }

}