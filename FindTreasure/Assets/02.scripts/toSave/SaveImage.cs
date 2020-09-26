using System.IO;
using TTM.Classes;
using UnityEngine;

public class SaveImage
{
    Texture2D Saves;
    string Name;

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
        Saves.Apply();
    }
    public void SetName(string name)
    {
        Name = name;
    }

    void Save()
    {
        byte[] imageBytes = Saves.EncodeToJPG();
        DataPath path = new DataPath("JPG/" +AdminCurState.Instance.Competition, Name);

        path.SetJPG();
        if (!File.Exists(path.ToString()))
            File.WriteAllBytes(path.ToString(), imageBytes);
        else{
            DataPath modpath = new DataPath(path.Dir(), "Modified", ".txt");
            if (!File.Exists(modpath.ToString())) File.Create(modpath.ToString());
            File.AppendAllText(modpath.ToString(), Name+'\n');
            FTP.FtpUploadUsingwebClient(modpath);
        }
        Debug.Log("File Available: "+File.Exists(path.ToString()));
        FTP.FtpUploadUsingwebClient(path);
    }

    public void SaveIMG() => Save();

    public void SaveIMG(string name)
    {
        Name = name;
        Save();
    }

    public static void DeleteIMG(string name)
    {
        DataPath path = new DataPath("JPG/" +AdminCurState.Instance.Competition,name);
        path.SetJPG();
        if (File.Exists(path.ToString())){
            File.Delete(path.ToString());

            DataPath delpath = new DataPath(path.Dir(), "Deleted", ".txt");
            if (!File.Exists(delpath.ToString())) File.Create(delpath.ToString());
            File.AppendAllText(delpath.ToString(), name+'\n');
            FTP.FtpUploadUsingwebClient(delpath);

            FTP.FtpDeleteIMG(path);
        }
    }
}
