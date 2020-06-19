using System.Collections;
using System.Collections.Generic;

namespace ClassesTTMExtension
{
    #region Answer Extension
    public abstract class Ans
    {
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
