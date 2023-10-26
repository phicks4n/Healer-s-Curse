using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Animator transition;
    public float transitionTime;
    
    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } 
    }

    public void NextLevel(Collider2D player, int sceneBuildIndex)
    {
        StartCoroutine(LoadLevel(player, sceneBuildIndex));
    }

    IEnumerator LoadLevel(Collider2D player, int sceneBuildIndex)
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
