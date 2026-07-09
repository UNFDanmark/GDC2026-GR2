using System;
using Unity.Mathematics;
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
    [SerializeField] float universalDelayAmount = 4;

    public float numberOfNotesInChart;

    public GameObject enemyCurrentAttack; //assign på playEnemyAttack() & DESTROY efter useEnemyEffects()

    public float enemySoundDelay;


    
    

    void Awake()
    {
        rhythmManager = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmManager>();
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
        universalDelayAmount -= rhythmManager.currentNoteSpeed;
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
            rhythmManager.notesHit = 0;
            
            PlayEnemyAttack(currentEnemy.GetComponent<Enemy>().nextAttack); //after this is finished play the top if statement
            
            isPlayingPlayerRhythmGame = false;
            isEnemyAttacking = true;
            universalDelay = universalDelayAmount*rhythmManager.currentSpeed;
        }

        if (universalDelay <= 0 && isEnemyAttacking && !rhythmManager.isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou && !isPlayersTurn && !isPlayingPlayerRhythmGame)
        {
            if (isEnemyAttacking == true)
            {
                UseEnemyAttackEffects(rhythmManager.totalScore);
                rhythmManager.totalScore = 0;
                rhythmManager.notesHit = 0;
                player.TurnStarted();
                currentEnemy.GetComponent<Enemy>().DecreaseDurations();
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
            rhythmManager.currentSpeed = card.GetComponent<CardData>().noteSpeed;
            rhythmManager.notesQueue.AddRange(card.GetComponent<CardData>().noteChart); //MAYBE THIS LINE UP???
            cardManager.hand.Remove(card);
            cardManager.IsPrePlayersTurn = false;
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
            UsePowerRiff(cardData, (finalScoreAverage/10)+1);
        }

        if (cardData.cardType.HasFlag(CardType.healingTune))
        {
            UseHealingTheme(cardData, (finalScoreAverage/10)+1);
        }

        if (cardData.cardType.HasFlag(CardType.blockingBallad))
        {
            UseBlockingBallad(cardData, (finalScoreAverage/10)+1);
        }

        if (cardData.cardType.HasFlag(CardType.leechingHook))
        {
            UseLeechingHook(cardData, (finalScoreAverage/10)+1);
        }

        if (cardData.cardType.HasFlag(CardType.drumDraw))
        {
            UseDrumDraw(cardData, (finalScoreAverage/10)+1);
        }

        if (cardData.cardType.HasFlag(CardType.ostinatoBeam))
        {
            UseOstinatoBeam(cardData, (finalScoreAverage/10)+1);
        }

        if (cardData.cardType.HasFlag(CardType.mendingMelody))
        {
            UseMendingMelody(cardData, (finalScoreAverage/10)+1);
        }
        Destroy(cardData);

        if (cardData.cardType.HasFlag(CardType.agonizingAnthem))
        {
            UseAgonizingAnthem(cardData, (finalScoreAverage/10)+1);
        }
    }

    //Add new function for every card type
    void UsePowerRiff(CardData cardData,float multiplier)
    {
        print("Used Power Riff");
        currentEnemy.GetComponent<Enemy>().TakeDamage(cardData.damage * multiplier-1);
    }

    void UseHealingTheme(CardData cardData, float multiplier)
    {
        print("Used Healing Theme");

        player.HealPlayer(cardData.healing * multiplier);
    }

    void UseBlockingBallad(CardData cardData, float multiplier)
    {
        print("Used Blocking Ballad");
        float appliedBonusBlockIncrease = cardData.maxBonusBlockIncrease * multiplier;
        appliedBonusBlockIncrease = Mathf.Clamp(appliedBonusBlockIncrease, 0f, cardData.maxBonusBlockIncrease);
        
        player.GiveBlock(appliedBonusBlockIncrease, cardData.bonusBlockIncreaseDuration);
    }

    void UseLeechingHook(CardData cardData, float multiplier)
    {
        print("Used Leeching Hook");
        float damageToDeal = cardData.damage * multiplier - 1;
        float newLeechProcent = cardData.leechProcent * multiplier - 1;
        
        
        player.HealPlayer(currentEnemy.GetComponent<Enemy>().imaginaryDamageOnenemy(damageToDeal) * newLeechProcent);
        currentEnemy.GetComponent<Enemy>().TakeDamage(cardData.damage * multiplier-1);
    }

    void UseDrumDraw(CardData cardData, float multiplier)
    {
        print("Used DrumDraw");
        int extraCards;
        if (multiplier > 0.4f)
        {
            extraCards = cardData.extraCardDraw;
        }
        else if (multiplier > 0.2)
        {
            extraCards = cardData.extraCardDraw - 1;
        }
        else
        {
            extraCards = 1;
        }

        player.extraCardDraw = Mathf.Clamp(extraCards, 0, cardData.extraCardDraw);
    }

    void UseOstinatoBeam(CardData cardData, float multiplier)
    {
        print("Used Ostinato Beam");
        if (rhythmManager.notesHit == cardData.noteAmount)
        {
            print("Hit all notes for Ostinato Beam");
            currentEnemy.GetComponent<Enemy>().TakeDamage(cardData.damage);
        }
        else
        {
            print("Didn't hit all notes for Ostinato Beam");
        }
    }
    
    void UseMendingMelody(CardData cardData, float multiplier)
    {
        print("Used Mending Melody");
        if (rhythmManager.notesHit == cardData.noteAmount)
        {
            print("Hit all notes for Mending Melody");
            player.HealPlayer(cardData.healing);
        }
        else
        {
            print("Didn't hit all notes for Mending Melody");
        }
    }

    void UseAgonizingAnthem(CardData cardData, float multiplier)
    {
        print("Used Agonizing Anthem");
        currentEnemy.GetComponent<Enemy>().GiveDefenseDecrease(cardData.decreaseEnemyDefense, cardData.decreaseEnemyDefenseDuration);
    }
    
    
    #endregion
    
    
    
    #region Enemy Card Thongs

    private void PlayEnemyAttack(GameObject attack)
    {
        enemyCurrentAttack = Instantiate(currentEnemy.GetComponent<Enemy>().nextAttack);
        currentEnemy.GetComponent<Enemy>().GetNextAttack();
        rhythmManager.notesQueue.AddRange(attack.GetComponent<EnemyAttack>().noteChart);
        enemyCurrentAttack.GetComponent<EnemyAttack>().sound.PlayDelayed(1);
    }

    private void UseEnemyAttackEffects(float finalScore)
    {
        float finalScoreAverage = finalScore / numberOfNotesInChart;
        print(finalScoreAverage);
        EnemyAttack enemyAttack = enemyCurrentAttack.GetComponent<EnemyAttack>();
        //enemyAttack.damage =
        
        if (enemyCurrentAttack.GetComponent<EnemyAttack>().attackType.HasFlag(EnemyAttackType.basicAttack))
        {
            BasicAttack(enemyAttack ,finalScoreAverage);
        }
        
        if (enemyCurrentAttack.GetComponent<EnemyAttack>().attackType.HasFlag(EnemyAttackType.basicAttack))
        {
            BigAttack(enemyAttack, finalScoreAverage);
        }
        
        
        
        Destroy(enemyCurrentAttack);
    }

    private void BasicAttack(EnemyAttack attackData,float finalScoreAverage)
    {
        print("Enemy used big attack");
        float totalDamage = attackData.damage / finalScoreAverage;
        totalDamage = Mathf.Clamp(totalDamage, attackData.damage / 2, attackData.damage);
        player.TakeDamage(totalDamage);
    }

    private void BigAttack(EnemyAttack attackData, float finalScoreAverage)
    {
        print("Enemy used big attack");
        float totalDamage = attackData.damage / finalScoreAverage;
        totalDamage = Mathf.Clamp(totalDamage, attackData.damage / 2, attackData.damage);
        player.TakeDamage(totalDamage);
    }
    
    #endregion
}