using System.IO;
using TTM.Classes;
using UnityEngine;

public static class TextureExtension {
    public static Texture2D Load(this Texture2D dep , string compet, string quiz)
    {
        DataPath path = new DataPath("JPG/" + compet, quiz);
        path.SetJPG();
        byte[] bytetexture = File.ReadAllBytes(path.ToString());
        if (bytetexture.Length > 0)
        {
            dep.name = quiz;
            dep.LoadImage(bytetexture);
            return dep;
        }
        return null;
    }
}
