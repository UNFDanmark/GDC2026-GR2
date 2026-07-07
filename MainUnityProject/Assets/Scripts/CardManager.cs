using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] RhythmManager rhythmManager;
    [SerializeField]CombatManager combatManager;
    [SerializeField] Transform cardCanvas;

    [SerializeField] Transform[] evenAnchorPoints;
    [SerializeField] Transform[] oddAnchorPoints;

    [SerializeField] Transform cardSource;

    public List<GameObject> hand;
    
    [SerializeField] List<GameObject> cardDeck;
    
    [SerializeField] List<GameObject> cardLibrary;

    [SerializeField] float speed;

    [SerializeField] float cardLoweringT;

    public bool allowedToPlayCards;

    public float playTimerAmount;
    public float playTimer = 0;

    [Flags]
    enum NoteType
    {
        left = 1, 
        down = 2,
        up = 4, 
        right = 8 
    }

    [SerializeField] NoteType[] noteChart;

    void Awake()
    {
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
    }

    void Start()
    {
        DrawCard(2);
    }

    void Update()
    {
        playTimer -= Time.deltaTime;
        foreach (GameObject card in hand)
        {
            if (!combatManager.isPlayersTurn)
            {
                card.transform.position = new Vector3(card.transform.position.x, Mathf.Lerp(card.transform.position.y, card.GetComponent<CardMovement>().anchorTarget.position.y - 200, cardLoweringT), card.transform.position.z);
            }
        }
        if (!combatManager.isPlayersTurn)
        {
            cardLoweringT += Time.deltaTime;
        }
        else
        {
            cardLoweringT -= Time.deltaTime;
        }

        cardLoweringT = Mathf.Clamp(cardLoweringT, 0, 1);
    }

    public void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        { 
            GameObject newDrawnCard = Instantiate(cardDeck[Random.Range(0, cardDeck.Count)], cardSource.position, Quaternion.identity);
            newDrawnCard.transform.SetParent(cardCanvas);
            newDrawnCard.GetComponent<CardMovement>().lookingForAnchor = true;
            hand.Add(newDrawnCard);
            print("Drew 1 card");
        }
        ReorderAllCards();
    }

    

    public void ReorderAllCards()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].GetComponent<CardMovement>().lookingForAnchor = true;
            hand[i].GetComponent<CardMovement>().t = 0;
            evenAnchorPoints[i].GetComponent<Anchor>().isClaimed = false;
            oddAnchorPoints[i].GetComponent<Anchor>().isClaimed = false;
        }
        if (hand.Count % 2 == 1)
        {
            foreach (GameObject cards in hand)
            {
                foreach (Transform anchor in oddAnchorPoints)
                {
                    if (anchor.gameObject.GetComponent<Anchor>().isClaimed == false && cards.GetComponent<CardMovement>().lookingForAnchor == true)
                    {
                        print("yipiee tOdd");
                        cards.GetComponent<CardMovement>().anchorTarget = anchor;
                        cards.GetComponent<CardMovement>().lookingForAnchor = false;
                        anchor.gameObject.GetComponent<Anchor>().isClaimed = true;
                    } 
                }
            }
        }

        if (hand.Count % 2 == 0)
        {
            foreach (GameObject cards in hand)
            {
                foreach (Transform anchor in evenAnchorPoints)
                {
                    if (anchor.gameObject.GetComponent<Anchor>().isClaimed == false && cards.GetComponent<CardMovement>().lookingForAnchor == true)
                    {
                        print("yipiee tEven");
                        CardMovement tempCardMovement = cards.GetComponent<CardMovement>();
                        tempCardMovement.anchorTarget = anchor;
                        tempCardMovement.lookingForAnchor = false;
                        anchor.gameObject.GetComponent<Anchor>().isClaimed = true;
                    } 
                }
            }
        }
    }
}
