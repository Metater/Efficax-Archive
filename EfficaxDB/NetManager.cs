using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;

namespace EfficaxDB;

internal class NetManager : TcpServer
{
    private DB db;

    public NetManager(DB db, IPAddress address, int port) : base(address, port)
    {
        this.db = db;
    }

    protected override TcpSession CreateSession() { return new NetSession(db, this); }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Chat TCP server caught an error with code {error}");
    }
}