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

    [SerializeField] float universalDelay;
    [SerializeField] float universalDelayAmount = 3;

    public float numberOfNotesInChart;

    public GameObject enemyCurrentAttack; //assign på playEnemyAttack() & DESTROY efter useEnemyEffects()
    

    void Awake()
    {
        rhythmManager = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmManager>();
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
    }

    void Update()
    {
        if (universalDelay > 0)
        {
            universalDelay -= Time.deltaTime;
        }
        
        
        
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
            UseCardEffects(rhythmManager.totalScore);
            rhythmManager.totalScore = 0;
            
            PlayEnemyAttack(currentEnemy.GetComponent<Enemy>().nextAttack); //after this is finished play the top if statement
            
            isPlayingPlayerRhythmGame = false;
            isEnemyAttacking = true;
            universalDelay = universalDelayAmount*rhythmManager.currentSpeed;
        }

        if (universalDelay <= 0 && isEnemyAttacking && !rhythmManager.isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou && !isPlayersTurn && !isPlayingPlayerRhythmGame)
        {
            if (isEnemyAttacking == true)
            {
                cardManager.DrawCard(1 + extraCardDraw);
                rhythmManager.totalScore = 0;
            }
            isEnemyAttacking = false;
            isPlayersTurn = true;
        }
    }

    #region Player Card things

    public void PlayCard(GameObject card)
    {
        if (isPlayersTurn == true)
        {
            numberOfNotesInChart = card.GetComponent<CardData>().noteAmount;
            rhythmManager.notesQueue.AddRange(card.GetComponent<CardData>().noteChart);
            rhythmManager.currentSpeed = card.GetComponent<CardData>().noteSpeed;
            cardManager.hand.Remove(card);
            cardManager.ReorderAllCards();
            Destroy(card, 5);
        }

    }

    public void UseCardEffects(float finalScore)
    {
        print("Use Card effects");
        float finalScoreAverage = finalScore / numberOfNotesInChart;
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
            UsePowerRiff(cardData, finalScoreAverage);
        }
        
        Destroy(cardData);
    }

    //Add new function for every card type
    void UsePowerRiff(CardData cardData,float multiplier)
    {
        print("Used Power Riff");
        currentEnemy.GetComponent<Enemy>().TakeDamage(cardData.damage * multiplier);
    }
    #endregion
    
    #region Enemy Card Thongs

    private void PlayEnemyAttack(GameObject attack)
    {
        enemyCurrentAttack = Instantiate(currentEnemy.GetComponent<Enemy>().nextAttack);
        currentEnemy.GetComponent<Enemy>().GetNextAttack();
        rhythmManager.notesQueue.AddRange(attack.GetComponent<EnemyAttack>().noteChart);
        
    }

    private void UseEnemyAttackEffects(float finalScore)
    {
        EnemyAttack enemyAttack = enemyCurrentAttack.GetComponent<EnemyAttack>();
        //enemyAttack.damage =
        
        if (enemyCurrentAttack.GetComponent<EnemyAttack>().attackType.HasFlag(EnemyAttackType.basicAttack))
        {
            BasicAttack(enemyAttack ,finalScore);
        }
        Destroy(enemyCurrentAttack);
    }

    private void BasicAttack(EnemyAttack attackData,float finalScore)
    {
        player.TakeDamage(2);
        print($"Enemy did {finalScore/enemyCurrentAttack.GetComponent<EnemyAttack>().damage} damage");
    }
    
    #endregion
}