using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
    public int enemyType;
    int sceneBuildIndex = 7;

    //Level Move zoned enter, if collider is a player move game to another scene
    private void OnTriggerEnter2D(Collider2D player)
    {
        print("Trigger Entered:");
        
        if(player.tag == "Player")
        {
            GameData savedData = DataPersistenceManager.instance.GetGameData();

            savedData.enemyType = enemyType;
            savedData.playerPosition = player.gameObject.transform.position;

            DataPersistenceManager.instance.SaveEnemyType();
            DataPersistenceManager.instance.SavePlayerPosition();
            Debug.Log("I am using the player's last location " + savedData.playerPosition + " player " + player.gameObject.transform.position);
            GameManager.instance.NextLevel(player, sceneBuildIndex);
        }
    }

}
