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

    public void SetNewTexture(Texture2D ss,bool needtosave =true)
    {
        Saves = ss;
        if (ss.isReadable)
            Saves.Apply();
        NeedToSave = needtosave;
    }

    public Texture2D getTexture() => Saves;

    public void SetName(string name)
    {
        Name = name;
    }

    void Save()
    {
        if (!Saves.isReadable)
            Saves = CopyTexture(Saves);
        byte[] imageBytes = Saves.EncodeToJPG();
        DataPath path = new DataPath("JPG/" +AdminCurState.Instance.Competition, Name);
        if (!Directory.Exists(path[1]))
            Directory.CreateDirectory(path[1]);
        path.SetJPG();

        File.WriteAllBytes(path.ToString(), imageBytes);
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

    public Texture2D CopyTexture(Texture2D texture)
    {
        // Create a temporary RenderTexture of the same size as the texture
        RenderTexture tmp = RenderTexture.GetTemporary(
                            texture.width,
                            texture.height,
                            0,
                            RenderTextureFormat.Default,
                            RenderTextureReadWrite.Linear);

        // Blit the pixels on texture to the RenderTexture
        Graphics.Blit(texture, tmp);

        // Backup the currently set RenderTexture
        RenderTexture previous = RenderTexture.active;

        // Set the current RenderTexture to the temporary one we created
        RenderTexture.active = tmp;

        // Create a new readable Texture2D to copy the pixels to it
        Texture2D myTexture2D = new Texture2D(texture.width, texture.height);

        // Copy the pixels from the RenderTexture to the new Texture
        myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        myTexture2D.Apply();

        // Reset the active RenderTexture
        RenderTexture.active = previous;

        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tmp);

        // "myTexture2D" now has the same pixels from "texture" and it's readable.

        return myTexture2D;
    }
}
