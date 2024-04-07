using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/saves/";

    public static readonly string FILE_EXIT = ".json";

    public static void Initialize()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string filename, string data)
    {
        File.WriteAllText(SAVE_FOLDER + filename + FILE_EXIT, data);
    }

    public static string Load(string filename)
    {
        string fileLocation = SAVE_FOLDER + filename + FILE_EXIT;

        if (File.Exists(fileLocation))
        {
            string loadedData = File.ReadAllText(fileLocation);

            return loadedData;
        }

        return null;
    }
}
