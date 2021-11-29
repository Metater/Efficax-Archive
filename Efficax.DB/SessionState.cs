namespace Efficax.DB; //{}

internal enum SessionState
{
    Connecting,
    WaitingForRSAPublicKey,
    SentAESKey,
    Open
}