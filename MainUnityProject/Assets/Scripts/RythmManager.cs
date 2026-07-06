using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [Space(10)] [Header("Base Arrow Animators")] [SerializeField]
    Animator[] baseArrowAnimators;

    [Space(10)] [Header("Base Arrow Particles")] [SerializeField]
    GameObject[] baseArrowParticles;
    
    
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

    public float perfectThreshold;
    public float goodThreshold;
    public float okayThreshold;
    public float badThreshold;

    public float perfectMultiplier;
    public float goodMultiplier;
    public float okayMultiplier;
    public float badMultiplier;

    public float totalScore;
    
    
    
    #endregion
    
    void Start()
    {
        print(columns.Length);
        leftArrowAction.Enable();
        downArrowAction.Enable();
        upArrowAction.Enable();
        rightArrowAction.Enable();
        for (int i = 0; i < baseArrowParticles.Length; i++)
        {
            baseArrowParticles[i].SetActive(false);
        }
    }

    void Update()
    {
        if (leftArrowAction.WasPressedThisFrame())
        { HitNote(0); }

        if (downArrowAction.WasPressedThisFrame())
        { HitNote(1); }

        if (upArrowAction.WasPressedThisFrame())
        { HitNote(2); }

        if (rightArrowAction.WasPressedThisFrame())
        { HitNote(3); }
        
        for (int i = 0; 0 < columns.Length -1; i++)
        {
            if (columns[i].notesInColumn.Count -1 > 0 && columns[i].notesInColumn[0] == null )
            {
                columns[i].notesInColumn.RemoveAt(0);
            }
        }
        
    }

    void FixedUpdate()
    {
        beat += 1;
        if (nextSpawnCounter <= 0)
        {
            nextSpawnCounter = 2;
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
        baseArrowAnimators[column].SetTrigger("hit");
        //Play a sound cuz its cool
        //also one for missing
        CalculateScore(distance, column);
        Debug.Log(distance);
    }
    
    private float CalculateScore(float distance, int column)
    {
        float finalScore = 0;
        float usedMultiplier = 0;
        
        if (distance < perfectThreshold || perfectThreshold * -1 < distance)
        {
            usedMultiplier = perfectMultiplier;
            baseArrowParticles[column].SetActive(false);
            baseArrowParticles[column].SetActive(true);
        } else if (distance < goodThreshold || goodThreshold * -1 < distance)
        {
            usedMultiplier = goodMultiplier;
        } else if(distance < okayThreshold || okayThreshold * -1 < distance)
        {
            usedMultiplier = okayMultiplier;
        } else if (distance < badThreshold || badThreshold * -1 < distance)
        {
            usedMultiplier = badMultiplier;
        }
        else
        {
            //miss
        }

        finalScore = 1 * usedMultiplier;
        
        totalScore += finalScore;
        return finalScore;
    }
}












//Wall of Shame

// newSpawnedArrow.transform.rotation = targetArrows[arrowType].rotation;