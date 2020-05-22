using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletController : MonoBehaviour
{
    public float lifeDuration = 5f;
    public float speed = 20.0f;
    private float lifeTimer;
    private float fix = 0.8f;

    private bool didItCollide = false;
    
    void Start () {
        lifeTimer = lifeDuration;
        float newYPositon = transform.position.y - fix;
        transform.position = new Vector3(transform.position.x, newYPositon, transform.position.z);
    }
 
    void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Triggered on the collision... " + collision.gameObject.name);
        
        // if(didItCollide){
        //     return;
        // }
        // didItCollide = true;
        if(collision.gameObject.name.Contains("Cube")){
            Debug.Log("Came here");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
