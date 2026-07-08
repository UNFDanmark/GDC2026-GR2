using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("References")] [SerializeField]
    RhythmManager rhythmManager;

    CardManager cardManager;

    [Header("Uncategorized")] [SerializeField]
    public GameObject currentEnemy;

    public Player player;

    public bool isPlayersTurn;
    public bool isEnemyAttacking;
    public bool isPlayingPlayerRhythmGame;

    public GameObject usedCard;
    
    public int extraCardDraw;

    void Awake()
    {
        rhythmManager = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmManager>();
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
    }

    void Update()
    {
        if (!rhythmManager.isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou &&
            !isEnemyAttacking && !isPlayingPlayerRhythmGame) //play this when the enemy attack is done also
        {
            isEnemyAttacking = false;
            isPlayersTurn = true;
        }

        if (rhythmManager.isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou &&
            !isEnemyAttacking && !isPlayingPlayerRhythmGame && isPlayersTurn)
        {
            isPlayersTurn = false;
            isPlayingPlayerRhythmGame = true;
        }

        if (!rhythmManager.isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou &&
            !isEnemyAttacking && !isPlayersTurn && isPlayingPlayerRhythmGame)
        {
            currentEnemy.GetComponent<Enemy>().Attack(); //after this is finished play the top if statement
            isPlayingPlayerRhythmGame = false;
            isEnemyAttacking = true;
        }
        
        

        // if (!rhythmManager.isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou &&
        //     isEnemyAttacking && !isPlayersTurn && !isPlayingPlayerRhythmGame)
        // {
        //     isEnemyAttacking = false;
        //     isPlayersTurn = true;
        // }
        
        // if (!rhythmManager.isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou && !isEnemyAttacking && !isPlayingPlayerRhythmGame)
        // {
        //     if (isPlayersTurn == false)
        //     {
        //         cardManager.DrawCard(1 + extraCardDraw);
        //         extraCardDraw = 0;
        //     }
        //     isPlayersTurn = true;
        // }
        //
        // if(rhythmManager.isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou && !isEnemyAttacking)
        // {
        //     isPlayersTurn = false;
        //     isPlayingPlayerRhythmGame = true;
        // }
        //
        // if (!rhythmManager.isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou && !isEnemyAttacking && !isPlayersTurn && isPlayingPlayerRhythmGame)
        // {
        //     currentEnemy.GetComponent<Enemy>().Attack();
        //     isEnemyAttacking = true;
        //     isPlayingPlayerRhythmGame = false;
        // }
        //
        // if (!rhythmManager.isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou && isEnemyAttacking && !isPlayersTurn && !isPlayingPlayerRhythmGame)
        // {
        //     isEnemyAttacking = false;
        // }
        
        
        
        
        
        
        // if (rhythmManager.notesQueue.Count > 0 || isEnemyTurn)
        // {
        //     isPlayersTurn = false;
        // }
        // else
        // {
        //     if (!isPlayersTurn )
        //     {
        //         cardManager.DrawCard(1);
        //     }
        //     isPlayersTurn = true;
        // }
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

    public void UseCardEffects(float finalScore, float numberOfNotesInChart)
    {
        CardData cardData = GetComponent<CardData>();
        float totalBlock;
        float totalLeech;
        int totalDrawnCards;
        bool hitAllNotes;
        float enemyAttackDecrease;
        float playerAttackIncrease;
        float totalDamagePercent = 1;
        float totalHealingPercent = 1;

        float totalDamage = finalScore / numberOfNotesInChart * totalDamagePercent * player.bonusAttackIncrease + 1;
        float totalHealing = finalScore / numberOfNotesInChart * totalHealingPercent * player.bonusAttackIncrease + 1;
        if (cardData.cardType.HasFlag(CardType.powerRiff)) 
        {
            usePowerRiff(finalScore, numberOfNotesInChart);
        }
        
        Destroy(cardData);
    }

    void usePowerRiff(float finalScore, float numberOfNotesInChart)
    {
        print("Used Power Riff");
    }
    
}