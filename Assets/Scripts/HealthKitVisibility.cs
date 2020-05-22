using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
public class HealthKitVisibility : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject healthKit;
    private bool visibility;
    void Start()
    {
        randomizeVisibility();
        healthKit.SetActive(visibility);
    }

    private void randomizeVisibility(){
        Random random = new Random();
        int value = random.Next(0, 3);
        visibility = value == 0?true:false;
        // Debug.Log(visibility);
    }
}
