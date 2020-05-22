using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

using Debug = UnityEngine.Debug;

public class FloorController : MonoBehaviour
{
    public GameObject[] floorPrefabs;
    private List<GameObject> tilesGenerated;
    private Transform playerTransform;
    private int floorsOnScreen = 10;
    private int floorsCount = 10; //UDV
    private float zLocationSpawn =  0.0f;
    private float floorLength = 10.0f;
    private string PLAYER = "Player";
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(PLAYER).transform;
        tilesGenerated = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //This will spawn new floors as the user progresses
        var playerCurrentPosition = playerTransform.position;
        float nextPosition = (float) floorsOnScreen * floorLength;
        float safePosition = (float) floorsCount * floorLength;
        float threshold = zLocationSpawn - nextPosition;
        if(playerCurrentPosition.z > threshold){
            spawnNextFloor();
            if(tilesGenerated.Count > 11){
                deletePreviewFloor();
            }
        }
    }

    void spawnNextFloor(int spawnIndex = -1){
        GameObject referenceToFloor;
        System.Random randomFloor = new System.Random();
        int floorPrefabNumber  = randomFloor.Next(0, 9);
        referenceToFloor = Instantiate(floorPrefabs[floorPrefabNumber]) as GameObject;
        referenceToFloor.transform.SetParent(transform);
        referenceToFloor.transform.position = Vector3.forward * zLocationSpawn;
        tilesGenerated.Add(referenceToFloor);
        zLocationSpawn = zLocationSpawn + floorLength;
    }

    void deletePreviewFloor(){
        //Delete the first floor.
        if(tilesGenerated.Count > 0){
            Destroy(tilesGenerated[0]);
            tilesGenerated.RemoveAt(0);
        }
    }
}
