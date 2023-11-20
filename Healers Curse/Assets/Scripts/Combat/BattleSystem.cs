using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, WAIT }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enempyPrefab3;

    public GameObject deepRoots;
    public GameObject seedVillage; 
    public GameObject elvenVillage; 
    public GameObject bossArena; 

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Character playerUnit;
    Enemy enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public EnemyHUD enemyHUD;

    bool Block = false;

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

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    

    IEnumerator SetupBattle()
    {
        GameData savedData = DataPersistenceManager.instance.GetGameData();
        GameObject enemyGo;

        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Character>();

        
        switch (savedData.sceneIndex)
        {
            case 0:
                seedVillage.SetActive(true);
                break;
            case 2:
                deepRoots.SetActive(true);
                break;
            case 5:
                elvenVillage.SetActive(true);
                break;
            default:
                break;
        }

        if (seedVillage)
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

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage, enemyUnit.armor, playerUnit.currentHP, playerUnit.maxHP);

        int hasEnergy = playerUnit.currentEP;

        
        if (hasEnergy >= playerUnit.costOfAttack)
        {

            playerUnit.Energy(playerUnit.costOfAttack);

            enemyHUD.SetHP(enemyUnit.currentHP);
            playerHUD.SetEP(playerUnit.currentEP);

            dialogueText.text = "Attack deals " + enemyUnit.damageTaken + " damage!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
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

    IEnumerator PlayerRecover()
    {
        playerUnit.Recover(10);

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

        if (!Block)
        {
            damageDone = enemyUnit.damage;
        }
        else
            damageDone = enemyUnit.damage - playerUnit.armor;

        bool isDead = playerUnit.TakeDamage(damageDone, playerUnit.armor, enemyUnit.currentHP, enemyUnit.maxHP, Block);

        playerHUD.SetHP(playerUnit.currentHP);

        dialogueText.text = enemyUnit.enemyName + " deals " + playerUnit.damageTaken + " damage!";

        Block = false;

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();

        }
        else
        {
            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }
    }

    void EndBattle()
    {
        GameData savedData = DataPersistenceManager.instance.GetGameData();

        if (state == BattleState.WON)
        {
            dialogueText.text = "Victory!";
            SceneManager.LoadSceneAsync(savedData.sceneIndex);
        }
        else if (state == BattleState.LOST)
        {
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
        dialogueText.text = "You brace for an attack!";
        state = BattleState.WAIT;
        StartCoroutine(EnemyTurn());
    }

    /*public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Character playerUnit;
    Enemy enemyUnit;

    public Text characterInfo;
    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Character>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Enemy>();

        characterInfo.text = playerUnit.unitName;

        playerHUD.SetHUDCharacter(playerUnit);
        enemyHUD.SetHUDEnemy(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHPEnemy(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        // Damage the enemy

        state = BattleState.WAIT;

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }


        // Change state based on what happened

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }

    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.enemyName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHPCharacter(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }



    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHPCharacter(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength!";

        state = BattleState.WAIT;

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }


    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }*/

}
