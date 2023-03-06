// Author: Atharva Shivankar <ads8046@rit.edu>
// COPADS Project 3: Secure Messaging
// Date: Nov 17 2022

namespace Messenger {
    static class Messenger {

        /// <summary>
        /// The driver method of the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            if (args.Length is < 1 or > 3) {
                Console.WriteLine("Invalid number of arguments. Please refer to the help below.\n");
                Helper.PrintHelp();
                Environment.Exit(3);
            }

            else {
                var controller = new Routing();
                var keyProcessor = new ProcessKey();
                var validOptions = new[] {"getKey", "keyGen", "sendKey", "sendMsg", "getMsg"};
                var option = args[0];
                
                var optionIsValid = Array.Exists( validOptions, element => element == option  );

                if (optionIsValid) {
                    switch (option) {
                        case "getKey" when args.Length == 2: {
                            var email = args[1];
                            controller.GetKey(email);
                            var keyFileName = email + ".key";
                            keyProcessor.ProcessKeyFile(keyFileName);
                            break;
                        }
                        case "keyGen" when args.Length == 2: {
                            var keySize = int.Parse(args[1]);
                            keyProcessor.KeyGen(keySize);
                            break;
                        }
                        case "sendKey" when args.Length == 2: {
                            var email = args[1];
                            keyProcessor.SendKey(email);
                            break;
                        }
                        case "sendMsg" when args.Length == 3: {
                            var email = args[1];
                            var message = args[2];
                    
                            keyProcessor.SendMsg(email, message);
                            break;
                        }
                        case "getMsg" when args.Length == 2: {
                            var email = args[1];
                            keyProcessor.GetMsg(email);
                            break;
                        }
                        default:
                            Console.WriteLine("Invalid option command. Refer to the help for valid operations.");
                            Helper.PrintHelp();
                            Environment.Exit(3);
                            break;
                    }
                }
                
                else {
                    Console.WriteLine("Invalid option command. Read help for valid operations.");
                    Helper.PrintHelp();
                    Environment.Exit(4);
                }
            }

            Console.ReadKey();
        }
        
    }
}