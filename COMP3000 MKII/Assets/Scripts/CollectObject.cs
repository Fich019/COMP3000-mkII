using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            string methodName = "getObjs";

            SendMessageOptions messageOptions = SendMessageOptions.DontRequireReceiver;

            Transform hitObject = other.transform;

            hitObject.SendMessage(methodName, 1, messageOptions);

            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
