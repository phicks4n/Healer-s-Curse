using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, WAIT }

public class BattleSystem : MonoBehaviour, IDataPersistence
{
    [Header("INK JSON")]
    [SerializeField] private TextAsset inkJSON;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public GameObject bossPrefab;

    public GameObject deepRoots;
    public GameObject seedVillage; 
    public GameObject elvenVillage; 
    public GameObject bossArena; 

    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public Animator animator;

    Character playerUnit;
    Enemy enemyUnit;
    //DialogueManager dialogue;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public EnemyHUD enemyHUD;
    public GameObject skillMenu;

    public GameObject AttackAnimation;
    public GameObject Death;

    bool Block = false;
    bool superBlock = false;
    int skillUse = 0;
    int scared = 0;
    int dodge = 0;
    int numOfBattles = 0;

    public BattleState state;

    void Start()
    {
        deepRoots = GameObject.Find("Deep Roots");
        seedVillage = GameObject.Find("Seed Village Square");
        elvenVillage = GameObject.Find("EV Forest");
        bossArena = GameObject.Find("EV Boss");

        deepRoots.SetActive(false);
        seedVillage.SetActive(false);
        elvenVillage.SetActive(false);
        bossArena.SetActive(false);
        skillMenu.SetActive(false);

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    

    IEnumerator SetupBattle()
    {
        GameData savedData = DataPersistenceManager.instance.GetGameData();
        GameObject enemyGo;

        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Character>();

        SetPlayerStats();

        
        switch (savedData.sceneIndex)
        {
            case 1:
                seedVillage.SetActive(true);
                break;
            case 3:
                deepRoots.SetActive(true);
                break;
            case 6:
                elvenVillage.SetActive(true);
                break;
            default:
                break;
        }

        if(savedData.sceneIndex == 6 && savedData.enemyType == 4)
        {
            elvenVillage.SetActive(false);
            bossArena.SetActive(true);
        }

        if (savedData.sceneIndex == 1)
        {
            enemyGo = Instantiate(enemyPrefab2, enemyBattleStation);
            enemyUnit = enemyGo.GetComponent<Enemy>();
        }
        else
        {
            switch (savedData.enemyType)
            {
                case 1:
                    enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
                    enemyUnit = enemyGo.GetComponent<Enemy>();
                    break;
                case 3:
                    enemyGo = Instantiate(enemyPrefab1, enemyBattleStation);
                    enemyUnit = enemyGo.GetComponent<Enemy>();
                    break;
                case 4:
                    enemyGo = Instantiate(bossPrefab, enemyBattleStation);
                    enemyUnit = enemyGo.GetComponent<Enemy>();
                    break;
            }
        }

       

        dialogueText.text = "A " + enemyUnit.enemyName + " is attacking!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(1f);

        dialogueText.text = playerUnit.unitName + "'s turn!";

    }

    IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnit.TakeDamage((int)(1.2* playerUnit.damage), enemyUnit.armor, playerUnit.currentHP, playerUnit.maxHP);

        int hasEnergy = playerUnit.currentEP;

        
        if (hasEnergy >= playerUnit.costOfAttack)
        {

            playerUnit.Energy(playerUnit.costOfAttack);

            enemyHUD.SetHP(enemyUnit.currentHP);
            playerHUD.SetEP(playerUnit.currentEP);

            dialogueText.text = "Attack deals " + enemyUnit.damageTaken + " damage!";

            AttackAnimation.SetActive(true);

            yield return new WaitForSeconds(1);

            AttackAnimation.SetActive(false);

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "Your energy is too low!";
            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }  
    }

    IEnumerator SkillAttack()
    {
        switch (skillUse)
        {
            case 1:
                bool isDead = enemyUnit.TakeDamage(playerUnit.damage, enemyUnit.armor, playerUnit.currentHP, playerUnit.maxHP);

                int hasEnergy = playerUnit.currentEP;

                Block = true;

                if (hasEnergy >= playerUnit.costOfBash)
                {

                    playerUnit.Energy(playerUnit.costOfBash);

                    enemyHUD.SetHP(enemyUnit.currentHP);
                    playerHUD.SetEP(playerUnit.currentEP);

                    dialogueText.text = "Attack deals " + enemyUnit.damageTaken + " damage!";

                    AttackAnimation.SetActive(true);

                    yield return new WaitForSeconds(1);

                    AttackAnimation.SetActive(false);

                    yield return new WaitForSeconds(2f);

                    if (isDead)
                    {
                        state = BattleState.WON;
                        StartCoroutine(EndBattle());
                        break;
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                        break;
                    }
                }
                else
                {
                    dialogueText.text = "Your energy is too low!";
                    state = BattleState.PLAYERTURN;
                    StartCoroutine(PlayerTurn());
                    break;
                }
                
            case 2:
                hasEnergy = playerUnit.currentEP;
                if(hasEnergy >= playerUnit.costOfSpook)
                {
                    playerUnit.Energy(playerUnit.costOfSpook);
                    playerHUD.SetEP(playerUnit.currentEP);
                    scared = 2;
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                    break;
                }
                else
                {
                    dialogueText.text = "Your energy is too low!";
                    state = BattleState.PLAYERTURN;
                    StartCoroutine(PlayerTurn());
                    break;
                }
                
            case 3:
                superBlock = true;
                hasEnergy = playerUnit.currentEP;
                if (hasEnergy >= playerUnit.costOfBlock)
                {
                    playerUnit.Energy(playerUnit.costOfBlock);
                    playerHUD.SetEP(playerUnit.currentEP);
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                    break;
                }
                else
                {
                    dialogueText.text = "Your energy is too low!";
                    state = BattleState.PLAYERTURN;
                    StartCoroutine(PlayerTurn());
                    break;
                }
                
        }

        yield return new WaitForSeconds(1);
    }

