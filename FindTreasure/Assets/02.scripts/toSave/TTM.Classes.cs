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

        //---------------------------추가(종료시간, 대회이름, 최대점수)--------------------------//
        public String ConName { get; set; }
        public int MaxScore { get; set; }
        public DateTime EndingTime { get; set; }

        #region Set Times
        public void setNowStart() { StartTime = DateTime.Now; }

        public void setNowEnd() {
            EndTime = DateTime.Now;
        }
        #endregion

        public override string ToString()
        {
            string compLog = "";

            compLog = $"Name : {Name}\n";
            compLog += "Mode : " + (Mode ? "Team" : "Individual") + "\n";
            if (Mode) compLog += $"MaxMember : {MaxMember} \n";
            compLog += $"Password : {Password}";

            //---------------------------추가(종료시간, 대회이름, 최대점수)--------------------------//
            compLog += $"Title : {ConName}\n";
            compLog += $"MaxScore : {MaxScore}\n";
            compLog += $"EndingTime : {EndingTime}\n";

            return compLog;
        }
    }
    

    #region Data Classes ver2.0
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
     
    public class QuiDictionary : Dictionary<string, Q>,ITTMDictionary
    {
        public Dictionary<string, int> transCode = new Dictionary<string, int>();
        public int Competition { get; set; }

        public QuiDictionary() => Competition = -1;
        public QuiDictionary(int code) => Competition = code;

        public bool GetQuizz(int competition)
        {
            this.Clear();
            transCode.Clear();
            //where 조건 설정
            Param where = new Param();
            where.Add("idcompetition", competition);

            //데이터 조정
            BackendReturnObject bro = new BackendReturnObject();
            bro = Backend.GameSchemaInfo.Get("Quizz", where, 100);
            if (bro.IsSuccess())
            {
                JsonData data = bro.GetReturnValuetoJSON()["rows"];
                foreach (JsonData item in data)
                {
                    var it = GetQuiz(item);
                    Add(it.Title, it);
                }
                Debug.Log(data.ToJson());
                return true;
            }
            else
            {
                Debug.Log(bro.ToString());
                return false;
            }
        }
        
        protected virtual Q GetQuiz(JsonData quiz)
        {
            Q item = new Q();

            item.Title = quiz["title"]["S"].ToString();

            var quizcode = quiz["idquiz"]["N"].ToString();
            transCode.Add(item.Title, int.Parse(quizcode));

            item.Str = quiz["context"]["S"].ToString();

            item.Kind = int.Parse(quiz["kind"]["N"].ToString());

            item.Score = int.Parse(quiz["score"]["N"].ToString());

            var ans = quiz["answer"]["S"].ToString();
            switch (item.Kind)
            {
                case 0:
                    item.Answer = bool.Parse(ans);
                    break;
                case 1:
                    item.Answer = int.Parse(ans);
                    JsonData data = quiz["choices"]["L"];
                    var count = data.Count;
                    item.List = new string[4];
                    for (int i = 0; i < count; i++)
                    {
                        item.List[i] = data[i]["S"].ToString();
                    }
                    break;
                case 2:
                    item.Answer = ans;
                    break;
            }

            return item;
        }

        public bool AvailableCode(int code)
        {
            foreach (var tcode in transCode.Values)
                if (tcode == code) return false;
            return true;
        }

        public virtual int CurrentCode(string name)
        {
            int code = -1;
            if (transCode.TryGetValue(name, out code))
                PlayerPrefs.SetInt("a_quiz", code);
            Debug.Log($"a_quiz : {code}");
            return code;
        }
    }

    public class PQuizDicitionary:QuiDictionary
    {
        public PQuizDicitionary() : base() { }
        public PQuizDicitionary(int code) : base(code) { }
        protected override Q GetQuiz(JsonData quiz)
        {
            Q item = new Q();

            item.Title = quiz["title"]["S"].ToString();

            var quizcode = quiz["idquiz"]["N"].ToString();
            transCode.Add(item.Title, int.Parse(quizcode));

            item.Str = quiz["context"]["S"].ToString();

            item.Kind = int.Parse(quiz["kind"]["N"].ToString());

            if(item.Kind == 1)
            {
                JsonData data = quiz["choices"]["L"];
                var count = data.Count;
                item.List = new string[4];
                for (int i = 0; i < count; i++)
                {
                    item.List[i] = data[i]["S"].ToString();
                }
            }

            return item;
        }
        public override int CurrentCode(string name)
        {//정답 체크시
            int code = -1;
            if (transCode.TryGetValue(name, out code))
                PlayerPrefs.SetInt("p_quiz", code);
            Debug.Log($"p_quiz : {code}");
            return code;
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
            this.Clear();
            transCode.Clear();
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
                if (DateTime.TryParse(data["endtime"]["S"].ToString(), out date))   comp.EndTime = date;

            if (data.Keys.Contains("info"))
            {
                if (data["info"].Keys.Contains("NULL")) comp.Info = null;
                else comp.Info = data["info"]["S"].ToString();
            }
            if (data.Keys.Contains("userpass"))
                comp.UserPass = int.Parse(data["userpass"]["N"].ToString());

            /*-----------------------------------------추가------------------------------------------------------*/
            comp.ConName = comp.Name;
            //comp.MaxScore;
            comp.EndingTime = comp.EndTime;

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
        public virtual int CurrentCode(string name)
        {
            int code = -1;
            if (transCode.TryGetValue(name, out code))
                PlayerPrefs.SetInt("a_competition", code);
            // a_competition에 저장된 int 값을 where용 Param.Add("code", code)의 code변수 값으로 사용
            Debug.Log($"a_Competition : {code}");
            return code;
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

            /*-----------------------------------------추가------------------------------------------------------*/
            comp.ConName = comp.Name;
            //comp.MaxScore;
            comp.EndingTime = comp.EndTime;

            return comp;
        }
        public override int CurrentCode(string name)
        {
            int code = -1;
            if (transCode.TryGetValue(name, out code))
                PlayerPrefs.SetInt("p_competition", code);
            Debug.Log($"p_Competition : {code}");
            return code;
        }
    }

    public interface ITTMDictionary
    {
        bool AvailableCode(int code);

       int CurrentCode(string name);
    }

    #region AnswerClass
    public class SAnswer
    {
       public static int CheckAnswer(int idcompetition, KeyValuePair<int,string> ans)
        {
            Param where = new Param();
            where.Add("idcompetition", idcompetition);
            where.Add("idquiz", ans.Key);

            BackendReturnObject bro = new BackendReturnObject();
            Backend.GameSchemaInfo.Get("Quizz", where, 1);
            if (bro.IsSuccess())
            {
                JsonData data = bro.GetReturnValuetoJSON()["row"];
                var answer = data["answer"]["S"].ToString();
                if(answer == ans.Value)
                {
                    var score = data["score"]["N"].ToString();
                    return int.Parse(score);
                }
                else
                {
                    Debug.Log($"Wrong Answer : {ans} != {answer}");
                    return 0;
                }
            }
            else
            {
                Debug.Log("Deleted Quizz");
                return -1;
            }
        }
    }

    #endregion
    #endregion
}