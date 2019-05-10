using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace TwitchBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread botThread = new Thread(new ThreadStart(RunBot));
            botThread.Start();
        }

        public static void RunBot()
        {
            IrcClient irc = new IrcClient("irc.chat.twitch.tv", 6667, "meleeonlybot", @"C:\Users\nicho\source\repos\MeleeOnlyBot\oauth.txt");
            MessageHandler messageHandler = new MessageHandler(irc);

            irc.joinRoom("umber__");
            while (true)
            {
                messageHandler.Message(irc.readMessage());
            }
        }

    }

    
}
