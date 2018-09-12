using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwitchBot
{
    class CommandHandler
    {
        IrcClient irc;

        public CommandHandler(IrcClient irc)
        {
            this.irc = irc;
        }

        public void Command(String message)
        {
            if (message.Contains("~quote"))
            {
                Quote();
            }

            else if (message.Contains("~addquote "))
            {
                AddQuote(message);
            }

            else if (message.Contains("~rmquote "))
            {
                RemoveQuote(message);
            }

            else if (message.Contains("~super"))
            {
                Superpower(message);
            }

            else if (message.Contains("~seppuku"))
            {
                Seppuku(message);
            }

            else if (message.Contains("~8ball"))
            {
                EightBall();
            }

            else if (message.Contains("~commands"))
            {
                CommandsList();
            }
        }

        public void Quote()
        {
            StreamReader quoteReader = new StreamReader(@"C:\Users\nicho\source\repos\TwitchBot\Quote.txt");

            string quote = "No quotes have been added yet";
            string line = "";
            int quoteNumber = 0;
            int quotesVisited = 0;
            var rng = new Random();

            while ((line = quoteReader.ReadLine()) != null)
            {
                if (rng.Next(++quotesVisited) == 0)
                {
                    quote = line;
                    quoteNumber = quotesVisited;
                }
            }

            quoteReader.Close();
            irc.sendChatMessage("#" + quoteNumber + ": " + quote);
        }

        public void AddQuote(string message)
        {
            string quote = message.Substring(message.IndexOf("~addquote") + 10);

            using (StreamWriter quoteWriter = File.AppendText(@"C:\Users\nicho\source\repos\TwitchBot\Quote.txt"))
            {
                quoteWriter.WriteLine(quote);
            }
        }

        public void RemoveQuote(string message)
        {
            string quoteNumber = message.Substring(message.IndexOf("~rmquote") + 9);
            string line = "";
            int lineNumber = 0;

            if (quoteNumber.All(Char.IsDigit))
            {
                int lineToDelete = int.Parse(quoteNumber);
                using (StreamReader reader = new StreamReader(@"C:\Users\nicho\source\repos\TwitchBot\Quote.txt"))
                {
                    using (StreamWriter writer = new StreamWriter(@"C:\Users\nicho\source\repos\TwitchBot\TempQuote.txt"))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (++lineNumber == lineToDelete)
                            {
                                continue;
                            }

                            writer.WriteLine(line);
                        }
                    }
                }
                File.Delete(@"C:\Users\nicho\source\repos\TwitchBot\Quote.txt");
                File.Move(@"C:\Users\nicho\source\repos\TwitchBot\TempQuote.txt", @"C:\Users\nicho\source\repos\TwitchBot\Quote.txt");
            }
        }

        public void EightBall()
        {
            StreamReader answerReader = new StreamReader(@"C:\Users\nicho\source\repos\TwitchBot\EightBall.txt");

            string answer = "";
            string line = "";
            int visited = 0;
            var rng = new Random();

            while ((line = answerReader.ReadLine()) != null)
            {
                if (rng.Next(++visited) == 0)
                {
                    answer = line;
                }
            }

            answerReader.Close();
            irc.sendChatMessage(answer);
        }

        public void Superpower(string message)
        {
            StreamReader powerReader = new StreamReader(@"C:\Users\nicho\source\repos\TwitchBot\Powers.txt");

            string power = "";
            string line = "";
            int powersVisited = 0;
            var rng = new Random();

            while ((line = powerReader.ReadLine()) != null)
            {
                if (rng.Next(++powersVisited) == 0)
                {
                    power = line;
                }
            }

            powerReader.Close();
            irc.sendChatMessage("@" + message.Substring(1, message.IndexOf("!") - 1) + " Your superpower is: " + power + "!");
        }

        public void Seppuku(string message)
        {
            irc.sendChatMessage("/timeout " + message.Substring(1, message.IndexOf("!") - 1) + " 10");
            irc.sendChatMessage(message.Substring(1, message.IndexOf("!") - 1) + " has commited seppuku.");
        }

        public void CommandsList()
        {
            irc.sendChatMessage("~quote  |  ~addquote \"quote\"  |  ~super  |  ~seppuku  |  ~8Ball");
        }
    }
}
