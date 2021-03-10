using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DefaultNamespace;
using UnityEngine;

public class Save
{
    public static void SaveFile(int score,String player)
    {
        var destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);

        var data = new GameData(score,player);
        var bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public static GameData LoadFile()
    {
        var destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        var data = (GameData) bf.Deserialize(file);
        file.Close();

        return data;
    }
}