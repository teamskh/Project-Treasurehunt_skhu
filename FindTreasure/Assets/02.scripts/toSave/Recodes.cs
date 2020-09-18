using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TTM.Classes;

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
    }

}