using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficaxDB;

public class DB
{
    //public ConcurrentDictionary<>

    public ConcurrentBag<ulong> dbAuthTokens = new();
    public int port;

    public DB()
    {
        string[] secrets = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\db.secrets");
        int dbAuthTokenCount = int.Parse(secrets[0]);
        for (int i = 1; i < dbAuthTokenCount + 1; i++)
            dbAuthTokens.Add(ulong.Parse(secrets[i]));
        port = int.Parse(secrets[dbAuthTokenCount + 1]);
    }
}