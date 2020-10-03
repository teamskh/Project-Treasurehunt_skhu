using System.IO;
using TTM.Classes;
using UnityEngine;

public class SaveImage
{
    Texture2D Saves;
    string Name;
    public bool NeedToSave = false;

    static SaveImage instance;

    public static SaveImage Instance
    {
        get
        {
            if (instance == null) instance = new SaveImage();
            return instance;
        }
    }

    public void SetNewTexture(Texture2D ss)
    {
        Saves = ss;
        if (ss.isReadable)
            Saves.Apply();
        NeedToSave = true;
    }
    public void SetName(string name)
    {
        Name = name;
    }

    void Save()
    {
        byte[] imageBytes = Saves.EncodeToJPG();
        DataPath path = new DataPath("JPG/" +AdminCurState.Instance.Competition, Name);
        if (!Directory.Exists(path[1]))
            Directory.CreateDirectory(path[1]);

        path.SetJPG();
        if (!File.Exists(path.ToString()))
            File.WriteAllBytes(path.ToString(), imageBytes);
        else{
            DataPath modpath = new DataPath(path.Dir(), "Modified", ".txt");
            if (!File.Exists(modpath.ToString())) File.Create(modpath.ToString());
            File.AppendAllText(modpath.ToString(), Name+'\n');
            FTP.ImageServerUpload(modpath);
        }
        Debug.Log("File Available: "+File.Exists(path.ToString()));
        FTP.ImageServerUpload(path);
        NeedToSave = false;
    }

    public void SaveIMG() => Save();

    public void SaveIMG(string name)
    {
        Name = name;
        Save();
    }


}
