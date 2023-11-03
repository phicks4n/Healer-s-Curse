using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : MonoBehaviour
{

    [Header("INK JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;



    private void Awake()
    {
        
        playerInRange = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(EnableCollision(3));

        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }

    }

    private IEnumerator EnableCollision (float delay) 
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider2D>().enabled = true;

    }
}
