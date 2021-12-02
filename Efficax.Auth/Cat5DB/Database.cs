using System.Collections.Generic;


public class Database
{
    private readonly Dictionary<string, DBTable> database = new();
}

public class DBTable
{
    private readonly Dictionary<string, DBEntry> table = new();
}

public class DBEntry
{

}