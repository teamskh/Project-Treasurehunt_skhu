using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class PlayerGameLog 
{
    public string Competition { get; } = "";
    Dictionary<string, int> PlayLog;
    public int Score { get 
        {
            int score = 0;
            foreach (var item in PlayLog.Values)
                score += item;
            return score;
        }
    }

    public PlayerGameLog(string comp)
    {
        Competition = comp;
        PlayLog = new Dictionary<string, int>();
    }

    public override string ToString()
    {
        var info = "Competition : " + Competition +'\n';
        
        foreach(var item in PlayLog)
        {
            info += $"name : {item.Key}, score : {item.Value}\n";
        }
        return info;
    }

    public void Record(string quizname, int score)
    {
        if(!PlayLog.ContainsKey(quizname))
            PlayLog.Add(quizname, score);
    }
    
    public List<string> SolvedQuizz()
    {
        return PlayLog.Keys.ToList();
    }

    public Recodes.Recode Summary()
    {
        Recodes.Recode recode = new Recodes.Recode(Competition,score : Score);

        //Rank 처리

        return recode;
    }
}
