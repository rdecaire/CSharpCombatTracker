using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;


namespace Combat_Tracker
{
    class SaveAndLoad
    {
        string filename;
        public SaveAndLoad(string file)
        {
            filename = file;
        }
        public void Save(List<Character> listToSave){
            string output = JsonConvert.SerializeObject(listToSave);
            using (FileStream fs = File.Open(filename, FileMode.OpenOrCreate))
            {
                AddText(fs, output);
            }
        }

        public List<Character> Load()
        {
            string openCharacterListJson = null;
            List<Character> newlist;
            using (FileStream fs = File.Open(filename, FileMode.Open))
            {
                
                byte[] b = new byte[1024]; // make a byte array
                UTF8Encoding temp = new UTF8Encoding(true); // make a UTF8 Encoding object
                while (fs.Read(b, 0, b.Length) > 0) // while the buffer created by the FileStream is bigger than 0
                                                    // i.e. there are still characters in the stream...
                {
                    openCharacterListJson += temp.GetString(b); // add the byte array to the list string in UTF8 encoding
                }
            }
            newlist = JsonConvert.DeserializeObject<List<Character>>(openCharacterListJson);
            return newlist;
        }
        
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

    }
}