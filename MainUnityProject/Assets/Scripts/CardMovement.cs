using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardMovement : MonoBehaviour
{
    [Header("t's")]
    public float t;

    public float playCardT;

    public float highlightCardT;
    
    
    
    bool isBeingPlayed;
    
    Transform spawn;
    public bool lookingForAnchor;
    public Transform anchorTarget;
    public float zoom;

    public float highLightSpeed = 0.2f;
    public float unHighLightSpeed = 2;

    CardManager cardManager;
    RhythmManager rhythmManager;
    CardData cardData;
    Animator animator;
    Button button;

    void Awake()
    {
        isBeingPlayed = false;
        cardData = GetComponent<CardData>();
        button = GetComponent<Button>();
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
        rhythmManager = cardData.rhythmManager;
        t = 0;
        lookingForAnchor = true;
        spawn = transform;
    }


    void OnDrawGizmos()
    {
        if (anchorTarget == null)
            return;
        
        Gizmos.DrawSphere(anchorTarget.position, 20f);
        Gizmos.DrawSphere(transform.position, 20f);
        Gizmos.DrawLine(transform.position, anchorTarget.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (button.IsHighlighted())
        {
            highlightCardT += Time.deltaTime * highLightSpeed;
            highlightCardT = Mathf.Clamp(highlightCardT, 0, 1);
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, anchorTarget.position.y + 100, highlightCardT), transform.position.z);
        }
        else
        {
            highlightCardT = 0;
        }
        
        if (!lookingForAnchor && !button.IsHighlighted())
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, anchorTarget.position.x, t), Mathf.Lerp(transform.position.y, anchorTarget.position.y, t), transform.position.z);
            t += Time.deltaTime * zoom;
        }

        if (isBeingPlayed && !button.IsHighlighted())
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, anchorTarget.position.y + 1000, playCardT), transform.position.z);
            playCardT += Time.deltaTime * zoom;
            if (playCardT >= 1)
            {
                cardData.PlayCard();
            }
        }
    }

    public void PlayCardAnim()
    {
        if (cardManager.playTimer <= 0 && cardManager.allowedToPlayCards && rhythmManager.columns[0].notesInColumn.Count == 0 && rhythmManager.columns[1].notesInColumn.Count == 0 && rhythmManager.columns[2].notesInColumn.Count == 0 && rhythmManager.columns[3].notesInColumn.Count == 0 )
        {
            cardManager.playTimer = cardManager.playTimerAmount;
            isBeingPlayed = true;
        }
    }

    void OnMouseEnter()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, anchorTarget.position.y + 100, highlightCardT), Mathf.Lerp(transform.position.z, anchorTarget.position.z + 100, highlightCardT));
        highlightCardT += Time.deltaTime * zoom;
    }
    public void MoveCardDown()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, anchorTarget.position.y - 175, playCardT), transform.position.z);
        playCardT += Time.deltaTime * zoom;
    }
}
