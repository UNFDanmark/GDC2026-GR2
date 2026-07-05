using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class RythmManager : MonoBehaviour
{
    [Header("Arrows")] 
    public Transform[] targetArrows;
    [Space(10)] 
    
    [Header("Positions")] [SerializeField]
    Transform[] spawnLocations;
    [Space(10)] 
    
    [Header("Sprites")] [SerializeField]
    Sprite[] arrowSprites;
    [Space(10)]
    
    [SerializeField] GameObject spawnedArrow;

    [SerializeField] int nextSpawnCounter;

    public int beat;

    [SerializeField]
    public SpawnedNotes[] columns;

    [Serializable]
    public struct SpawnedNotes
    {
        public List<Transform> notesInColumn;
    }

    public float missThreshold;
    
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

    void Update()
    {
        if (leftArrowAction.WasPressedThisFrame())
        {
            HitNote(0);
        }

        if (downArrowAction.WasPressedThisFrame())
        {
            HitNote(1);
        }

        if (upArrowAction.WasPressedThisFrame())
        {
            HitNote(2);
        }

        if (rightArrowAction.WasPressedThisFrame())
        {
            HitNote(3);
        }
    }

    void FixedUpdate()
    {
        beat += 1;
        if (nextSpawnCounter <= 0)
        {
            nextSpawnCounter = 5;
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
        columns[arrowType].notesInColumn.Add(newSpawnedArrow.transform);
        //assign sprite 
    }

    private void HitNote(int column)
    {
        float distance;
        distance = columns[column].notesInColumn[0].position.y - targetArrows[0].position.y;
        Destroy(columns[column].notesInColumn[0].gameObject);
        columns[column].notesInColumn.RemoveAt(0);
        Debug.Log(distance);
    }
}
