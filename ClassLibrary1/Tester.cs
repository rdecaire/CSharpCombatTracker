using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Data.SQLite;
using Newtonsoft.Json;


namespace ConsoleTest
{
    class Tester
    {
        
        static void Main(string[] args)
        {
            Character char1 = new Character("Bob", 7);
            Character char2 = new Character("Dave", 8);
            Character char3 = new Character("Jim", 4);
            char3.ApplyEffect(Ongoing.Keyword.ACID, Ongoing.EffectType.ONGOING, 5, 2);
            char3.ApplyEffect(Ongoing.Keyword.COLD, Ongoing.EffectType.DEFPEN, 3, 1);

            
            Console.WriteLine(char1.Name);
            Console.WriteLine(char3.NumEffects);
            Ongoing effect1 = char3.FindEffect(Ongoing.EffectType.ONGOING);
            Console.WriteLine(effect1.Modifier);
            
            List<Character> list1 = new List<Character>();
            list1.Add(char1);
            list1.Add(char2);
            list1.Add(char3);

            /*FileStream flStream = new FileStream("file.dat", FileMode.Create, FileAccess.Write);
            FileStream getStream = new FileStream("file.dat", FileMode.Open, FileAccess.Read);
            try
            {
                Type[] extraTypes = new Type[5];
                extraTypes[0] = typeof(List<Ongoing>);
                extraTypes[1] = typeof(Ongoing);
                extraTypes[2] = typeof(Ongoing.EffectType);
                extraTypes[3] = typeof(Ongoing.Keyword);
                extraTypes[4] = typeof(String);
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<Character>), extraTypes);
                xmlFormatter.Serialize(flStream, list1);
                List<Character> newlist = new List<Character>();
                xmlFormatter.Deserialize(getStream);
            }
            finally
            {
                flStream.Close();
                getStream.Close();
            }
            */
            string output = JsonConvert.SerializeObject(list1);
            using (FileStream fs = File.Create("file.txt"))
            {
                AddText(fs, output);
            }

            
            List<Character> newlist = JsonConvert.DeserializeObject<List<Character>>(output);

            Character newchar3 = newlist[2];
            Console.WriteLine(newchar3.NumEffects);
            Ongoing neweffect1 = newchar3.FindEffect(Ongoing.EffectType.ONGOING);
            Ongoing neweffect2 = newchar3.FindEffect(Ongoing.EffectType.DEFPEN);

            Console.WriteLine(neweffect1.Modifier);
            Console.WriteLine(neweffect1.Key);
            Console.WriteLine(neweffect1.Type);
            Console.WriteLine(neweffect2.Modifier);
            Console.WriteLine(neweffect2.Key);
            Console.WriteLine(neweffect2.Type);
            Console.ReadKey();
            

        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
