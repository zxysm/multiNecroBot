using System;
using System.Collections.Generic;
using System.IO;

namespace multiNecroBot
{
    class Program
    {
        static void Main(string[] args)
        {
            string mainDir = Directory.GetCurrentDirectory();
            string sessionsJsonPath = mainDir + "\\sessions.json";
            List<Auth> authList = new List<Auth>();
            List<Istance> istanceList = new List<Istance>();
            Boolean isEmpty = true;

            writeColor("Welcome in multiNecroBot", ConsoleColor.White);
            writeColor("Current directory " + mainDir, ConsoleColor.White);

            Auth prova = new Auth();

            authList = multiNecroBot.Json.ReadAuthJson(sessionsJsonPath);

            foreach (Auth a in authList)
            {
                try {
                    istanceList.Add(new Istance(a));
                    if (isEmpty)
                    {
                        if (a.authType == "ptc" || a.authType == "google")
                        {
                            isEmpty = false;
                        }
                    }
                }catch(Exception e)
                {
                    writeColor("Error: empty sessions file", ConsoleColor.Red);
                    return;
                }
            }

            if (!isEmpty)
            {
                startIstances(istanceList);
            }
            else
            {
                writeColor("Error: can't start session, because the sessions file is empty", ConsoleColor.Red);
                while (!Console.KeyAvailable)
                {
                    //a
                };
            }
            
        }

        public static void startIstances(List<Istance> istanceList)
        {
            writeColor("Press a key to start sessions..", ConsoleColor.White);

            while (!Console.KeyAvailable)
            {
                //a
            };

            try
            {
                createBat(istanceList);
                System.Diagnostics.Process.Start("START.bat");

            }
            catch (Exception e)
            {
                writeColor("Error while creating start.bat", ConsoleColor.Red);
            }
        }

        public static void createBat(List<Istance> a)
        {
            string bat = "";
            foreach (Istance b in a)
            {
                bat += "cd " + b.istancePath + "\n" + "start NecroBot.exe \n";
                writeColor("Started process of: " + b.auth.googleUsername + b.auth.ptcUsername, ConsoleColor.Green);
            }
            if (File.Exists(Directory.GetCurrentDirectory() + "\\START.bat"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\START.bat");
            }

            TextWriter tw = new StreamWriter(Directory.GetCurrentDirectory() + "\\START.bat", true);
            tw.WriteLine(bat);
            tw.Close();
        }

        public static void writeColor(String content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(content);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
