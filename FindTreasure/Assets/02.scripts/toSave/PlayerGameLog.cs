using System;
using System.Collections.Generic;
[Serializable]
public class PlayerGameLog 
{
    string Competition { get; } = "";
    int Size { get; } = 0;
    bool[] PlayLog { get; set; }

    public PlayerGameLog(string comp, int size)
    {
        Competition = comp;
        Size = size;
        PlayLog = new bool[size];
    }

    public override string ToString()
    {
        return "Competition : " + Competition + "  Size : " + Size;
    }
}
