using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public class PQuizDicitionary:Dictionary<string, Quiz>
    {
        public string Competition { get; set; }
        
        public void GetQuizz(string competition)
        {
            Param where = new Param();
            where.Add("name", competition);
            
        }
    }


    //관리자 Competition 전용 클래스
    public class CompetitionDictionary : Dictionary<string, Competition>, ITTMDictionary
    {
        //대회 이름을 변경할 때 혹은 이름에 해당하는 코드를 얻을 때 사용할 변수
        public Dictionary<string, int> transCode = new Dictionary<string, int>();

        #region GetCompetitions
        //대회 목록 받아오기
        public void GetCompetitions()
        {
            BackendReturnObject bro = new BackendReturnObject();
            bro = Backend.GameSchemaInfo.Get("competitions", new Param(), 100);
            if (bro.IsSuccess())
            {
                JsonData data = bro.GetReturnValuetoJSON()["rows"];
                foreach (JsonData item in data)
                {
                    var it = GetCompetition(item);
                    if (it != null)
                        Add(it.Name, it);
                }
            }
        }

        //대회 개별 해독하기 ver. Admin
        protected virtual Competition GetCompetition(JsonData data)
        {
            Debug.Log("Competition Dictionary Call");
            Competition comp = new Competition();
            comp.Name = data["name"]["S"].ToString();
            comp.Mode = bool.Parse(data["mode"]["BOOL"].ToString());
            if (comp.Mode) comp.MaxMember = int.Parse(data["maxmember"]["N"].ToString());
            comp.Password = data["password"]["S"].ToString();

            var code = int.Parse(data["code"]["N"].ToString());
            transCode.Add( comp.Name, code);

            DateTime date;
            if (data.Keys.Contains("starttime"))
                if (DateTime.TryParse(data["starttime"]["S"].ToString(), out date))
                    comp.StartTime = date;
            if (data.Keys.Contains("endtime"))
                if (DateTime.TryParse(data["endtime"]["S"].ToString(), out date))
                    comp.EndTime = date;

            if (data.Keys.Contains("info"))
            {
                if (data["info"].Keys.Contains("NULL")) comp.Info = null;
                else comp.Info = data["info"]["S"].ToString();
            }
            if (data.Keys.Contains("userpass"))
                comp.UserPass = int.Parse(data["userpass"]["N"].ToString());

            return comp;
        }
        #endregion

        #region ITTMDictionary 
        //기존의 코드들과 겹치는 값이 있는지 확인
        public bool AvailableCode(int code)
        {
            foreach (var tcode in transCode.Values)
                if (tcode == code) return false;
            return true;
        }

        //버튼을 눌렀을 때 현재의 대회 코드 선택하는 함수
        public void CurrentCode(string name)
        {
            int code = -1;
            if (transCode.TryGetValue(name, out code))
                PlayerPrefs.SetInt("a_competition", code);
            // a_competition에 저장된 int 값을 where용 Param.Add("code", code)의 code변수 값으로 사용
            Debug.Log($"a_Competition : {code}");
        }
        #endregion
    }

    public class PCompetitionDictionary : CompetitionDictionary
    {
        protected override Competition GetCompetition(JsonData data)
        {
            Debug.Log("PCompetitionDictionary Call");
            Competition comp = new Competition();
            comp.Name = data["name"]["S"].ToString();
            comp.Mode = bool.Parse(data["mode"]["BOOL"].ToString());
            if (comp.Mode) comp.MaxMember = int.Parse(data["maxmember"]["N"].ToString());

            DateTime date;
            if (data.Keys.Contains("starttime"))
                if (DateTime.TryParse(data["starttime"]["S"].ToString(), out date))
                    if (DateTime.Compare(date, new DateTime()) == 0)
                        return null;
                    else
                        comp.StartTime = date;
                
            if (data.Keys.Contains("endtime"))
                if (DateTime.TryParse(data["endtime"]["S"].ToString(), out date))
                    comp.EndTime = date;

            if (data.Keys.Contains("info"))
            {
                if (data["info"].Keys.Contains("NULL")) comp.Info = null;
                else comp.Info = data["info"]["S"].ToString();
            }
            if (data.Keys.Contains("userpass"))
                comp.UserPass = int.Parse(data["userpass"]["N"].ToString());

            var code = int.Parse(data["code"]["N"].ToString());
            transCode.Add(comp.Name,code);

            return comp;
        }
    }

    public interface ITTMDictionary
    {
        bool AvailableCode(int code);

       void CurrentCode(string name);
    }
}