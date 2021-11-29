using EfficaxDB;
using System.Net;

Console.WriteLine("Hello, World!");

DB db = new DB();

NetManager netManager = new NetManager(db, IPAddress.Any, db.port);

netManager.Start();

Console.ReadKey();

netManager.Stop();