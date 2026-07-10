using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardMovement : MonoBehaviour
{
    [Header("t's")]
    public float t;

    public float playCardT;

    public float highlightCardT;

    AudioManager audioManager;
    
    public bool isBeingPlayed;
    
    Transform spawn;
    public bool lookingForAnchor;
    public Transform anchorTarget;
    public float zoom;

    public float highLightSpeed = 0.2f;
    public float unHighLightSpeed = 2;

    CardManager cardManager;
    RhythmManager rhythmManager;
    CombatManager combatManager;
    CardData cardData;
    Animator animator;
    public Button button;
    public float scaleOnHighlight;
    public float highlightUpMovement = 150f;

    float cardPlayDelay;
    float cardPlayDelayAmount;

    bool startCardDelayTimer;

    public bool hasBeenPlayed;

    void Awake()
    {
        
        isBeingPlayed = false;
        cardData = GetComponent<CardData>();
        button = GetComponent<Button>();
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
        rhythmManager = cardData.rhythmManager;
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
        audioManager = Camera.main.gameObject.GetComponentInChildren<AudioManager>();
        t = 0;
        lookingForAnchor = true;
        spawn = transform;
        cardPlayDelayAmount = rhythmManager.universalCardPlayDelay;
        cardPlayDelay = cardPlayDelayAmount;
    }


    void OnDrawGizmos()
    {
        if (anchorTarget == null)
            return;
        
        Gizmos.DrawSphere(anchorTarget.position, 20f);
        Gizmos.DrawSphere(transform.position, 20f);
        Gizmos.DrawLine(transform.position, anchorTarget.position);
    }

    void FixedUpdate()
    {
        HandleTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingPlayed && !button.IsHighlighted())
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, anchorTarget.position.y + 1500, playCardT), transform.position.z);
            playCardT += Time.deltaTime * zoom;
            if (playCardT >= 1 && !hasBeenPlayed)
            {
                hasBeenPlayed = true;
             
            }
            return;
        }
        
        if (cardManager.IsPrePlayersTurn)
        {
            return;
        }
        
        if (button.IsHighlighted() && combatManager.isPlayersTurn)
        {
            highlightCardT += Time.deltaTime * highLightSpeed;
            highlightCardT = Mathf.Clamp(highlightCardT, 0, 1);
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, anchorTarget.position.y + highlightUpMovement, highlightCardT), transform.position.z);
            transform.localScale = new Vector3(scaleOnHighlight, scaleOnHighlight, scaleOnHighlight);
            transform.SetAsLastSibling();
        }
        else
        {
            highlightCardT = 0;
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        if (!lookingForAnchor && !button.IsHighlighted() && combatManager.isPlayersTurn)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, anchorTarget.position.x, t), Mathf.Lerp(transform.position.y, anchorTarget.position.y, t), transform.position.z);
            t += Time.deltaTime * zoom;
        }

        
        
    }

    void HandleTimer()
    {
        if (startCardDelayTimer)
        {
            print(cardPlayDelay);
            cardPlayDelay -= Time.deltaTime;
            if (cardPlayDelay <= 0)
            {
                cardManager.ReorderAllCards();
                PlayCardAnim();
                
                startCardDelayTimer = false;
                cardPlayDelay = cardPlayDelayAmount;
               
            }
        }
    }

    public void PlayCardAnim()
    {
        
           
            CardData newCardData = combatManager.AddComponent<CardData>();
            newCardData.damage = cardData.damage;
            newCardData.healing = cardData.healing;
            newCardData.maxBonusBlockIncrease = cardData.maxBonusBlockIncrease;
            newCardData.bonusBlockIncreaseDuration = cardData.bonusBlockIncreaseDuration;
            newCardData.leechProcent = cardData.leechProcent;
            newCardData.hitAllNotesRequirement = cardData.hitAllNotesRequirement;
            newCardData.extraCardDraw = cardData.extraCardDraw;
            newCardData.increaseDamage = cardData.increaseDamage;
            newCardData.decreaseEnemyDamage = cardData.decreaseEnemyDamage;
            newCardData.noteAmount = cardData.noteAmount;
            newCardData.noteSpeed = cardData.noteSpeed;
            newCardData.cardType = cardData.cardType;
            newCardData.bonusBlockIncreaseDuration = cardData.bonusBlockIncreaseDuration;
            newCardData.decreaseEnemyDamage = cardData.decreaseEnemyDamage;
            newCardData.decreaseEnemyDamageDuration = cardData.decreaseEnemyDamageDuration;
            newCardData.decreaseEnemyDefense = cardData.decreaseEnemyDefense;
            newCardData.decreaseEnemyDefenseDuration = cardData.decreaseEnemyDefenseDuration;
        
            combatManager.PlayCard(this.gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsAttacking", true);
            //Audio
            
        
    }

    public void ButtonPress()
    {
        if (cardManager.playTimer <= 0 && combatManager.isPlayersTurn && !rhythmManager
                .isThereCurrentlyNotesOnTheBattlefieldRightNowAtThisTimeQuestionMarkPrettyPleaseAndThankYou)
        {
            cardManager.IsPrePlayersTurn = true;
            cardManager.playTimer = cardManager.playTimerAmount;
            startCardDelayTimer = true;
            isBeingPlayed = true;
            cardManager.hand.Remove(gameObject);
            rhythmManager.currentSpeed = cardData.noteSpeed;
            audioManager.PlayCardMelody(GetComponent<CardData>().cardMelody);
        }
    }
}
