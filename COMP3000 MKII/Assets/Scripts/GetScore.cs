using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    public Text scoreText;
    private string scoreStr = "";

    // Start is called before the first frame update
    void Start()
    {
        
        scoreStr = PlayerPrefs.GetInt("Objects").ToString();

        scoreText.text = scoreStr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
