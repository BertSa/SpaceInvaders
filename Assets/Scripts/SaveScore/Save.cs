using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveScore
{
    public static class Save
    {
        public static void SaveFile(int score, string player)
        {
            var destination = Application.persistentDataPath + "/save.dat";

            var file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);

            var data = new GameData(score, player);
            var bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }

        public static GameData LoadFile()
        {
            var destination = Application.persistentDataPath + "/save.dat";

            if (!File.Exists(destination))
            {
                Debug.LogError("File not found");
                return null;
            }

            var file = File.OpenRead(destination);

            var bf = new BinaryFormatter();
            var data = (GameData)bf.Deserialize(file);
            file.Close();

            return data;
        }
    }
}