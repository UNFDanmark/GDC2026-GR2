using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] RhythmManager rhythmManager;
    [SerializeField] Transform cardCanvas;

    [SerializeField] Transform[] evenAnchorPoints;
    [SerializeField] Transform[] oddAnchorPoints;

    [SerializeField] Transform cardSource;

    [SerializeField] List<GameObject> hand;
    
    [SerializeField] List<GameObject> cardDeck;
    
    [SerializeField] List<GameObject> cardLibrary;

    [SerializeField] float speed;
    float tOdd;

    float tEven;

    [Flags]
    enum noteType
    {
        left = 1, 
        down = 2,
        up = 4, 
        right = 8 
    }

    [SerializeField] noteType[] noteChart;
    
    void Start()
    {
        DrawCard(2);
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

    

    void ReorderAllCards()
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
                        cards.GetComponent<CardMovement>().destination = anchor;
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
                        cards.GetComponent<CardMovement>().destination = anchor;
                        cards.GetComponent<CardMovement>().lookingForAnchor = false;
                        anchor.gameObject.GetComponent<Anchor>().isClaimed = true;
                    } 
                }
            }
        }
    }

    // void moveCard()
    // {
    //     
    // }
}
