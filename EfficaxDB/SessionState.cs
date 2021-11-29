using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficaxDB;

internal enum SessionState
{
    Connecting,
    WaitingForRSAPublicKey,
    SentAESKey,
    Open
}