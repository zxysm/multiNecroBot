using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiNecroBot
{
    class Json
    {
        public static List<Auth> ReadAuthJson(String jsonPath)
        {
            try
            {
                using (StreamReader r = new StreamReader(jsonPath))
                {
                    string json = r.ReadToEnd();
                    List<Auth> auths = JsonConvert.DeserializeObject<List<Auth>>(json);
                    return auths;
                }
            }
            catch (Exception e)
            {
                Program.writeColor("Error while reading account list: " + jsonPath, ConsoleColor.Red);
                return new List<Auth>();
            }
        }

        public static void writeAuthJson(String jsonPath, Auth auth)
        {
            try
            {
                string json = "{\n\t";
                json += "\"AuthType\": " + "\"" + auth.authType + "\",\n\t";
                if (auth.authType == "ptc")
                {
                    json += "\"GoogleUsername\": " + "\"\"" + ",\n\t";
                    json += "\"GooglePassword\": " + "\"\"" + ",\n\t";
                    json += "\"PtcPassword\": " + "\"" + auth.ptcPassword + "\",\n\t";
                    json += "\"PtcUsername\": " + "\"" + auth.ptcUsername + "\"\n}";
                }
                else if (auth.authType == "google")
                {
                    json += "\"GoogleUsername\": " + "\"" + auth.googleUsername + "\",\n\t";
                    json += "\"GooglePassword\": " + "\"" + auth.googlePassword + "\",\n\t";
                    json += "\"PtcPassword\": " + "\"\"" + ",\n\t";
                    json += "\"PtcUsername\": " + "\"\"" + "\n}";
                }


                //write string to file
                System.IO.File.WriteAllText(jsonPath, json);
            }
            catch (Exception e)
            {
                Program.writeColor("Error while writing auth.json: " + jsonPath, ConsoleColor.Red);
            }
        }
    }
}
