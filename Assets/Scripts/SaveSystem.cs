using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData (Highscores save) // Sets up the Stream (file pathway) to the saving location
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.rev";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, save);
        Debug.Log("created");
        stream.Close();
    }
    public static Highscores LoadData() // Finds the random location to save the file and loads the Data
    {
        string path = Application.persistentDataPath + "/save.rev";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Highscores foundLevels = formatter.Deserialize(stream) as Highscores;
            stream.Close();
            return foundLevels;
        }
        else
        {
            Highscores foundLevels = new Highscores();
            return foundLevels;
        }
    }
}
