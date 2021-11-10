Console.WriteLine("Hello, World!");


// NetInDispatcher routes packets into queues for each of the zones, EfficaxServer or higher level parts of it like the auth server part or server network manager

// Threads for one efficax server instance:
// 1: Polling Events, Deserializing Packets, NetInDispatcher
// 2: NetOutDispatcher
// A group of threads, each managing a zone, or more, no threads tick the same zone

// Issues how will zones and other threads get efficax clients, and how will those be thread safe, what needs to access them, what will they store?

// Eventually support queries from other servers and other interserver communication, will be used for:
// Client handover, syncing non-zone related data

// for inter efficax network requests trust them as a continuation of something, provide all context in the message,
// it will be sent then proccessed remotely and sent back with the necessary context

// Eventually encapsulate data that will be sent to places other than a NetPeer
// Other places: Auth Server, DB, Other Servers, Market Servers

// DISAJSADSIJAJIOAIJSDIJAIDSI
// Net in messages can even be from the server itself, have many inbound data types





// look for physics interactions on predecided ticks, as object gets closer throw in another group and do the approach tick faster and faster

// positions for entities sent to player are offset from the player, anything close enough to effect the player in some way







