namespace Efficax.DB; //{}

public enum SessionState
{
    Connecting,
    WaitingForRSAPublicKey,
    SentAESKey,
    Open
}