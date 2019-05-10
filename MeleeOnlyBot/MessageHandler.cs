using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwitchBot
{
    class MessageHandler
    {
        IrcClient irc;
        CommandHandler commandHandler;
        Random landmine;

        public MessageHandler(IrcClient irc)
        {
            this.irc = irc;
            commandHandler = new CommandHandler(irc);
            landmine = new Random();
        }

        public void Message(string message)
        {
            if (message.Contains("PING :tmi.twitch.tv"))
            {
                irc.sendIrcMessage("PONG :tmi.twitch.tv");
            }

            else if (message.Contains("~"))
            {
                commandHandler.Command(message);
            }

            else if ((message.ToLower().Contains("hotdog") || message.ToLower().Contains("hot dog")) && (message.ToLower().Contains("sandwhich") || message.ToLower().Contains("sandwich")) && message.ToLower().Contains("is") && message.ToLower().Contains(" a "))
            {
                Hotdog();
            }

            else if(message.Contains("PRIVMSG #umber__")) 
            {
                if (landmine.Next(5000) == 0)
                {
                    irc.sendChatMessage("/timeout " + message.Substring(1, message.IndexOf("!") - 1) + " 10");
                    irc.sendChatMessage("Oops! " + message.Substring(1, message.IndexOf("!") - 1) + " has stepped on a landmine!");
                }
            }

            
        }

      

        public void Hotdog()
        {
            var hotDogGod = new Random();

            if (hotDogGod.Next(2) == 0)
            {
                irc.sendChatMessage("No. A hot dog is absolutely in no way a sandwich.");
            }
            else
            {
                irc.sendChatMessage("Yes. A hot dog is totally a sandwich.");
            }
        }

    }
}







