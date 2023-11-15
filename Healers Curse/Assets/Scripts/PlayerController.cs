using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void SaveData(GameData data) 
    {
        data.playerPosition = this.transform.position;
    }

    public void LoadData(GameData data)
    {
        if (data.playerPosition == Vector2.zero)
        {
            //Use SceneDefault
        }
        else
        {
            // Use the provided player position
            SetPlayerPosition(data.playerPosition);
        }
    }



    private void SetPlayerPosition(Vector2 newPosition)
    {
        // Set the player position to the provided value
        this.transform.position = newPosition;
    }

}
