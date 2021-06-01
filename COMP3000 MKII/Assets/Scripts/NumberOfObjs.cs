using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfObjs : MonoBehaviour
{
    
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTotalObjs(int totalNumOfObjs)
    {
        slider.maxValue = totalNumOfObjs;
        slider.value = totalNumOfObjs;
    }

    public void setCurrentObjs()
    {
        slider.value -= 1;
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
