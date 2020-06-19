using DataInfo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using TTM.Classes;
using TTM.Save;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoMultipleManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] arObjectsToPlace;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f,0.1f,0.1f);

    public Text DebugforScreen;

    private ARTrackedImageManager m_TrackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

    private QuizDictionary m_Dics;

    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

        JsonLoadSave.LoadQuizs(out m_Dics);
        //PlayerQuizLoad.initLoad(Application.persistentDataPath + "PlayerQuiz.dat",out m_Dics);        -> 확인 후 삭제

        // setup all game objects in dictionary 
       /*foreach (GameObject arObject in arObjectsToPlace)
        {
            GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.transform.localScale.Set(0.4f, 0.4f, 0.4f);
            newARObject.name = arObject.name;
            arObjects.Add(arObject.name, newARObject);
        }*/
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            XRReferenceImage img = trackedImage.referenceImage; 
            UpdateInfo(trackedImage);
            //UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            //UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            string[] na = trackedImage.name.Split('_');
            arObjects[na[0]].SetActive(false);
            gameman.Instance.imageText = na[1];
            DestroyPrefabs(trackedImage);
        }
    }

    void DestroyPrefabs(ARTrackedImage trackedImage)
    {
        GameObject gameobj;
        arObjects.TryGetValue(trackedImage.referenceImage.name,out gameobj);
        Destroy(gameobj);
    }

    void UpdateInfo(ARTrackedImage trackedImage)
    { 
        if (trackedImage.trackingState != TrackingState.Tracking)
        {
            //tranckedimage 의 이름 찾아서 그거에 맞는 퀴즈찾아서, 퀴즈의 종류 파악해서, 이밑에다 달면 될듯..?
            Quiz quiz = new Quiz();
            string title = trackedImage.referenceImage.name;
            gameman.Instance.imageText = title;
            if(!m_Dics.TryGetValue(title, out quiz)) { DebugforScreen.text = "Can't find quiz"; }
            if (quiz.Str != null)
            {
                GameObject prefab = new GameObject();
                Transform trans = trackedImage.transform;
                switch (quiz.Kind)
                {
                    case 0:
                        prefab = Instantiate(arObjectsToPlace[0], trans.position, trans.rotation);
                        prefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        break;
                    case 1:
                        prefab = Instantiate(arObjectsToPlace[1], trans.position,trans.rotation);
                        prefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        Transform tform = null;
                        foreach(Transform Tr in GetComponentsInChildren<Transform>())
                        {
                            if(Tr.tag == "LIST") tform = Tr;
                        }
                        if (tform != null)
                            for (int i = 0; i < 4; i++) {
                                tform.GetComponentsInChildren<TextMesh>()[i].text = quiz.List[i];
                            }
                        break;
                    case 2:
                        prefab = Instantiate(arObjectsToPlace[0], trans.position, trans.rotation);
                        prefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        break;
                }
                prefab.transform.localScale.Set(scaleFactor.x,scaleFactor.y,scaleFactor.z);
                foreach(TextMesh t in prefab.GetComponentsInChildren<TextMesh>())
                {
                    if(t.tag=="STR")t.text = quiz.Str;

                }
                arObjects.Add(title, prefab);
            }
        }
        else
        {
            // Destroy object if you dont want to keep
        }
    }
    
}
