using Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{

    private TextMeshProUGUI levelText;
    private Slider experienceBarImage;
    private LevelSystem levelSystem;

    public static LevelWindow instance;

    private void Awake()
    {
        
    }

    private void Update()
    {
        GameData savedData = DataPersistenceManager.instance.GetGameData();

        if(savedData.numOfBattles == 1)
        {
            levelText.text = "LEVEL" + 1;
        }
        else if (savedData.numOfBattles == 2)
        {
            levelText.text = "LEVEL" + 2;
        }
        else if (savedData.numOfBattles == 3)
        {
            levelText.text = "LEVEL" + 3;
        }
        else if (savedData.numOfBattles == 4)
        {
            levelText.text = "LEVEL" + 4;
        }
        else if (savedData.numOfBattles == 5)
        {
            levelText.text = "LEVEL" + 5;
        }
    }
}

    /* private void Awake()
      {
          if (instance == null)
          {
              instance = this;
              Debug.Log("I am AWAKE");
          }
          else
          {
              Destroy(this.gameObject);
          }

          levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
          experienceBarImage = transform.Find("XPPanel").GetComponent<Slider>();

          SetLevelSystem(levelSystem);
      }
      public void SetOnContinue() 
      {
          GameData savedData = DataPersistenceManager.instance.GetGameData();

          SetLevelNumber(savedData.lvl);

          SetExperienceBarSize(savedData.exp);

          levelSystem.OnExpChange += LevelSystem_OnExpChange;
          levelSystem.OnLevelChange += LevelSystem_OnLevelChange;
      }

      private void SetExperienceBarSize(float experienceNormalized)
      {
          experienceBarImage.value = experienceNormalized;
      }

      private void SetLevelNumber(int levelNumber)
      {
          Debug.Log(levelText);
          levelText.text = "LEVEL " + levelNumber;
      }

      public void SetLevelSystem(LevelSystem levelSystem)
      {
          this.levelSystem = levelSystem;

          SetLevelNumber(levelSystem.GetLevel());
          SetExperienceBarSize(levelSystem.GetExpNormalized());

          levelSystem.OnExpChange += LevelSystem_OnExpChange;
          levelSystem.OnLevelChange += LevelSystem_OnLevelChange;
      }

      private void LevelSystem_OnLevelChange(object sender, System.EventArgs e)
      {
          SetLevelNumber(levelSystem.GetLevel());
      }

      private void LevelSystem_OnExpChange(object sender, System.EventArgs e)
      {
          SetExperienceBarSize(levelSystem.GetExpNormalized());
      }*/
