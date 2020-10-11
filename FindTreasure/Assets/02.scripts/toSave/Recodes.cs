using System;

namespace Recodes
{
    [Serializable]
    public class Recode
    {
        public string CompetitionName;
        public int Score;
        public DateTime ClearTime;
        public int Rank;

        public Recode(string comName, int score =0, int rank=0)
        {
            CompetitionName = comName;
            Score = score;
            ClearTime = DateTime.Now;
            Rank = rank;
        }
        public override string ToString()
        {
            var info = $"Competition : {CompetitionName} \n";
            info += $"Score : {Score}\n";
            info += $"ClearTime : {ClearTime}";
            return info;
        }
    }

}