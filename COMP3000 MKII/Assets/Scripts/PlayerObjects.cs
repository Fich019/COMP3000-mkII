using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerObjects : MonoBehaviour
{
    [SerializeField] static int objectsCollected, totalNumOfObjs;

    public NumberOfObjs objectbar;

    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Objects", 0);
        totalNumOfObjs = GameObject.FindGameObjectsWithTag("Collect").Length;
        objectbar = GameObject.FindGameObjectWithTag("Objectbar").GetComponent<NumberOfObjs>();
        objectbar.SetTotalObjs(totalNumOfObjs);
    }

    // Update is called once per frame
    void Update()
    {

        if (objectsCollected == totalNumOfObjs)
        {
            GameOver();
        }
    }

    public void getObjs(int obj)
    {
        objectsCollected = objectsCollected + obj;
        PlayerPrefs.SetInt("Objects", objectsCollected);
        ObjectsRemaining();
    }


    void ObjectsRemaining()
    {
        objectbar.setCurrentObjs();
    }
    public void GameOver()
    {
        SceneManager.LoadScene("EndScreen");
    }

}


