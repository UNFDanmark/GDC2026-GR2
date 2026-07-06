using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RhythmManager : MonoBehaviour
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
    
    [Header("Actions")]
    public InputAction leftArrowAction;
    public InputAction downArrowAction;
    public InputAction upArrowAction;
    public InputAction rightArrowAction;

    [Space(10)] [Header("Uncategorized")] [SerializeField]
    Transform mainCamera;

    [SerializeField]float perfectThreshold;
    [SerializeField]float goodThreshold;
    [SerializeField]float okayThreshold;
    [SerializeField]float badThreshold;
    [SerializeField]float missThreshold;

    [SerializeField]float perfectMultiplier;
    [SerializeField]float goodMultiplier;
    [SerializeField]float okayMultiplier;
    [SerializeField]float badMultiplier;
    
    [SerializeField] float missPenalty;

    [SerializeField] GameObject[] performanceWords;
    [SerializeField] Transform performanceWordSpawn;
    [SerializeField] Transform canvas;
    [SerializeField] float textDuration;
     
    public float totalScore;
    
    
    
    #endregion
    
    void Start()
    {
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
        for (int i = 0; i < columns.Length; i++)
        {
            if (columns[i].notesInColumn.Count > 0 && columns[i].notesInColumn[0] == null )
            {
                columns[i].notesInColumn.RemoveAt(0);
            }
        }
        if (leftArrowAction.WasPressedThisFrame())
        {
            baseArrowAnimators[0].SetTrigger("hit");
            HitNote(0); 
        }

        if (downArrowAction.WasPressedThisFrame())
        {
            baseArrowAnimators[1].SetTrigger("hit");
            HitNote(1);
        }

        if (upArrowAction.WasPressedThisFrame())
        {
            baseArrowAnimators[2].SetTrigger("hit");
            HitNote(2); 
        }

        if (rightArrowAction.WasPressedThisFrame())
        {
            baseArrowAnimators[3].SetTrigger("hit");
            HitNote(3);
        }
        
        
    }

    void FixedUpdate()
    {
        beat += 1;
        if (nextSpawnCounter <= 0)
        {
            nextSpawnCounter = 1;
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
        newSpawnedArrowScript.mother = this;
        columns[arrowType].notesInColumn.Add(newSpawnedArrow.transform);
    }

    private void HitNote(int column)
    {
        float distance;
        if (columns[column].notesInColumn.Count > 0)
        {
            distance = columns[column].notesInColumn[0].position.y - targetArrows[0].position.y;
            if(CalculateScore(distance, column) != 0)
            {
                Destroy(columns[column].notesInColumn[0].gameObject);
                CalculateScore(distance, column);
                Debug.Log(distance);
            }
        }
        else
        {
            OnMiss();
        }
        
    }
    
    
    private float CalculateScore(float distance, int column)
    {
        float finalScore = 0;
        float usedMultiplier = 0;

        if (distance < 0)
        {
            if (perfectThreshold * -1 < distance)
            {
                usedMultiplier = perfectMultiplier;
                baseArrowParticles[column].SetActive(false);
                baseArrowParticles[column].SetActive(true);
                print("Perfect!");
                StartCoroutine("CreatePerformanceText", 0);
            } else if (goodThreshold * -1 < distance)
            {
                usedMultiplier = goodMultiplier;
                baseArrowParticles[column].SetActive(false);
                baseArrowParticles[column].SetActive(true);
                print("Good!");
                StartCoroutine("CreatePerformanceText", 1);
            } else if(okayThreshold * -1 < distance)
            {
                usedMultiplier = okayMultiplier;
                print("Okay");
                StartCoroutine("CreatePerformanceText", 2);
            } else if (badThreshold * -1 < distance)
            {
                usedMultiplier = badMultiplier;
                print("Bad :(");
                StartCoroutine("CreatePerformanceText", 3);
            }
            else
            {
                print("Do u see a ghost or what Idot");
            }
        }

        if (distance > 0)
        {
            if (distance < perfectThreshold)
            {
                usedMultiplier = perfectMultiplier;
                baseArrowParticles[column].SetActive(false);
                baseArrowParticles[column].SetActive(true);
                print("Perfect!");
                StartCoroutine("CreatePerformanceText", 0);
                
            } else if (distance < goodThreshold)
            {
                usedMultiplier = goodMultiplier;
                baseArrowParticles[column].SetActive(false);
                baseArrowParticles[column].SetActive(true);
                print("Good!");
                StartCoroutine("CreatePerformanceText", 1);
            } else if(distance < okayThreshold)
            {
                usedMultiplier = okayMultiplier;
                print("Okay");
                StartCoroutine("CreatePerformanceText", 2);
                
            } else if (distance < badThreshold)
            {
                usedMultiplier = badMultiplier;
                print("Bad :(");
                StartCoroutine("CreatePerformanceText", 3);

            }
            else if (distance < missThreshold)
            {
                usedMultiplier = missPenalty;
                print("FUCK YOU!");
            }
        }
        
        finalScore = 1 * usedMultiplier / 2;
        
        totalScore += finalScore;
        return finalScore;
    }

    public void OnMiss()
    {
        totalScore += missPenalty;
        print("You missed, sucker!");
        //play miss sound
    }

    IEnumerator CreatePerformanceText(int wordType)
    {
        GameObject performanceText = Instantiate(performanceWords[wordType], performanceWordSpawn.position, Quaternion.identity);
        performanceText.transform.SetParent(canvas);
        yield return new WaitForSeconds(textDuration);
        Destroy(performanceText);
    }
}












//Wall of Shame

// newSpawnedArrow.transform.rotation = targetArrows[arrowType].rotation;