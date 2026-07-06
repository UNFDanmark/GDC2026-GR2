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

    [SerializeField] float Speed;
    float tOdd;

    float tEven;

    void Start()
    {
        DrawCard(5);
    }

    void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        { 
            GameObject newDrawnCard = Instantiate(cardDeck[Random.Range(0, cardDeck.Count)], cardSource.position, Quaternion.identity);
            newDrawnCard.transform.SetParent(cardCanvas);
            hand.Add(newDrawnCard);
            print("Drew 1 card");
            moveAllCardsToAnchor();
        }
    }

    void moveAllCardsToAnchor()
    {
        if (hand.Count % 2 == 1)
        {
            foreach (GameObject cards in hand)
            {
                foreach (Transform anchor in oddAnchorPoints)
                {
                    if (anchor.gameObject.GetComponent<Anchor>().isClaimed == false)
                    {
                        for (int i = 0; cards.transform.position != anchor.position; i++)
                        {
                            tOdd += Time.deltaTime * Speed;
                            cards.transform.position = new Vector3(Mathf.Lerp(cardSource.position.x, anchor.position.x, tOdd), Mathf.Lerp(cardSource.position.y, anchor.position.y, tOdd), 0);
                        }

                        if (cards.transform.position == anchor.position)
                        {
                            tOdd = 0;
                        }
                        
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
                    if (anchor.gameObject.GetComponent<Anchor>().isClaimed == false)
                    {
                        for (int i = 0; cards.transform.position != anchor.position; i++)
                        {
                            tEven += Time.deltaTime * Speed;
                            cards.transform.position = new Vector3(Mathf.Lerp(cards.transform.position.x, anchor.position.x, tEven), Mathf.Lerp(cards.transform.position.y, anchor.position.y, tEven), 0);
                        }

                        if (cards.transform.position == anchor.position)
                        {
                            tEven = 0;
                        }
                        
                    } 
                }
            }
        }
    }

    void moveCard()
    {
        
    }
}
