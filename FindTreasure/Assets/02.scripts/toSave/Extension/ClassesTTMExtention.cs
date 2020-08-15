using System.Collections;
using System.Collections.Generic;

namespace ClassesTTMExtension
{
    #region Answer Extension
    public abstract class Ans
    {
<<<<<<< Updated upstream
=======
        Q item = new Q();
        item.Title = quiz["title"]["S"].ToString();
       
        item.Str = quiz["context"]["S"].ToString();
        
        item.Kind = int.Parse(quiz["kind"]["N"].ToString());

        item.Score = int.Parse(quiz["score"]["N"].ToString());//추가

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
                for(int i = 0; i < count; i++)
                {
                    item.List[i] = data[i]["S"].ToString();
                }
                break;
            case 2:
                item.Answer = ans;
                break;
        }

        return item;
>>>>>>> Stashed changes
    }

    public class AnsB : Ans
    {
        bool Banswer { get; set; }
        public AnsB() { }
        public AnsB(bool b) { Banswer = b; }
        public bool GetAnswer() { return Banswer; }
    }
    public class AnsI : Ans
    {
        int Ianswer { get; set; }
        public AnsI() { }
        public AnsI(int i) { Ianswer = i; }
        public int GetAnswer() { return Ianswer; }
    }

    public class AnsW : Ans
    {
        string Wanswer { get; set; }
        public AnsW() { }
        public AnsW(string w) { Wanswer = w; }
        public string GetAnswer() { return Wanswer; }
    }
    #endregion

}
