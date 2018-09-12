using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwitchBot
{
    class Program
    {
        static void Main(string[] args)
        {
            IrcClient irc = new IrcClient("irc.chat.twitch.tv", 6667, "meleeonlybot", @"C:\Users\nicho\source\repos\TwitchBot\oauth.txt");
            MessageHandler messageHandler = new MessageHandler(irc);

            irc.joinRoom("umber__");
            while(true)
            {
                messageHandler.Message(irc.readMessage());          
            }
        }


    }
}
