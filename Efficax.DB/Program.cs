﻿Console.WriteLine("Hello, World!");

EfficaxDB db = new EfficaxDB();

DBServer dbServer = new DBServer(db, IPAddress.Any, db.secrets.port);

dbServer.Start();

Console.ReadKey();

dbServer.Stop();