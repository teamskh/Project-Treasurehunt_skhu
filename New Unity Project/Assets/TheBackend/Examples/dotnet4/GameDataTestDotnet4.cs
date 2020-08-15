using UnityEngine;
using LitJson;
using BackEnd;
using System.Collections.Generic;
using System;
using static BackEnd.BackendAsyncClass;

public class GameDataTestDotnet4: MonoBehaviour
{

    // chart를 비동기로 저장시 Update()에서 처리를 해야합니다. 이 값은 Update에서 구현하기 위한 플래그 값 입니다.
   
    BackendReturnObject chart = new BackendReturnObject();
    bool isChartSuccess = false;

    //gameinfo update, gameinfo delete, GetContentsByIndate 사용
    string Indate;

    string public_table_name = "public_table";
    string private_table_name = "priavte_table";
    string log_name = "log";

    // 테스트 아이디 비밀번호
    string id = "id1";
    string pw = "thebackend";

    List<string> gamer_indates = new List<string>();
    BackendReturnObject bro = new BackendReturnObject();
    bool isSuccess = false;

    void Start()
    {
        Backend.Initialize(BRO =>
        {
            Debug.Log("Backend.Initialize " + BRO);
            // 성공
            if (BRO.IsSuccess())
            {
                Backend.BMember.CustomLogin(id, pw, callback =>
                {
                    Debug.Log("CustomLogin " + callback);
                    isSuccess = callback.IsSuccess();
                    bro = callback;
                });
            }
            // 실패
            else
            {
                Debug.LogError("Failed to initialize the backend");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {

        if (isSuccess)
        {
            Debug.Log("-------------Update(SaveToken)-------------");
            BackendReturnObject saveToken = Backend.BMember.SaveToken(bro);
            if (saveToken.IsSuccess())
            {
                Debug.Log("로그인 성공");
            }
            else
            {
                Debug.Log("로그인 실패: " + saveToken.ToString());
            }
            isSuccess = false;
            bro.Clear();
        }

        // 차트를 비동기로 저장하는 경우에만 필요한 부분입니다.
        if (isChartSuccess)
        {
            Debug.Log("-----------------Update-----------------");
            PlayerPrefsClear();
            Backend.Chart.SaveChart(chart);
            isChartSuccess = false;

            if (chart.IsSuccess())
            {
                JsonData rows = chart.GetReturnValuetoJSON()["rows"];
                string ChartName, ChartContents;
                // get chart contents with chartName
                for (int i = 0; i < rows.Count; i++)
                {
                    ChartName = rows[i]["chartName"]["S"].ToString();
                    ChartContents = PlayerPrefs.GetString(ChartName);
                    Debug.Log(string.Format("{0}\n{1}", ChartName, ChartContents));
                }
            }

            chart.Clear();
        }
    }

    // 게임 테이블 리스트 불러옴
    public void GetTableList()
    {
        Debug.Log("-----------------Get Table List-----------------");
        BackendReturnObject tablelist = Backend.GameInfo.GetTableList();
        Debug.Log(tablelist);

        if (tablelist.IsSuccess())
        {
            SetTable(tablelist.GetReturnValuetoJSON());
        }
    }

    public void AGetTableList()
    {
        Debug.Log("-----------------A Get Table List-----------------");

        BackendAsync(Backend.GameInfo.GetTableList, tablelist =>
        {
            Debug.Log(tablelist);

            if (tablelist.IsSuccess())
            {
                SetTable(tablelist.GetReturnValuetoJSON());
            }
        });
    }

    List<string> PublicTables = new List<string>();
    List<string> PrivateTables = new List<string>();

    void SetTable(JsonData data)
    {
        JsonData publics = data["publicTables"];
        foreach (JsonData row in publics)
        {
            PublicTables.Add(row.ToString());
        }

        JsonData privates = data["privateTables"];
        foreach (JsonData row in privates)
        {
            PrivateTables.Add(row.ToString());
        }
    }

    /*
     *  퍼블릭 테이블 안의 정보를 반환함 
     *  default limit = 10
     *  firstkey 를 통해 다음 정보를 가져올 수 있음
     */
    public void GetPublicContents()
    {
        Debug.Log("-----------------Get Public Contents-----------------");

        BackendReturnObject backendReturnObject = Backend.GameInfo.GetPublicContents(public_table_name, 20);
        Debug.Log(backendReturnObject);
    }

    public void AGetPublicContents()
    {
        Debug.Log("-----------------AGet Public Contents-----------------");
        BackendAsync(Backend.GameInfo.GetPublicContents, public_table_name, bro =>
        {
            Debug.Log(bro);
        });

        BackendAsync(Backend.GameInfo.GetPublicContents, public_table_name, 2, bro =>
        {
            Debug.Log(bro);

            if (bro.IsSuccess())
            {
                GetGameInfo(bro.GetReturnValuetoJSON());
            }
        });
    }


    /*
     *  프라이빗 테이블 안의 로그인된 유저 정보를 반환함 
     *  모든 정보를 반환함 (limit/ offset 적용 불가)
     */
    public void GetPrivateContents()
    {
        Debug.Log("-----------------Get Private Contents-----------------");
        BackendReturnObject backendReturnObject = Backend.GameInfo.GetPrivateContents(private_table_name);
        Debug.Log(backendReturnObject);
        if (backendReturnObject.IsSuccess())
        {
            GetGameInfo(backendReturnObject.GetReturnValuetoJSON());
            Debug.Log(backendReturnObject.GetReturnValuetoJSON()["rows"].Count);
        }
    }



    public void AGetPrivateContents()
    {
        Debug.Log("-----------------AGet Private Contents-----------------");
        BackendAsync(Backend.GameInfo.GetPrivateContents, private_table_name, bro =>
        {
            Debug.Log(bro);
            if (bro.IsSuccess())
            {
                GetGameInfo(bro.GetReturnValuetoJSON());
            }
        });

    }

    // 특정 닉네임으로 유저의 indates를 받아옴
    private void GetGamerIndateByNickname()
    {
        Debug.Log("-----------------Get GamerIndate By Nickname-----------------");
        BackendReturnObject bro = Backend.Social.GetGamerIndateByNickname(id);
        Debug.Log(bro);

        if (bro.IsSuccess())
        {
            JsonData data = bro.GetReturnValuetoJSON();
            gamer_indates.Clear();
            for (int i = 0; i < data["rows"].Count; i++)
            {
                var indate = data["rows"][i]["inDate"]["S"].ToString();
                Debug.Log(indate);
                gamer_indates.Add(indate);
            }
        }
    }

    // 퍼블릭 테이블에 있는 특정 유저의 정보를 받아옴
    public void GetPublicContentsByGamerIndate()
    {
        // 유저의 indate를 먼저 가져옴
        GetGamerIndateByNickname();
        Debug.Log("-----------------Get Public Contents By GamerIndate-----------------");
        if (gamer_indates.Count > 0)
        {
            Debug.Log(Backend.GameInfo.GetPublicContentsByGamerIndate(public_table_name, gamer_indates[0]));
        }
        else
        {
            Debug.Log("there is NO member whoes nickname is " + id);
        }

    }

    public void AGetPublicContentsByGamerIndate()
    {
        // 유저의 indate를 먼저 가져옴
        GetGamerIndateByNickname();
        Debug.Log("-----------------A Get Public Contents By GamerIndate-----------------");
        if (gamer_indates.Count > 0)
        {
            BackendAsync(Backend.GameInfo.GetPublicContentsByGamerIndate, public_table_name, gamer_indates[0], result =>
            {
                Debug.Log(result);
            });
        }
    }

    // 게임정보 한개 가져옴
    public void GetContentsByIndate()
    {
        JsonData json = Backend.GameInfo.GetPublicContents(public_table_name).GetReturnValuetoJSON();
        // 데이터가 존재하는지 확인
        if (json["rows"].Count > 0)
        {
            Indate = json["rows"][0]["inDate"]["S"].ToString();
            Debug.Log("-----------------Get Contents By Indate (public)-----------------");

            BackendReturnObject contents = Backend.GameInfo.GetContentsByIndate(public_table_name, Indate);
            Debug.Log(contents);
            if (contents.IsSuccess())
            {
                GetGameInfo(contents.GetReturnValuetoJSON());
            }
        }
        else
        {
            Debug.Log("there is no data");
        }
    }

    public void AGetContentsByIndate()
    {
        JsonData json = Backend.GameInfo.GetPrivateContents(private_table_name).GetReturnValuetoJSON();
        // 데이터가 존재하는지 확인
        if (json["rows"].Count > 0)
        {
            Indate = json["rows"][0]["inDate"]["S"].ToString();
            Debug.Log("-----------------A Get Contents By Indate(private)-----------------");
            BackendAsync(Backend.GameInfo.GetContentsByIndate, private_table_name, Indate, contents =>
            {
                Debug.Log(contents.ToString());
                if (contents.IsSuccess())
                {
                    GetGameInfo(contents.GetReturnValuetoJSON());
                }
            });
        }
        else
        {
            Debug.Log("there is no data");
        }
    }

    void GetGameInfo(JsonData returnData)
    {
        // ReturnValue가 존재하고, 데이터가 있는지 확인
        if (returnData != null)
        {
            Debug.Log("returnvalue is not null");
            // for the rows 
            if (returnData.Keys.Contains("rows"))
            {
                Debug.Log("returnvalue contains rows");
                JsonData rows = returnData["rows"];

                for (int i = 0; i < rows.Count; i++)
                {
                    GetData(rows[i]);
                }
            }
            // for an row
            else if (returnData.Keys.Contains("row"))
            {
                Debug.Log("returnvalue contains row");
                JsonData row = returnData["row"];

                GetData(row[0]);
            }
        }
        else
        {
            Debug.Log("contents has no data");
        }
    }

    // json parsing 활용
    void GetData(JsonData data)
    {

        string scoreKey = "score";
        string lunchKey = "lunch";
        string listKey = "list_string";

        // score 라는 key가 존재하는지 확인
        if (data.Keys.Contains(scoreKey))
        {
            var score = data[scoreKey]["N"];
            Debug.Log("score: " + score);
        }
        else
        {
            Debug.Log("there is no key " + scoreKey);
        }
        //Debug.Log("data.Keys.Contains(scoreKey" + data.Keys.Contains(scoreKey));
        // lunch 라는 key가 존재하는지 확인
        if (data.Keys.Contains(lunchKey))
        {
            JsonData lunch = data[lunchKey]["M"];
            var howmuchKey = "how much";
            var whenKey = "when";
            var whatKey = "what";

            if (lunch.Keys.Contains(howmuchKey) && lunch.Keys.Contains(whenKey) && lunch.Keys.Contains(whatKey))
            {
                var howmuch = lunch[howmuchKey]["N"].ToString();
                var when = lunch[whenKey]["S"].ToString();
                var what = lunch[whatKey]["S"].ToString();

                Debug.Log(when + " " + what + " " + howmuch);
            }
            else
            {
                Debug.Log("there is no key (" + howmuchKey + " || " + whenKey + " || " + whatKey + ")");
            }
        }
        else
        {
            Debug.Log("there is no key " + lunchKey);
        }

        // list_string 라는 key가 존재하는지 확인
        if (data.Keys.Contains(listKey))
        {
            List<string> returnlist = new List<string>();
            JsonData list = data[listKey]["L"];
            var listCount = list.Count;
            if (listCount > 0)
            {
                for (int j = 0; j < listCount; j++)
                {
                    var listdata = list[j]["S"].ToString();
                    returnlist.Add(listdata);
                }
                Debug.Log(JsonMapper.ToJson(returnlist));
            }
            else
            {
                Debug.Log("list has no data");
            }
        }
        else
        {
            Debug.Log("there is no key " + listKey);
        }
    }

    // 게임 정보 생성
    public void GameInfoInsert()
    {
        Debug.Log("-----------------GameInfo Insert-----------------");

        Param lunch = new Param();
        lunch.Add("how much", 332);
        lunch.Add("when", "yesterday");
        lunch.Add("what", "eat chocolate");

        Dictionary<string, int> dic = new Dictionary<string, int>
        {
            { "dic1", 1 },
            { "dic4", 2 },
            { "dic2", 4 }
        };

        Dictionary<string, string> dic2 = new Dictionary<string, string>
        {
            { "mm", "j" },
            { "nn", "n" },
            { "dd", "2" }
        };

        String[] list = { "a", "b" };
        int[] list2 = { 400, 500, 600 };


        string jsonstring = @"{""inDate"":""2019-05-08T06:58:01.608Z"",""nickname"":""GM로엠"",""uuid"":""YV85MTYyMzY2MjM5MDUwNDc2MjMz"",""fightingPower"":203575853.67367488,""characterType"":0,""skinType"":0}";
        string jsonstring2 = @"{""inDate"":""2019-05-08T06:58:01.608Z"",""nickname"":""GM로로로로아아"",""uuid"":""YV85MTYyMzY2MjM5MDUwNDc2MjMz"",""fightingPower"":203575853.67367488,""characterType"":1,""skinType"":11}";
        List<string> stringList = new List<string>();
        stringList.Add(jsonstring);
        stringList.Add(jsonstring2);


        Param param2 = new Param();
        param2.Add("이름", "cheolsu");
        param2.Add("score", 99);
        param2.Add("lunch", lunch);
        Param param3 = new Param();
        param3.Add("dic_num", dic);
        param3.Add("dic_string", dic2);
        param3.Add("list_string", list);
        //param.Add("list_num", list2);

        Debug.Log(stringList.ToArray());
        Param[] paramList = { param2, param3 };

        Param param = new Param();
        param.Add("이름", "cheolsu");
        param.Add("score", 99);
        param.Add("lunch", lunch);
        param.Add("dic_num", dic);
        param.Add("dic_string", dic2);
        param.Add("list_string", list);
        param.Add("list_num", list2);
        param.Add("params", paramList);


        BackendReturnObject insert = Backend.GameInfo.Insert(public_table_name, param);
        Debug.Log(insert.ToString());
        if (insert.IsSuccess())
        {
            Indate = insert.GetInDate();
            Debug.Log("indate : " + Indate);
        }
    }

    public void AGameInfoInsert()
    {
        Debug.Log("-----------------A GameInfo Insert-----------------");

        Param lunch = new Param();
        lunch.Add("how much", 332);
        lunch.Add("when", "yesterday");
        lunch.Add("what", "eat chocolate");

        Dictionary<string, int> dic = new Dictionary<string, int>
        {
            { "dic1", 1 },
            { "dic4", 2 },
            { "dic2", 4 }
        };

        Dictionary<string, string> dic2 = new Dictionary<string, string>
        {
            { "mm", "j" },
            { "nn", "n" },
            { "dd", "2" }
        };

        String[] list = { "a", "b" };
        int[] list2 = { 400, 500, 600 };

        byte[] bytes = new byte[] { 0xA1, 0xB2, 0xC3, 0x11, 0x2F };

        Param param = new Param();
        param.Add("이름", bytes);
        param.Add("score", 99);
        param.Add("lunch", lunch);
        param.Add("dic_num", dic);
        param.Add("dic_string", dic2);
        param.Add("list_string", list);
        param.Add("list_num", list2);

        BackendAsync(Backend.GameInfo.Insert, private_table_name, param, insertComplete =>
        {
            Debug.Log("insert : " + insertComplete.ToString());
            if (insertComplete.IsSuccess())
            {
                Indate = insertComplete.GetInDate();
                Debug.Log("indate : " + Indate);
            }
        });

    }

    // 게임 정보 수정
    public void GameInfoUpdate()
    {
        Debug.Log("-----------------GameInfo Update-----------------");

        Param param = new Param();
        param.Add("score", 102);

        Debug.Log(Backend.GameInfo.Update(public_table_name, Indate, param).ToString());
    }

    public void AGameInfoUpdate()
    {
        Debug.Log("-----------------A GameInfo Update-----------------");
        Param param = new Param();
        param.Add("score", 1101);

        BackendAsync(Backend.GameInfo.Update, private_table_name, Indate, param, updateComplete =>
        {
            Debug.Log(updateComplete.ToString());
        });
    }

    // 게임 정보 삭제
    public void GameInfoDelete()
    {
        Debug.Log("-----------------GameInfo Delete-----------------");
        Debug.Log(Backend.GameInfo.Delete(public_table_name, Indate).ToString());
    }

    public void AGameInfoDelete()
    {
        Debug.Log("-----------------A GameInfo Delete-----------------");
        BackendAsync(Backend.GameInfo.Delete, private_table_name, Indate, deleteComplete =>
        {
            Debug.Log(deleteComplete.ToString());
        });
    }

    // 게임 로그 생성
    public void InsertLog()
    {
        Debug.Log("-----------------Insert Log-----------------");
        Param param = new Param();
        param.Add("이름", "cheolsu");
        param.Add("Level", 99);
        param.Add("Exp", 151111352520);
        param.Add("Gold", 0);
        param.Add("Stamina", 1);
        param.Add("Cash", 333);
        param.Add("LeftTilePiece", 442);
        param.Add("RightTilePiece", 230);
        Debug.Log(Backend.GameInfo.InsertLog(log_name, param).ToString());
    }

    public void AInsertLog()
    {
        Debug.Log("-----------------AInsert Log-----------------");
        Param param = new Param();
        param.Add("이름", "cheolsu");
        param.Add("Level", 11);
        param.Add("Exp", 151111352520);
        param.Add("Gold", 2);
        param.Add("Stamina", 44);
        param.Add("Cash", 1000);

        BackendAsync(Backend.GameInfo.InsertLog, log_name, param, insertLogComplete =>
        {
            Debug.Log(insertLogComplete.ToString());
        });
    }


    /*
     * chart list 를 불러온 후, 차트내용까지 PlayerPrefs에 자동 저장
     */
    public void GetChartAndSave()
    {
        Debug.Log("-----------------Get Chart And Save-----------------");
        PlayerPrefsClear();
        BackendReturnObject getchart = Backend.Chart.GetChartAndSave();
        Debug.Log(getchart);

        if (getchart.IsSuccess())
        {
            JsonData rows = getchart.GetReturnValuetoJSON()["rows"];
            string ChartName, ChartContents;
            // get chart contents with chartName
            for (int i = 0; i < rows.Count; i++)
            {
                ChartName = rows[i]["chartName"]["S"].ToString();
                ChartContents = PlayerPrefs.GetString(ChartName);
                Debug.Log(string.Format("{0}\n{1}", ChartName, ChartContents));
            }
        }
    }

    public void AGetChartSave()
    {
        Debug.Log("-----------------A Get Chart And Save-----------------");
        PlayerPrefsClear();
        // GetChartAndSave 대신 GetChartAndSaveAsync 사용
        BackendAsync(Backend.Chart.GetChartAndSaveAsync, chartsave =>
        {
            Debug.Log(chartsave.ToString());
            isChartSuccess = chartsave.IsSuccess();
            chart = chartsave;
            // update() 에서 saveChart(chart); 호출하여야 PlayerPrefs에 정상적으로 저장됨
        });
    }

    /*
     * chart list 를 불러옴
     */
    List<string> ChartFileUuidList = new List<string>();

    public void GetChartList()
    {
        Debug.Log("-----------------Get Chart List-----------------");
        BackendReturnObject getchartlist = Backend.Chart.GetChartList();
        Debug.Log(getchartlist);
        SetChartFilUUIDList(getchartlist);
    }


    public void AGetChartList()
    {
        PlayerPrefsClear();
        Debug.Log("-----------------A Get Chart List-----------------");
        BackendAsync(Backend.Chart.GetChartList, list =>
        {
            Debug.Log(list);
            SetChartFilUUIDList(list);
        });
    }

    private void SetChartFilUUIDList(BackendReturnObject listBRO)
    {
        if (listBRO.IsSuccess())
        {
            ChartFileUuidList.Clear();
            JsonData rows = listBRO.GetReturnValuetoJSON()["rows"];
            string ChartFileUuid;
            for (int i = 0; i < rows.Count; i++)
            {
                ChartFileUuid = string.Empty;
                JsonData data = rows[i];

                if (data.Keys.Contains("selectedChartFile"))
                {
                    ChartFileUuid = data["selectedChartFile"]["M"]["uuid"]["S"].ToString();
                }
                else if (data.Keys.Contains("selectedChartFileId"))
                {
                    if (!data["selectedChartFileId"].Keys.Contains("NULL"))
                        ChartFileUuid = data["selectedChartFileId"]["N"].ToString();
                }

                // ChartFileUuid 가 정상적으로 존재하는 경우 -> 리스트에 저장
                if (!string.IsNullOrEmpty(ChartFileUuid))
                {
                    Debug.Log(ChartFileUuid);
                    ChartFileUuidList.Add(ChartFileUuid);
                }

            }
        }
    }

    /*
     * chart의 적용된 파일 uuid를 통해 내용을 가져옴
     */
    public void GetChartContents()
    {
        Debug.Log("-----------------GetChartContents-----------------");
        BackendReturnObject contents;
        for (int i = 0; i < ChartFileUuidList.Count; i++)
        {
            contents = Backend.Chart.GetChartContents(ChartFileUuidList[i]);
            Debug.Log(ChartFileUuidList[i] + " " + contents);
        }
    }

    public void AGetChartContents()
    {
        Debug.Log("-----------------AGetChartContents-----------------");
        for (int i = 0; i < ChartFileUuidList.Count; i++)
        {
            BackendAsync(Backend.Chart.GetChartContents, ChartFileUuidList[i], callback =>
            {
                Debug.Log(callback);
            });
        }
    }

    public void PlayerPrefsClear()
    {
        PlayerPrefs.DeleteAll();
    }
}