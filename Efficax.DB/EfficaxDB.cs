namespace Efficax.DB; //{}

public class EfficaxDB
{
    //public ConcurrentDictionary<>

    public readonly DBSecrets secrets;

    public EfficaxDB()
    {
        secrets = new DBSecrets();
    }
}