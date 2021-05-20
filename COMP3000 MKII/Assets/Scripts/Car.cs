using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{

    
    public float moveSpeed = 100;

    public int damage = 1;

    public float timeRemaining = 3;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 facingDirection = transform.forward;

        Vector3 velocity = facingDirection * moveSpeed;

        GetComponent<Rigidbody>().AddForce(velocity);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Die();
        }

        if (collision.gameObject.layer == 10)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }

    private void Die()
    {

        Destroy(gameObject);
    }
}
