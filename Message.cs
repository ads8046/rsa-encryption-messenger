// Author: Atharva Shivankar <ads8046@rit.edu>
// COPADS Project 3: Secure Messaging
// Date: Nov 17 2022

namespace Messenger; 

/// <summary>
/// A class to initialize message objects when sending and receiving
/// messages from the server.
/// </summary>
public class Message {
    public string email { get; set; }
    public string content { get; set; }
}