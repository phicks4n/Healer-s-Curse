using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public int sceneBuildIndex;

    //Level Move zoned enter, if collider is a player move game to another scene
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered:");

        if(other.tag == "Player")
        {
            //Playered entered, so move to other level
            print("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
