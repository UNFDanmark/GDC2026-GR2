using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class RythmManager : MonoBehaviour
{
    [Header("Arrows")] 
    [SerializeField] Transform[] targetArrows;
    [Space(10)] 
    
    [Header("Positions")] [SerializeField]
    Transform[] spawnLocations;
    [Space(10)]
    
    [SerializeField] GameObject spawnedArrow;

    [SerializeField] int nextSpawnCounter;
    
    //Note Management
    public Note[] Testnotes;

    public Vector3 test;
        
    [Header("Actions")]
    public InputAction leftArrowAction;
    public InputAction downArrowAction;
    public InputAction upArrowAction;
    public InputAction rightArrowAction;
    
    void Start()
    {
        leftArrowAction.Enable();
        downArrowAction.Enable();
        upArrowAction.Enable();
        rightArrowAction.Enable();
    }

    void FixedUpdate()
    {
        if (nextSpawnCounter <= 0)
        {
            nextSpawnCounter = 20;
            CreateNote();
        }
        else
        {
            nextSpawnCounter -= 1;
        }
    }

    private void CreateNote()
    {
        int arrowType = UnityEngine.Random.Range(0, 4);
        GameObject newSpawnedArrow = Instantiate(spawnedArrow, spawnLocations[arrowType].position, Quaternion.identity);
        newSpawnedArrow.GetComponent<SpawnedArrow>().target = targetArrows[arrowType];
        newSpawnedArrow.GetComponent<SpawnedArrow>().spawn = spawnLocations[arrowType];
    }
}
