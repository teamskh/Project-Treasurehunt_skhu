using System;
using System.Collections;
using System.Collections.Generic;
using ClassesTTMExtension;

namespace TTM.Classes
{
    public class Competition
    {
        public bool Mode { get; set; }
        public int MaxMember { get; set; }
        public string Password { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Info { get; set; }
        public int Userword { get; set; }
        public int wordNumber { get; set; }

        #region Set Times
        public void setNowStart() { StartTime = DateTime.Now; }

        public void setNowEnd() { EndTime = DateTime.Now; }
        #endregion
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

        #region Answer Extention
        public Ans GetAns()
        {
            if(Kind ==0){ return new AnsB(Banswer); }
            else if(Kind == 1) { return new AnsI(Ianswer); }
            else if(Kind == 2) { return new AnsW(Wanswer); }
            return null;
        }

        #endregion
    }


}