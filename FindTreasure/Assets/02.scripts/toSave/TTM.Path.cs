using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTM.Path
{
    public static class Address
    {
        static string EnginePath = Application.dataPath;
        static string JsonContents = "/Resources/FindTreasure";
        // 상황 봐서 Debug용만 사용할 예정
        static string CompetitionSavePath = "/Compet";
        static string QuizMadeSavePath = "/QuizMade";
        static string QuizsSavePath = "/Quiz";
        static string AnswersSavePath = "/Answer";

        static string ImageSavePath = "/Resources/ARPictures/";

        static string JsonExs = ".json";
        static string ImgJPGExs = ".jpg";
        static string ImgPNGExs = ".png";

        #region File Path

        #region Json Path
        public static string GetJsonContentsPath()
        {
            return EnginePath + JsonContents;
        }

        public static string GetComptSavePath(string FileName)
        {
            return EnginePath + JsonContents + CompetitionSavePath + FileName + JsonExs;
        }

        public static string GetQuizMadeSavePath(string FileName)
        {
            return EnginePath + JsonContents + QuizMadeSavePath + FileName + JsonExs;
        }

        public static string GetQuizSavePath(string FileName)
        {
            return EnginePath + JsonContents + QuizsSavePath + FileName + JsonExs;
        }

        public static string GetAnswerSavePath(string FileName)
        {
            return EnginePath + JsonContents + AnswersSavePath + FileName + JsonExs;
        }
        #endregion

        #region File Name

        public static string GetComptFile(string FileName)
        {
            return CompetitionSavePath + FileName + JsonExs;
        }

        public static string GetQuizMadeFile(string FileName)
        {
            return QuizMadeSavePath + FileName + JsonExs;
        }

        public static string GetQuizFile(string FileName)
        {
            return  QuizsSavePath + FileName + JsonExs;
        }

        public static string GetAnswerFile(string FileName)
        {
            return  AnswersSavePath + FileName + JsonExs;
        }

        #endregion

        public static string GetJPGImageSavePath(string ImgName)
        {
            return ImageSavePath + ImgName + ImgJPGExs;
        }

        public static string GetPNGImageSavePath(string ImgName)
        {
            return ImageSavePath + ImgName + ImgPNGExs;
        }
        #endregion

    }
}
