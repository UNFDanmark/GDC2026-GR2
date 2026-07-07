using System;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("References")] [SerializeField]
    RhythmManager rhythmManager;

    CardManager cardManager;

    [Header("Uncategorized")] [SerializeField]
    public GameObject currentEnemy;

    public bool isPlayersTurn;
    bool isPlayingRhythmGame;

    void Awake()
    {
        rhythmManager = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmManager>();
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
    }

    void Update()
    {
        if (rhythmManager.notesQueue.Count > 0)
        {
            isPlayersTurn = false;
        }
        else
        {
            if (!isPlayersTurn )
            {
                cardManager.DrawCard(1);
            }
            isPlayersTurn = true;
        }
    }

    public void PlayCard(GameObject card)
    {
        if (isPlayersTurn == true)
        {
            
            rhythmManager.notesQueue.AddRange(card.GetComponent<CardData>().noteChart);
            cardManager.hand.Remove(card);
            cardManager.ReorderAllCards();
            Destroy(card);
        }

    }
}