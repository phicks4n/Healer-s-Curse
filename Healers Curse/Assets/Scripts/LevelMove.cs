using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public int sceneBuildIndex;
    public Animator transition;
    public float transitionTime;
    public GameObject GameManager;
    

    //Level Move zoned enter, if collider is a player move game to another scene
    private void OnTriggerEnter2D(Collider2D player)
    {
        print("Trigger Entered:");

        
        DontDestroyOnLoad(GameManager);


        if(player.tag == "Player")
        {
            StartCoroutine(LoadLevel(player));
        }
    }

    IEnumerator LoadLevel(Collider2D player)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Playered entered, so move to other level
        print("Switching Scene to " + sceneBuildIndex);
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

        //Play animation
        transition.SetTrigger("End");
    }
}
