using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RythmManager : MonoBehaviour
{
    #region Variables

    [Header("Positions")] [SerializeField]
    public Transform[] targetArrows;
    public Transform[] spawnLocations;
    public Transform[] tombStones;
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

    [Space(10)] [Header("Uncategorized")] [SerializeField]
    Transform mainCamera;
    
    #endregion
    
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
        SpawnedArrow newSpawnedArrowScript = newSpawnedArrow.GetComponent<SpawnedArrow>();
        
        newSpawnedArrowScript.target = targetArrows[arrowType];
        newSpawnedArrowScript.spawn = spawnLocations[arrowType];
        newSpawnedArrowScript.tomb = tombStones[arrowType];
        newSpawnedArrow.GetComponent<SpriteRenderer>().sprite = arrowSprites[arrowType];
        newSpawnedArrow.transform.SetParent(mainCamera);
        columns[arrowType].notesInColumn.Add(newSpawnedArrow.transform);
    }

    private void HitNote(int column)
    {
        float distance;
        distance = columns[column].notesInColumn[0].position.y - targetArrows[0].position.y;
        Destroy(columns[column].notesInColumn[0].gameObject);
        columns[column].notesInColumn.RemoveAt(0);
        Debug.Log(distance);
    }

    private float CalculateScore()
    {
        float finalScore = 0;
        
        //Insert crazy matematik
        
        return finalScore;
    }
}












//Wall of Shame

// newSpawnedArrow.transform.rotation = targetArrows[arrowType].rotation;