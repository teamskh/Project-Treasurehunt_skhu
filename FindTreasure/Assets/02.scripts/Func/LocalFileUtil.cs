using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LocalFileUtil 
{
    private static string Modifiedpath = "{0}/JPG/{1}/Modified.txt";
    private static string Deletedpath = "{0}/JPG/{1}/Deleted.txt";
    private static string Filepath = "{0}/JPG/{1}/{2}.jpg";

    private static string SetString(string format, string competition)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/JPG"))
            Directory.CreateDirectory(Application.persistentDataPath + "/JPG");
        if (!Directory.Exists(Application.persistentDataPath + "/JPG/" + competition))
            Directory.CreateDirectory(Application.persistentDataPath + "/JPG/" + competition);
        return string.Format(format, Application.persistentDataPath, competition);
    } 

    private static void Add(string competiton, string quiz,string format)
    {
        string path = SetString(format, competiton);
        if (!File.Exists(path))
        {
            File.Create(path);
        }

        using(StreamWriter writer =new StreamWriter(new FileStream(path, FileMode.Append)))
        {
            writer.WriteLine(quiz);
        }
    }

    public static void ModifiedAdd(string competition, string quiz) => Add(competition, quiz, Modifiedpath);

    public static void DeletedAdd(string competition, string quiz) => Add(competition, quiz, Deletedpath);

    private static List<string> GetNames(string competition,string format)
    {
        List<string> list = new List<string>();
        var path = SetString(format, competition);

        if (!File.Exists(path)) return null;

        using(StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
        {
            while (!reader.EndOfStream)
            {
                list.Add(reader.ReadLine());
            }
        }
        return list;
    }

    private static void SaveList(string competition,string format,List<string> list)
    {
        var path = SetString(format, competition);
        
        using(StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Truncate)))
        {
            foreach(var name in list)
            {
                writer.WriteLine(name);
            }
        }
    }

    private static void Remove(string competition,string quiz, string format)
    {
        List<string> list = GetNames(competition, format);

        if (list == null) return;
        if (!list.Remove(quiz)) return;

        SaveList(competition, format, list);
    }

    public static void ModifiedRemove(string competition, string quiz) => Remove(competition, quiz, Modifiedpath);
    public static void DeletedRemove(string competition, string quiz) => Remove(competition, quiz, Deletedpath);
    
    public static void FileRename(string competition,string before, string after)
    {
        string beforepath = string.Format(Filepath, Application.persistentDataPath, competition, before);
        string afterpath = string.Format(Filepath, Application.persistentDataPath, competition, after);

        if (File.Exists(beforepath))
        {
            File.Move(beforepath, afterpath);
        }
    }
}
