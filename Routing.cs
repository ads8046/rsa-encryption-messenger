// Author: Atharva Shivankar <ads8046@rit.edu>
// COPADS Project 3: Secure Messaging
// Date: Nov 17 2022

namespace Messenger;
using System;


/// <summary>
/// Makes the initial call to retrieve the key from the server
/// </summary>
public class Routing {
    private static readonly HttpClient Client = new();
    private const string BaseUrl = "http://kayrun.cs.rit.edu:5000";

    
    /// <summary>
    /// Retrieves a public key for the given email from the server and
    /// writes it locally to a '.key' file.
    /// </summary>
    /// <param name="email">Email of the person whose public key needs to be retrieved</param>
    public void GetKey(string email) {
        try {
            var reqUrl = BaseUrl + "/Key/" + email;

            HttpResponseMessage response = Client.GetAsync(reqUrl).Result;

            if (!response.IsSuccessStatusCode) return;
            string content = response.Content.ReadAsStringAsync().Result;
            
            var path = email + ".key";
            File.WriteAllText(path, content);
            
        }
        catch (HttpRequestException e) {
            Console.WriteLine("Exception occured (getKey) :{0} ", e.Message);
        }
    }

}