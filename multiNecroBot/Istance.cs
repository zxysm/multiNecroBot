using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace multiNecroBot
{
    class Istance
    {
        //directory path of the istance
        public string istancePath = "";
        public string exe_Path = "";
        public String mainDir = Directory.GetCurrentDirectory() + "\\";
        public string nativeDir = Directory.GetCurrentDirectory() + "\\relase\\";
        public Auth auth;

        public Istance(Auth auth)
        {
            this.auth = auth;
            init();
        }

        public String printInfo()
        {
            String a = "Istance Username = " + auth.ptcUsername + auth.googleUsername + "\n" +
                "Istance dir = " + istancePath;
            return a;
        }

        private void init()
        {
            if (auth.authType == "ptc")
            {
                this.istancePath = mainDir + auth.ptcUsername + "\\";
            }
            else if (auth.authType == "google")
            {
                this.istancePath = mainDir + auth.googleUsername + "\\";
            }
            else
            {
                throw new Exception("empty istance");
                Program.writeColor("Empty auth in this istance", ConsoleColor.Red);
                return;
            }

            if (!Directory.Exists(this.istancePath))
            {
                Program.writeColor("Adding istance path " + istancePath, ConsoleColor.DarkBlue);
                createIstancePath();
                Program.writeColor("Istance suceffully created!", ConsoleColor.Green);
                Program.writeColor(printInfo(), ConsoleColor.Green);
            }

            this.exe_Path = this.istancePath + "NecroBot.exe";
        }

        private void modifyAuthFile()
        {
            Json.writeAuthJson(istancePath + "config\\auth.json", auth);
            Program.writeColor("Writed auth.json: " + istancePath + "config\\auth.json", ConsoleColor.DarkBlue);
        }

        private void createIstancePath()
        {
            try {
                Directory.CreateDirectory(istancePath);
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(nativeDir, "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(nativeDir, istancePath));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(nativeDir, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(nativeDir, istancePath), true);
            } catch (Exception e)
            {
                Program.writeColor("Error copying native program..", ConsoleColor.Red);
                return;
            }

            modifyAuthFile();
        }

        public string getPath()
        {
            return this.istancePath;
        }
    }
}
