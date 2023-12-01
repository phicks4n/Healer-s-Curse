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

    private void Awake()
    {
        levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        experienceBarImage = transform.Find("XPPanel").GetComponent<Slider>();

        SetLevelSystem(levelSystem);

        transform.Find("50expButton").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(50));
        transform.Find("500expButton").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(500));
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
    }
}
