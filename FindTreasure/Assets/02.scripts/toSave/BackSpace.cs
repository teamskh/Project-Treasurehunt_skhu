using System.Collections.Generic;

public class BackSpace : Stack<string>
{
    #region Private Variable
    #endregion

    #region Public Variable
    #endregion

    #region Instance
    private static BackSpace _Instance;
    #endregion

    #region Singleton
    public static BackSpace Instance
    {
        get
        {
            if (_Instance == null) _Instance = new BackSpace();
            return _Instance;
        }
    }
    #endregion

    public override string ToString()
    {
        string StackScenes = "";
        foreach(var item in this)
        {
            StackScenes += item + "\n";
        }
        return StackScenes;
    }
}
