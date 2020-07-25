using System;
using System.Collections;
using System.Collections.Generic;

namespace TTM.Classes
{
    public class Competition
    {
        public string Name { get; set; }
        public bool Mode { get; set; }
        public int MaxMember { get; set; }
        public string Password { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Info { get; set; }
        public int UserPass { get; set; }
        public int wordNumber { get; set; }

        #region Set Times
        public void setNowStart() { StartTime = DateTime.Now; }

        public void setNowEnd() { EndTime = DateTime.Now; }
        #endregion

        public override string ToString()
        {
            string compLog = "";

            compLog = $"Name : {Name}\n";
            compLog += "Mode : " + (Mode ? "Team" : "Individual") + "\n";
            if (Mode) compLog += $"MaxMember : {MaxMember} \n";
            compLog += $"Password : {Password}";

            return compLog;
        }
    }

    public class QuizInfo
    {
        public string Str { get; set; }
        public int Score { get; set; }
        public int Kind { get; set; }
        public string[] List { get; set; }
        public bool Banswer { get; set; }
        public int Ianswer { get; set; }
        public string Wanswer { get; set; }

        public void SetMax() { Score = 30;}

        public void SetListInit(){
            if (Kind == 1)
                List = new string[4];
        }
    }

    public class Quiz
    {
        public string Str { get; set; }
        public int Kind { get; set; }
        public string[] List { get; set; }

        public bool CopyList(string[] list)
        {
            if (Kind == 1)
            {
                List = (string[])list.Clone();
                return true;
            }
            return false;
        }
    }

    public class Answer
    {
        public int Kind { get; set; }
        public int Score { get; set; }
        public bool Banswer { get; set; }
        public int Ianswer { get; set; }
        public string Wanswer { get; set; }

    }

    public class Q
    {
        public string Title { get; set; }
        public string Str { get; set; }
        public int? Score { get; set; }
        public int? Kind { get; set; }
        public String[] List { get; set; }
        public object Answer { get; set; }

        public override string ToString()
        {
            string quiz = "";
            quiz += $"Title : {Title}\n";
            quiz += $"Str : {Str}\n";
            quiz += $"Score : {Score}\n";
            quiz += $"Kind : {Kind}\n";
            if(Kind ==1)
                for(int i = 0; i < 4; i++)
                {
                    quiz += $"List[{i}] : {List[i]}\n";
                }
            quiz += $"Answer : {Answer}";
            return base.ToString();
        }
    }
}