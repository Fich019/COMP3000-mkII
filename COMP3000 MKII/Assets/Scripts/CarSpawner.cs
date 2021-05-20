using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    public GameObject car;

    public Transform carSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int spawn = Random.Range(0, 500);

        if (spawn == 5)
        {
            Spawn();
        }
    }



    private void Spawn()
    {
        Vector3 carPosition = carSpawn.position;

        Quaternion carRotation = carSpawn.rotation;

        Instantiate(car, carPosition, carRotation);

    }
}
