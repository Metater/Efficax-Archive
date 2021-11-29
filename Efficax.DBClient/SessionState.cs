namespace Efficax.DBClient; //{}

internal enum SessionState
{
    Connecting,
    SentRSAPublicKey,
    SentAuthToken,
    Open
}