using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficaxDBClient; //{}

internal enum ConnectionState
{
    Connecting,
    SentRSAPublicKey,
    SentAuthToken,
    WaitingForConfirmation,
    Open
}