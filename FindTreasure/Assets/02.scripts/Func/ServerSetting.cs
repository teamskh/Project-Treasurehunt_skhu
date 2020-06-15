using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class ServerSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        // [.net4][il2cpp] 사용 시 필수 사용
        Backend.Initialize(() =>
        {
            // 초기화 성공한 경우 실행
            if (Backend.IsInitialized)
            {
                // example
                // 버전체크 -> 업데이트
            }
            // 초기화 실패한 경우 실행
            else
            {

            }
        });

        // 초기화
        // [.net3] 사용시
        Backend.Initialize(BRO =>
        {
            // 초기화 성공한 경우 실행
            if (BRO.IsSuccess())
            {
                // example
                // 버전체크 -> 업데이트
            }
            // 초기화 실패한 경우 실행
            else
            {

            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
