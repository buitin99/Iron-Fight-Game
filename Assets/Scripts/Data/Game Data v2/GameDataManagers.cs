using System.IO;
using UnityEngine;

public class GameDataManagers<T> where T : class, new ()
{
    public static T LoadData ()
    {
        string path = Application.persistentDataPath +  $"/Saves/{typeof(T).Name}.sav";
        // Does the file exist?
        if (File.Exists(path))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(path);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            T data = JsonUtility.FromJson<T>(fileContents);
            return data;
        }
        else
        {
            T newData = new T();
            return newData;
        }
    }

    public static void SaveData (T data)
    {
        if(!Directory.Exists(Application.persistentDataPath + "/Saves")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
        }
        string path = Application.persistentDataPath + $"/Saves/{typeof(T).Name}.sav";
        Debug.Log(path);
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(data);

        // Write JSON to file.
        File.WriteAllText(path, jsonString);
    }
}
