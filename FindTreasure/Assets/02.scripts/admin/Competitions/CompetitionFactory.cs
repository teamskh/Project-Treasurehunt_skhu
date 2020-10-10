using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;

public class CompetitionFactory : MonoBehaviour
{
    [SerializeField]
    Transform AddPanel;

    [SerializeField]
    GameObject AddButton;

    Button addButton, okButtton, cancelButton;

    InputField nameField, passwordField;

    Dropdown memberDropDown;

    ToggleGroup modeToggle;

    GameObject allText;

    Competition comp = new Competition();

    //활성화
    private void OnEnable()
    {
        // 대회 추가 버튼 클릭 리스너 등록
        addButton = AddButton.GetComponent<Button>();
        addButton?.onClick.AddListener(() => setAddPanelActive(true));

        if (AddPanel != null)
        {
            setAddPanelActive(false);

            //각종 필드 연결
            // 이름, 종류, 인원수, 비밀번호
            StartCoroutine(SetFields());

            // OK, CANCEL 버튼 클릭 리스너 등록
            okButtton = AddPanel.Find("OK")?.GetComponent<Button>();
            okButtton?.onClick.AddListener(Save);
            okButtton?.onClick.AddListener(() => setAddPanelActive(false));

            //패널 비활성화, 필드 공백
            cancelButton = AddPanel.Find("Cancel")?.GetComponent<Button>();
            cancelButton?.onClick.AddListener(resetFields);
            cancelButton?.onClick.AddListener(() => setAddPanelActive(false));
        }
    }

    #region Coroutine
    IEnumerator SetFields()
    {
        yield return null;
        //대회 이름
        nameField = AddPanel.Find("Competition_Name")?.GetComponent<InputField>();

        //단체전 개인전
        modeToggle = AddPanel.Find("Competition_Mode")?.GetComponent<ToggleGroup>();

        //단체전 팀 인원
        memberDropDown = AddPanel.Find("Competition_Member")?.GetComponent<Dropdown>();
        memberDropDown.enabled = false;

        //비밀번호
        passwordField = AddPanel.Find("Competition_Password")?.GetComponent<InputField>();

        //AllText
        allText = AddPanel.Find("AllText").gameObject;

        // toggle 값 변경 하면 불리는 리스너 등록
        modeToggle.GetComponentInChildren<Toggle>()?.onValueChanged.AddListener((bool b) =>setMemberDDown(b));
        basicToggleSetting();
    }

    IEnumerator BlinkAllText(float second)
    {
        allText.SetActive(true);
        yield return new WaitForSeconds(second);
        allText.SetActive(false);
    }
    #endregion

    #region Listeners
    // Add, OK, Cancel 버튼 리스너
    void setAddPanelActive(bool A) { 
        AddPanel.gameObject.SetActive(A);
        if (A) allText.SetActive(false);
    }
    
    // Team Toggle 리스너
    // Team 값이 false면 Member Dropdown 비활성화
    void setMemberDDown(bool A) { memberDropDown.enabled = A; }

    #endregion
    
    void resetFields() {
        //name Field
        nameField.SetTextWithoutNotify("");
       //Toggle group Field 
        basicToggleSetting();
        //Member Dropdown
        memberDropDown.SetValueWithoutNotify(0);
        //password Field
        passwordField.SetTextWithoutNotify("");
    }

    void basicToggleSetting()
    {
        var toggles = modeToggle.GetComponentsInChildren<Toggle>();
        modeToggle.SetAllTogglesOff();
        toggles[1].isOn = true;
    }

    void Save() {
        //이름 필드 비었을 때, AllText용 coroutine
        if ((comp.Name = nameField.textComponent.text).Length < 1)  goto COROUTINE;
        //modeToggle의 첫번째 토글 = Team 토글 
        //true 값 팀전, false 값 개인전
        comp.Mode = modeToggle.GetComponentInChildren<Toggle>().isOn;
        //MaxMember 값 받기
        if (comp.Mode) comp.MaxMember = memberDropDown.value + 2;
        //비밀 번호 필드 비었을 때, AllText용 coroutine
        if ((comp.Password = passwordField.text).Length < 1) goto COROUTINE;
        goto EXIT;

    COROUTINE:
        StartCoroutine(BlinkAllText(0.1f));
        return;
    EXIT:
        //Competition 서버 전송
        Param param = new Param();
        param.SetCompetition(comp).InsertCompetition();
        FTP.AvaliablePath(new DataPath("JPG/" + comp));

        //버튼 리스트 리셋용 호출
        var script = GetComponent<CompetitionToServer>();
        script?.SetList();
        resetFields();
        return;
    }
    public void resetP()
    {
        resetFields();
    }
    
}
