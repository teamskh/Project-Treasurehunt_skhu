using TTM.Classes;


public static class MakeRandomCode 
{
    public static bool MakeCode<T>(T dic, out int code) where T : ITTMDictionary
    {
        for(int i = 0; i < 1000; i++)
        {
            if (dic.AvailableCode(i)) {
                code = i;
                return true;
            }
        }
        code = -1;
        return false; 
    }
}

