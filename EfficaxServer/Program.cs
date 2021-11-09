Console.WriteLine("Hello, World!");


// NetInDispatcher routes packets into queues for each of the zones, EfficaxServer or higher level parts of it like the auth server part or server network manager

// Threads for one efficax server instance:
// 1: Polling Events, Deserializing Packets, NetInDispatcher
// 2: NetOutDispatcher
// A group of threads, each managing a zone, or more, no threads tick the same zone

// Issues how will zones and other threads get efficax clients, and how will those be thread safe, what needs to access them, what will they store?