    IEnumerator PlayerRecover()
    {
        playerUnit.Recover(20);

        playerHUD.SetEP(playerUnit.currentEP);
        dialogueText.text = "You recovered some energy!";

        state = BattleState.WAIT;

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        float damageDone;
        if(scared > 0)
        {
            dialogueText.text = enemyUnit.enemyName + " is too scared to attack!";
            scared--;
            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }
        else
        {
            if(dodge > 0)
            {
                dialogueText.text = "You avoid an incoming attack";
                dodge--;
                state = BattleState.PLAYERTURN;
                StartCoroutine(PlayerTurn());
            }
            else
            {
                if (Block)
                {
                    damageDone = enemyUnit.damage - playerUnit.armor;
                    if (damageDone < 0) 
                    {
                        damageDone = 1;
                    }
                }
                else if (superBlock)
                {
                    damageDone = 0;
                }
                else
                {
                    damageDone = enemyUnit.damage;
                }

                bool isDead = playerUnit.TakeDamage(damageDone, playerUnit.armor, enemyUnit.currentHP, enemyUnit.maxHP, Block);

                playerHUD.SetHP(playerUnit.currentHP);

                dialogueText.text = enemyUnit.enemyName + " deals " + playerUnit.damageTaken + " damage!";

                Block = false;
                superBlock = false;

                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    state = BattleState.LOST;
                    StartCoroutine(EndBattle());

                }
                else
                {
                    state = BattleState.PLAYERTURN;
                    StartCoroutine(PlayerTurn());
                }
            }
            
        } 
    }

    IEnumerator EndBattle()
    {
        GameData savedData = DataPersistenceManager.instance.GetGameData();

        if (state == BattleState.WON)
        {
            dialogueText.text = "Victory!";
            
            DataPersistenceManager.instance.SaveNumOfBattles(savedData.numOfBattles + 1);

            if (savedData.sceneIndex == 1)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                yield return StartCoroutine(EndDialogue());
                SceneManager.LoadSceneAsync(savedData.sceneIndex);
            }
            else if(savedData.sceneIndex == 6 && savedData.enemyType == 4)
            {
                dialogueText.text = "Raging Minatour Defeated";
                StartCoroutine(GameEnds());
            }
            else
            {
                yield return new WaitForSeconds(2f);
                SceneManager.LoadSceneAsync(savedData.sceneIndex);
            }
            
        }
        else if (state == BattleState.LOST)
        {
            Death.SetActive(true);
            dialogueText.text = "You have died...";
            SceneManager.LoadSceneAsync(savedData.sceneIndex);
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        state = BattleState.WAIT;
        StartCoroutine(PlayerAttack());
    }

    public void OnRecoverButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        state = BattleState.WAIT;
        StartCoroutine(PlayerRecover());
    }

    public void OnBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        Block = true;
        playerUnit.Recover(10);

        playerHUD.SetEP(playerUnit.currentEP);

        dialogueText.text = "You brace for an attack!";
        state = BattleState.WAIT;
        StartCoroutine(EnemyTurn());
    }

    public void OnSkillButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        skillMenu.SetActive(true);
    }

    public void OnSkill1()
    {

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        skillMenu.SetActive(false);
        skillUse = 1;
        StartCoroutine(SkillAttack());
    }
    public void OnSkill2()
    {

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        skillMenu.SetActive(false);
        skillUse = 2;
        StartCoroutine(SkillAttack());
    }
    public void OnSkill3()
    {

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        skillMenu.SetActive(false);
        skillUse = 3;
        StartCoroutine(SkillAttack());
    }

    public void onCancel()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        skillMenu.SetActive(false);
    }

    IEnumerator EndDialogue()
    {
        DialogueManager dialogue = DialogueManager.GetInstance();

        while (dialogue.dialogueIsPlaying)
        {
            yield return null;
        }
    }
    public void SetPlayerStats()
    {
        GameData savedData = DataPersistenceManager.instance.GetGameData();


        playerUnit.maxHP = (float)savedData.health;
        playerUnit.currentHP = (float)savedData.currentHealth;
        playerUnit.maxEP = savedData.energy;
        playerUnit.currentEP = savedData.energy;
        playerUnit.armor = savedData.armor;
        playerUnit.damage = savedData.attack;
    }

    IEnumerator GameEnds()
    {
        yield return new WaitForSeconds(3f);

        dialogueText.text = "YOU WIN!!";
        SceneManager.LoadSceneAsync(0);

    }


    public void SaveData(GameData data) 
    {
        data.currentHealth = (int)playerUnit.currentHP;
    }

    public void LoadData(GameData data)
    {

    }

}
