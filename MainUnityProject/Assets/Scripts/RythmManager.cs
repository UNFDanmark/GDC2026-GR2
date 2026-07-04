using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class RythmManager : MonoBehaviour
{
    [Header("Arrows")]
    //[SerializeField] Transform [] //lav liste af taret arrows

    [Space(10)] [Header("Positions")] [SerializeField]
    Transform[] spawnLocations;
    [Space(10)]
    
    [SerializeField] GameObject spawnedArrow;

    [SerializeField] int nextSpawnCounter;
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (nextSpawnCounter == 0)
        {
            int ArrowType = UnityEngine.Random.Range(0, 3);
            GameObject newSpawnedArrow = Instantiate(spawnedArrow, spawnLocations[ArrowType]);
            newSpawnedArrow.
        }
        else
        {
            nextSpawnCounter -= 1;
        }
    }

}
