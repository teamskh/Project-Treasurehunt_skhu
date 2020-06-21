using UnityEngine;
using LitJson;
using BackEnd;
using System.Collections.Generic;
using System;
using static BackEnd.BackendAsyncClass;

public class realtimerank : MonoBehaviour
{
    private void test()
    {
        Backend.RTRank.GetRTRankByUuid("c104df80-aee4-11ea-8d65-076db9a598b3", 2); //상위 2명 랭킹정보 조회
    }

    public void 
}