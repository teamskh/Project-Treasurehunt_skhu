using System;
using System.Collections.Generic;
[Serializable]
public class PlayerGameLog 
{
    string Competition { get; } = "";
    int Size { get; } = 0;
    bool[] PlayLog { get; set; }
    public int Score { get; set; }

    public PlayerGameLog(string comp, int size)
    {
        Competition = comp;
        Size = size;
        PlayLog = new bool[size];
        Score = 0;
    }

    public override string ToString()
    {
        return "Competition : " + Competition + "  Size : " + Size;
    }

    public void Record(int quizid, int score)
    {
        PlayLog[quizid] = (score > 0) ? true : false;
        Score += score;
    }

    public Recodes.Recode Summary()
    {
        Recodes.Recode recode = new Recodes.Recode(Competition,score : Score);

        //Rank 처리

        return recode;
    }
}
