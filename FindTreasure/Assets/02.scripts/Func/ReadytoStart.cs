using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadytoStart 
{
    private static string Directorypath = "{0}/JPG/{1}";

    private static string SetString(string competition) => string.Format(Directorypath, Application.persistentDataPath, competition);

    public static void Ready(string competition)
    {
        try {
            var Dirpath = SetString(competition);

            Directory.Delete(Dirpath, true);

            Directory.CreateDirectory(Dirpath);
        } catch (DirectoryNotFoundException e)
        {
            string path = SetString("");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = SetString(competition);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}
