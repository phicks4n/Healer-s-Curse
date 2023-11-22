using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        //Inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);


    }

    void FixedUpdate()
    {
        if(DialogueManager.GetInstance().dialogueIsPlaying || SceneManager.GetActiveScene().buildIndex == 7)
        {
            return;
        }

        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void SaveData(GameData data) 
    {
        if(data.sceneIndex != 7 && data.enemyType == 0)
        {
            data.playerPosition = this.transform.position;
        }
    }

    public void LoadData(GameData data)
    {
        GameData savedData = DataPersistenceManager.instance.GetGameData();

        //If the player is exiting combat, put him in his last known location
        if(data.enemyType != 0 && SceneManager.GetActiveScene().buildIndex != 7)
        {
            SetPlayerPosition(data.playerPosition);

            Debug.Log("I am using the player's last location" + data.playerPosition);

            //Set enemyType to 0 and Save it
            data.enemyType = 0;
            DataPersistenceManager.instance.SaveEnemyType();
            return;
        }

        Debug.Log("I am continuing");

        if (data.playerPosition == Vector2.zero || savedData.sceneIndex != savedData.initialSceneIndex || savedData.sceneIndex == 1)
        {
            //Use SceneDefault
            SetSceneDefault(data);
        }
        else
        {
            // Use the provided player position
            SetPlayerPosition(data.playerPosition);
        }
    }

    private void SetSceneDefault(GameData data)
    {
        Vector2 newPosition;

        //Seed Village
        if(data.sceneIndex == 0)
        {
            newPosition = new Vector2(-46.52f, 13.54f);
            SetPlayerPosition(newPosition);
        }
        //PC House
        else if(data.sceneIndex == 1)
        {
            newPosition = new Vector2(7.01f, -4.22f);
            SetPlayerPosition(newPosition);
        }
        //Deep Roots
        else if(data.sceneIndex == 2)
        {
            newPosition = new Vector2(29.46f, 11.51f);
            SetPlayerPosition(newPosition);
        }
        //Elven Village
        else if(data.sceneIndex == 5)
        {
            newPosition = new Vector2(-.52f, -54.27f);
            SetPlayerPosition(newPosition);
        }
    }



    private void SetPlayerPosition(Vector2 newPosition)
    {
        // Set the player position to the provided value
        this.transform.position = newPosition;
    }

}
